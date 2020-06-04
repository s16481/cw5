using System.ComponentModel.DataAnnotations;

namespace cw4.Models
{
    public class Studies
    {
        [Key]
        public int IdStudy { get; set; }
        public string Name { get; set; }
    }
}