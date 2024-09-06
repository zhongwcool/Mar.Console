using Newtonsoft.Json;

namespace Mar.Cheese;

public static class JsonUtil
{
    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="json">json data</param>
    public static void Save(string filename, string json)
    {
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs);
        sw.WriteLine(json);
        sw.Flush();
        sw.Close();
        fs.Close();
        $"successfully save data to {filename}".PrintGreen();
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="json">json data</param>
    public static async void SaveAsync(string filename, string json)
    {
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs);
        await sw.WriteLineAsync(json);
        await sw.FlushAsync();
        sw.Close();
        fs.Close();
        $"successfully save data to {filename}".PrintGreen();
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="model">json in model</param>
    public static void Save(string filename, object? model)
    {
        var json = JsonConvert.SerializeObject(model);
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs);
        sw.WriteLine(json);
        sw.Flush();
        sw.Close();
        fs.Close();
        $"successfully save data to {filename}".PrintGreen();
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="model">json in model</param>
    public static async Task SaveAsync(string filename, object? model)
    {
        var json = JsonConvert.SerializeObject(model);
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs);
        await sw.WriteLineAsync(json);
        await sw.FlushAsync();
        sw.Close();
        fs.Close();
        $"successfully save data to {filename}".PrintGreen();
    }

    /// <summary>
    ///     Load data from json
    /// </summary>
    public static T? Load<T>(string filename)
    {
        if (!File.Exists(filename)) return default;
        using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs);
        var json = sr.ReadToEnd();
        sr.Close();
        fs.Close();

        if (string.IsNullOrEmpty(json)) return default;
        var model = JsonConvert.DeserializeObject<T>(json);
        return model;
    }

    /// <summary>
    ///     Load data from json
    /// </summary>
    public static async Task<T?> LoadAsync<T>(string filename)
    {
        if (!File.Exists(filename)) return default;
        using var fs = new FileStream(filename, FileMode.Open);
        using var sr = new StreamReader(fs);
        var json = await sr.ReadToEndAsync();
        sr.Close();
        fs.Close();

        if (string.IsNullOrEmpty(json)) return default;
        var model = JsonConvert.DeserializeObject<T>(json);
        return model;
    }
}