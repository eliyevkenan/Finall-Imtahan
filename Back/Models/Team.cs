using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Team
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string SocialIcon1 { get; set; }
        public string SocialIcon2 { get; set; }
        public string SocialIcon3 { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
