
using System.Xml.Serialization;
namespace Api2.Models
{
    [XmlRoot("ExchangeRequest")]
    public class ExchangeRequest
    {
        public string From { get; set; } 
        public string To { get; set; } 
        public decimal Value { get; set; }
    }
}
