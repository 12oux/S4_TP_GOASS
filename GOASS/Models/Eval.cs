using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Eval
    {
        public int EvalID { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }

    }
}
