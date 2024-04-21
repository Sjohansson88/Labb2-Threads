namespace Labb2_Threads
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var car1 = new Car("Ferrari");
            var car2 = new Car("McLaren");

            var race1Task = car1.StartRaceAsync();
            var race2Task = car2.StartRaceAsync();

            // Starta en oändlig loop för att lyssna på användarinput
            while (true)
            {
                Console.WriteLine("Type 'status' to get race status:");
                string input = Console.ReadLine();

                // Om användaren skrev in "status", skriv ut status för båda bilarna
                if (input.ToLower() == "status")
                {
                    Console.WriteLine("");
                    car1.PrintStatus();
                    Console.WriteLine("");
                    car2.PrintStatus();
                    Console.WriteLine("");
                }
                // Vänta på att antingen race1Task eller race2Task ska slutföras eller användaren ska skriva in "status"
                await Task.WhenAny(race1Task, race2Task);

                // Kontrollera om någon av racen är klara
                if (car1.Finished || car2.Finished)
                {
                    break; // Om någon bil har gått i mål, avsluta loopen
                }

                // Läs in användarinput
            
            }

            // När loopen avslutas har en vinnare blivit bestämd, skriv ut vinnaren
            string winner = car1.Finished ? car1.Name :
                            car2.Finished ? car2.Name :
                            "No winner";
            Console.WriteLine($"The winner is {winner}!");

            // Skriv ut slutlig status för båda bilarna
            Console.WriteLine("");
            car1.PrintStatus();
            Console.WriteLine("");
            car2.PrintStatus();

            Console.ReadKey();
        }

    }
}
