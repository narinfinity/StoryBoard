using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoryBoard.Web.Models
{
    public class GroupViewModel
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

        public List<StoryViewModel> Stories { get; set; }
    }
}