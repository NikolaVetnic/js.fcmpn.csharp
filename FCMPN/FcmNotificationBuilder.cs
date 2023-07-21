using System.Text.Json.Serialization;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json;

public class FcmNotificationBuilder
{
    private readonly Message _notification = new Message();
    private Dictionary<string, string> _data = new Dictionary<string, string>();

    public static FcmNotificationBuilder Create()
    {
        return new FcmNotificationBuilder();
    }

    public FcmNotificationBuilder()
    {
        SetAndroidChannelId("ENMESHED");
    }

    private void SetAndroidChannelId(string channelId)
    {
        _data.Add("android_channel_id", channelId);
    }

    public FcmNotificationBuilder AddContent(NotificationContent content)
    {
        _data.Add("content", JsonConvert.SerializeObject(content));
        SetContentAvailable(true);

        return this;
    }

    public FcmNotificationBuilder SetNotificationText(string title, string body)
    {
        _notification.Notification = new Notification()
        {
            Title = title,
            Body = body
        };

        return this;
    }

    private void SetContentAvailable(bool contentAvailable)
    {
        _data.Add("contentAvailable", contentAvailable ? "1" : "0");
    }

    public FcmNotificationBuilder SetTag(int notificationId)
    {
        _data.Add("tag", notificationId.ToString());
        return this;
    }

    public FcmNotificationBuilder AddToken(string deviceToken)
    {
        _notification.Token = deviceToken;
        return this;
    }

    public Message Build()
    {
        _notification.Data = _data;
        return _notification;
    }

    public class NotificationContent
    {
        [JsonPropertyName("accRef")]
        public string? accRef { get; set; }

        [JsonPropertyName("eventName")]
        public string? eventName { get; set; }

        [JsonPropertyName("payload")]
        public PayloadContent? payload { get; set; }

        [JsonPropertyName("sentAt")]
        public string? sentAt { get; set; }
    }

    public class PayloadContent
    {
        [JsonPropertyName("someProperty")]
        public string? someProperty { get; set; }
    }
}