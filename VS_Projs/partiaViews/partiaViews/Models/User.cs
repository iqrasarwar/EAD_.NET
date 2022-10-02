using System.ComponentModel.DataAnnotations;
namespace partiaViews.Models
{
    public class User
    {
        public User(int age)
        {
            this.age = age;
        }
        [Required(ErrorMessage = "age must be entered")]
        [Range(10,100)]
        public int age { get; set; }
    }
}
