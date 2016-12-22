using EyeCT4EventsMVC.Models.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface IReactie
    {
        List<Reactie> ReactieBijMedia(int MediaID);
    }
}
