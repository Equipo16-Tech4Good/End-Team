namespace API.Model.DTOs
{
    public class UpdateUserDTO
    {
        public string Token { get; set; }
        public int NivelEstanque { get; set; } = 0;
        public int RachaConexion { get; set; } = 0;
    }
}
