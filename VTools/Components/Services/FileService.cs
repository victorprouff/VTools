namespace VTools.Components.Services;

public static class FileService
{
    public static string GetTempFileName(string contentRootPath) => Path.Combine(GetTempDirectoryPath(contentRootPath), Path.GetRandomFileName());

    public static void DeleteTempFiles(string contentRootPath)
    {
        var di = new DirectoryInfo(GetTempDirectoryPath(contentRootPath));

        foreach (var file in di.GetFiles())
        {
            file.Delete();
        }
    }

    private static string GetTempDirectoryPath(string contentRootPath) =>
        Path.Combine(contentRootPath, "files_temp");
}