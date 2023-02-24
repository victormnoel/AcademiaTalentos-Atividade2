using ConsoleApp1.Controller;
using Microsoft.Xrm.Sdk;
using MyWork2.SharedProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace MyWork2.ClassLibrary
{
    public class UpdateOpportunity : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            OpportunityController opportunityController = new OpportunityController(Service);
            ContaController contaController = new ContaController(Service);


            Entity opportunity = (Entity)context.InputParameters["Target"];

            EntityReference accountReference = opportunity.Attributes.Contains("parentaccountid") ? opportunity.GetAttributeValue<EntityReference>("parentaccountid") : null;

            if (accountReference != null)
            {
                Entity account = contaController.GetAccountById(accountReference.Id, "vct_vlr_total_oportunidades");
                opportunityController.Increment(accountReference, account);
  
            }
        }
    }
}
