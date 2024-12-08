using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Iniciando o cliente SignalR...");

// URL do Hub SignalR no servidor
var hubUrl = "https://localhost:7206/notifications"; // Ajuste para o seu endpoint

// Configurando o HubConnection
var connection = new HubConnectionBuilder()
    .WithUrl(hubUrl)
    .WithAutomaticReconnect() // Reconexão automática em caso de desconexão
    .Build();

// Tratando mensagens recebidas do Hub
connection.On<string>("ReceiveNotification", (message) =>
{
    Console.WriteLine($"Mensagem recebida do servidor: {message}");
});

// Conectando ao servidor
try
{
    await connection.StartAsync();
    Console.WriteLine("Conectado ao SignalR!");

    // Enviando mensagens para o servidor
    string input;
    do
    {
        Console.Write("Digite uma mensagem para enviar (ou 'sair' para encerrar): ");
        input = Console.ReadLine();

        if (input != null && input.ToLower() != "sair")
        {
            await connection.SendAsync("SendNotification", input);
        }

    } while (input != null && input.ToLower() != "sair");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao conectar ao SignalR: {ex.Message}");
}
finally
{
    await connection.StopAsync();
    Console.WriteLine("Desconectado do SignalR.");
}