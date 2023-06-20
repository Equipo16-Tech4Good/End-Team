using API.Model.DTOs;

namespace API.Model.Responses
{
    public class ResponseLogros : ResponseBase
    {
        public List<LogroDTO>? Data { get; set; }
    }
}
