using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_ttdcol137
    {
        [DataMember(Order = 0)]
        [Column(Name = "Paid")]
        public string Paid { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "Orno")]
        public string Orno { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "Clot")]
        public string Clot { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "Cwar")]
        public string Cwar { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "Item")]
        public string item { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "Loca")]
        public string Loca { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "Qtya")]
        public decimal Qtya { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "Date")]
        public string Date { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "User")]
        public string User { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "Dele")]
        public int Dele { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public Ent_ttdcol137()
        {
            Paid = string.Empty;
            Orno = string.Empty;
            Clot = string.Empty;
            Cwar = string.Empty;
            Loca = string.Empty;
            Qtya = 0;
            Date = string.Empty;
            User = string.Empty;
            refcntd = 0;
            refcntu = 0;
        }
        public Ent_ttdcol137(string pPaid, string pOrno, string pClot, string pCwar, string pLoca, decimal pQtya,
                             string pDate, string pUser, int pDele, int prefcntd, int prefcntu)
        {
            this.Paid = pPaid;
            this.Orno = pOrno;
            this.Clot = pClot;
            this.Cwar = pCwar;
            this.Loca = pLoca;
            this.Qtya = pQtya;
            this.Date = pDate;
            this.User = pUser;
            this.Dele = pDele;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
        }

        public string Lot { get; set; }
    }
}

