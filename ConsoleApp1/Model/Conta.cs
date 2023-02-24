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
    public class Conta
    {
        #region PROPS
        public CrmServiceClient ServiceClient { get; set; }
        public string LogicalName { get; set; }
        #endregion

        #region Construtor
        public Conta(CrmServiceClient serviceClient)
        {
            this.ServiceClient = serviceClient;
            this.LogicalName = "account";
        }
        #endregion

        public Guid CreateAccount(string name, string cnpj, decimal despesasReceitas, string empresaSubsidiarioCnpj, int numeroFuncionario, int regiao)
        {
            Entity conta = new Entity(this.LogicalName);
            conta["name"] = name;
            conta["vct_cnpj"] = cnpj;
            conta["vct_desp_receita"] = despesasReceitas;
            Entity buscaSubsidiario = this.GetAccountByCnpj(empresaSubsidiarioCnpj);
            if (buscaSubsidiario != null)
                conta["vct_empresa_subsidiaria"] = new EntityReference(this.LogicalName, buscaSubsidiario.Id);
            conta["vct_numerosdefuncionarios"] = numeroFuncionario;
            conta["vct_regiao"] = new OptionSetValue(regiao);
            return ServiceClient.Create(conta);

        }
        public Entity GetAccountByCnpj(string cnpj)
        {
            QueryExpression queryAccount = new QueryExpression(this.LogicalName);
            queryAccount.ColumnSet.AddColumns("accountid");
            queryAccount.Criteria.AddCondition("vct_cnpj", ConditionOperator.Equal, cnpj);
            EntityCollection entidades = this.ServiceClient.RetrieveMultiple(queryAccount);
            if (entidades.Entities.Count > 0)
                return this.ServiceClient.RetrieveMultiple(queryAccount).Entities[0];
            else
                return null;
        }

    }
}
