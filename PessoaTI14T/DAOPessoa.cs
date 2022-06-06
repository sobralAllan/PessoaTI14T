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
        public DAOPessoa()
        {
            conexao = new MySqlConnection("server=localhost;DataBase=turma14;Uid=root;password=");
            try
            {
                conexao.Open();//Tentando conectar ao BD
                MessageBox.Show("Conectado com Sucesso!");
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
                MessageBox.Show(resultado + " linha afetada!");
            }catch(Exception erro)
            {
                MessageBox.Show("Algo deu errado!\n\n" + erro);
            }
        }//fim do método inserir
    }//fim da classe
}//fim do projeto
