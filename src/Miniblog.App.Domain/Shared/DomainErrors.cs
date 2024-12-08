using FluentResults;

namespace Miniblog.App.Domain.Shared;

public static class DomainErrors
{
    public static string UsuarioJaExiste = "Usuário já existe com o e-mail cadastrado";

    public static string DadosInvalidos = "Dados inválidos";

    public static string UsuarioNaoCadastrado = "Usuário não cadastrado";

    public static CustomError NaoForamEncontradosRegistrosParaOsParametrosInformados = new CustomError("Não foram encontrados registros para os parametros informados", 404);

    public static string DataAtualizacaoMenorDataCriacao = "A data de atualização não pode ser menor do que a data de criação";

    public static string EstePostNaoPertenceAoUsuarioInformado = "Este post não pertence ao usuário informado";

    public static string CampoObrigatorio(string nomeCampo) => $"O campo {nomeCampo} é de preenchimento obrigatorio.";
}

public sealed class CustomError : IError
{
    public string Message { get; set; }
    public int ErrorCode { get; set; } // Propriedade personalizada para o código de erro
    public List<IError> Reasons { get; set; } = new List<IError>();
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

    public CustomError(string message, int errorCode)
    {
        Message = message;
        ErrorCode = errorCode;
    }

    // Método auxiliar para adicionar metadados
    public CustomError WithMetadata(string key, object value)
    {
        Metadata[key] = value;
        return this;
    }
}