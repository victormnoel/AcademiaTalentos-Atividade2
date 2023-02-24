using ConsoleApp1.Controller;
using Microsoft.Xrm.Sdk;
using MyWork2.SharedProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork2.ClassLibrary
{
    public class DeleteOpportunity : IPlugin
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

            Entity opportunityPreImage = (Entity)context.PreEntityImages["PreImage"];
            EntityReference accountReference = opportunityPreImage.Attributes.Contains("parentaccountid") ? opportunityPreImage.GetAttributeValue<EntityReference>("parentaccountid") : null;

            if (accountReference != null)
            {
                Entity account = contaController.GetAccountById(accountReference.Id, "vct_vlr_total_oportunidades");
                opportunityController.Decrement(accountReference, account);
            }
        }
    }
}
