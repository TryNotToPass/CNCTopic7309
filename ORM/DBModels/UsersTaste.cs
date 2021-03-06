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

        public int? FavoriteRaceID { get; set; }

        public int? BadTemperBallerID { get; set; }

        public int? FoulKingBallerID { get; set; }
    }
}
