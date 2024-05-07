using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2_Threads
{
    public class Car
    {
        public string Name { get; }
        public int Position { get; private set; } = 0;
        public int BaseSpeed { get; }
        public int ActualSpeed { get; private set; }
        public HashSet<string> ProblemsEncountered { get; private set; } = new HashSet<string>(); 
        private static readonly Random random = new Random();
        public bool Finished { get; private set; }

        public Car(string name)
        {
            Name = name;
            BaseSpeed = 120; 
            ActualSpeed = BaseSpeed;
        }

        public async Task StartRaceAsync()
        {
            Console.WriteLine($"{Name} started the race!");

            while (Position < 10 && !Finished) 
            {
                await MoveAsync();
            }

            if (Position >= 10) 
            {
                Finished = true; 
                Console.WriteLine($"{Name} finished the race!");
            }
        }

        private async Task MoveAsync()
        {
            await Task.Delay(3000); 

            if (!Finished)
            {
                string problem = GetRandomProblem();
                if (!ProblemsEncountered.Contains(problem)) 
                {
                    ProblemsEncountered.Add(problem);
                    
                }
            }

           
            Position++;
        }



        private string GetRandomProblem()
        {
            
            int randomNumber = random.Next(1, 51);
            string problem = "";

            if (randomNumber == 1)
            {
                Console.WriteLine($"{Name} has no fuel and needs to refill"); 
                Thread.Sleep(3000);
                return "Out of fuel";
            }
            else if (randomNumber <= 2)
            {
                Console.WriteLine($"{Name} has got a punctured tire and need to change it");
                Thread.Sleep(2000);
                return "Flat tire";
            }
            else if (randomNumber <= 6)
            {
                Console.WriteLine($"{Name} has hit a bird and needs to clean the windshield");
                Thread.Sleep(1000);
                return "Bird on windshield";
            }
            else if (randomNumber <= 11)
            {
                Console.WriteLine($"{Name} has a problem with the engine and is slowed down by 10 km/h");
                ActualSpeed -= 10;
                return "Engine problem";
            }

            if (!string.IsNullOrEmpty(problem) && !ProblemsEncountered.Contains(problem))
            {
                Console.WriteLine($"{Name} has encountered {problem}");
                ProblemsEncountered.Add(problem);

                return problem;
            }

            else
            {
                return ""; 
            }
        }

        public void PrintStatus()
        {
            Console.WriteLine($"Status for {Name}:");
            Console.WriteLine($"Position: {Position} km");
            Console.WriteLine($"Speed: {ActualSpeed} km/h");
            Console.WriteLine($"Problems encountered: {string.Join(", ", ProblemsEncountered)}");
        }

    }
}






