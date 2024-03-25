using Newtonsoft.Json;

namespace Mar.Cheese;

public static class JsonUtil
{
    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="json">json data</param>
    public static async void Save(string filename, string json)
    {
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs);
        await sw.WriteLineAsync(json);
        await sw.FlushAsync();
        sw.Close();
        fs.Close();
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="model">json in model</param>
    public static async Task Save<T>(string filename, T model)
    {
        var json = JsonConvert.SerializeObject(model);
        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var sw = new StreamWriter(fs);
        await sw.WriteLineAsync(json);
        await sw.FlushAsync();
        sw.Close();
        fs.Close();
    }

    /// <summary>
    ///     Load data from json
    /// </summary>
    public static async Task<T?> Load<T>(string filename)
    {
        using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs);
        var json = await sr.ReadToEndAsync();
        sr.Close();
        fs.Close();

        if (string.IsNullOrEmpty(json)) return default;
        var model = JsonConvert.DeserializeObject<T>(json);
        return model;
    }
}