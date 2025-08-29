using System.Text;
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
        if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
        if (json == null) throw new ArgumentNullException(nameof(json));

        try
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"保存JSON文件失败: {ex.Message}", ex);
        }
    }

    /// <summary>
    ///     save data to json file
    /// </summary>
    /// <param name="filename">target file</param>
    /// <param name="json">json data</param>
    public static async Task SaveAsync(string filename, string json)
    {
        if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
        if (json == null) throw new ArgumentNullException(nameof(json));

        try
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            await sw.WriteLineAsync(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"保存JSON文件失败: {ex.Message}", ex);
        }
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
        if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
        if (!File.Exists(filename)) return default;

        try
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            var json = sr.ReadToEnd();
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"读取JSON文件失败: {ex.Message}", ex);
        }
    }

    /// <summary>
    ///     Load data from json
    /// </summary>
    public static async Task<T?> LoadAsync<T>(string filename)
    {
        if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
        if (!File.Exists(filename)) return default;

        try
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            var json = await sr.ReadToEndAsync();
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"读取JSON文件失败: {ex.Message}", ex);
        }
    }
}