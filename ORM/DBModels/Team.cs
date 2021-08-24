namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TeamName { get; set; }

        [Required]
        [StringLength(50)]
        public string Local { get; set; }

        public int BallerCount { get; set; }

        [Required]
        [StringLength(50)]
        public string Owner { get; set; }

        [StringLength(50)]
        public string HomeCourt { get; set; }

        [StringLength(100)]
        public string TeamColor { get; set; }
    }
}
