using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface IActiveDirectory
    {
        bool GebruikerAuthentiseren(string gebruikersnaam, string wachtwoord);
        string CreateUserAccount(string userName, string userPassword);
        void ToevoegenAanGroep(string gebruikersnaam, string groepnaam);
    }
}