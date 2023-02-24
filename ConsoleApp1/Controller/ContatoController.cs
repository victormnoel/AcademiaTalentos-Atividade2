using ConsoleApp1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class ContatoController
    {
        #region PROPS
        public CrmServiceClient ServiceClient { get; set; }
        public Contato Contato { get; set; }
        #endregion

        #region Construtor
        public ContatoController(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Contato = new Contato(ServiceClient);
        }
        #endregion 

        public Guid CreateContact(string nomeContato, string email, string cpf, Guid accountId)
        {
            return Contato.CreateContact(nomeContato, email,cpf,accountId);
           
        }
        public Entity GetContactByCpf(string cpf)
        {
            return Contato.GetContactByCpf(cpf);
        }
    }
}
