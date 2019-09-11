namespace TheTankGame.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(
            IReader reader, 
            IWriter writer, 
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

            this.isRunning = false;
        }

        public void Run()
        {
            this.isRunning = true;
            while(this.isRunning == true)
            {
                List<string> inputParameters = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                
                writer.WriteLine(this.commandInterpreter.ProcessInput(inputParameters));
                if(inputParameters[0] == "Terminate")
                {
                    this.isRunning = false;
                }
            }
        }
    }
}