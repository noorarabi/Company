namespace Company.Models
{
    public class Slider
    {
        public Guid SliderId { get; set; }
        
        public string SliderTitle { get; set; } = string.Empty;
       
        public string? SubTitle { get; set; }
        public byte[]? SliderImg { get; set; }
    }
}
