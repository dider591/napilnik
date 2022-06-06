using System;
using System.IO;

namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder logger1 = new Pathfinder(new ConsoleLogWritter());
            Pathfinder logger2 = new Pathfinder(new FileLogWritter());
            Pathfinder logger3 = new Pathfinder(new SecureFileLogWritter());
            Pathfinder logger4 = new Pathfinder(new SecureConsoleLogWritter());
            Pathfinder logger5 = new Pathfinder(new SecureConsoleLogAndFileWritter());

            logger5.Find();
        }
    }

    interface ILogger
    {
        void Find();
    }

    interface Iwriter
    {
        public void WriteError(string message);
    }

    class Pathfinder : ILogger
    {
        private Iwriter _writer;
        private string _message;

        public Pathfinder(Iwriter writer)
        {
            _writer = writer;
            _message = "LOG";
        }

        public void Find()
        {
            _writer.WriteError(_message);
        }       
    }

    class ConsoleLogWritter : Iwriter
    {
        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : Iwriter
    {
        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureFileLogWritter : FileLogWritter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class SecureConsoleLogWritter : ConsoleLogWritter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class SecureConsoleLogAndFileWritter : FileLogWritter
    {
        public override void WriteError(string message)
        {
            Console.WriteLine(message);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }
}