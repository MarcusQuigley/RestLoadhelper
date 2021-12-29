using System.Diagnostics;
if (args == null || args.Length < 2)
{
    DisplayHelp();
    return;
}

var verb = args[0];
var address = args[1];
var secondsgap = (args.Length > 2) ? int.Parse(args[2]) : 2;
var minutes = (args.Length > 3) ? int.Parse(args[3]) : 2;
var requests = (args.Length > 4) ? int.Parse(args[4]) : 10;
var workers = (args.Length > 5) ? int.Parse(args[5]) : 2;

var fireTimer = new Timer((o) =>
{
    using (var aProcess = new Process())
    {

        var processInfo = new ProcessStartInfo
        {
            FileName = "hey",  //https://github.com/rakyll/hey
            Arguments = $" -n {requests} -c {workers} -m {verb} -T 'application/x-www-form-urlencoded'  {address}"
        };
        aProcess.StartInfo = processInfo;
        aProcess.Start();
    }
}, null, 1000, secondsgap * 1000);

var endTimer = new Timer((o) =>
{
    Console.WriteLine($"Program is exiting after {minutes} minutes");
    Environment.Exit(0);
}, null, minutes * 60000, 1000);

Console.Read();


void DisplayHelp()
{
    Console.WriteLine("LBTester is a tool to load balance a website, kinda");
    Console.WriteLine("Usage : lbtester <verb> <address> <secondgap> <minutes> <requests> <workers>");
    Console.WriteLine("verb: GET, POST");
    Console.WriteLine("address: Web address");
    Console.WriteLine("secondgap: Number of seconds gap");
    Console.WriteLine("minutes: For how long");
    Console.WriteLine("requests: Number of requests each call");
    Console.WriteLine("workers: Concurrent workers");

}


