using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhcol028
    {
        [DataMember(Order = 0)]
        [Column(Name = "PAID")] 
        public string PAID{ get; set; }
        [DataMember(Order = 1)]
        [Column(Name = "CDIS")]
        public string CDIS{ get; set; }
        [DataMember(Order = 2)]
        [Column(Name = "EMNO")]
        public string EMNO{ get; set; }
        [DataMember(Order = 3)]
        [Column(Name = "SITM")]
        public string SITM{ get; set; }
        [DataMember(Order = 4)]
        [Column(Name = "SWAR")]
        public string SWAR{ get; set; }
        [DataMember(Order = 5)]
        [Column(Name = "SLOC")]
        public string SLOC{ get; set; }
        [DataMember(Order = 6)]
        [Column(Name = "SLOT")]
        public string SLOT{ get; set; }
        [DataMember(Order = 7)]
        [Column(Name = "SQTY")]
        public string SQTY{ get; set; }
        [DataMember(Order = 8)]
        [Column(Name = "TITM")]
        public string TITM{ get; set; }
        [DataMember(Order = 9)]
        [Column(Name = "TWAR")]
        public string TWAR{ get; set; }
        [DataMember(Order = 10)]
        [Column(Name = "TLOC")]
        public string TLOC{ get; set; }
        [DataMember(Order = 11)]
        [Column(Name = "TLOT")]
        public string TLOT{ get; set; }
        [DataMember(Order = 12)]
        [Column(Name = "TQTY")]
        public string TQTY{ get; set; }
        [DataMember(Order = 13)]
        [Column(Name = "LOGN")]
        public string LOGN{ get; set; }
        [DataMember(Order = 14)]
        [Column(Name = "DATR")]
        public string DATR{ get; set; }
        [DataMember(Order = 15)]
        [Column(Name = "PROC")]
        public string PROC{ get; set; }
        [DataMember(Order = 16)]
        [Column(Name = "SORN")]
        public string SORN{ get; set; }
        [DataMember(Order = 17)]
        [Column(Name = "SPON")]
        public string SPON{ get; set; }
        [DataMember(Order = 18)]
        [Column(Name = "TORN")]
        public string TORN{ get; set; }
        [DataMember(Order = 19)]
        [Column(Name = "TPON")]
        public string TPON{ get; set; }
        [DataMember(Order = 20)]
        [Column(Name = "MESS")]
        public string MESS{ get; set; }
        [DataMember(Order = 21)]
        [Column(Name = "REFCNTD")]
        public string REFCNTD{ get; set; }
        [DataMember(Order = 22)]
        [Column(Name = "REFCNTU")]
        public string REFCNTU{ get; set; }
        [DataMember(Order = 23)]
        public bool Error { get; set; }
        [DataMember(Order = 24)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 25)]
        public string SuccessMsg { get; set; }
        [DataMember(Order = 26)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 27)]
        public string UNIT { get; set; }
        [DataMember(Order = 28)]
        public string USER { get; set; }
        [DataMember(Order = 29)]
        public string WHLOT { get; set; }
        [DataMember(Order = 30)]
        public string KTLC { get; set; }
        [DataMember(Order = 31)]
        public string SUBI { get; set; }
    }
}
