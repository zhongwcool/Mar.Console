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
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
        using var sw = new StreamWriter(fs);
        sw.WriteLine(json);
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="json">json data</param>
    public static async Task SaveAsync(string filename, string json)
    {
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
        using var sw = new StreamWriter(fs);
        await sw.WriteLineAsync(json);
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="model">json in model</param>
    public static void Save(string filename, object? model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var json = JsonConvert.SerializeObject(model);
        Save(filename, json);
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="model">json in model</param>
    public static async Task SaveAsync(string filename, object? model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var json = JsonConvert.SerializeObject(model);
        await SaveAsync(filename, json);
    }

    /// <summary>
    ///     Load data from json
    /// </summary>
    public static T? Load<T>(string filename)
    {
        if (!File.Exists(filename)) return default;

        using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var sr = new StreamReader(fs);

        var json = sr.ReadToEnd();
        return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
    }

    /// <summary>
    ///     Load data from json
    /// </summary>
    public static async Task<T?> LoadAsync<T>(string filename)
    {
        if (!File.Exists(filename)) return default;

        using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var sr = new StreamReader(fs);

        var json = await sr.ReadToEndAsync();
        return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
    }
}