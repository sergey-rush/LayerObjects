using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using LOB.BLL;
using LOB.Core;

namespace LOB.Web.Controllers.api
{
    public class DataController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [GuidSelector]
        public HttpResponseMessage GeoData(Guid id)
        {
            List<AttributeValue> attributeValues = Attributes.GetPagedAttributeValues(1, 10000, id);

            GeoData geoData = new GeoData();
            geoData.Type = "FeatureCollection";
            List<Feature> features = new List<Feature>();
            int counter = 1;

            foreach (AttributeValue av in attributeValues)
            {
                Feature feature = new Feature();
                feature.Id = counter;
                Geometry geometry = new Geometry();
                Point point = new Point();
                point.Latitude = av.Latitude;
                point.Longitude = av.Longitude;
                
                geometry.Coordinates = new List<double>();
                geometry.Coordinates.Add(av.Latitude);
                geometry.Coordinates.Add(av.Longitude);
                feature.Geometry = geometry;
                Properties properties = new Properties();
                properties.BalloonContentHeader = String.Format("<font size=3><b>{0}</b></font>", av.Caption);
                properties.BalloonContentBody = String.Format("<p>{0}</p>", av.Value);
                properties.BalloonContentFooter = String.Format("<font size=1>{0}</font>", av.ElementId);
                properties.ClusterCaption = String.Format("<strong>{0}</strong>", av.AttributeId);
                properties.HintContent = String.Format("<strong>{0}</strong>", av.Caption);
                feature.Properties = properties;
                features.Add(feature);
                counter++;
            }

            geoData.Features = features;


            return Request.CreateResponse(HttpStatusCode.OK, geoData, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}