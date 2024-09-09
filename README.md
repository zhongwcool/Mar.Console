# Mar.Console

[![latest version](https://img.shields.io/nuget/v/Mar.Console)](https://www.nuget.org/packages/Mar.Console) [![downloads](https://img.shields.io/nuget/dt/Mar.Console)](https://www.nuget.org/packages/Mar.Console)

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

![How to use ConsoleUtil](https://raw.githubusercontent.com/zhongwcool/Mar.Console/main/Assets/145158.png)

## JsonUtil

Convert object to json string.

### How to use

#### Load

Load mail list from json file.

```c#
    private void Prepare()
    {
        var model = JsonUtil.Load<Mail>(JSON_FILE);
    }
    
    private const string JSON_FILE = "mails.json";
```

#### Save

You can just call like `JsonUtil.Save<MyModel>(file, model);` or `JsonUtil.Save(file, json);`