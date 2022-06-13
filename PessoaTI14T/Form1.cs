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
            textBox1.Text = Convert.ToString(pessoa.ConsultarCodigo() + 1);
            textBox1.ReadOnly = true;
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

        public void Limpar()
        {
            textBox1.Text = "" + pessoa.ConsultarCodigo();//Código
            maskedTextBox1.Text = "";//CPF
            textBox2.Text = "";//Nome
            maskedTextBox2.Text = "";//Telefone
            textBox4.Text = "";//Endereço
        }//fim do método limpar

        public void AtivarCampos()
        {
            textBox1.ReadOnly = false;//Código
            maskedTextBox1.ReadOnly = true;//CPF
            textBox2.ReadOnly = true;//Nome
            maskedTextBox2.ReadOnly = true;//Telefone
            textBox4.ReadOnly = true;//Endereço
        }//fim do ativar

        public void InativarCampos()
        {
            textBox1.ReadOnly = true;//Código
            maskedTextBox1.ReadOnly = false;//CPF
            textBox2.ReadOnly = false;//Nome
            maskedTextBox2.ReadOnly = false;//Telefone
            textBox4.ReadOnly = false;//Endereço
        }//fim do Inativar
        private void Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.ReadOnly == false)
                {
                    Limpar();
                    InativarCampos();
                }
                else 
                { 
                    long cpf = TratarCPF(maskedTextBox1.Text);//Coletando o dado do campo CPF
                    string nome = textBox2.Text;//Coletando o dado do campo nome
                    string telefone = maskedTextBox2.Text;//Coletando o dado do campo telefone
                    string endereco = textBox4.Text;//Coletando o dado do campo Endereço
                                                    //Chamar o método inserir que foi criado na classe DAOPessoa
                    pessoa.Inserir(cpf, nome, telefone, endereco);//Inserir no banco os dados do formulário
                    Limpar();//Limpo os campos
                }
            }catch(Exception erro)
            {
                MessageBox.Show("" + erro);
            }
        }//fim do botão cadastrar

        public long TratarCPF(string cpf)
        {
            string tratamento = cpf.Replace(",", "");
            tratamento        = tratamento.Replace("-", "");
            return Convert.ToInt64(tratamento);
        }//fim do tratarCPF

        private void Consultar_Click(object sender, EventArgs e)
        {
            if (textBox1.ReadOnly == true)
            {
                AtivarCampos();
            }
            else
            {
                maskedTextBox1.Text = "" + pessoa.ConsultarCPF(Convert.ToInt32(textBox1.Text));//Preenchendo o campo CPF
                textBox2.Text = pessoa.ConsultarNome(Convert.ToInt32(textBox1.Text));//Preenchendo o campo nome
                maskedTextBox2.Text = pessoa.ConsultarTelefone(Convert.ToInt32(textBox1.Text));//Prenchendo o campo telefone
                textBox4.Text = pessoa.ConsultarEndereco(Convert.ToInt32(textBox1.Text));
            }
        }//fim do botão consultar

        private void Atualizar_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                //Se o campo nome está vazio, então preenche com o dados do banco...
                maskedTextBox1.Text = "" + pessoa.ConsultarCPF(Convert.ToInt32(textBox1.Text));//Preenchendo o campo CPF
                textBox2.Text = pessoa.ConsultarNome(Convert.ToInt32(textBox1.Text));//Preenchendo o campo nome
                maskedTextBox2.Text = pessoa.ConsultarTelefone(Convert.ToInt32(textBox1.Text));//Prenchendo o campo telefone
                textBox4.Text = pessoa.ConsultarEndereco(Convert.ToInt32(textBox1.Text));
            }
            else
            {
                //Atualizar o CPF
                pessoa.Atualizar(Convert.ToInt32(textBox1.Text), "CPF", TratarCPF(maskedTextBox1.Text));
                //Atualizar o Nome
                pessoa.Atualizar(Convert.ToInt32(textBox1.Text), "nome", textBox2.Text);
                //Atualizar o Telefone
                pessoa.Atualizar(Convert.ToInt32(textBox1.Text), "telefone", maskedTextBox2.Text);
                //Atualizar o Endereço
                pessoa.Atualizar(Convert.ToInt32(textBox1.Text), "endereco", textBox4.Text);
                Limpar();//Limpo os campos
            }
        }//fim do botão Atualizar

        private void Excluir_Click(object sender, EventArgs e)
        {
            pessoa.Deletar(Convert.ToInt32(textBox1.Text));
            Limpar();//Limpo os campos
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
