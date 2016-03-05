using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoryBoard.Core.Entities
{
    public class Group : IEntityIdentity<int>
    {       
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(256)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(512)]
        public string Content { get; set; }
        
        public List<Story> Stories { get; set; }
    }
}
