using ChairsLib;
using ChairsLib.Models;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

int PORT = 7531;

ChairsRepositoryList repository = new ChairsRepositoryList();
TcpListener listener = new TcpListener(IPAddress.Any, PORT);

listener.Start();
Console.WriteLine($"Server started on port {PORT}");

while (true)
{
    TcpClient client = listener.AcceptTcpClient();
    IPEndPoint clientEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + clientEndPoint.Address);

    Task.Run(() => HandleClient(client));
}

listener.Stop(); // not reached due to while true loop

void HandleClient(TcpClient client)
{
    using NetworkStream stream = client.GetStream();
    using StreamReader reader = new StreamReader(stream);
    using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

    string receivedData = reader.ReadLine();

    if (int.TryParse(receivedData, out int minWeight))
    {
        IEnumerable<Chair> chairsWithMinWeight = repository.GetAll()
            .Where(c => c.MaxWeight >= minWeight);

        string jsonResponse = JsonSerializer.Serialize(chairsWithMinWeight);
        writer.WriteLine(jsonResponse);
    }

    client.Close();
}
