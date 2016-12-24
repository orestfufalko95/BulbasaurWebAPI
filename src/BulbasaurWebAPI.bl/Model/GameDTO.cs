using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulbasaurWebAPI.bl.Model
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool IsChosenByUser { get; set; }
    }
}
