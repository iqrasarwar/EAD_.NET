using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAD_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked_2(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button11_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if(textBox.Text == "0")
                textBox.Text = "";
            Button button = (Button)sender;
            textBox.Text += button.Text;
        }
        public static bool isNumeric(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c != '.')
                {
                    if (!char.IsDigit(c))
                        return false;
                }

            }
            return true;
        }
        string solve(string x)
        {
            Stack<char> cs = new Stack<char>();
            Stack<double> iss = new Stack<double>();
            for (int i = 0; i < x.Length; i++)
            {
                char c = x[i];
                int index = i;
                while (Char.IsDigit(x[index]) || Char.IsDigit('.'))
                {
                    index++;
                    if (index == x.Length)
                        break;
                }
                string num;
                if (i != index)
                {
                    num = x.Substring(i, index-i);
                    i = index - 1;
                }
                else
                    num = Convert.ToString(x[i]);
                bool b = isNumeric(num);
                if (b)
                {
                    double op;
                    op = Convert.ToDouble(num);
                    iss.Push(op);
                }
                else if (num != ")" || num != "(")
                    cs.Push(c);

            }
            if (!(cs.Count == 0) && iss.Count >= 2)
            {
                char Operator_ = cs.Pop();
                double operand2 = iss.Pop(), operand1 = iss.Pop(), res = 0;
                if (Operator_ == '+')
                    res = (operand1 + operand2);
                else if (Operator_ == '-')
                    res = (operand1 - operand2);
                else if (Operator_ == '*')
                    res = (operand1 * operand2);
                else if (Operator_ == '/')
                {
                    if (operand2 != 0)
                        res = (operand1 / operand2);
                }
                string r = Convert.ToString(res);
                return r;
            }
            return x;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox.Text.Length > 0)
            textBox.Text = textBox.Text.Substring(0, textBox.Text.Length-1);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            String before = textBox.Text;
            String ret = solve(before);
            textBox.Text = ret;
        }

        
    }
}
