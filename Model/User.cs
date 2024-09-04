using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Students.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
    
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } 
    
        [Range(1, 120)]
        public int Age { get; set; }
    
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    
        public string? ImagePath { get; set; }
        
        [NotMapped]
        public IFormFile? UserImage { get; set; }
        
        
      //  public int StudentId { get; set; }
       // public string Adress { get; set; }
      //  public int PhoneNumber { get; set; }
     //   public string Gender { get; set; }
     //   public int ClassNumber { get; set; }



    }
}
