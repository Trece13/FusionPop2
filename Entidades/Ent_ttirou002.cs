using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace whusa
{
    [DataContract]
    public class Ent_ttirou002
    {
        [DataMember(Order = 0)]
        [Column(Name = "MCNO")]
        public string mcno { get; set; }
        [Column(Name = "DSCA")]
        public string dsca { get; set; }
        [Column(Name = "CWOC")]
        public string cwoc { get; set; }
        [Column(Name = "CCUR")]
        public string ccur { get; set; }
        [Column(Name = "MCR1")]
        public string mcr1 { get; set; }
        [Column(Name = "MCR2")]
        public string mcr2 { get; set; }
        [Column(Name = "MCR3")]
        public string mcr3 { get; set; }
        [Column(Name = "CPCP")]
        public string cpcp { get; set; }
        [Column(Name = "MCCP")]
        public string mccp { get; set; }
        [Column(Name = "MDCP")]
        public string mdcp { get; set; }
        [Column(Name = "TLTP")]
        public string tltp { get; set; }
        [Column(Name = "CTKT")]
        public string ctkt { get; set; }
        [Column(Name = "TNUM")]
        public string tnum { get; set; }
        [Column(Name = "REFCNTD")]  
        public string refcntd { get; set; }
        [Column(Name = "REFCNTU")]
        public string refcntu { get; set; }

        public Ent_ttirou002()
        {
            this.mcno    =string.Empty;
            this.dsca    =string.Empty;
            this.cwoc    =string.Empty;
            this.ccur    =string.Empty;
            this.mcr1    =string.Empty;
            this.mcr2    =string.Empty;
            this.mcr3    =string.Empty;
            this.cpcp    =string.Empty;
            this.mccp    =string.Empty;
            this.mdcp    =string.Empty;
            this.tltp    =string.Empty;
            this.ctkt    =string.Empty;
            this.tnum    =string.Empty;
            this.refcntd =string.Empty;
            this.refcntu =string.Empty; 
        }

        public Ent_ttirou002(string _mcno,string _dsca,string _cwoc,string _ccur,string _mcr1,string _mcr2,string _mcr3,string _cpcp,string _mccp,string _mdcp,string _tltp,string _ctkt,string _tnum,string _refcntd,string _refcntu)
        {
            this.mcno    =_mcno;
            this.dsca    =_dsca;
            this.cwoc    =_cwoc;
            this.ccur    =_ccur;
            this.mcr1    =_mcr1;
            this.mcr2    =_mcr2;
            this.mcr3    =_mcr3;
            this.cpcp    =_cpcp;
            this.mccp    =_mccp;
            this.mdcp    =_mdcp;
            this.tltp    =_tltp;
            this.ctkt    =_ctkt;
            this.tnum    =_tnum;
            this.refcntd = _refcntd;
            this.refcntu = _refcntu;           
        }
    }
}
