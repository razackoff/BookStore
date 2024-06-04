namespace BookStore.Utilities;

public static class FileUtility
{
    public static bool IsImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return false;

        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };

        return allowedTypes.Contains(file.ContentType.ToLower());
    }
    
    public static bool IsPDF(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return false;

        return file.ContentType?.Contains("pdf", StringComparison.OrdinalIgnoreCase) ?? false;
    }
}