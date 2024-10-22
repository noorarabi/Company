using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Cv
    {
        public int CvId { get; set; }
        public string? Student_Name { get; set; }
        [EnumDataType(typeof(Uni))]
        public Uni University { get; set; }
        public string? Major {  get; set; }  
        public DateTime Date_of_Graduation { get; set; }   
        [EnumDataType(typeof(Citys))]
        public Citys City { get; set; } 
        public byte[]? CV { get; set; }
        public string? TrainingName { get; set; }    
        [ForeignKey("Trains")]
        public int TrainId { get; set; }
        
        public Train? Trains { get; set; }    

       public enum Citys
        {
           Amman,Irbid,Zarqaa,Maan,Aqaba,Tafila,Karak,Ajlon,Jarash,Madaba,Balqaa,Mafraq
               
        }
        public enum Uni
        {
            The_University_of_Jordan, Yarmouk_University, Jordan_University_of_Science_and_Technology, Mutah_University
                , Al_al_Bayt_University, Al_Hussein_Bin_Talal_University, Al_Balqa_Applied_University,Princess_Sumaya_University_for_Technology
                , Tafila_Technical_University, The_World_Islamic_Sciences_and_Education_University, The_Hashemite_University
                , Zarqa_University, Philadelphia_University, Amman_Arab_University, Jerash_University, Ajloun_National_University
                , Al_Isra_University, Middle_East_University, Applied_Science_Private_University, Other
        }
    }
}
