using System;

namespace LOB.Core
{
    public class Position
    {
        public int Id { get; set; }
        public Guid UserUid { get; set; }
        public string Title { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime Created { get; set; }

        public Position()
        {
            
        }

        public Position(Route route)
        {
            Id = route.Id;
            Lat = route.Lat;
            Lng = route.Lng;
        }
    }
}