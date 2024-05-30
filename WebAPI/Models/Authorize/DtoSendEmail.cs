using Aop.Api.Request;

namespace WebAPI.Models.Authorize
{
    public class DtoSendEmail
    {

        public string ToAddress { get; set; }


        public string Title {  get; set; }


        public string Body { get; set; }

    }
}
