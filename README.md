# Mar.Console

[![latest version](https://img.shields.io/nuget/v/Mar.Console)](https://www.nuget.org/packages/Mar.Console) [![downloads](https://img.shields.io/nuget/dt/Mar.Console)](https://www.nuget.org/packages/Mar.Console)

Modify namespace form Mar.Console to Mar.Cheese to avoid conflicts.

## ExceptionUtil

Get text message from exception.

## SystemUtil

Print System info.

## EmailUtil

Send mail directly with smtp.

## ConsoleUtil

Print console message in different color.

### How to use

You can use this tool like `"".PrintGreen();`

```c#
$"任务执行结果：{result}".PrintGreen();
```
### Demo

![](https://i.imgur.com/gOyzemr.png)

## JsonUtil

Convert object to json string.

### How to use

#### Load

Load mail list from json file.

```c#
    private void Prepare()
    {
        var task = Task.Run(() =>
        {
            var model = JsonUtil.Load<MailMode>(JSON_FILE);
            return model?.Mails;
        });

        task.ContinueWith(_ =>
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                if (task.Result == null) return;
                foreach (var mail in task.Result)
                {
                    EmailList.Add(mail);
                }
            });
        });
    }
    
    private const string JSON_FILE = "mails.json";
```

#### Save

You can use this tool like `JsonUtil.Save<MyModel>(file, model);` or `JsonUtil.Save(file, json);`