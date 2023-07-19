using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Configuration;

using static FcmNotificationBuilder;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.AddUserSecrets<Program>();
        var config = builder.Build();

        var accRef = config["AccRef"];
        var deviceToken = config["DeviceToken"];

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("/Users/nikolavetnic/Documents/Texts/j&s-soft/aduwuvovud.json"),
        });

        string rndString = RandomString(10);

        var builtNotification = FcmNotificationBuilder
            .Create()
            .SetTag(1)
            .SetNotificationText("My Notification Title", "Notification content.")
            .AddContent(new NotificationContent()
            {
                accRef = accRef,
                eventName = "dynamic",
                payload = new PayloadContent
                {
                    someProperty = rndString
                },
                sentAt = "2021-01-01T00:00:00.000Z",
            })
            .AddToken(deviceToken)
            .Build();

        var response = await FirebaseMessaging.DefaultInstance.SendAsync(builtNotification);
        Console.WriteLine("Successfully sent " + rndString + " as Payload.SomeProperty: " + response);
    }

    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
