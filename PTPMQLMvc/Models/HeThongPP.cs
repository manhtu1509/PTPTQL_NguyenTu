using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PTPMQLMvc.Models
{
    [Table("HeThongPP")]
    public class HeThongPP
    {
        public string? MaHTPP{get; set;}
        public string? TenHTPP{get; set;}
    }
    
}