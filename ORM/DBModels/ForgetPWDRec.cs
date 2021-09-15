namespace ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForgetPWDRec")]
    public partial class ForgetPWDRec
    {
        [Key]
        public Guid GUID { get; set; }

        public DateTime ExpireDate { get; set; }

        public int WrongTime { get; set; }
    }
}
