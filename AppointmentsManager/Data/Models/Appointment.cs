using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.Data.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150), Column(TypeName ="nvarchar(150)")]
        public string Title { get; set; }
        
        [MaxLength(300), Column(TypeName = "nvarchar(300)")]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime ModifiedDate { get; set; } = DateTime.Now;  
        public DateTime Date { get; set; } = DateTime.Now;
        
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; } = "Address";

        [MaxLength(10), Column(TypeName = "nvarchar(10)")]
        public string Time { get; set; } = "12:30";
        public bool Done { get; set; } = false;
        public bool Deleted { get; set; } = false;
        public byte LevelOfImportance { get; set; } = 2;
    }
}
