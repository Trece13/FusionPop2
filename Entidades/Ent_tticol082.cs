using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace whusa.Entidades
{
    public class Ent_tticol082
    {
        public Ent_tticol082()
        {
            this.Error = false;
            this.OORG = string.Empty;
            this.ORNO = string.Empty;
            this.OSET = string.Empty;
            this.PONO = string.Empty;
            this.SQNB = string.Empty;
            this.ADVS = string.Empty;
            this.ITEM = string.Empty;
            this.QTYT = string.Empty;
            this.UNIT = string.Empty;
            this.CWAR = string.Empty;
            this.PRIO = string.Empty;
            this.PRIT = string.Empty;
            this.LOGN = string.Empty;
            this.STAT = string.Empty;
            this.TIME = string.Empty;
            this.TBL  = string.Empty;
            this.PAID = string.Empty;
            this.DSCA = string.Empty;
            this.MCNO = string.Empty;
            this.DSCAM= string.Empty;
            this.LOCA = string.Empty;
            this.CUNI = string.Empty;
            this.QTYC = string.Empty;
            this.PICK = string.Empty;
            this.PICK_URL = string.Empty;
            //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
            this.RAND = string.Empty;
            //JC 041021 Actualizar solo el pick que estan cambiando
            this.OLDP = string.Empty;
        }

        public string OORG { get; set; }
        public string ORNO { get; set; }
        public string OSET { get; set; }
        public string PONO { get; set; }
        public string SQNB { get; set; }
        public string ADVS { get; set; }
        public string ITEM { get; set; }
        public string QTYT { get; set; }
        public string UNIT { get; set; }
        public string CWAR { get; set; }
        public string PRIO { get; set; }
        public string PRIT { get; set; }
        public string LOGN { get; set; }
        public string STAT { get; set; }
        public string TIME { get; set; }
        public object SQBN { get; set; }
        public string TBL   { get; set; }
        public string PAID  { get; set; }
        public string DSCA { get; set; }
        public string MCNO { get; set; }
        public string DSCAM { get; set; }
        public string LOCA { get; set; }
        public string CUNI { get; set; }
        public string QTYC { get; set; }
        public string PICK_URL { get; set; }
        public bool Error { get; set; }
        public string TipeMsgJs { get; set;}
        public string ErrorMsg { get; set; }
        public string SuccessMsg { get; set; }

        public string STAP { get; set; }

        public string PICK { get; set; }

        public string TYPW { get; set; }
        //JC 230721 Cambio para que se envíe el dato con el numero aleatorio
        public string RAND { get; set; }
        //JC 041021 Actualizar solo el pick que estan cambiando
        public string OLDP { get; set; }
    }
}
