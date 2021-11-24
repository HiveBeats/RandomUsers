namespace WebApi.Features.RandomUsers.Requests
{
    public class RandomUsersRequest
    {
        public int? Count { get; set; }
        public string Location { get; set; }
        public string Nationality { get; set; }
    }  
}
