using ConsoleApp1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Controller
{
    public class ContaController
    {
        #region PROPS
        public CrmServiceClient ServiceClient { get; set; }
        public  Conta Conta { get; set; }
        #endregion

        #region Construtor
        public ContaController(CrmServiceClient crmServiceClient) 
        {
            this.ServiceClient = crmServiceClient;
            this.Conta = new Conta(ServiceClient);
        }
        #endregion 

        public Guid Create(string name, string cnpj, decimal despesasReceitas, string empresaSubsidiarioCnpj, int numeroFuncionario, int regiao)
        {
          
           return Conta.CreateAccount(name, cnpj, despesasReceitas, empresaSubsidiarioCnpj, numeroFuncionario, regiao);
         
        }

        public Entity GetAccountByCnpj(string cnpj)
        {
           return Conta.GetAccountByCnpj(cnpj);
        }
    }
}
