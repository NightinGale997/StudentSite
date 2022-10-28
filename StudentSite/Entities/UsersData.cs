namespace StudentSite.Entities
{
    public class UsersData
    {
        public List<User> Users { get; set; }
        public List<ChangeOperation> ChangeOperations { get; set; }
        public List<PointOperation> PointOperations { get; set; }
    }
}
