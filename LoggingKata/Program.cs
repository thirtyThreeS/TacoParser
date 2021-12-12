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
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized . . . . . . . we care!");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // DONE - Log and error if you get 0 lines and a warning if you get 1 line
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

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();
            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // DONE - TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // DONE - Create a `double` variable to store the distance
            ITrackable point1 = null;
            ITrackable point2 = null;

            double distance = 0;

            //// DONE - Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            ////HINT NESTED LOOPS SECTION---------------------
            //// Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                // Create a new corA Coordinate with your locA's lat and long
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    // Create a new Coordinate with your locB's lat and long
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        point1 = locA;
                        point2 = locB;
                    }

                }
            }
            //// Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            //try
            //{
                Console.WriteLine("The Two Taco Bells furthest from each other are listed below!");
                Console.WriteLine($"Name: {point1.Name} Lat: {point1.Location.Latitude}, Long: {point1.Location.Longitude}");
                Console.WriteLine($"Name: {point2.Name} Lat: {point2.Location.Latitude}, Long: {point2.Location.Longitude}");
                Console.WriteLine($"{point1.Name} and {point2.Name} are the furthest away from each other with distance being approx. {distance * 0.00062137} miles. . .");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}



        }
    }
}
