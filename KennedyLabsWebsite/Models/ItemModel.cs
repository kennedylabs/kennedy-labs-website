using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KennedyLabsWebsite.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Page")]
        public int PageId { get; set; }

        [ForeignKey("ParentSection")]
        public int SectionId { get; set; }

        public int Ordinal { get; set; }

        [Required]
        public string Text { get; set; }

        public string Context { get; set; }

        public string Type { get; set; }

        public SectionModel Page { get; set; }
        public SectionModel ParentSection { get; set; }
    }
}
