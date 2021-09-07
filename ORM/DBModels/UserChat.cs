namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserChat
    {
        public int ID { get; set; }

        public Guid UserID { get; set; }

        [Required]
        [StringLength(2000)]
        public string Chat { get; set; }

        [Required]
        [StringLength(10)]
        public string About { get; set; }

        public DateTime Date { get; set; }
    }
}
