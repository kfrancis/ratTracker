using System;
using System.Collections.Generic;
using System.Text;

namespace RatTracker.Schools
{
    public struct GeoCoordinate
    {
        private readonly string latlng;
        private readonly string name;

        public string LatLng { get { return latlng; } }
        public string Name { get { return name; } }

        public GeoCoordinate(double latitude, double longitude, string name)
        {
            this.latlng = $"[{latitude},{longitude}]";
            this.name = name;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", latlng, Name);
        }
    }
}
