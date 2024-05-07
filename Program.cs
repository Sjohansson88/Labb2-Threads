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


            while (true)
            {
                Console.WriteLine("Type 'status' to get race status:");
                string input = Console.ReadLine();


                if (input.ToLower() == "status")
                {
                    Console.WriteLine("");
                    car1.PrintStatus();
                    Console.WriteLine("");
                    car2.PrintStatus();
                    Console.WriteLine("");
                }

                await Task.WhenAny(race1Task, race2Task);


                if (car1.Finished || car2.Finished)
                {
                    break;
                }



            }


            string winner = car1.Finished ? car1.Name :
                            car2.Finished ? car2.Name :
                            "No winner";
            Console.WriteLine($"The winner is {winner}!");


            Console.WriteLine("");
            car1.PrintStatus();
            Console.WriteLine("");
            car2.PrintStatus();

            Console.ReadKey();
        }

    }
}
