using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryBoard.Core.Entities
{
    public class Story : IEntityIdentity<int>
    {        
        [Key]
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

        public string UserId { get; set; }            
        public User User { get; set; }
        public List<Group> Groups { get; set; }
    }
}
