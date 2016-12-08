using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Exceptions
{
    public class NoDatabaseConnectionException : Exception
    {
        public NoDatabaseConnectionException(string message)
            : base("Er is geen verbinding met de database.")
        {
        }
    }
}