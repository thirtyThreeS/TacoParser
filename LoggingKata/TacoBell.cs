using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    internal class TacoBell : ITrackable
    {
        public TacoBell(double latitude, double longitude, string name)
        {
            Name = name;
            Location = new Point { Longitude = longitude, Latitude = latitude };
        }

        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
