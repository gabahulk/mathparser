using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpretador_de_funções
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount++;
            dataGridView2.RowCount++;
            dataGridView3.RowCount++;
            dataGridView3.ColumnCount++;
            dataGridView1.Rows[0].HeaderCell.Value = "X[1]";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.RowCount = (int)numericUpDown1.Value;
            dataGridView2.RowCount = (int)numericUpDown1.Value;
            dataGridView3.RowCount = (int)numericUpDown1.Value;
            dataGridView3.ColumnCount = (int)numericUpDown1.Value;
            int aux = (int)numericUpDown1.Value-1 ;
            dataGridView1.Rows[aux].HeaderCell.Value = "X[" + (aux+1) + "]";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] variaveis =new double[(int)numericUpDown1.Value];
            double[] variaveisd1 = new double[(int)numericUpDown1.Value];
            double[] variaveisd2 = new double[(int)numericUpDown1.Value];
            double[] variaveisgrad = new double[(int)numericUpDown1.Value];
            double[] variaveishess = new double[(int)numericUpDown1.Value];
            double[] gradiente = new double[(int)numericUpDown1.Value];
            double[,] hessiana = new double[(int)numericUpDown1.Value,(int)numericUpDown1.Value];
            string funcao = textBox1.Text;
            for (int i = 0; i < dataGridView1.RowCount; i++)
			{
			 variaveis[i]=Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
             variaveisd1[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
             variaveisd2[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
             variaveisgrad[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
             variaveishess[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
			}
            Interpretador a =new Interpretador(); // aqui se instancia um intrepretador, cujos parametros do construtor são a função(string),os valores das variaveis em um vetor de double e o numero inteiro de variaveis
            label4.Text = "ƒ(x) =" + a.interpretar(funcao, variaveis).ToString("0.00000000");
            label7.Text = "ƒ'(x) =" + Math.Round(a.derivada(funcao, (int)numericUpDown2.Value, variaveisd1), 8).ToString("0.00000000");
            label8.Text = "ƒ''(x) =" +Math.Round( a.derivada_segunda(funcao, (int)numericUpDown2.Value, variaveisd2),8).ToString("0.0000000");
            gradiente=a.gradiente(funcao,variaveisgrad);
            hessiana=a.hessiana(funcao,variaveishess);
            for (int i = 0; i < (int)numericUpDown1.Value; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = Math.Round(gradiente[i], 8).ToString("0.0000000");
            }
            for (int i = 0; i < (int)numericUpDown1.Value; i++)
            {
                for (int j = 0; j < (int)numericUpDown1.Value; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = Math.Round(hessiana[i, j], 8).ToString("0.0000000");
                }
            }
        }
    }
}
