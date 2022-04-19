using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Eval
    {
        public int EvalID { get; set; }

        [DisplayName("Nom de famille")]
        public string NomClient { get; set; }

        [DisplayName("Prénom")]
        public string PrenomClient { get; set; }

        [DisplayName("Courriel")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Le format de l'adresse courriel est invalide")]
        [EmailAddress(ErrorMessage = "Veuillez saisir une adresse courriel valide")]
        public string Email { get; set; }
        public string Comment { get; set; }

    }
}
