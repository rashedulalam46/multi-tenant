using System.ComponentModel.DataAnnotations;

namespace MultiTenant.Models
{
    public class Companies
    {
        [Key]
        public string ComCode { get; set; }
        public string ComName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string LandmarkName { get; set; }
        public string StreetNo { get; set; }
        public string StreetName { get; set; }
        public string AptSuit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string ThemeColor { get; set; }        
        public string Logo { get; set; }
        public string StatusCode { get; set; }
        public string InsertDate { get; set; }
        public string InsertedBy { get; set; }
    }
}
