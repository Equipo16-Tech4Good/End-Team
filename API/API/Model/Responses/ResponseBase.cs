namespace API.Model.Responses
{
    public abstract class ResponseBase
    {
        public string Mensaje { get; set; }
        public int Status { get; set; }
    }
}
