using System.Collections.Generic;

namespace BulbasaurWebAPI.bl.Model
{
    public class UserProfileDTO
    {
        public string tokenResponse { get; set; }
        public UserDTO userDTO { get; set; }
        public List<UserDTO> friends { get; set; }
        public List<GameDTO> games { get; set; }

//        public int id { get; set; }
//        public string name { get; set; }
//        public string surname { get; set; }
//        public string adress { get; set; }
//        public string about { get; set; }
//        public bool online { get; set; }
//        public string token { get; set; }
//        public string picture { get; set; }


    }
}
