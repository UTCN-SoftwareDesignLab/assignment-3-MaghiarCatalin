using System;
using System.ComponentModel.DataAnnotations;

namespace MVCRealtimeSignalR.Models
{
    public class Consultation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [MaxLength(30, ErrorMessage = "PatientName cannot be greater than 30")]
        [MinLength(5, ErrorMessage = "PatientName cannot be shorter than 5")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [MaxLength(30, ErrorMessage = "DoctorName cannot be greater than 30")]
        [MinLength(5, ErrorMessage = "DoctorName cannot be shorter than 5")]
        public string DoctorName { get; set; }

        [MaxLength(300, ErrorMessage = "Details cannot be greater than 300")]
        public string Details { get; set; }
    }
}