using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeHomeW7_RickyJonathan_0706022210005
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> listo = new List<string>();
        private Bitmap[] poster;
        private Button backButton;
        public void seat()
        {
            Button[,] btn = new Button[10, 10];
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


            Random random = new Random(); 

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].Name = alphabet[i] + (j + 1).ToString();
                    btn[i, j].Text = btn[i, j].Name;
                    btn[i, j].Width = 40; 
                    btn[i, j].Height = 40; 

                    
                    int randomNumber = random.Next(2);
                    if (randomNumber == 0)
                    {
                        btn[i, j].BackColor = Color.Green; 
                        btn[i, j].Enabled = true; 
                    }
                    else
                    {
                        btn[i, j].BackColor = Color.Red; 
                        btn[i, j].Enabled = false;
                    }

                    
                    btn[i, j].Click += Button_Click;
                    btn[i, j].Top = i * btn[i, j].Height+50; 
                    btn[i, j].Left = j * btn[i, j].Width+130; 
                    panel1.Controls.Add(btn[i, j]);
                }
            }
        }

        Button clickedButton;

        private void Button_Click(object sender, EventArgs e)
        {
            clickedButton = sender as Button; // Store the clicked button reference
            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;

                
                if (clickedButton.BackColor == Color.Green)
                {
                    clickedButton.BackColor = Color.Yellow;
                   
                }
            
                else if (clickedButton.BackColor == Color.Yellow)
                {
                   
                    DialogResult result = MessageBox.Show("Seat " + buttonName + " is already reserved. Do you want to mark it as occupied?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        clickedButton.BackColor = Color.Red; 
                        clickedButton.Enabled = false;
                    }
                }
            }
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            bool hasReservedSeats = false;

            foreach (Control control in panel1.Controls)
            {
                if (control is Button button && button.BackColor == Color.Yellow)
                {
                    button.BackColor = Color.Green; // Set color to green for available seat
                    button.Enabled = true; // Enable the button for available seat
                    hasReservedSeats = true; // Set flag to indicate there are reserved seats
                }
            }

            if (!hasReservedSeats)
            {
                MessageBox.Show("There are no picked seats to reset.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void reserveButton_Click(object sender, EventArgs e)
        {
            string reservedSeats = ""; // String to store reserved seats

            // Loop through all buttons to check for reserved seats
            foreach (Control control in panel1.Controls)
            {
                if (control is Button button && button.BackColor == Color.Yellow)
                {
                    reservedSeats += button.Text + ", "; // Add seat number to the reservedSeats string
                }
            }

            // Check if reservedSeats string is not empty
            if (!string.IsNullOrEmpty(reservedSeats))
            {
                reservedSeats = reservedSeats.Substring(0, reservedSeats.Length - 2); // Remove last 2 characters (", ")

                DialogResult result = MessageBox.Show($"Do you want to mark the following seats as occupied?\n{reservedSeats}", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Loop through all buttons again to mark reserved seats as occupied
                    foreach (Control control in panel1.Controls)
                    {
                        if (control is Button button && button.BackColor == Color.Yellow)
                        {
                            button.BackColor = Color.Red; // Set color to red for occupied seat
                            button.Enabled = false; // Disable the button for occupied seat
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("There are no reserved seats to mark as occupied.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void CreatePanel()
        {
            this.BackColor = Color.Black;
            string[] a = System.IO.File.ReadAllLines(@"C:\Users\KIMBERLY\OneDrive\Documents\judulfilm.txt");
            string[] b = a.SelectMany(line => line.Split(',')).ToArray();
            listo = b.ToList();
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.Black;
            label2.BackColor = Color.Orange;
            poster = new Bitmap[8];
            poster[0] = Properties.Resources.ggs;
            poster[1] = Properties.Resources.fatman;
            poster[2] = Properties.Resources.anakjalanan;
            poster[3] = Properties.Resources.lordbling;
            poster[4] = Properties.Resources.pasfoto;
            poster[5] = Properties.Resources.sonic;
            poster[6] = Properties.Resources.suparman;
            poster[7] = Properties.Resources.taxi;
            PictureBox[] pictureBoxes = new PictureBox[8];
            for (int i = 0; i < 4; i++)
            {
                PictureBox pictureBoxTop = new PictureBox();
                pictureBoxTop.Size = new Size(100, 150);
                pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
                pictureBoxTop.Location = new Point(i * 180 + 10, 70);
                pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxTop.Image = poster[i];

                this.panel1.Controls.Add(pictureBoxTop);

            }
            for (int i = 4; i < 8; i++)
            {
                PictureBox pictureBoxBottom = new PictureBox();
                pictureBoxBottom.Size = new Size(100, 150);
                pictureBoxBottom.BorderStyle = BorderStyle.FixedSingle;
                pictureBoxBottom.Location = new Point((i - 4) * 180 + 10, 270);
                pictureBoxBottom.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxBottom.Image = poster[i];

                this.panel1.Controls.Add(pictureBoxBottom);
            }
            Label label0 = new Label();
            label0.Text = listo[0];
            label0.AutoSize = true;
            label0.TextAlign = ContentAlignment.MiddleCenter;
            label0.Location = new Point(45, 53);
            label0.ForeColor = Color.White;
            label0.Font = new Font(label0.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label0);

            Label label01 = new Label();
            label01.Text = listo[1];
            label01.AutoSize = true;
            label01.TextAlign = ContentAlignment.MiddleCenter;
            label01.Location = new Point(217, 53);
            label01.ForeColor = Color.White;
            label01.Font = new Font(label01.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label01);

            Label label02 = new Label();
            label02.Text = listo[2];
            label02.AutoSize = true;
            label02.TextAlign = ContentAlignment.MiddleCenter;
            label02.Location = new Point(380, 53);
            label02.ForeColor = Color.White;
            label02.Font = new Font(label02.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label02);

            Label label03 = new Label();
            label03.Text = listo[3];
            label03.AutoSize = true;
            label03.TextAlign = ContentAlignment.MiddleCenter;
            label03.Location = new Point(555, 53);
            label03.ForeColor = Color.White;
            label03.Font = new Font(label03.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label03);

            Label label04 = new Label();
            label04.Text = listo[4];
            label04.AutoSize = true;
            label04.TextAlign = ContentAlignment.MiddleCenter;
            label04.Location = new Point(16, 255);
            label04.ForeColor = Color.White;
            label04.Font = new Font(label04.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label04);

            Label label05 = new Label();
            label05.Text = listo[5];
            label05.AutoSize = true;
            label05.TextAlign = ContentAlignment.MiddleCenter;
            label05.Location = new Point(179, 255);
            label05.ForeColor = Color.White;
            label05.Font = new Font(label05.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label05);

            Label label06 = new Label();
            label06.Text = listo[6];
            label06.AutoSize = true;
            label06.TextAlign = ContentAlignment.MiddleCenter;
            label06.Location = new Point(390, 255);
            label06.ForeColor = Color.White;
            label06.Font = new Font(label06.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label06);

            Label label07 = new Label();
            label07.Text = listo[7];
            label07.AutoSize = true;
            label07.TextAlign = ContentAlignment.MiddleCenter;
            label07.Location = new Point(586, 255);
            label07.ForeColor = Color.White;
            label07.Font = new Font(label07.Font, FontStyle.Bold);

            this.panel1.Controls.Add(label07);
            this.panel1.Controls.Add(selectggs);
            this.panel1.Controls.Add(selectfatman);
            this.panel1.Controls.Add(selectaj);
            this.panel1.Controls.Add(selectlord);
            this.panel1.Controls.Add(selectricky);
            this.panel1.Controls.Add(selectsonic);
            this.panel1.Controls.Add(selectsuparman);
            this.panel1.Controls.Add(selecttaxi);
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreatePanel();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void selectggs_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10); 
            backButton.Click += BackButton_Click; 
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[0];
            Label label0 = new Label();
            label0.Text = ("Movie Name = "+listo[0]);
            label0.AutoSize = true;
            label0.TextAlign = ContentAlignment.MiddleCenter;
            label0.Location = new Point(220, 70);
            label0.ForeColor = Color.White;
            label0.Font = new Font(label0.Font.FontFamily, 15, FontStyle.Bold);
            int jam6 = 00;
            for (int i = 1; i <= 3; i++)
            {

                Button button = new Button();
                button.Text = jam6 + ":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label0.Bottom + 10);                        
                panel1.Controls.Add(button);
                button.Click += Buttonn_Click;
                jam6++;
            }

            this.panel1.Controls.Add(label0);
            this.panel1.Controls.Add(pictureBoxTop);

        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            CreatePanel();
        }
        public void createbackbutton()
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
        }

        private void selectfatman_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[1];
            Label label01 = new Label();
            label01.Text = ("Movie Name = " + listo[1]);
            label01.AutoSize = true;
            label01.TextAlign = ContentAlignment.MiddleCenter;
            label01.Location = new Point(220, 70);
            label01.ForeColor = Color.White;
            label01.Font = new Font(label01.Font.FontFamily, 15, FontStyle.Bold);
            int jam5 = 3;
            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jam5 +":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label01.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jam5++;
            }

            this.panel1.Controls.Add(label01);
            this.panel1.Controls.Add(pictureBoxTop);
        }

        private void selectaj_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[2];
            Label label02 = new Label();
            label02.Text = ("Movie Name = " + listo[2]);
            label02.AutoSize = true;
            label02.TextAlign = ContentAlignment.MiddleCenter;
            label02.Location = new Point(220, 70);
            label02.ForeColor = Color.White;
            label02.Font = new Font(label02.Font.FontFamily, 15, FontStyle.Bold);
            int jam4 = 6;
            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jam4 +":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label02.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jam4++;
            }

            this.panel1.Controls.Add(label02);
            this.panel1.Controls.Add(pictureBoxTop);
        }

        private void selectlord_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[3];
            Label label03 = new Label();
            label03.Text = ("Movie Name = " + listo[3]);
            label03.AutoSize = true;
            label03.TextAlign = ContentAlignment.MiddleCenter;
            label03.Location = new Point(220, 70);
            label03.ForeColor = Color.White;
            label03.Font = new Font(label03.Font.FontFamily, 15, FontStyle.Bold);
            int jam3 = 9;
            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jam3+":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label03.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jam3++;
            }

            this.panel1.Controls.Add(label03);
            this.panel1.Controls.Add(pictureBoxTop);
        }

        private void selectricky_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[4];
            Label label04 = new Label();
            label04.Text = ("Movie Name = " + listo[4]);
            label04.AutoSize = true;
            label04.TextAlign = ContentAlignment.MiddleCenter;
            label04.Location = new Point(220, 70);
            label04.ForeColor = Color.White;
            label04.Font = new Font(label04.Font.FontFamily, 15, FontStyle.Bold);
            int jam2 = 12;
            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jam2+":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label04.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jam2++;
            }

            this.panel1.Controls.Add(label04);
            this.panel1.Controls.Add(pictureBoxTop);
        }

        private void selectsonic_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[5];
            Label label05 = new Label();
            label05.Text = ("Movie Name = " + listo[5]);
            label05.AutoSize = true;
            label05.TextAlign = ContentAlignment.MiddleCenter;
            label05.Location = new Point(220, 70);
            label05.ForeColor = Color.White;
            label05.Font = new Font(label05.Font.FontFamily, 15, FontStyle.Bold);
            int jam1 = 22;
            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jam1 +":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label05.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jam1++;
            }

            this.panel1.Controls.Add(label05);
            this.panel1.Controls.Add(pictureBoxTop);
        }

        private void selectsuparman_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[6];
            Label label06 = new Label();
            label06.Text = ("Movie Name = " + listo[6]);
            label06.AutoSize = true;
            label06.TextAlign = ContentAlignment.MiddleCenter;
            label06.Location = new Point(220, 70);
            label06.ForeColor = Color.White;
            label06.Font = new Font(label06.Font.FontFamily, 15, FontStyle.Bold);
            int jam = 16;

            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jam + ":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label06.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jam++;
            }

            this.panel1.Controls.Add(label06);
            this.panel1.Controls.Add(pictureBoxTop);
        }

        private void selecttaxi_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = DefaultBackColor;
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            panel1.Controls.Add(backButton);
            PictureBox pictureBoxTop = new PictureBox();
            pictureBoxTop.Size = new Size(200, 350);
            pictureBoxTop.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTop.Location = new Point(10, 70);
            pictureBoxTop.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTop.Image = poster[7];
            Label label08 = new Label();
            label08.Text = ("Movie Name = " + listo[7]);
            label08.AutoSize = true;
            label08.TextAlign = ContentAlignment.MiddleCenter;
            label08.Location = new Point(220, 70);
            label08.ForeColor = Color.White;
            label08.Font = new Font(label08.Font.FontFamily, 15, FontStyle.Bold);
            int jamm = 19;
            for (int i = 1; i <= 3; i++)
            {
                Button button = new Button();
                button.Text = jamm + ":00";
                button.Size = new Size(100, 30);
                button.BackColor = DefaultBackColor;
                button.Location = new Point(220 + ((i - 1) * 110), label08.Bottom + 10);
                button.Click += Buttonn_Click;
                panel1.Controls.Add(button);
                jamm++;
            }

            this.panel1.Controls.Add(label08);
            this.panel1.Controls.Add(pictureBoxTop);
        }
        private void Buttonn_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            createbackbutton();
            Button reserveButton = new Button();
            reserveButton.Text = "Reserve";
            reserveButton.Size = new Size(100, 30);
            reserveButton.BackColor = DefaultBackColor;
            reserveButton.Location = new Point(545, 400);
            reserveButton.Click += reserveButton_Click;
            panel1.Controls.Add(reserveButton);
            Button resetButton = new Button();
            resetButton.Text = "Reset";
            resetButton.Size = new Size(100, 30);
            resetButton.BackColor = DefaultBackColor;
            resetButton.Location = new Point(545, 350);
            resetButton.Click += resetButton_Click;
            panel1.Controls.Add(resetButton);
            seat();
        }
       
    }
}
