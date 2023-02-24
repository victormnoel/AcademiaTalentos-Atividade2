using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MyWork2.SharedProject.Model;

namespace MyWork2.SharedProject.Controller
{
    public class OpportunityController
    {
        #region PROPS
        public IOrganizationService service { get; set; }  
        public Opportunity opportunity { get; set; }
        #endregion

        #region Construtor
        public OpportunityController(IOrganizationService service)
        {
            this.service = service;
            this.opportunity = new Opportunity(service);
        }
        #endregion


        public Entity GetOpportunitydByLead(Guid leadId, string[] columns)
        {
            return opportunity.GetOpportunitydByLead(leadId, columns);
        }
        public void Decrement(EntityReference accountReference, Entity account) 
        { 
            opportunity.Decrement(accountReference, account);
        }
        public void Increment(EntityReference accountReference, Entity account) 
        {
            opportunity.Increment(accountReference, account);
        }
    }
}
