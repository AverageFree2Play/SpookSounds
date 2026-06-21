using System;
using System.Media;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Spook Sounds Player";
        Console.WriteLine("Press 'Q' at any time to quit.\n");

        string[] soundPlaylist = new string[]
        {
            "spook1.wav",
            "spook2.wav",
            "spook3.wav",
            "spook4.wav",
            "spook5.wav",
            "spook6.wav"
        };

        int minSeconds = 10;
        int maxSeconds = 10;

        Random random = new Random();

        _ = Task.Run(async () =>
        {
            while (true)
            {
                int nextDelay = random.Next(minSeconds, maxSeconds + 1);
                await Task.Delay(nextDelay*1000);

                // 2. Pick a random file from the playlist array
                int randomIndex = random.Next(0, soundPlaylist.Length);
                string chosenSound = soundPlaylist[randomIndex];

                // 3. Play the chosen file if it exists
                if (File.Exists(chosenSound))
                {
                    using (SoundPlayer player = new SoundPlayer(chosenSound))
                    {
                        player.Play();
                    }
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Played: {chosenSound} | Next in {nextDelay:F1}s");
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Tried to play {chosenSound} but file was not found!");
                }
            }
        });

        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Q)
            {
                Console.WriteLine("\nExiting application. Goodbye!");
                break;
            }
        }
    }
}