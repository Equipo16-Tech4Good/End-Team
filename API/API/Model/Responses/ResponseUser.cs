using API.Model.Entity;

namespace API.Model.Responses
{
    public class ResponseUser : ResponseBase
    {
        public Usuario? Data { get; set; }
    }
}
