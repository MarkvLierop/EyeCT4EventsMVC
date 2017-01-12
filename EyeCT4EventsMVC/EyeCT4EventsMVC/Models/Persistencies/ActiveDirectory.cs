using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Web;
using EyeCT4EventsMVC.Models.Domain_Classes.Gebruikers;
using EyeCT4EventsMVC.Models.Interfaces;

namespace EyeCT4EventsMVC.Models.Persistencies
{
    public class ActiveDirectory : IActiveDirectory
    {
        DirectoryEntry LDAPConnection;
        DirectorySearcher ds;
        private DirectoryEntry ConnectieBezoekerOu()
        {
            return new DirectoryEntry("LDAP://192.168.15.10/OU=Bezoekers,OU=Gebruikers,OU=EyeCT4Events,DC=EyeCT4Events,DC=nl");
        }
        private DirectoryEntry ConnectieBeheerderOu()
        {
            return new DirectoryEntry("LDAP://192.168.15.10/OU=Beheerder,OU=Gebruikers,OU=EyeCT4Events,DC=EyeCT4Events,DC=nl");
        }
        private DirectoryEntry ConnectieMedewerkerOu()
        {
            return new DirectoryEntry("LDAP://192.168.15.10/OU=Medewerkers,OU=Gebruikers,OU=EyeCT4Events,DC=EyeCT4Events,DC=nl");
        }
        private DirectoryEntry ConnectieGebruikersOu()
        {
            return new DirectoryEntry("LDAP://192.168.15.10/OU=Gebruikers,OU=EyeCT4Events,DC=EyeCT4Events,DC=nl");
        }
        private DirectoryEntry ConnectieGroepenOU()
        {
            return new DirectoryEntry("LDAP://192.168.15.10/OU=Groepen,OU=EyeCT4Events,DC=EyeCT4Events,DC=nl");
        }
        private DirectoryEntry LDAPConnectie(DirectoryEntry LDAPConnectie)
        {
            LDAPConnection = LDAPConnectie;
            LDAPConnection.Username = @"EyeCT4Events\Administrator";
            LDAPConnection.Password = "EyeCT4Events";

            return LDAPConnection;
        }
        private DirectoryEntry GebruikerDirectoryEntry(string gebruikersnaam)
        {
            DirectorySearcher searcher = new DirectorySearcher(LDAPConnectie(ConnectieBezoekerOu()));
            searcher.Filter = "(sAMAccountName=" + gebruikersnaam + ")";
            SearchResult result = searcher.FindOne();

            if (result != null)
            {
                return new DirectoryEntry(result.Path);
            }
            return null;
        }
        /// <summary>
        /// Public Methods
        /// </summary>
        /// <param name="gebruikersnaam"></param>
        /// <param name="wachtwoord"></param>
        /// <returns></returns>
        public bool GebruikerAuthentiseren(string gebruikersnaam, string wachtwoord)
        {
            LDAPConnection = ConnectieGebruikersOu();
            LDAPConnection.Username = @"EyeCT4Events\" + gebruikersnaam;
            LDAPConnection.Password = wachtwoord;
            try
            {
                if (LDAPConnection.NativeGuid == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void CreateUserAccount(string userName, string userPassword) //type
        {
            DirectoryEntry ouEntry = LDAPConnectie(ConnectieBezoekerOu());

            try
            {
                DirectoryEntry childEntry = ouEntry.Children.Add("CN=" + userName, "user");
                ouEntry.Username = userName;
                ouEntry.Password = userPassword;
                childEntry.CommitChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }

            DirectorySearcher searcher = new DirectorySearcher(LDAPConnectie(ConnectieGebruikersOu()));
            searcher.Filter = "(sAMAccountName=" + userName + ")";
            searcher.PropertiesToLoad.Add("samaccountname");

            SearchResult result = searcher.FindOne();

            if (result != null)
            {
                // create new object from search result    
                DirectoryEntry entryToUpdate = result.GetDirectoryEntry();

                entryToUpdate.Properties["samaccountname"].Value = userName;
            }
        }
        public void CreateGroup(string groepNaam)
        {
            DirectoryEntry ouEntry = LDAPConnectie(ConnectieGroepenOU());

            try
            {
                DirectoryEntry childEntry = ouEntry.Children.Add("CN=" + groepNaam, "group");
                childEntry.CommitChanges();
                ouEntry.CommitChanges();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<Gebruiker> GetAlleGebruikers()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();

            DirectorySearcher ds = new DirectorySearcher(LDAPConnectie(ConnectieGebruikersOu()));
            SearchResultCollection src = ds.FindAll();
            foreach (SearchResult sr in src)
            {
                if (sr.Path.IndexOf("CN") != -1)
                {
                    Bezoeker b = new Bezoeker();
                    b.Gebruikersnaam = sr.Properties["samaccountname"][0].ToString();
                    b.Voornaam = sr.Properties["givenname"][0].ToString();
                    b.Achternaam = sr.Properties["sn"][0].ToString();
                    gebruikers.Add(b);
                }
            }

            return gebruikers;
        }
        public Gebruiker GebruikerZoeken(string gebruikersnaam)
        {
            Gebruiker gebruiker = null;

            DirectorySearcher searcher = new DirectorySearcher(LDAPConnectie(ConnectieBezoekerOu()));
            searcher.Filter = "(sAMAccountName=" + gebruikersnaam + ")";
            SearchResult result = searcher.FindOne();

            if (result != null)
            {
                gebruiker = new Bezoeker();
                gebruiker.Gebruikersnaam = result.Properties["samaccountname"][0].ToString();
                gebruiker.Voornaam = result.Properties["givenname"][0].ToString();
                gebruiker.Achternaam = result.Properties["sn"][0].ToString();
            }
            return gebruiker;
        }
        public void GebruikerUpdaten(string gebruikersnaam, string title, string street, string l, string st, string postcode,
                                    string afdeling, string mail, string manager, string telnummer)
        {
            DirectorySearcher searcher = new DirectorySearcher(LDAPConnectie(ConnectieGebruikersOu()));
            searcher.Filter = "(sAMAccountName=" + gebruikersnaam + ")";
            searcher.PropertiesToLoad.Add("title");
            searcher.PropertiesToLoad.Add("samaccountname");
            searcher.PropertiesToLoad.Add("streetAddress");
            searcher.PropertiesToLoad.Add("1");
            searcher.PropertiesToLoad.Add("st");
            searcher.PropertiesToLoad.Add("postalCode");
            searcher.PropertiesToLoad.Add("department");
            searcher.PropertiesToLoad.Add("mail");
            searcher.PropertiesToLoad.Add("telephoneNumber");

            SearchResult result = searcher.FindOne();

            if (result != null)
            {
                // create new object from search result    
                DirectoryEntry entryToUpdate = result.GetDirectoryEntry();

                entryToUpdate.Properties["title"].Value = title;
                entryToUpdate.Properties["samaccountname"].Value = gebruikersnaam;
                entryToUpdate.Properties["streetAddress"].Value = street;
                entryToUpdate.Properties["l"].Value = l;
                entryToUpdate.Properties["st"].Value = st;
                entryToUpdate.Properties["postalCode"].Value = postcode;
                entryToUpdate.Properties["department"].Value = afdeling;
                entryToUpdate.Properties["mail"].Value = mail;
                entryToUpdate.Properties["telephoneNumber"].Value = telnummer;

                entryToUpdate.CommitChanges();
            }
        }  // Werkt.
        public void VerwijderenGebruiker(string gebruikersnaam)
        {
            DirectoryEntry ouEntry = LDAPConnectie(ConnectieBezoekerOu());

            try
            {
                ouEntry.Children.Remove(GebruikerDirectoryEntry(gebruikersnaam));
                ouEntry.CommitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ToevoegenAanGroep(string gebruikersnaam, string groepnaam)
        {
            try
            {
                DirectoryEntry userGroup = LDAPConnectie(ConnectieGroepenOU());
                
                userGroup.Invoke("Add", new object[] { "LDAP://192.168.15.10/CN=Jan,OU=Gebruikers,OU=EyeCT4Events,DC=EyeCT4Events,DC=nl" });
                userGroup.CommitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GebruikerToevoegenGroep(string gebruiker, string groepNaam) // werkt niet
        {
            DirectoryEntry ent = LDAPConnectie(ConnectieGroepenOU());
            ent.AuthenticationType = AuthenticationTypes.Secure;

            ent.Invoke("remove", new object[] { GebruikerDirectoryEntry(gebruiker) });
            ent.CommitChanges();
        }        // Foutmelding: server is unwilling to process the request.
    }
}