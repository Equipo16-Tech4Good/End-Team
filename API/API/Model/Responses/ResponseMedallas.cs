using API.Model.DTOs;

namespace API.Model.Responses
{
    public class ResponseMedallas : ResponseBase
    {
        public MedallasDeUsuarioDTO Data { get; set; }
    }
}
