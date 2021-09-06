namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Picture
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Pic { get; set; }

        [Required]
        [StringLength(10)]
        public string About { get; set; }

        public int InfoID { get; set; }
    }
}
