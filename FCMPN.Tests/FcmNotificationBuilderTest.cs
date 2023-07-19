using Newtonsoft.Json;

public class FcmNotificationBuilderTest
{
    [Fact]
    public void TestBuildMessage()
    {
        var content = new FcmNotificationBuilder.NotificationContent
        {
            accRef = "accRef",
            eventName = "eventName",
            payload = new FcmNotificationBuilder.PayloadContent { someProperty = "someValue" },
            sentAt = "2021-01-01T00:00:00.000Z"
        };

        var message = FcmNotificationBuilder.Create()
            .AddContent(content)
            .SetNotificationText("title", "body")
            .SetTag(1)
            .AddToken("deviceToken")
            .Build();

        Assert.Equal("deviceToken", message.Token);

        Assert.True(message.Data.ContainsKey("content"));
        Assert.Equal(JsonConvert.SerializeObject(content), message.Data["content"]);

        Assert.True(message.Data.ContainsKey("title"));
        Assert.Equal("title", message.Data["title"]);

        Assert.True(message.Data.ContainsKey("body"));
        Assert.Equal("body", message.Data["body"]);

        Assert.True(message.Data.ContainsKey("tag"));
        Assert.Equal("1", message.Data["tag"]);
    }
}
