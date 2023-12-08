using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betway_result_center_api.Models
{
    public class CountriesListModel
    {
     
        public List<Countrieslist> Countries { get; set; }

    }
    public class Countrieslist
    {
        public Int16 CountryId { get; set; }
        public string CountryName { get; set; }
    }
}