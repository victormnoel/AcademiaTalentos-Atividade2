using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Contato
    {
        #region PROPS
        public CrmServiceClient ServiceClient { get; set; }
        public string LogicalName { get; set; }
        #endregion

        #region Construtor
        public Contato(CrmServiceClient serviceClient)
        {
            this.ServiceClient = serviceClient;
            this.LogicalName = "contact";

        }

        public Guid CreateContact(string nomeContato, string email, string cpf, Guid accountId)
        {
            Entity contato = new Entity("contact");
            contato["vct_cpf"] = cpf;
            contato["emailaddress1"] = email;
            contato["firstname"] = nomeContato;
            contato["parentcustomerid"] = new EntityReference("account", accountId);

            return ServiceClient.Create(contato);

        }
        #endregion

        public Entity GetContactByCpf(string cpf)
        {
            QueryExpression queryContact = new QueryExpression(this.LogicalName);
            queryContact.ColumnSet.AddColumns("accountid");
            queryContact.Criteria.AddCondition("vct_cpf", ConditionOperator.Equal, cpf);
            EntityCollection entidades = this.ServiceClient.RetrieveMultiple(queryContact);
            if (entidades.Entities.Count > 0)
                return this.ServiceClient.RetrieveMultiple(queryContact).Entities[0];
            else
                return null;
        }
    }
}
