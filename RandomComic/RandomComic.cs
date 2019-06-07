using System;
using Comics;

namespace RandomComic
{
    class Program
    {
        static void Main(string[] args)
        {
            string comicProvider = "xkcd";

            IComicGenerator comicGenerator = 
                ComicGeneratorFactory.GetComicGenerator(comicProvider);

            Console.WriteLine("Hit 'Enter' for a random comic!");
            while(true)
            {
                try
                {
                    Console.ReadLine();

                    //Get random comic and print it to the console
                    Comic comic = comicGenerator.RandomComic();
                    Console.WriteLine('"' + comic.title + '"' + 
                        " published on " + comic.published_date + ':');
                    Console.WriteLine();
                    Console.WriteLine(comic.transcript);
                }

                catch
                {
                    Console.WriteLine("Failed to generate comic. Quitting...");
                    break;
                }
            }
        }
    }

    class ComicGeneratorFactory
    {
        //Factory for IComicGenerators
        public static IComicGenerator GetComicGenerator(string provider)
        {
            switch(provider)
            {
                case "xkcd":
                    return new xkcdGenerator();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}