namespace WebApi.Configuration
{
    public class RandomUserServiceConfig
    {   
        public string ApiUrl { get; set; }
        public int MaxUsersToFetchCount { get; set; }
        public int DefaultUsersToFetchCount { get; set; }
    }
}
