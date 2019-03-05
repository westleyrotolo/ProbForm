using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbForm.Models
{
    public class Player
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
    }
}

