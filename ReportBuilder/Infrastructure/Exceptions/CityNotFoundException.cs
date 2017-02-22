using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCatcher.Models.Exceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}