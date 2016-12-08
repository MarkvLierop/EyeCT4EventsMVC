using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Exceptions
{
    public class FoutBijUitvoerenQueryException : Exception
    {
        public FoutBijUitvoerenQueryException(string message)
            : base("De database query kan niet uitgevoerd worden. \n De volgende fout wordt meegegeven: " + message)
        {

        }
    }
}