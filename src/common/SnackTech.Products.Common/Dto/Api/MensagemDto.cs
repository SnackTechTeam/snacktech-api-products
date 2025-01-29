namespace SnackTech.Products.Common.Dto.Api
{
    public class MensagemDto(string mensagem)
    {
        public string Mensagem { get; set; } = mensagem;
    }
}