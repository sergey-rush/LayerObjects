using System.Collections.Generic;
using Newtonsoft.Json;

namespace LOB.Core
{
    //var data = "{\"type\": \"FeatureCollection\",\"features\": [
    //{\"type\": \"Feature\",
    //\"id\": 0,
    //\"geometry\": {\"type\": \"Point\", \"coordinates\": [55.831903, 37.411961]},
    //\"properties\": {\"balloonContentHeader\": \"<font size=3><b>BalloonContentHeader</b></font>\", \"balloonContentBody\": \"<p>BalloonContentBody</p>\", \"balloonContentFooter\": \"<font size=1>balloonContentFooter</font>\", \"clusterCaption\": \"<strong>Кластер точек</strong>\", \"hintContent\": \"<strong>Текст подсказки</strong>\"}}]}";

    public class Properties
    {
        [JsonProperty(PropertyName = "balloonContentHeader")]
        public string BalloonContentHeader { get; set; }

        [JsonProperty(PropertyName = "balloonContentBody")]
        public string BalloonContentBody { get; set; }

        [JsonProperty(PropertyName = "balloonContentFooter")]
        public string BalloonContentFooter { get; set; }

        [JsonProperty(PropertyName = "clusterCaption")]
        public string ClusterCaption { get; set; }

        [JsonProperty(PropertyName = "hintContent")]
        public string HintContent { get; set; }

    }
    public class GeoData
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "features")]
        public List<Feature> Features { get; set; }
    }


    public class Feature
    {
        public Feature()
        {
            Type = "Feature";
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public Properties Properties { get; set; }
    }

    

    public class Geometry
    {
        public Geometry()
        {
            Type = "Point";
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class Point
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}