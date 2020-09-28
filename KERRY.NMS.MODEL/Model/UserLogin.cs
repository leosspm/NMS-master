namespace KERRY.NMS.MODEL.Model
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool Blocked { get; set; }
        public int AvatarId { get; set; }
    }
}
