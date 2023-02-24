using ConsoleApp1.Controller;
using ConsoleApp1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Workflow.ComponentModel.Compiler;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();
            ContaController contaController = new ContaController(serviceClient);
            ContatoController contatoController = new ContatoController(serviceClient);
            Entity buscarConta = new Entity();
            Entity buscarContato = new Entity();



            while (buscarConta != null)
            {
                Console.WriteLine("Por favor informe o nome da conta :");
                string nomeConta = Console.ReadLine();
                Console.WriteLine("Por favor digite o CNPJ  :");
                string cnpj = Console.ReadLine();
                Console.WriteLine("Por favor digite as depesas da receita");
                decimal despesasReceita = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Por favor digite o CNPJ da Empresa subsidiário");
                string empresaSubsidiariaCnpj = Console.ReadLine();
                Console.WriteLine("Por favor digite o número de funcionários");
                int numeroFuncionario = int.Parse(Console.ReadLine());
                Console.WriteLine("Por favor digite o número da sua região : ");
                Console.WriteLine("1 - Norte ");
                Console.WriteLine("2 - Sul ");
                Console.WriteLine("3 - Centro Oeste");
                Console.WriteLine("4 - Sudeste");
                Console.WriteLine("5 - Nordeste");
                int regiao = int.Parse(Console.ReadLine());


                buscarConta = contaController.GetAccountByCnpj(cnpj);

                if (buscarConta == null)
                {

                    Guid accountId = contaController.Create(nomeConta, cnpj, despesasReceita, empresaSubsidiariaCnpj, numeroFuncionario, regiao);
                    Console.Clear();
                    Console.WriteLine("Conta criada com sucesso");

                    Console.WriteLine("Deseja criar um contato para esta conta? (S/N)");
                    string criarContato = Console.ReadLine().ToUpper();
                    if (criarContato == "S")
                    {
                        while (buscarContato != null)
                        {
                            Console.WriteLine("Por favor digite o Nome do contato :");
                            string nomeContato = Console.ReadLine();
                            Console.WriteLine("Por favor digite Email :");
                            string email = Console.ReadLine();
                            Console.WriteLine("Por favor digite o CPF :");
                            string cpf = Console.ReadLine();

                            buscarContato = contatoController.GetContactByCpf(cpf);

                            if (buscarContato == null)
                            {
                                contatoController.CreateContact(nomeContato, email, cpf, accountId);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Cpf ja cadastrado");
                                Console.WriteLine("Por favor insira os dados novamente :");

                            }
                        }

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Operações realizadas com sucesso!");
                        Console.ReadKey();
                        return;
                    }
                        
                }
                else
                {
                    Console.WriteLine("Cnpj digitado ja esta vinculado a uma conta");
                    Console.WriteLine("Por favor insira os dados novamente :");
                }
               
            }
            Console.Clear();
            Console.WriteLine("Operações realizadas com sucesso!");
            Console.ReadKey();
        }
    }
}
