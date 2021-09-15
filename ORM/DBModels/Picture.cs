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

        [StringLength(50)]
        public string PicTitle { get; set; }

        [StringLength(2000)]
        public string PicText { get; set; }

        [StringLength(1000)]
        public string HyperLink { get; set; }
    }
}
