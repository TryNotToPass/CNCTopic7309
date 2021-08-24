namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsersTaste")]
    public partial class UsersTaste
    {
        public int ID { get; set; }

        public Guid UserID { get; set; }

        public int? FavoriteBallerID { get; set; }

        public int? FavoriteTeamID { get; set; }

        public int? HateBallerID { get; set; }

        public int? HateTeamID { get; set; }
    }
}
