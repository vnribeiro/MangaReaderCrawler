namespace MangaReaderDownloader.ConsoleApp.Infrastructure.Utils;

public static class AppUtils
{
  /// <summary>
  /// Removes illegal characters from a given path.
  /// </summary>
  /// <param name="path">The path to filter.</param>
  /// <returns>The filtered path without illegal characters.</returns>
  public static string FilterPath(string path)
  {
    string[] illegalChars = ["\\", "/", ":", "*", "?", "\"", "<", ">", "|"];

    foreach (string illegalChar in illegalChars)
    {
      path = path.Replace(illegalChar, "");
    }

    return path;
  }

  /// <summary>
  /// Creates a directory at the specified path if it does not already exist.
  /// </summary>
  /// <param name="path">The path of the directory to create.</param>
  /// <returns>The path of the created directory.</returns>
  public static string CreateDirectoryIfNotExist(string path)
  {
    // Get the project root directory
    var currentDirectory = Directory.GetCurrentDirectory();
    var projectDirectory = Directory.GetParent(currentDirectory)!.Parent!.Parent!.FullName ?? 
    throw new InvalidOperationException("Could not determine the project root directory.");

    path = Path.Combine(projectDirectory, path);

    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }

    return path;
  }
}
