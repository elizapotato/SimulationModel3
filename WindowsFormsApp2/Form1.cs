using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Object bullet = new Object();
        Space air = new Space();

        private void startB_Click(object sender, EventArgs e)
        {
            air.NullX();
            bullet.SetValues((double)boxSqr.Value, (double)boxWeight.Value);
            air.SetValues((double)boxHight.Value,(double)boxSpeed.Value, (double)boxAngle.Value, bullet);

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(air.GetX(), air.GetY());

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            air.Work();
            labTime.Text = "Time =" + air.GetT();
            labX.Text = "X =" + air.GetX();
            labY.Text = "Y =" + air.GetY();
            chart1.Series[0].Points.AddXY(air.GetX(), air.GetY());
            if (air.GetY() <= 0) timer1.Stop();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.BackColor = Color.Red;
                timer1.Stop();
            }
            else
            {
                checkBox1.BackColor = Color.LightGray;
                timer1.Start();
            } 
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, EventArgs e)
        {
           
        }
    }

    class Object
    {
        public double S, m;

        public void SetValues(double S1, double m1)
        {
            S = S1; m = m1;
        }      
    }

    class Space
    {
        const double g = 9.81;
        const double C = 0.15;
        const double p = 1.29;
        const double dt = 0.01;
        double v0, a, y = 0, vx, vy, k, x = 0, t = 0;

        public void SetValues(double y01,double v01,double a1, Object Obj)
        {
            y = y01;v0 = v01;a = a1;
            vx = v0 * Math.Cos(a * Math.PI / 180);
            vy = v0 * Math.Sin(a * Math.PI / 180);
            k = 0.5 * C * p * Obj.S / Obj.m;
        }
        
        public void Work()
        {
            t += dt;
            vx = vx - k * vx * Math.Sqrt(vx * vx + vy * vy) * dt;
            vy = vy - (g + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dt;
            x = x + vx * dt;
            y = y + vy * dt;
        }

        public void NullX()
        {
            x = 0;
        }

        public double GetX()
        {
            return x;
        }
        public double GetY()
        {
            return y;
        }
        public double GetT()
        {
            return t;
        }
    }
}
