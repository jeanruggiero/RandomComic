using System;
using xkcd;

namespace RandomComic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hit 'Enter' for a random comic!");
            while(true)
            {
                Console.ReadLine();
                Comic comic = new Comic();
                Console.WriteLine('"' + comic.title + '"' + " published on " +
                    comic.published_date + ':');
                Console.WriteLine();
                Console.WriteLine(comic.transcript);
            }
        }
    }
}