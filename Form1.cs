using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;



namespace Puzzle
{
    public partial class frmPuzzelGame : Form
    {
        int inNullSliceIndex;
        int move = 0;
        List<Bitmap> lstOriginalPictureList = new List<Bitmap>();
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        Stack<int> st1 = new Stack<int>();
        Stack<int> st2 = new Stack<int>();
        Queue<int> que1 = new Queue<int>();


        public frmPuzzelGame()
        {
            InitializeComponent();
            lstOriginalPictureList.AddRange(new Bitmap[] { Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6, Properties.Resources._7, Properties.Resources._8, Properties.Resources._null });
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Shuffle();

        }

       void Shuffle()
        {
            st1.Clear();
            st2.Clear();
            que1.Clear();
            for (int i = 0; i < 9; i++)
            {
                ((PictureBox)gbPuzzleBox.Controls[i]).Image = lstOriginalPictureList[i];

            }
            inNullSliceIndex = 8;

            //First shuffle
            List<int> move1st = new List<int>(new int[] { -3, -1 });
            Random r1st = new Random();
            int j1st;
            j1st = move1st[r1st.Next(0, move1st.Count)] + inNullSliceIndex;
            PictureBox temp = new PictureBox();
            temp.Image = ((PictureBox)gbPuzzleBox.Controls[inNullSliceIndex]).Image;
            ((PictureBox)gbPuzzleBox.Controls[inNullSliceIndex]).Image = ((PictureBox)gbPuzzleBox.Controls[j1st]).Image;
            ((PictureBox)gbPuzzleBox.Controls[j1st]).Image = temp.Image;
            inNullSliceIndex = j1st;
            que1.Enqueue(inNullSliceIndex);
            //shuffle
            for (int i = 0; i < 9; i++)
            {
                int j;
                if (inNullSliceIndex >= 6)
                {
                    if (inNullSliceIndex == 6)
                    {
                        List<int> move = new List<int>(new int[] { -3, 1 });
                        j = Moveset(move);
                    }
                    else if (inNullSliceIndex == 8)
                    {
                        List<int> move = new List<int>(new int[] { -3, -1 });
                        j = Moveset(move);
                    }
                    else
                    {
                        List<int> move = new List<int>(new int[] { -3, -1, 1 });
                        j = Moveset(move);
                    }
                }
                else if (inNullSliceIndex <= 2)
                {
                    if (inNullSliceIndex == 2)
                    {
                        List<int> move = new List<int>(new int[] { -1, 3 });
                        j = Moveset(move);
                    }
                    else if (inNullSliceIndex == 0)
                    {
                        List<int> move = new List<int>(new int[] { 1, 3 });
                        j = Moveset(move);
                    }
                    else
                    {
                        List<int> move = new List<int>(new int[] { -1, 1, 3 });
                        j = Moveset(move);
                    }
                }
                else
                {
                    if (inNullSliceIndex == 3)
                    {
                        List<int> move = new List<int>(new int[] { -3, 1, 3 });
                        j = Moveset(move);
                    }
                    else if (inNullSliceIndex == 5)
                    {
                        List<int> move = new List<int>(new int[] { -3, -1, 3 });
                        j = Moveset(move);
                    }
                    else
                    {
                        List<int> move = new List<int>(new int[] { -3, -1, 1, 3 });
                        j = Moveset(move);
                    }
                }
               
                que1.Enqueue(inNullSliceIndex);
                inNullSliceIndex = j;
            }
        }
        private int Moveset(List<int> m)
        {
            int k = que1.Dequeue();
            Random r = new Random();
            int j;
            do
            {
                j = m[r.Next(0, m.Count)] + inNullSliceIndex;
            } while (j == k);
            PictureBox temp = new PictureBox();
            temp.Image = ((PictureBox)gbPuzzleBox.Controls[inNullSliceIndex]).Image;
            ((PictureBox)gbPuzzleBox.Controls[inNullSliceIndex]).Image = ((PictureBox)gbPuzzleBox.Controls[j]).Image;
            ((PictureBox)gbPuzzleBox.Controls[j]).Image = temp.Image;
            return j;
        }

        private void Swap(object sender, EventArgs e)
        {
            int inPictureBoxIndex = gbPuzzleBox.Controls.IndexOf(sender as Control); ;
            int a = (inPictureBoxIndex % 3 == 0) ? -1 : inPictureBoxIndex - 1;
            int b = inPictureBoxIndex - 3;
            int c = (inPictureBoxIndex % 3 == 2) ? -1 : inPictureBoxIndex + 1;
            int d = inPictureBoxIndex + 3;

            List<double> vitri = new List<double> { a, b, c, d };

            if (vitri.Contains(inNullSliceIndex))
            {
                st1.Push(inNullSliceIndex);
                PictureBox temp = new PictureBox();
                temp.Image = ((PictureBox)gbPuzzleBox.Controls[inNullSliceIndex]).Image;
                ((PictureBox)gbPuzzleBox.Controls[inNullSliceIndex]).Image = ((PictureBox)gbPuzzleBox.Controls[inPictureBoxIndex]).Image;
                ((PictureBox)gbPuzzleBox.Controls[inPictureBoxIndex]).Image = temp.Image;
                inNullSliceIndex = inPictureBoxIndex;
                st2.Push(inPictureBoxIndex);
                move++;
                button4.Text = "Step:" + move;
            }
            if(Win()==true)
            {
                MessageBox.Show(" YOU WIN !!!!!   Step:  "+ move);
               
            }     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void Undo()
        {
            if (st1.Count != 0 && st2.Count != 0)
            {
                move--;
                button4.Text = "Step:" + move;
                int i = st1.Pop();
                int j = st2.Pop();

                PictureBox temp = new PictureBox();
                temp.Image = ((PictureBox)gbPuzzleBox.Controls[i]).Image;
                ((PictureBox)gbPuzzleBox.Controls[i]).Image = ((PictureBox)gbPuzzleBox.Controls[j]).Image;
                ((PictureBox)gbPuzzleBox.Controls[j]).Image = temp.Image;

                inNullSliceIndex = i;
            }
            else MessageBox.Show("Không thể Undo");
        }
       
        bool Win()
        {
            int i;
            for(i=0;i<8;i++)
            {
                if (((PictureBox)gbPuzzleBox.Controls[i]).Image != lstOriginalPictureList[i])
                    break;
            }
            if (i == 8) return true;
            else return false;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Shuffle();
            move = 0;
            button4.Text = "Step:" + move;
        }
    }
}
