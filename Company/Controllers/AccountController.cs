using Company.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Company.Controllers
{
  
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                IdentityUser user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Name
                };

                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                  


                    if (model.IsSelected)
                    {
                       
                        //var roleExists = await _roleManager.RoleExistsAsync("Admain");
                        //if (!roleExists)
                        //{
                           
                            IdentityRole role = new IdentityRole { Name = "Admain" };
                            await _roleManager.CreateAsync(role);
                        //}

                        await _userManager.AddToRoleAsync(user, "Admain");
                        //SendEmail(user.Email);
                        return RedirectToAction("Login");
                    }
                    
                    IdentityRole role1 = new IdentityRole { Name = "Student" };
                    await _roleManager.CreateAsync(role1);
                    await _userManager.AddToRoleAsync(user,"Student");
                    //SendEmail(user.Email);
                    return RedirectToAction("Login");
                }

               
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            
            return View(model);
        }


        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByNameAsync(model.Name);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model);
                }

                
                var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password,model.RememberMe, false);

                if (result.Succeeded)
                {
                   
                    if (await _userManager.IsInRoleAsync(user, "Admain"))
                    {
                        
                        return RedirectToAction("Index","CompanyPanels", new { area = "Company" });
                    }
                    else if (await _userManager.IsInRoleAsync(user, "SuperAdmain"))
                    {
                        return RedirectToAction("Index", "AdmainPanels", new { area = "SuperAdmain" });
                    }
                    else 
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

            //public IActionResult SendEmail(string email)
            //{
            //    try
            //    {
                    
            //        MailMessage mail = new MailMessage();
            //        mail.From = new MailAddress(""); 
            //        mail.To.Add(email); 
            //        mail.Subject = "Welcom To Our WebSite"; 
            //        mail.Body = $"Hi{email}"; 

                    
            //        SmtpClient smtpClient = new SmtpClient("smtp.example.com", 587);
            //        smtpClient.Credentials = new NetworkCredential("","");
            //        smtpClient.EnableSsl = true;

                   
            //        smtpClient.Send(mail);

                    
            //        return Content("Email sent successfully!");
            //    }
            //    catch (Exception ex)
            //    {
                    
            //        return Content($"Error occurred while sending email: {ex.Message}");
            //    }
            //}
        }
    }




