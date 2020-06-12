using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assessment_CS {
    public static class Interface {
        public static void MenuPrincipal() {
            Console.Clear();
            Console.WriteLine("Aniversariantes diarios!");
            Console.WriteLine("-------------------------------------------");
            AniversarianteDoDia();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Escolha uma das opcoes abaixo: ");
            Console.WriteLine("1 - Pesquisar Pessoa ");
            Console.WriteLine("2 - Adicionar Pessoas ");
            Console.WriteLine("3 - Editar Pessoa ");
            Console.WriteLine("4 - Sair ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao) {
                case 1:
                    BuscaPessoa();
                    break;
                case 2:
                    CadastrarPessoa();
                    break;
                case 3:
                    EditarPessoa();
                    break;
                case 4:
                    Deletar();
                    break;
                case 5:
                    Console.WriteLine("Tchau Amigao!");
                    break;
                default:
                    Console.WriteLine("Opcao errada amigao!");
                    break;
            }
        }

        private static void Deletar() {
            throw new NotImplementedException();
        }

        private static void EditarPessoa() {
            int i = 0;
            Console.Clear();
            Console.WriteLine("Entre com o nome da pessoa que deseja editar:");
            string[] nomeESobrenome = Console.ReadLine().Split(' ');
            string nome = nomeESobrenome[0];


            var listaDePessoasEncontradas = Dados.BuscarTodasPessoas(nome);

            if (listaDePessoasEncontradas.Count() == 0) {
                Console.WriteLine("Nenhum usuario Encontrado");
            } else {
                Console.WriteLine("");
                Console.WriteLine("Pesssoas Encontradas: ");

                foreach (var pessoa in listaDePessoasEncontradas) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobreNome);
                    i++;
                }

                Console.WriteLine("");
                Console.WriteLine("Digite o numero correspondente a pessoa que deseja editar: ");
                int escolha = int.Parse(Console.ReadLine());

                var pessoas = Dados.BuscarTodasPessoas();

                foreach (var pessoa in listaDePessoasEncontradas) {
                    if (pessoa.Id == escolha) {
                        Pessoa pessoaVelha = Dados.BuscarPessoaPorId(escolha);
                        Console.WriteLine("Entre com o novo nome do meliante: ");
                        String nomeT = Console.ReadLine();
                        Console.WriteLine("Entre com o novo Sobrenome do meliante: ");
                        String sobrenomeT = Console.ReadLine();
                        DateTime aniversarioN = RecebeETransformaData();

                        Pessoa pessoaNova = new Pessoa(pessoaVelha.Id, nomeT, sobrenomeT, aniversarioN);

                        Dados.Editar(pessoaNova);

                        Console.WriteLine("Pessoa alterada!");
                    } else {
                        Console.WriteLine("Escolha Errada Amigao!");
                        break;
                    }
                }
            }
            VoltarProMenu();
        }

        private static void AniversarianteDoDia() {
            DateTime hj = DateTime.Today;
            var niverToday = Dados.BuscarTodasPessoas(hj);
            if (niverToday.Count() == 0) {
                Console.WriteLine("Nenhum aniversario hj amigao!!");
            } else {
                foreach (var pessoa in niverToday) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobreNome);
                }
            }
        }

        public static void CadastrarPessoa() {
            Console.Clear();

            Console.WriteLine("Entre com o nome: ");
            String nome = Console.ReadLine();
            Console.WriteLine("Entre com o sobre nome: ");
            String sobreNome = Console.ReadLine();

            DateTime aniversarioD = RecebeETransformaData();

            var pessoas = Dados.BuscarTodasPessoas();

            Pessoa p = new Pessoa(nome, sobreNome, aniversarioD);

            foreach (var pessoa in pessoas) {
                Pessoa ultimo = pessoas.Last(x => x.Id == pessoa.Id);
                p.Id = ultimo.Id + 1;
            }

            Console.WriteLine(p);
            Console.WriteLine("");
            Console.WriteLine("Esta tudo certo com a adicao? (s/n) ");
            string opcao = Console.ReadLine();
            if (opcao == "s") {
                Console.WriteLine("Ok, adicionando pessoa...");
                Dados.Salvar(p);
            } else {
                CadastrarPessoa();
            }
            VoltarProMenu();
        }

        public static DateTime RecebeETransformaData() {
            Console.WriteLine("Entre com a data de nascimento em dd/mm/yyyy: ");
            string data = Console.ReadLine();

            DateTime dataC = new DateTime();
            if (data.Contains("/")) {
                string[] vet = data.Split('/');
                int ano = int.Parse(vet[2]);
                int mes = int.Parse(vet[1]);
                int dia = int.Parse(vet[0]);
                dataC = new DateTime(ano, mes, dia);
            } else {
                Console.WriteLine("Entre com uma data valida por favor.");
                VoltarProMenu();
            }
            return dataC;
        }

        public static void BuscaPessoa() {
            int i = 0;
            Console.Clear();
            Console.WriteLine("Entre com o nome da pessoa que deseja buscar:");
            string[] nomeESobrenome = Console.ReadLine().Split(' ');
            string nome = nomeESobrenome[0];


            var listaDePessoasEncontradas = Dados.BuscarTodasPessoas(nome);

            if (listaDePessoasEncontradas.Count() == 0) {
                Console.WriteLine("Nenhum usuario Encontrado");
            } else {
                Console.WriteLine("");
                Console.WriteLine("Pesssoas Encontradas: ");

                foreach (var pessoa in listaDePessoasEncontradas) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobreNome);
                    i++;
                }

                Console.WriteLine("");
                Console.WriteLine("Digite o numero correspondente a pessoa que deseja ter mais detalhes: ");
                int escolha = int.Parse(Console.ReadLine());

                if (escolha > i || escolha < 0) {
                    Console.WriteLine("Escolha errada amigao!");
                } else {
                    Console.WriteLine(Dados.BuscarPessoaPorId(escolha));
                }
            }
            VoltarProMenu();

        }

        public static void VoltarProMenu() {
            Console.WriteLine("Aperte x para voltar ao menu.");
            string volta = Console.ReadLine();
            if (volta == "x") {
                MenuPrincipal();
            } else {
                Console.WriteLine("Opcao errada amigo!");
                VoltarProMenu();
            }
        }
    }
}
