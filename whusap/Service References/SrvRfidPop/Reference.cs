﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace whusap.SrvRfidPop {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SrvRfidPop.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ProWhcol133", ReplyAction="http://tempuri.org/IService1/ProWhcol133Response")]
        string ProWhcol133(string PAID, string RFID, string EVNT, string ORNO, string DATE, string LOGN, string PROC, string REFCNTD, string REFCNTU);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ProWhcol133Ora", ReplyAction="http://tempuri.org/IService1/ProWhcol133OraResponse")]
        bool ProWhcol133Ora(string PAID, string RFID, string EVNT, string ORNO, string LOGN, string PROC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Update133ss", ReplyAction="http://tempuri.org/IService1/Update133ssResponse")]
        bool Update133ss(string PAID, string RFID, string EVNT, string ORNO, string DATE, string LOGN, string PROC, string REFCNTD, string REFCNTU);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SelectWhcol133Oss", ReplyAction="http://tempuri.org/IService1/SelectWhcol133OssResponse")]
        System.Data.DataTable SelectWhcol133Oss(string RFID, string EVNT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SelectWhcol133ORfidss", ReplyAction="http://tempuri.org/IService1/SelectWhcol133ORfidssResponse")]
        System.Data.DataTable SelectWhcol133ORfidss(string RFID, string EVNT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SelectWhcol131Ora", ReplyAction="http://tempuri.org/IService1/SelectWhcol131OraResponse")]
        System.Data.DataTable SelectWhcol131Ora(string PAID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SelectTicol011McnoOra", ReplyAction="http://tempuri.org/IService1/SelectTicol011McnoOraResponse")]
        System.Data.DataTable SelectTicol011McnoOra(string MCNO, string STAT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertTicol080Ora", ReplyAction="http://tempuri.org/IService1/InsertTicol080OraResponse")]
        bool InsertTicol080Ora(string ORNO, string PONO, string ITEM, string CWAR, string QUNE, string LOGN, string DATE, string PROC, string CLOT, string REFCNTD, string REFCNTU, string PDAT, string PICK, string OORG);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SelectWhcol133Evnt", ReplyAction="http://tempuri.org/IService1/SelectWhcol133EvntResponse")]
        System.Data.DataTable SelectWhcol133Evnt(string RFID, string EVNT, string PROC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateWhcol131", ReplyAction="http://tempuri.org/IService1/UpdateWhcol131Response")]
        bool UpdateWhcol131(string PAID, string QTYA, string STAT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InitProcRfid", ReplyAction="http://tempuri.org/IService1/InitProcRfidResponse")]
        void InitProcRfid(string RFID, string EVNT, string LOGN, string PROC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InitProcRfid022", ReplyAction="http://tempuri.org/IService1/InitProcRfid022Response")]
        void InitProcRfid022(string RFID, string EVNT, string LOGN, string PROC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InitProcRfid025", ReplyAction="http://tempuri.org/IService1/InitProcRfid025Response")]
        void InitProcRfid025(string RFID, string EVNT, string LOGN, string PROC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Updtwhcol133RfidSS", ReplyAction="http://tempuri.org/IService1/Updtwhcol133RfidSSResponse")]
        bool Updtwhcol133RfidSS(string PAID, string RFID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SelectWhcol133OPaidAssing", ReplyAction="http://tempuri.org/IService1/SelectWhcol133OPaidAssingResponse")]
        System.Data.DataTable SelectWhcol133OPaidAssing(string RFID, string EVNT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Insert133ss", ReplyAction="http://tempuri.org/IService1/Insert133ssResponse")]
        bool Insert133ss(string PAID, string RFID, string EVNT, string ORNO, string DATE, string LOGN, string PROC, string REFCNTD, string REFCNTU);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : whusap.SrvRfidPop.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<whusap.SrvRfidPop.IService1>, whusap.SrvRfidPop.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ProWhcol133(string PAID, string RFID, string EVNT, string ORNO, string DATE, string LOGN, string PROC, string REFCNTD, string REFCNTU) {
            return base.Channel.ProWhcol133(PAID, RFID, EVNT, ORNO, DATE, LOGN, PROC, REFCNTD, REFCNTU);
        }
        
        public bool ProWhcol133Ora(string PAID, string RFID, string EVNT, string ORNO, string LOGN, string PROC) {
            return base.Channel.ProWhcol133Ora(PAID, RFID, EVNT, ORNO, LOGN, PROC);
        }
        
        public bool Update133ss(string PAID, string RFID, string EVNT, string ORNO, string DATE, string LOGN, string PROC, string REFCNTD, string REFCNTU) {
            return base.Channel.Update133ss(PAID, RFID, EVNT, ORNO, DATE, LOGN, PROC, REFCNTD, REFCNTU);
        }
        
        public System.Data.DataTable SelectWhcol133Oss(string RFID, string EVNT) {
            return base.Channel.SelectWhcol133Oss(RFID, EVNT);
        }
        
        public System.Data.DataTable SelectWhcol133ORfidss(string RFID, string EVNT) {
            return base.Channel.SelectWhcol133ORfidss(RFID, EVNT);
        }
        
        public System.Data.DataTable SelectWhcol131Ora(string PAID) {
            return base.Channel.SelectWhcol131Ora(PAID);
        }
        
        public System.Data.DataTable SelectTicol011McnoOra(string MCNO, string STAT) {
            return base.Channel.SelectTicol011McnoOra(MCNO, STAT);
        }
        
        public bool InsertTicol080Ora(string ORNO, string PONO, string ITEM, string CWAR, string QUNE, string LOGN, string DATE, string PROC, string CLOT, string REFCNTD, string REFCNTU, string PDAT, string PICK, string OORG) {
            return base.Channel.InsertTicol080Ora(ORNO, PONO, ITEM, CWAR, QUNE, LOGN, DATE, PROC, CLOT, REFCNTD, REFCNTU, PDAT, PICK, OORG);
        }
        
        public System.Data.DataTable SelectWhcol133Evnt(string RFID, string EVNT, string PROC) {
            return base.Channel.SelectWhcol133Evnt(RFID, EVNT, PROC);
        }
        
        public bool UpdateWhcol131(string PAID, string QTYA, string STAT) {
            return base.Channel.UpdateWhcol131(PAID, QTYA, STAT);
        }
        
        public void InitProcRfid(string RFID, string EVNT, string LOGN, string PROC) {
            base.Channel.InitProcRfid(RFID, EVNT, LOGN, PROC);
        }
        
        public void InitProcRfid022(string RFID, string EVNT, string LOGN, string PROC) {
            base.Channel.InitProcRfid022(RFID, EVNT, LOGN, PROC);
        }
        
        public void InitProcRfid025(string RFID, string EVNT, string LOGN, string PROC) {
            base.Channel.InitProcRfid025(RFID, EVNT, LOGN, PROC);
        }
        
        public bool Updtwhcol133RfidSS(string PAID, string RFID) {
            return base.Channel.Updtwhcol133RfidSS(PAID, RFID);
        }
        
        public System.Data.DataTable SelectWhcol133OPaidAssing(string RFID, string EVNT) {
            return base.Channel.SelectWhcol133OPaidAssing(RFID, EVNT);
        }
        
        public bool Insert133ss(string PAID, string RFID, string EVNT, string ORNO, string DATE, string LOGN, string PROC, string REFCNTD, string REFCNTU) {
            return base.Channel.Insert133ss(PAID, RFID, EVNT, ORNO, DATE, LOGN, PROC, REFCNTD, REFCNTU);
        }
    }
}
