# FolderSyncAppSDET

Simple .NET (C#) console app/tool that continuously syncs two folders.



## How to use

#### First open the SyncFolderConsoleApp in cmd, then write dotnet run 'parameters'

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `-source` | `string` |  Your source folder path **(needs to exist)** |
| `-copy` | `string` | Your destination folder path (will be created if missing)  |
| `-interval` | `int` |  The interval to sync **in seconds** |
| `-log` | `string` |  Your log file (.txt) path (will be created if missing) | 


#### Example

```http
dotnet run -source C:\Users\Admin\Desktop\source -copy C:\Users\Admin\Desktop\copy -interval 5 -log C:\Users\Admin\Desktop\syncLog.txt
```

#### It's not foolproof, but it works.
