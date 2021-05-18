using System;
using System.ComponentModel.DataAnnotations;

namespace MVCRealtimeSignalR.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required !")]
        [MaxLength(30, ErrorMessage = "Username cannot be greater than 30")]
        [MinLength(5, ErrorMessage = "Username cannot be shorter than 5")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [MaxLength(6, ErrorMessage = "IdentityNumber cannot be greater than 6")]
        [MinLength(6, ErrorMessage = "IdentityNumber cannot be shorter than 6")]
        public string IdentityNumber { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [MaxLength(10, ErrorMessage = "PersonalNumericalCode cannot be greater than 10")]
        [MinLength(10, ErrorMessage = "PersonalNumericalCode cannot be shorter than 10")]
        public string PersonalNumericalCode { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }


        [MaxLength(300, ErrorMessage = "Address cannot be greater than 300")]
        [MinLength(10, ErrorMessage = "Address cannot be shorter than 10")]
        [Required(ErrorMessage = "This field is required !")]
        public string Address { get; set; }
    }
}