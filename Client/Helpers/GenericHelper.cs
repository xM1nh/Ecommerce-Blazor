namespace Client.Helpers;

public static class GenericHelper
{
    public static string GetDescription(string description)
    {
        string appendDots = "...";
        int maxLength = 100;
        int descLength = description.Length;
        return descLength > maxLength ? $"{description.Substring(0, 100)}{appendDots}" : description;
    }
}
