using System.Net;
using System.Text.Json;

namespace DeskFlow.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Deixa a requisição seguir o fluxo normal dela
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se qualquer erro acontecer em qualquer camada, ele cai aqui!
                _logger.LogError(ex, "Um erro não tratado ocorreu na API: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            // Padrão: Erro interno do servidor (500)
            var statusCode = HttpStatusCode.InternalServerError;
            var mensagem = "Ocorreu um erro interno no servidor. Tente novamente mais tarde.";

            // Se for um erro de regra de negócio (ArgumentException), devolve Bad Request (400)
            if (exception is ArgumentException || exception is InvalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                mensagem = exception.Message;
            }

            context.Response.StatusCode = (int)statusCode;

            // Cria o JSON limpo exigido pelo professor
            var resultado = JsonSerializer.Serialize(new
            {
                status = context.Response.StatusCode,
                erro = mensagem,
                data = DateTime.UtcNow
            });

            return context.Response.WriteAsync(resultado);
        }
    }
}
