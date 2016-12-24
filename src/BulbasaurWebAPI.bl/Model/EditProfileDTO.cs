namespace BulbasaurWebAPI.bl.Model
{
    public class EditProfileDTO : UserInfoDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}