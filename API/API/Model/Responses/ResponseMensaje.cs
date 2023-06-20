using API.Model.Entity;

namespace API.Model.Responses
{
    public class ResponseMensaje : ResponseBase
    {
        public Mensaje? Data { get; set; }
    }
}
