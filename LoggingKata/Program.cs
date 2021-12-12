using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized . . . . . . . we care!");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                logger.LogError("lines not read");
            }

            if (lines.Length == 1)
            {
                logger.LogWarning("lines not split with .Split() in project file TacoParser");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable point1 = null;
            ITrackable point2 = null;

            double distance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        point1 = locA;
                        point2 = locB;
                    }

                }
            }
                Console.WriteLine("The Two Taco Bells furthest from each other are listed below!");
                Console.WriteLine($"Name: {point1.Name} Lat: {point1.Location.Latitude}, Long: {point1.Location.Longitude}");
                Console.WriteLine($"Name: {point2.Name} Lat: {point2.Location.Latitude}, Long: {point2.Location.Longitude}");
                Console.WriteLine($"{point1.Name} and {point2.Name} are the furthest away from each other with distance being approx. {Math.Round(distance * 0.00062137)} miles apart.");
        }
    }
}
