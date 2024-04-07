using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegrationTests.Setup.Extensions;

public static class HttpResponseMessageExtensions
{
    private static async Task<T> ParseAndValidate<T>(
        this HttpResponseMessage message,
        HttpStatusCode expectedStatus = HttpStatusCode.OK)
    {
        if (message.StatusCode != expectedStatus)
        {
            throw new Exception($"Expected Status {expectedStatus} but got {message.StatusCode}. \nServer responded with \n{await message.Content.ReadAsStringAsync()}");
        }

        var defaultOption = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        defaultOption.Converters.Add(new JsonStringEnumConverter());
        defaultOption.DefaultIgnoreCondition = JsonIgnoreCondition.Never;

        return await message.Content.ReadFromJsonAsync<T>(defaultOption) ?? throw new Exception($"Could not parse result to ${typeof(T).Name}");
    }

    public static async Task<T> ParseAndValidate<T>(
        this Task<HttpResponseMessage> message,
        HttpStatusCode expectedStatus = HttpStatusCode.OK) =>
        await (await message).ParseAndValidate<T>(expectedStatus);
}