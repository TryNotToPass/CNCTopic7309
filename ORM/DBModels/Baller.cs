namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Baller
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TeamName { get; set; }

        [Required]
        [StringLength(10)]
        public string Position { get; set; }

        public int Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birth { get; set; }

        [StringLength(50)]
        public string University { get; set; }
    }
}
