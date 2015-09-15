using Nancy;
using Nancy.Responses;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class PingModule : NancyModule
    {
        public PingModule()
        {
            Get["ping"] = _ => new TextResponse("__NAME__.Api\nStatus=active");
        }
    }
}