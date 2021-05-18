using System;
using System.ComponentModel.DataAnnotations;

namespace MVCRealtimeSignalR.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required !")]
        [MaxLength(20, ErrorMessage = "Username cannot be greater than 20")]
        [MinLength(5, ErrorMessage = "Username cannot be shorter than 5")]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Password cannot be greater than 20")]
        [MinLength(5, ErrorMessage = "Password cannot be shorter than 5")]
        [Required(ErrorMessage = "This field is required !")]
        public string Password { get; set; }

        [Range(0, 2,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int AccountType { get; set; }
    }
}