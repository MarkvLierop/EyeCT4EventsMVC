// <copyright file="FoutBijOpslaanBestandException.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Exceptions
{
    public class FoutBijOpslaanBestandException : Exception
    {
        public FoutBijOpslaanBestandException(string message)
            : base("Bestand kan niet opgeslagen worden. \n" + message)
        {
        }
    }
}