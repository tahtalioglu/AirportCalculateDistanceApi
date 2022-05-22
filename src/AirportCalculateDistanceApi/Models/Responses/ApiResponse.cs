using System.Text.Json.Serialization;

namespace AirportCalculateDistanceApi.Models.Responses
{
    public class ApiResponse
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
       
        [JsonPropertyName("city_iata")]
        public string CityIATA { get; set; }

        [JsonPropertyName("iata")]
        public string IATA { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("timezone_region_name")]
        public string TimeZone { get; set; }

        [JsonPropertyName("country_iata")]
        public string CountryIATA { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("hubs")]
        public int Hubs { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }
    }

}
