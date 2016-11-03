using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KennedyLabsWebsite.Models
{
    public class PageModel
    {
        [Key]
        public int Id { get; set; }

        public int Ordinal { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<SectionModel> RootSections { get; set; }
    }
}
