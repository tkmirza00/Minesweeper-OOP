using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int myRow= 16;
        const int myColumn= 16;
        static char[,] ValueArray = new char[myRow, myColumn];
        GameControl myGameControl;
        int time = 0;
        public Form1()
        {
           
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myGameControl = new GameControl();
            myGameControl.Flags = 40;
            MyButton[] Tiles = new MyButton[myRow*myColumn];
            
            //Panel pnl = new Panel();
            //pnl.Size = new System.Drawing.Size(3080, 3080);
            //pnl.Location = new Point(10,10);
            //pnl.BorderStyle = BorderStyle.None;
            //this.Controls.Add(pnl);
           // pnl.BackColor = Color.Red;
            for (int i = 0; i < myRow; i++)
            {
                for (int n = 0; n < myColumn; n++)
                {
                    Tiles[i] = new MyButton();
                    Tiles[i].Size = new Size(24, 24);
                    Tiles[i].Location = new System.Drawing.Point(n * 24 + 50, 110 + i * 24);
                    Tiles[i].Name = String.Format("Button{0}/{1}", i,n);
                    Tiles[i].Text = " ";
                    Tiles[i].ForeColor = System.Drawing.Color.Black;
                    Tiles[i].BackColor = System.Drawing.Color.White;
                    Tiles[i].AutoSize = false;
                    Tiles[i].MouseDown += new MouseEventHandler(this.button1_Click);
                    Tiles[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    Tiles[i].row = i;
                    Tiles[i].column = n;
                    Tiles[i].IsOpen = false;
                    this.Controls.Add(Tiles[i]);
                    //pnl.Controls.Add(Tiles[i]);
                }

            }
            initializeBoard();
        }
        void initializeBoard() {
            label1.Text = (myGameControl.CellUnOpened).ToString();
            label2.Text = (time).ToString();
            label6.Text = (myGameControl.Flags).ToString();
            for (int i = 0; i < 40; i++)
            {
                int mines1, mines2;
                Random rnd = new Random();
                mines1 = rnd.Next(myRow);
                mines2 = rnd.Next(myColumn);
               // mines1 = mines / 100;
              //  mines2 = mines % 100;
                if (ValueArray[mines1, mines2] != 'M')
                {
                    ValueArray[mines1, mines2] = 'M';
                }
                else i--;
            }
            for (int i = 0; i < myRow; i++)
            {
                for (int n = 0; n < myColumn; n++)
                {
                    //     MessageBox.Show((CheckAdjacent(i, n)).ToString());
                    if (ValueArray[i, n] != 'M')
                    {

                        ValueArray[i, n] = char.Parse(CheckAdjacent(i, n));
                        //           MessageBox.Show((ValueArray[i, n]).ToString());
                    }
                }
            }
        }



        private void button1_Click(object sender, MouseEventArgs e)
        {
            
            MyButton btn = (MyButton)sender;
            btn.IsOpen = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Right && !myGameControl.GameLose)
            {

                if (btn.Text == "F") { btn.Text = " "; myGameControl.Flags++; }
                else if (btn.Text == " " && myGameControl.Flags != 0 && btn.BackColor == System.Drawing.Color.White) { btn.Text = "F"; myGameControl.Flags--; }
                label6.Text = (myGameControl.Flags).ToString(); 
            }
            else if(e.Button == System.Windows.Forms.MouseButtons.Left){
            if (!myGameControl.GameLose && btn.Text !="F")
            {
                    myGameControl.CellUnOpened--;
                label6.Text = (myGameControl.Flags).ToString();        
                
                
                btn.Text = (ValueArray[btn.row, btn.column]).ToString();
                if (myGameControl.CellUnOpened == 10) {
                    MessageBox.Show("You Win");
                }

                if (btn.Text == "M")
                {
                    btn.Text = "\U0001F4A3";
                        myGameControl.NoMines--;
                    btn.ForeColor = Color.Red;
                    MessageBox.Show("You Lose","Loss");
                        myGameControl.GameLose = true;
                    for (int i = 0; i < myRow; i++)
                    {
                        for (int j = 0; j < myColumn; j++)
                        {
                            MyButton b = Controls.Find(String.Format("Button{0}/{1}", i, j), true).FirstOrDefault() as MyButton;
                            if (ValueArray[i, j] == 'M')
                            {
                                
                                b.Text = "\U0001F4A3";
                                b.ForeColor = Color.Red;
                            }

                            if (b.Text== "F")
                            {
                                b.Text = "\U0000274C";
                                b.ForeColor = Color.Red;
                            }
                        }
                    }

                }
              
                if (btn.Text == ('0').ToString())
                {
                    OpenAdjacent(btn.row, btn.column);
                }
                    myGameControl.CellUnOpened = 0;
                for (int i = 0; i < myRow; i++)
                {
                    for (int j = 0; j < myColumn; j++)
                    {
                        MyButton b = Controls.Find(String.Format("Button{0}/{1}", i, j), true).FirstOrDefault() as MyButton;
                        if (b.Text == " ") { myGameControl.CellUnOpened++; }
                    }
                }
                label1.Text = (myGameControl.CellUnOpened).ToString();
                if (btn.Text == "0")
                {
                    btn.Text = " ";
                    btn.BackColor = Color.Gray;
                }
            }
        }
        }
        public void OpenAdjacent(int i, int j) 
        {
            if ((i - 1) > -1)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i - 1, j), true).FirstOrDefault() as MyButton;
                if (b.IsOpen) 
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i - 1, j] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i - 1, j]).ToString();
                    ValueArray[i - 1, j] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i - 1, j);
                    
                }
                else
                {
                    b.Text = (ValueArray[i - 1, j]).ToString();
                
                }    }
            if ((j - 1) > -1)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i, j - 1), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i, j - 1] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i, j - 1]).ToString();
                    ValueArray[i, j - 1] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i, j - 1);

                }
                else
                {
                    b.Text = (ValueArray[i, j - 1]).ToString();
                   
                }

            }
            if ((i - 1) > -1 && (j - 1) > -1)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i - 1, j - 1), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i - 1, j - 1] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i - 1, j - 1]).ToString();
                    ValueArray[i - 1, j - 1] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i - 1, j - 1);
                }
                else
                {
                    b.Text = (ValueArray[i - 1, j - 1]).ToString();
                  
                }

            }
            if ((i + 1) < myRow)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i + 1, j), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i + 1, j] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i + 1, j]).ToString();
                    ValueArray[i + 1, j] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i + 1, j);
                }
                else
                {
                    b.Text = (ValueArray[i + 1, j]).ToString();
                
                }
            }
            if ((j + 1) < 8)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i, j + 1), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i, j + 1] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i, j + 1]).ToString();
                    ValueArray[i, j + 1] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i, j + 1);
                }
                else
                {
                    b.Text = (ValueArray[i, j + 1]).ToString();
                  
                }
            }
            if ((i + 1) < myRow && (j + 1) < myColumn)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i + 1, j + 1), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i + 1, j + 1] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i + 1, j + 1]).ToString();
                    ValueArray[i + 1, j + 1] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i + 1, j + 1);
                }
                else
                {
                    b.Text = (ValueArray[i + 1, j + 1]).ToString();
                  
                }

            }
            if ((i + 1) < myRow && (j - 1) >-1)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i + 1, j - 1), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i + 1, j - 1] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i + 1, j - 1]).ToString();
                    ValueArray[i + 1, j - 1] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i + 1, j - 1);

                }
                else
                {
                    b.Text = (ValueArray[i + 1, j - 1]).ToString();
                    
                }

            }
            if ((i - 1) > -1 && (j + 1) < myColumn)
            {
                MyButton b = Controls.Find(String.Format("Button{0}/{1}", i - 1, j + 1), true).FirstOrDefault() as MyButton;
                if (b.IsOpen)
                {
                    myGameControl.CellUnOpened--;
                }
                if (ValueArray[i - 1, j + 1] == '0' && b.Text != "0")
                {
                    b.Text = (ValueArray[i - 1, j + 1]).ToString();
                    ValueArray[i - 1, j + 1] = ' ';
                    b.BackColor = Color.Gray;
                    OpenAdjacent(i - 1, j + 1);

                }
                else
                {
                    b.Text = (ValueArray[i - 1, j + 1]).ToString();
                   
                }
            }
        }
        public string CheckAdjacent(int i,int j) {
            int NumOfMines=0;
            if ((i - 1) > -1) {
                if (ValueArray[i - 1, j] == 'M') { NumOfMines++; } 
            }
            if ((j - 1) > -1)
            {
                if (ValueArray[i, j-1] == 'M') { NumOfMines++; }
            }
            if ((i - 1) > -1 && (j-1)>-1)
            {
                if (ValueArray[i - 1, j-1] == 'M') { NumOfMines++; }
            }
            if ((i + 1) < myRow)
            {
                if (ValueArray[i + 1, j] == 'M') { NumOfMines++; }
            }
            if ((j + 1) < myColumn)
            {
                if (ValueArray[i, j+1] == 'M') { NumOfMines++; }
            }
            if ((i + 1) < myRow && (j+1)<myColumn)
            {
                if (ValueArray[i + 1, j+1] == 'M') { NumOfMines++; }
            }
            if ((i + 1) < myRow && (j - 1) > -1)
            {
                if (ValueArray[i + 1, j - 1] == 'M') { NumOfMines++; }
            }
            if ((i - 1) > -1 && (j + 1) < myColumn)
            {
                if (ValueArray[i - 1, j + 1] == 'M') { NumOfMines++; }
            }

            return (NumOfMines).ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!myGameControl.GameLose) {
                time++;
                label2.Text = (time).ToString();
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {

            time = 0;
           // myGameControl.SetValues(10,false,64,0,10);
            for (int i = 0; i < myRow; i++)
            {
                for (int j = 0; j < myColumn; j++)
                {
                    ValueArray[i, j] = ' ';
                }
            }

            for (int i = 0; i < myRow; i++)
            {
                  for (int n = 0; n < myColumn; n++)
                  {
                    MyButton btn = Controls.Find(String.Format("Button{0}/{1}", i , n), true).FirstOrDefault() as MyButton;
                    btn.Text = " ";
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.Black;
                    }

                }
            myGameControl.SetValues(40, false, 256, 0, 40);

            initializeBoard();
            
        }
        

       
    }
 
}
