using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa
{
    
    public class Ent_ttccol307
    {
        public Ent_ttccol307()
        {
            this.Error = false;
            this.ErrorMsg = string.Empty;
            this.USRR_AUX = string.Empty;
            this.STAT_AUX = string.Empty;
            this.PAID_AUX = string.Empty;
            this.PROC_AUX = string.Empty;
            this.CWAR_AUX = string.Empty;
            this.USRR = string.Empty;
            this.STAT = string.Empty;
            this.PAID = string.Empty;
            this.PROC = string.Empty;
            this.CWAR = string.Empty;
        }

        [DataMember(Order = 2)]
        [Column(Name = "USRR")]
        public string USRR { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "STAT")]
        public string STAT { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "PAID")]
        public string PAID { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "REFCNTD")]
        public int REFCNTD { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "REFCNTU")]
        public int REFCNTU { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "PROC")]
        public string PROC { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "CWAR")]
        public string CWAR { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "USRR_AUX")]
        public string USRR_AUX { get; set; }


        [DataMember(Order = 10)]
        [Column(Name = "STAT_AUX")]
        public string STAT_AUX { get; set; }


        [DataMember(Order = 11)]
        [Column(Name = "PAID_AUX")]
        public string PAID_AUX { get; set; }


        [DataMember(Order = 12)]
        [Column(Name = "REFCNTD_AUX")]
        public int REFCNTD_AUX { get; set; }


        [DataMember(Order = 13)]
        [Column(Name = "REFCNTU_AUX")]
        public int REFCNTU_AUX { get; set; }


        [DataMember(Order = 14)]
        [Column(Name = "PROC_AUX")]
        public string PROC_AUX { get; set; }


        [DataMember(Order = 15)]
        [Column(Name = "CWAR_AUX")]
        public string CWAR_AUX { get; set; }

        public bool Error { get; set; }
        public string TypeMsgJs { get; set; }
        public string ErrorMsg { get; set; }
        public string SuccessMsg { get; set; }
    }
}
