// <copyright file="IReactie.cs" company="Unitech">
//     Company copyright tag.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeCT4EventsMVC.Models.Domain_Classes;

namespace EyeCT4EventsMVC.Models.Interfaces
{
    public interface IReactie
    {
        List<Reactie> ReactieBijGerapporteerdeMedia();
        List<Reactie> GerapporteerdeReactie();
    }
}
