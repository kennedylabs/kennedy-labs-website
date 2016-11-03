using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KennedyLabsWebsite.Models
{
    public class SectionModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Page")]
        public int PageId { get; set; }

        [ForeignKey("ParentSection")]
        public int? ParentSectionId { get; set; }

        public int Ordinal { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string SecondaryText { get; set; }

        public string TertiaryText { get; set; }

        public PageModel Page { get; set; }

        public SectionModel ParentSection { get; set; }

        public ICollection<SectionModel> ChildSections { get; set; }

        public ICollection<ItemModel> Items { get; set; }
    }
}
