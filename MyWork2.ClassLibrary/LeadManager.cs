using ConsoleApp1.Controller;
using ConsoleApp1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MyWork2.SharedProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace MyWork2.ClassLibrary
{
    public class LeadManager : IPlugin
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



            Entity lead = (Entity)context.InputParameters["Target"];

            Entity leadPostImage = (Entity)context.PostEntityImages["PostImage"];

            Guid leadId = lead.Id;

            OptionSetValue status = leadPostImage.Attributes.Contains("statuscode") ? leadPostImage.GetAttributeValue<OptionSetValue>("statuscode") : new OptionSetValue(0);

            if (status.Value == 3)
            {

                Entity opportunity = opportunityController.GetOpportunitydByLead(leadId, new string[] { "parentaccountid" });

                EntityReference accountReference = opportunity.Attributes.Contains("parentaccountid") ? opportunity.GetAttributeValue<EntityReference>("parentaccountid") : null;


                if (accountReference != null)
                {
                    Entity account = contaController.GetAccountById(accountReference.Id, "vct_vlr_total_oportunidades");
                    opportunityController.Increment(accountReference, account);
                }
            }



        }
    }
}
