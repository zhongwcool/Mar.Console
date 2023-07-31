# Mar.Console

## About

Now we have ConsoleUtil and JsonUtil in this tool.

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

You can use this tool like `JsonUtil.Load<MyModel>(file);`

#### Save

You can use this tool like `JsonUtil.Save<MyModel>(file, model);` or `JsonUtil.Save(file, json);`