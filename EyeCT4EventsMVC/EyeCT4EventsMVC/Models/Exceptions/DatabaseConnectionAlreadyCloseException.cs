// <copyright file="DatabaseConnectionAlreadyCloseException.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Exceptions
{
    public class DatabaseConnectionAlreadyCloseException : Exception
    {
        public DatabaseConnectionAlreadyCloseException(string message)
            : base("De database connectie is al gesloten: " + message)
        {
        }
    }
}