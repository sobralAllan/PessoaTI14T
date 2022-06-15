using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PessoaTI14T
{
    class DAOPessoa
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public string resultado;
        //Declarar os vetores...
        public int[] vetorCodigo;
        public long[] vetorCPF;
        public string[] vetorNome;
        public string[] vetorTelefone;
        public string[] vetorEndereco;
        public int i;//Declarando o contador do for e do while
        public int contador;//Utilizado para contar as voltas do while
        public string msg;
        public int contarCodigo;
        public DAOPessoa()
        {
            conexao = new MySqlConnection("server=localhost;DataBase=turma14;Uid=root;password=");
            try
            {
                conexao.Open();//Tentando conectar ao BD
                
            }catch(Exception erro)
            {
                MessageBox.Show("Algo deu errado!\n\n" + erro);//Enviando a mesagem de erro aos usuários
                conexao.Close();//fechando a conexão com o banco de dados
            }
        }//fim do DAOPessoa

        public void Inserir(long cpf, string nome, string telefone, string endereco)
        {
            try
            {
                //Preparar os dados para inserir no banco
                dados = "('','" + cpf + "','" + nome + "','" + telefone + "','" + endereco + "')";
                comando = "Insert into pessoaTI14T(codigo, cpf, nome, telefone, endereco) values " + dados;

                //Executar o comando na base de dados
                MySqlCommand sql = new MySqlCommand(comando, conexao);
                resultado = "" + sql.ExecuteNonQuery();
                if (resultado == "1")
                {
                    MessageBox.Show("Cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Não Cadastrado!");
                }
            }catch(Exception erro)
            {
                MessageBox.Show("Algo deu errado!\n\n" + erro);
            }
        }//fim do método inserir

        public void PreencherVetor()
        {
            string query = "select * from pessoaTI14T";//Comando para coletar dados do banco de dados

            //Instanciando os vetores...
            vetorCodigo   = new int[100];
            vetorCPF      = new long[100];
            vetorNome     = new string[100];
            vetorTelefone = new string[100];
            vetorEndereco = new string[100];

            //Preencher os vetores criados anteriormente, ou seja, dar valores iniciais para os vetores
            for(i=0; i < 100; i++)
            {
                vetorCodigo[i]   = 0;
                vetorCPF[i]      = 0;
                vetorNome[i]     = "";
                vetorTelefone[i] = "";
                vetorEndereco[i] = "";
            }//fim do for

            //Realizar os comandos de consulta ao Banco de Dados
            MySqlCommand coletar = new MySqlCommand(query, conexao);
            //Ler os dados de acordo com o que está no Banco de Dados
            MySqlDataReader leitura = coletar.ExecuteReader(); //Variável leitura, faz uma consulta ao BD

            i = 0;//Declaração do contador 0
            contador = 0;//Declarar o contador 0 para o while
            contarCodigo = 0;//Instanciando o contador para o código
            //Preencher os vetores com dados do banco de dados
            while(leitura.Read())//Enquanto for verdadeiro, executa o que está no while
            {
                vetorCodigo[i]   = Convert.ToInt32(leitura["codigo"]);
                vetorCPF[i]      = Convert.ToInt64(leitura["cpf"]);
                vetorNome[i]     = leitura["nome"] + "";//Concateno o leitura["nome"] com aspas para torná-lo um texto
                vetorTelefone[i] = leitura["telefone"] + "";
                vetorEndereco[i] = leitura["endereco"] + "";
                contarCodigo = contador;//Armazenando a última posição do contador
                i++;//Contador sai da posição zero e vai se incrementando
                contador++;//Contar os loops do while
            }//fim do while

            leitura.Close();//Fechar a conexão e leitura do banco de dados
        }//fim do preencherVetor

        //Criar um consultar Tudo por MessageBox
        public string ConsultarTudo()
        {
            PreencherVetor();//Primeira Coisa -> Preencher os vetor com dados do BD
            msg = "";
            for(i = 0; i < contador; i++)
            {
                //Armazenar temporariamente os dados do BD na variável MSG
                msg += "Código: "     + vetorCodigo[i]   +
                       ", CPF: "      + vetorCPF[i]      +
                       ", nome: "     + vetorNome[i]     +
                       ", telefone: " + vetorTelefone[i] +
                       ", endereço: " + vetorEndereco[i] +
                       "\n\n";
            }//fim do for
            return msg;//Retorna todos os dados armazenados na variável msg
        }//fim do consultarTudo

        public int ConsultarCodigo()
        {
            PreencherVetor();//Preencher os vetores com os dados do BD
            return vetorCodigo[contarCodigo];
        }//fim do consultarCodigo

        public long ConsultarCPF(int cod)
        {
            PreencherVetor();
            for (i = 0; i < contador; i++)
            {
                if (vetorCodigo[i] == cod)
                {
                    return vetorCPF[i];
                }//fim do if
            }//fim do for
            return -1;
        }//fim do consultarCPF

        public string ConsultarNome(int cod)
        {
            PreencherVetor();
            for(i = 0; i < contador; i++)
            {
                if(vetorCodigo[i] == cod)
                {
                    return vetorNome[i];
                }
            }//fim do for
            return "Nome não encontrado!";
        }//fim do consultarNome

        public string ConsultarTelefone(int cod)
        {
            PreencherVetor();
            for(i=0; i < contador; i++)
            {
                if(vetorCodigo[i] == cod)
                {
                    return vetorTelefone[i];
                }
            }
            return "Telefone não encontrado!";
        }//fim do consultarTelefone

        public string ConsultarEndereco(int cod)
        {
            PreencherVetor();
            for(i=0; i < contador; i++)
            {
                if(vetorCodigo[i] == cod)
                {
                    return vetorEndereco[i];
                }
            }
            return "Endereço não encontrado!";
        }//fim do consultarEndereco

        public string Atualizar(int cod, string campo, string novoDado)
        {
            try
            {
                string query = "update pessoaTI14T set " + campo + " = '" + novoDado + "' where codigo = '" + cod + "'";

                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                if (resultado == "1")
                {
                    return "Atualizado!";
                }                
            }
            catch (Exception erro)
            {
                MessageBox.Show("" + erro);
            }
            return "Não Atualizado!";
        }//fim do atualizar

        public string Atualizar(int cod, string campo, long novoDado)
        {
            try
            {
                string query = "update pessoaTI14T set " + campo + " = '" + novoDado + "' where codigo = '" + cod + "'";

                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                if (resultado == "1")
                {
                    return "Atualizado!";
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("" + erro);
            }
            return "Não Atualizado!";
        }//fim do atualizar

        public void Deletar(int cod)
        {
            try
            {
                string query = "delete from pessoati14t where codigo = '" + cod + "'";
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                
                if (resultado == "1")
                {
                    MessageBox.Show("Deletado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Não Deletado!");
                }
            }
            catch(Exception erro)
            {
                MessageBox.Show("" + erro);
            }
        }//fim do deletar
    }//fim da classe
}//fim do projeto
