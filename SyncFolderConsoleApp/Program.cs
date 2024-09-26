using System.Runtime.CompilerServices;
using System;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Hello! Keep this app open, it syncs folders periodically!");

Console.WriteLine("Where is the original folder located? (paste it and press Enter)");
string? originalFolder = Console.ReadLine();
Console.WriteLine($"The original folder is in: {originalFolder}");

Console.WriteLine("Where is the copy folder located? (paste it and press Enter)");
string? copyFolder = Console.ReadLine();
Console.WriteLine($"The original folder is in: {copyFolder}");

Console.WriteLine("What's the interval for the sync operation? (in seconds)");
int interval;
GetIntFromText();
Console.WriteLine($"The folders should synq every {interval} seconds.");
while(true){
  SyncFolders();
  Thread.Sleep(interval * 1000); //seconds to miliseconds
}

void GetIntFromText()
{
    string? newNumber = Console.ReadLine();
    int i;
    if(int.TryParse(newNumber, out i) == false){
        Console.WriteLine("That's not a number... try again!");
        GetIntFromText();;
    }
    else{
        interval = i;
    }
}

void SyncFolders()
{
    Console.WriteLine($"Synq!");
}