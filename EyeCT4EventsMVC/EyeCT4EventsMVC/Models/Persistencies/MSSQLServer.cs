// <copyright file="MSSQLServer.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Exceptions;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public abstract class MSSQLServer
    {
        protected string connString;
        protected SqlCommand command;
        protected SqlConnection sQLcon;
        protected SqlDataReader reader;

        protected Gebruiker gebruiker;
        protected Categorie[] categorieArray;

        public void Connect()
        {
            try
            {
                this.connString = "Data Source=192.168.10.21,20;Initial Catalog=EyeCT4Events;Persist Security Info=True;User ID=sa;Password=PTS16";
                sQLcon = new SqlConnection(connString);
                sQLcon.Open();
            }
            catch (SqlException e)
            {
                throw new NoDatabaseConnectionException(e.Message);
            }
        }

        public void Close()
        {
            try
            {
                sQLcon.Close();
                sQLcon.Dispose();
            }
            catch (SqlException e)
            {
                throw new DatabaseConnectionAlreadyCloseException(e.Message);
            }
        }
    }
}