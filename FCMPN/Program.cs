using Newtonsoft.Json;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.AddUserSecrets<Program>();
        var config = builder.Build();

        var deviceToken = config["DeviceToken"];
        var accRef = config["AccRef"];

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("/Users/nikolavetnic/Documents/Texts/j&s-soft/aduwuvovud.json"),
        });

        var content = new
        {
            accRef = accRef,
            eventName = "dynamic",
            payload = new
            {
                someProperty = "someValue"
            },
            sentAt = "2021-01-01T00:00:00.000Z",
        };

        var serializedContent = JsonConvert.SerializeObject(content);

        var message = new Message()
        {
            Data = new Dictionary<string, string>()
            {
                { "content", serializedContent },
            },
            Token = deviceToken,
        };

        var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Successfully sent message: " + response);
    }
}
