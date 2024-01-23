using SharedLibrary.Responses;

namespace Client.Helpers;

public static class HttpResponseHelper
{
    public static ServiceResponse CheckResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResponse(false, "Error occured. Try again later...");
        }
        else
        {
            return new ServiceResponse(true, null!);
        }
    }

    public static async Task<string> ReadContent(HttpResponseMessage response)
    {
        return await response.Content.ReadAsStringAsync();
    }
}
