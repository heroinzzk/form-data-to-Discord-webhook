using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Como te llamas:");
        string nombre = Console.ReadLine();

        Console.WriteLine("Cual es tu tag de discord:");
        string tag = Console.ReadLine();

        Console.WriteLine($"Buenas tardes {nombre}, {tag}, Bienvenido a las pruebas de: @xxx");
        Console.WriteLine("Presiona una tecla para continuar...");
        Console.ReadKey();

        Console.WriteLine($"Cual es tu proposito para estar aqui? {nombre}");
        string proposito = Console.ReadLine();


        string webhookUrl = "xxx";
        DiscordWebhook.EnviarMensaje(webhookUrl, nombre, tag, proposito);

        Console.WriteLine($"Gracias por tu tiempo {nombre}, {tag}, nos contactaremos pronto...");
        Console.WriteLine("Presiona cualquier tecla para salir");
        Console.ReadKey();
    }

    internal static class DiscordWebhook
    {
        public static void EnviarMensaje(string webhookUrl, string nombre, string tag, string proposito)
        {
            EnviarMensajeAsync(webhookUrl, nombre, tag, proposito).GetAwaiter().GetResult();
        }

        private static async Task EnviarMensajeAsync(string webhookUrl, string nombre, string tag, string proposito)
        {
            using var cliente = new HttpClient();


            var payload = new
            {
                content = $"Nuevo registro:\nNombre: {nombre}\nTag: {tag}\nPropósito: {proposito}"
            };


            var json = JsonSerializer.Serialize(payload);

            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var respuesta = await cliente.PostAsync(webhookUrl, contenido);
                if (!respuesta.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al enviar el mensaje: {respuesta.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al enviar mensaje: {ex.Message}");
            }
        }
    }
}