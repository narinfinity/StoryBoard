using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoryBoard.Web.Models
{
    public class StoryViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [MaxLength(512)]
        public string Content { get; set; }
        
        public UserViewModel User { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Group field is required.")]
        public int GroupId { get; set; }        
        public List<GroupViewModel> Groups { get; set; }
    }
}