using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol182
    {
        [DataMember(Order = 1)]
        [Column(Name = "OORG")]
        public string OORG { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "ORNO")]
        public string ORNO { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "PONO")]
        public string PONO { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "ADVS")]
        public string ADVS { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "ITEM")]
        public string ITEM { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "QTYT")]
        public string QTYT { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "UNIT")]
        public string UNIT { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "CWAR")]
        public string CWAR { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "MCNO")]
        public string MCNO { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "TIME")]
        public string TIME { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "PRIO")]
        public string PRIO { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "PICK")]
        public string PICK { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "PAID")]
        public string PAID { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "LOCA")]
        public string LOCA { get; set; }

        [DataMember(Order = 15)]
        [Column(Name = "LOGN")]
        public string LOGN { get; set; }

        [DataMember(Order = 16)]
        [Column(Name = "STAT")]
        public string STAT { get; set; }

        [DataMember(Order = 17)]
        [Column(Name = "REFCNTD")]
        public string REFCNTD { get; set; }

        [DataMember(Order = 18)]
        [Column(Name = "REFCNTU")]
        public string REFCNTU { get; set; }

        public Ent_tticol182() 
        {
            OORG    = " ";
            ORNO    = " ";
            PONO    = " ";
            ADVS    = " ";
            ITEM    = " ";
            QTYT    = " ";
            UNIT    = " ";
            CWAR    = " ";
            MCNO    = " ";
            TIME    = " ";
            PRIO    = " ";
            PICK    = " ";
            PAID    = " ";
            LOCA    = " ";
            LOGN    = " ";
            STAT    = " ";
            REFCNTD = " ";
            REFCNTU = " ";
        }
    }
}
