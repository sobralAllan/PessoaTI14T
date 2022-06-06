using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PessoaTI14T
{
    public partial class Form1 : Form
    {
        DAOPessoa pessoa;
        public Form1()
        {
            InitializeComponent();
            pessoa = new DAOPessoa();//Abrindo a conexão com o Banco de Dados
        }//Fim do método construtor

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                //int codigo = Convert.ToInt32(textBox1.Text);//Coletando o dado do campo código
                string tratamentoCPF = maskedTextBox1.Text.Replace(",", "");
                tratamentoCPF = tratamentoCPF.Replace("-", "");
                long cpf = Convert.ToInt64(tratamentoCPF);//Coletando o dado do campo CPF
                string nome = textBox2.Text;//Coletando o dado do campo nome
                string telefone = maskedTextBox2.Text;//Coletando o dado do campo telefone
                string endereco = textBox4.Text;//Coletando o dado do campo Endereço
                                                //Chamar o método inserir que foi criado na classe DAOPessoa
                pessoa.Inserir(cpf, nome, telefone, endereco);//Inserir no banco os dados do formulário
            }catch(Exception erro)
            {
                MessageBox.Show("" + erro);
            }
        }//fim do botão cadastrar

        private void Consultar_Click(object sender, EventArgs e)
        {

        }//fim do botão consultar

        private void Atualizar_Click(object sender, EventArgs e)
        {

        }//fim do botão Atualizar

        private void Excluir_Click(object sender, EventArgs e)
        {

        }//fim do botão Excluir

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }//textbox de código

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }//maskedTextBox de CPF

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }//textBox de nome

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }//MaskedTextBox de Telefone

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }//TextBox de Endereço
    }//fim da classe
}//fim do projeto
