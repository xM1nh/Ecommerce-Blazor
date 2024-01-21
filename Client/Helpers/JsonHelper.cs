using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace Client.Helpers;

public static class JsonHelper
{
    public static string SerializeObject(object modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());

    public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;

    public static IEnumerable<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IEnumerable<T>>(jsonString, JsonOptions())!;

    public static StringContent GenerateStringContent(string serialiazedObject) => new(serialiazedObject, Encoding.UTF8, "application/json");

    public static JsonSerializerOptions JsonOptions()
    {
        return new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
        };
    }
}
