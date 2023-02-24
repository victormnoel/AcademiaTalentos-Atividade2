using Microsoft.Rest;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            String url = "org5fef03d8";
            String clientId = "aab3b356-0aee-440f-8558-810905c2d5b3";
            String clientSecret = "24n8Q~~AKQmmMtuCwAcynYNjUV.OLutyZKmoqc0V";

            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão realizada com sucesso");
            else
                Console.WriteLine("Error na conexão");
            return serviceClient;
        }
    }
}
