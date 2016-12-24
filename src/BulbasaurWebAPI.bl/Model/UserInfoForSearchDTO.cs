using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulbasaurWebAPI.bl.Model
{
    public class UserInfoForSearchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
