using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAD_WF
{
    public partial class Form1 : Form
    {
        bool clear = false;
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
            Button button = (Button)sender;
            char c = Convert.ToChar(button.Text);
            //clear the intiall zero
            if (textBox.Text == "0")
                textBox.Text = "";
            //dont allow operator without any value
            if (textBox.Text.Length == 0 && !char.IsDigit(c) && c != '.')
                return;
            if (clear == true && !char.IsDigit(c) && c != '.')
            {
                textBox.Text = "";
                return;
            }
            //if ans was calculates clear textbox before next entry
            if (clear)
            {
                textBox.Clear();
                clear = false;
            }
            //calculate if an expression is present already
            if (!char.IsDigit(c) && c != '=' && c != '.')
            {
                textBox.Text = solve(textBox.Text);
            }
            int length = textBox.Text.Length, i = length;
            //to avoid double decimal in single number
            while (i>0)
            {
                if (textBox.Text[i-1] == '.' && c == '.')
                    return;
                if (!char.IsDigit(textBox.Text[i - 1]))
                    break;
                i--;
            }
            if (length >= 1)
            {
                char text = textBox.Text[length - 1];
                //if last char is decimal append zero to it before next operator
                if (textBox.Text[length - 1] == '.' && !char.IsDigit(c))
                    textBox.Text += '0';
                //if a operator exists at last and . is entered, append 0 
                //if 4+ exits . is pressed make it 4+0.
                if (!char.IsDigit(textBox.Text[length - 1]) && c == '.')
                    textBox.Text += '0';
                //if an opertaor exits and user enters another operator overwrite previous
                // if 45+ exits and - is entered convert it to 45- instead 45+-
                if (!char.IsDigit(c) && c != '=' && c != '.')
                    if (!char.IsDigit(text) && text != '=' && text != '.')
                        textBox.Text = textBox.Text.Substring(0, length - 1);
            }
            //if there is nothing in textbox and . entred append zero before it
            else if (button.Text == "." && length == 0)
                textBox.Text += "0";
            textBox.Text += button.Text;
        }
        //check if given str contains a number
        public static bool IsNumeric(string str)
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
            //extract operatoes in opertorStack
            //extract operands in oprendstack
            Stack<char> operatorsStack = new();
            Stack<decimal> operandStack = new();
            for (int i = 0; i < x.Length; i++)
            {
                char c = x[i];
                int index = i;
                while (char.IsDigit(x[index]) || x[index] == '.') //get a number indexes
                {
                    index++;
                    if (index == x.Length)
                        break;
                }
                string num;
                if (i != index) //if num exist get substring
                {
                    num = x.Substring(i, index-i);
                    i = index - 1;
                }
                else
                    num = Convert.ToString(x[i]); 
                bool b = IsNumeric(num); //if num is correct numeric
                if (b)
                {
                    decimal op;
                    op = Convert.ToDecimal(num);
                    operandStack.Push(op); //push in operand stack
                }
                else
                    operatorsStack.Push(c);

            }
            if (!(operatorsStack.Count == 0) && operandStack.Count >= 2)
            {
                char Operator_ = operatorsStack.Pop();
                decimal operand2 = operandStack.Pop(), operand1 = operandStack.Pop(), res = 0;
                if (Operator_ == '+')
                    res = operand1 + operand2;
                else if (Operator_ == '-')
                    res = operand1 - operand2;
                else if (Operator_ == '*')
                    res = operand1 * operand2;
                else if (Operator_ == '/')
                {
                    if (operand2 != 0)
                        res = operand1 / operand2;
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
            if (textBox.Text.Length >= 1)
            {
                if (textBox.Text[textBox.Text.Length - 1] == '.')
                    textBox.Text += '0';
            }
            string before = textBox.Text;
            string ret = solve(textBox.Text);
            if (before != ret) //if nothing is calculated dont clear else set clear true
                clear = true;
            textBox.Text = ret;
        }
    }
}
