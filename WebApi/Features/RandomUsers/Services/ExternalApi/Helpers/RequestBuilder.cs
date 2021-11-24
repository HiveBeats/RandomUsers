using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Features.RandomUsers.Services.ExternalApi.Helpers
{
    public class RequestBuilder
    {
        private string _url;
        private NameValueCollection _queryString;

        public static string Gender { get; } = "gender";
        public static string Name { get; } = "name";
        public static string Location { get; } = "location";
        public static string Email { get; } = "email";
        public static string Login { get; } = "login";
        public static string Registered { get; } = "registered";
        public static string Dob { get; } = "dob";
        public static string Phone { get; } = "phone";
        public static string Cell { get; } = "cell";
        public static string Id { get; } = "id";
        public static string Picture { get; } = "picture";
        public static string Nationality { get; } = "nat";

        public RequestBuilder(string baseUrl)
        {
            _url = baseUrl;
            _queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
        }

        public RequestBuilder ForCorrelation(string correlationId)
        {
            if (!string.IsNullOrWhiteSpace(correlationId))
                _queryString.Add("seed", correlationId);
            return this;
        }

        public RequestBuilder WithCount(int count)
        {
            _queryString.Add("results", count.ToString());
            return this;
        }

        public RequestBuilder ForNationality(string nationality)
        {
            if (!string.IsNullOrWhiteSpace(nationality))
                _queryString.Add("nat", nationality);
            return this;
        }

        public RequestBuilder IncludingFields(string csvFields)
        {
            if (!string.IsNullOrWhiteSpace(csvFields))
                _queryString.Add("inc", csvFields);
            return this;
        }

        public RequestBuilder ExcludingFields(string csvFields)
        {
            if (!string.IsNullOrWhiteSpace(csvFields))
                _queryString.Add("exc", csvFields);
            return this;
        }

        public string Build()
        {
            return _url + "?" +_queryString.ToString();
        }
    }
}
