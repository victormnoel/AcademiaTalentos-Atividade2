using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MyWork2.SharedProject.Controller;
using System.Web.Services.Description;
using ConsoleApp1.Model;

namespace MyWork2.SharedProject.Model
{
    public class Opportunity
    {
        #region PROPS
        public IOrganizationService service { get; set; }
        public string logicalName { get; set; } 

        #endregion

        #region Construtor
        public Opportunity(IOrganizationService service)
        {
            this.service = service;
            this.logicalName = "opportunity";
        }
        #endregion

        public Entity GetOpportunitydByLead(Guid leadId, string[] columns)
        {
            QueryExpression queryOpportunity = new QueryExpression(this.logicalName);
            queryOpportunity.ColumnSet.AddColumns(columns);
            queryOpportunity.Criteria.AddCondition("originatingleadid", ConditionOperator.Equal, leadId);
            EntityCollection entitycollection = this.service.RetrieveMultiple(queryOpportunity);

            if (entitycollection.Entities.Count() > 0)
                return entitycollection.Entities.FirstOrDefault();
            else
                Console.WriteLine("Nenhuma oportunidade encontrada");
            return null;
        }

        public void Decrement(EntityReference accountReference, Entity account)
        {
           
            int valorTotalOportunidades = account.Attributes.Contains("vct_vlr_total_oportunidades") ? account.GetAttributeValue<int>("vct_vlr_total_oportunidades") : 0;
            if (valorTotalOportunidades != 0)
            {
                valorTotalOportunidades--;
                Entity accountUpdate = new Entity("account");
                accountUpdate.Id = accountReference.Id;
                accountUpdate["vct_vlr_total_oportunidades"] = valorTotalOportunidades;
                service.Update(accountUpdate);
            }
        }
        public void Increment(EntityReference accountReference ,Entity account)
        {
            int valorTotalOportunidades = account.Attributes.Contains("vct_vlr_total_oportunidades") ? account.GetAttributeValue<int>("vct_vlr_total_oportunidades") : 0;
            valorTotalOportunidades++;

            Entity accountUpdate = new Entity("account");
            accountUpdate.Id = accountReference.Id;
            accountUpdate["vct_vlr_total_oportunidades"] = valorTotalOportunidades;
            service.Update(accountUpdate);
        }
    }
}
