using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class IntefazDAL_tticol082
    {
        tticol082 dal = new tticol082();

        public DataTable ConsultarTticol082()
        {
            return dal.ConsultarTticol082();
        }

        public bool ActualizarPrioridadTticol082(Ent_tticol082 myObj)
        {
            return dal.ActualizarPrioridadTticol082(myObj);
        }

        public bool InsertarregistroItticol093(Ent_tticol082 myObj)
        {
            return dal.InsertarregistroItticol093(myObj);
        }

        public bool InsertarregistroItticol082(Ent_tticol082 myObj)
        {
            return dal.InsertarregistroItticol082(myObj);
        }

        public int PrioridadMaxima()
        {
            return dal.PrioridadMaxima();
        }

        public DataTable ConsultaPlantTticol082()
        {
            return dal.ConsultaPlantTticol082();
        }
        //JC 281021 Cambiar Prioridad a picks
        public DataTable ConsultaPlantTticol182()
        {
            return dal.ConsultaPlantTticol182();
        }
        public DataTable ConsultaWarehouseTticol082( string plant )
        {
            return dal.ConsultaWarehouseTticol082( plant );
        }
        //JC 281021 Cambiar Prioridad a picks
        public DataTable ConsultaWarehouseTticol182(string plant)
        {
            return dal.ConsultaWarehouseTticol182(plant);
        }
        public object ConsultaMachineTticol082(string plant, string warehouse)
        {
            return dal.ConsultaMachineTticol082(plant,warehouse);
        }
        //JC 281021 Cambiar Prioridad a picks
        public object ConsultaMachineTticol182(string plant, string warehouse)
        {
            return dal.ConsultaMachineTticol082(plant, warehouse);
        }
        public DataTable ConsultarTticol082PorPlant(string plant,string warehouse,string machine)
        {
            return dal.ConsultarTticol082PorPlant(plant,warehouse,machine);
        }
        //JC 281021 Cambiar Prioridad a picks
        //public DataTable ConsultarTticol182PorPlant(string plant, string warehouse, string machine)
        public DataTable ConsultarTticol182PorPlant(string plant)
        {
            return dal.ConsultarTticol182PorPlant(plant);
        }

        public DataTable ConsultarPalletIDTticol082(string PalletID)
        {
            return dal.ConsultarPalletIDTticol082(PalletID);
        }
        //JC 021021 Desasignar Pallets de un Picking
        public DataTable ConsultarPalletID_x_Picking(string PalletID)
        {
            return dal.ConsultarPalletID_x_Picking(PalletID);
        }

        public DataTable ConsultarPalletID_x_FreePicking(string PalletID)
        {
            return dal.ConsultarPalletID_x_FreePicking(PalletID);
        }
        //JC 281021 Listado de Picking que se pueden desasignar
        public DataTable ConsultarPicksTticol082(string Picks)
        {
            return dal.ConsultarPicksTticol082(Picks);
        }

        public DataTable ConsultarPicksTticol182(string Picks)
        {
            return dal.ConsultarPicksTticol182(Picks);
        }

        public DataTable ConsultarPalletIDTticol083(string PalletID)
        {
            return dal.ConsultarPalletIDTticol083(PalletID);
        }

        public DataTable ConsultarPalletIDOnTunnelTticol083(string PickID)
        {
            return dal.ConsultarPalletIDOnTunnelTticol083(PickID);
        }

        public bool Actualizartticol022(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol022(myObj);
        }

        public bool Actualizartticol042(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol042(myObj);
        }

        public bool Actualizartticol082(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol082(myObj);
        }

        public bool Actualizartticol082Stat(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol082Stat(myObj);
        }


        public bool Actualizartticol082SinRandom(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol082SinRandom(myObj);
        }

        public bool Actualizartticol222(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol222(myObj);
        }

        public bool Actualizartticol242(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol242(myObj);
        }

        public bool Actualizartwhcol131(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131(myObj);
        }

        public DataTable ConsultarPalletIDTticol082MFG(string PalletID)
        {
            return dal.ConsultarPalletIDTticol082MFG(PalletID);
        }

        public DataTable ConsultarPalletIDTticol082OnTunnelMFG(string PickID)
        {
            return dal.ConsultarPalletIDTticol082OnTunnelMFG(PickID);
        }

        public bool Actualizartticol022MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol022MFG(myObj);
        }

        public bool Actualizartticol042MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol042MFG(myObj);
        }

        public bool Actualizartticol082MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol082MFG(myObj);
        }

        public bool Actualizartticol222MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol222MFG(myObj);
        }

        public bool Actualizartwhcol131MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131MFG(myObj);
        }

        //public bool Actualizartticol082MFG(Ent_tticol082 myObj)
        //{
        //    return dal.Actualizartticol082MFG(myObj);
        //}

        public bool Actualizartwhcol130MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol130MFG(myObj);
        }

        public bool Actualizartwhcol130(Ent_tticol082 MyObj)
        {
            return dal.Actualizartwhcol130(MyObj);
        }

        public bool Actualizartticol242MFG(Ent_tticol082 MyObj)
        {
            return dal.Actualizartticol222MFG(MyObj);
        }

        public DataTable ConsultarRegistrosBloquedos()
        {
            return dal.ConsultarRegistrosBloquedos();
        }

        public bool Actualizartwhcol131STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131STAT(myObj);
        }

        public bool Actualizartwhcol130STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol130STAT(myObj);
        }
        public bool Actualizartticol022STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol022STAT(myObj);
        }

        public bool Actualizartticol042STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol042STAT(myObj);
        }

        public bool Actualizartwhcol131Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131Cant(myObj);
        }

        public bool Actualizartwhcol130Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol130Cant(myObj);
        }
        public bool Actualizartticol222Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol222Cant(myObj);
        }

        public bool Actualizartticol242Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol242Cant(myObj);
        }

        public DataTable ConsultarOtrosRegistros()
        {
            return dal.ConsultarOtrosRegistros();
        }

        public DataTable ConsultarTticol082PorPlantPono(string plant,int prio,string advs)
        {
            return dal.ConsultarTticol082PorPlantPono(plant,prio,advs);
        }

        public DataTable ConsultarRegistrosBloquedos(string _operator)
        {
            return dal.ConsultarRegistrosBloquedos(_operator);
        }

        public DataTable GetMachine()
        {
            return dal.GetMachine();
        }

        public DataTable GetPicks(Ent_tticol082 MyObj082)
        {
            return dal.GetPicks(MyObj082);
        }

        public bool UpdatePrio(Ent_tticol082 MyObj082)
        {
            return dal.UpdatePrio(MyObj082);
        }

        public DataTable getNextPrio(Ent_tticol082 MyObj082)
        {
            return dal.getNextPrio(MyObj082);
        }

        public DataTable ExistPrio(Ent_tticol082 MyObj082)
        {
            return dal.ExistPrio(MyObj082);
        }

        public bool UpdatePrio182(Ent_tticol082 MyObj082)
        {
            return dal.UpdatePrio182(MyObj082);
        }

        public DataTable getNextPrio182(Ent_tticol082 MyObj082)
        {
            return dal.getNextPrio182(MyObj082);
        }

        public DataTable ExistPrio182(Ent_tticol082 MyObj082)
        {
            return dal.ExistPrio182(MyObj082);
        }

        public DataTable ConsultarPalletIDTticol082PIckAbreb(string PickID)
        {
            return dal.ConsultarPalletIDTticol082PIckAbreb(PickID);
        }

        public bool Actualizartticol082Pick(Ent_tticol082 MyObj)
        {

            return dal.Actualizartticol082Pick(MyObj);
        }

        public DataTable GetTticol082PaidsOrno(Ent_tticol082 ent_tticol082)
        {
            return dal.GetTticol082PaidsOrno(ent_tticol082);
        }

        public DataTable GetTticol082LastPick(Ent_tticol082 ent_tticol082)
        {
            return dal.GetTticol082LastPick(ent_tticol082);
        }


        public bool UpdateTticol082Pick(Ent_tticol082 obj082)
        {
            return dal.UpdateTticol082Pick(obj082);
        }

        public bool DeleteTticol182P(EntidadPicking MyObjPicking)
        {
            return dal.DeleteTticol182P(MyObjPicking);
        }
    }
}
