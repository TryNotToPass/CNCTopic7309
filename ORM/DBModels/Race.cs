namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Race
    {
        public int ID { get; set; }

        public int RaceNum { get; set; }

        [StringLength(10)]
        public string Score { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string TeamName { get; set; }

        public double Shoot { get; set; }

        public double ThreePoint { get; set; }

        public double Penalty { get; set; }

        public int BackBoard { get; set; }

        public int Assistance { get; set; }

        public int Block { get; set; }

        public int Steal { get; set; }

        public int Miss { get; set; }

        public int RestrictedArea { get; set; }

        public int Foul { get; set; }
    }
}
