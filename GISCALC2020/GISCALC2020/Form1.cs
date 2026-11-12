using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MapComLib;
using static System.Net.Mime.MediaTypeNames;

namespace GISCALC2020
{
    public partial class Form1 : Form
    {

        MapComLib.Convert cv = new MapComLib.Convert();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Left = (Screen.GetBounds(this).Width - Width) / 2;
            Top = (Screen.GetBounds(this).Height - Height) / 2;

            textTOKYOX.Text = "488542484";
            textTOKYOY.Text = "126111879";
            InitCombo();
        }

        private void InitCombo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("通算ミリ秒(9999999999)");
            comboBox1.Items.Add("DMS:度　分　秒(999 99 99.999)");
            comboBox1.Items.Add("DEG:度(999.999)");
            comboBox1.Items.Add("正規化座標(±99999999 m)");
            comboBox1.Items.Add("DMM:NMEA(13338.5982 3336.8297)");
            comboBox1.Items.Add("DBU(9999999999)");
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Clear();
            comboBox2.Items.Add("通算ミリ秒(9999999999)");
            comboBox2.Items.Add("DMS:度　分　秒(999 99 99.999)");
            comboBox2.Items.Add("DEG:度(999.999)");
            comboBox2.Items.Add("正規化座標(±99999999 m)");
            comboBox2.Items.Add("DMMNMEA(13338.5982 3336.8297)");
            comboBox2.Items.Add("DBU(9999999999)");
            comboBox2.SelectedIndex = 0;

            comboTK.Items.Clear();

            for(int i = 1; i < 20; i++)
            {
                comboTK.Items.Add(i.ToString() + "系");
            }
            comboTK.SelectedIndex = 0;

            comboWK.Items.Clear();

            for (int i = 1; i < 20; i++)
            {
                comboWK.Items.Add(i.ToString() + "系");
            }
            comboWK.SelectedIndex = 0;
        }

        private void btnTokyo2Wgs_Click(object sender, EventArgs e)
        {
            long mx = 0;
            long my = 0;

            long zx = 0;
            long zy = 0;


            try
            {
                mx = long.Parse(textTOKYOX.Text);
                my = long.Parse(textTOKYOY.Text);

                cv.ConvJ2W(mx, my, ref zx, ref zy);


                textWGSX.Text = zx.ToString();
                textWGSY.Text = zy.ToString();
            }
            catch (Exception ex)
            {

            }

        }

        private void btnWgs2Tokyo_Click(object sender, EventArgs e)
        {
            long mx = 0;
            long my = 0;

            long zx = 0;
            long zy = 0;


            try
            {
                mx = long.Parse(textWGSX.Text);
                my = long.Parse(textWGSY.Text);

                cv.ConvW2J(mx, my, ref zx, ref zy);


                textTOKYOX.Text = zx.ToString();
                textTOKYOY.Text = zy.ToString();
            }
            catch (Exception ex)
            {

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textTX1.Text = "";
            textTX2.Text = "";
            textTX3.Text = "";
            textTY1.Text = "";
            textTY2.Text = "";
            textTY3.Text = "";


            if (comboBox1.SelectedIndex != 1)
            {
                textTX2.Visible = false;
                textTX3.Visible = false;
                textTY2.Visible = false;
                textTY3.Visible = false;
            }
            else
            {
                textTX2.Visible = true;
                textTX3.Visible = true;
                textTY2.Visible = true;
                textTY3.Visible = true;
            }

            if (comboBox1.SelectedIndex != 3)
            {
                comboTK.Visible = false;
            }
            else
            {
                comboTK.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            textWX1.Text = "";
            textWX2.Text = "";
            textWX3.Text = "";
            textWY1.Text = "";
            textWY2.Text = "";
            textWY3.Text = "";

            if (comboBox2.SelectedIndex != 1)
            {
                textWX2.Visible = false;
                textWX3.Visible = false;
                textWY2.Visible = false;
                textWY3.Visible = false;
            }
            else
            {
                textWX2.Visible = true;
                textWX3.Visible = true;
                textWY2.Visible = true;
                textWY3.Visible = true;
            }

            if (comboBox2.SelectedIndex != 3)
            {
                comboWK.Visible = false;
            }
            else
            {
                comboWK.Visible = true;
            }
        }

        private void btnChange1_Click(object sender, EventArgs e)
        {

            try
            {
                double lon = Double.Parse(textTOKYOX.Text);
                double lat = Double.Parse(textTOKYOY.Text);

                double lo1 = lon / (3600 * 1000);
                double lo2 = lon % (3600 * 1000) / (60 * 1000);
                double lo3 = lon % (60 * 1000) / 1000;

                double la1 = lat / (3600 * 1000);
                double la2 = lat % (3600 * 1000) / (60 * 1000);
                double la3 = lat % (60 * 1000) / 1000;

                double lx = 0.0;
                double ly = 0.0;
                int kijyunkei = comboTK.SelectedIndex + 1;


                switch (comboBox1.SelectedIndex)
                {
                    case 0://通算ミリ秒

                        textTX1.Text = textTOKYOX.Text;
                        textTY1.Text = textTOKYOY.Text;

                        break;
                    case 1://度分秒

                        textTX1.Text = ((int)lo1).ToString();
                        textTX2.Text = ((int)lo2).ToString();
                        textTX3.Text = lo3.ToString();

                        textTY1.Text = ((int)la1).ToString();
                        textTY2.Text = ((int)la2).ToString();
                        textTY3.Text = la3.ToString();

                        break;
                    case 2://度
                        textTX1.Text = lo1.ToString();
                        textTY1.Text = la1.ToString();

                        break;
                    case 3://正規化座標

                        cv.gpconv(lon / 1000, lat / 1000, kijyunkei, ref lx, ref ly);

                        textTX1.Text = lx.ToString();
                        textTY1.Text = ly.ToString();

                        break;
                    case 4://NMEA

                        textTX1.Text = ((int)lo1 * 100 + (int)lo2 + (lo3 / 60)).ToString();
                        textTY1.Text = ((int)la1 * 100 + (int)la2 + (la3 / 60)).ToString();
                        break;
                    case 5://DBU

                        double DBUX = lon / 1024;
                        double DBUY = lat / 1024;

                        textTX1.Text = ((int)DBUX).ToString();
                        textTY1.Text = ((int)DBUY).ToString();
                        break;

                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnChange2_Click(object sender, EventArgs e)
        {

            try
            {
                double lo1 = 0.0;
                double lo2 = 0.0;
                double lo3 = 0.0;

                double lon = 0.0;

                double la1 = 0.0;
                double la2 = 0.0;
                double la3 = 0.0;

                double lat = 0.0;

                double lx = 0.0;
                double ly = 0.0;

                int kijyunkei = comboTK.SelectedIndex + 1;

                switch (comboBox1.SelectedIndex)
                {
                    case 0://通算ミリ秒

                        textTOKYOX.Text = textTX1.Text;
                        textTOKYOY.Text = textTY1.Text;

                        break;
                    case 1://度分秒

                        lo1 = Double.Parse(textTX1.Text);
                        lo2 = Double.Parse(textTX2.Text);
                        lo3 = Double.Parse(textTX3.Text);

                        lon = lo1 * 3600 * 1000 + lo2 * 60 * 1000 + lo3 * 1000;

                        la1 = Double.Parse(textTY1.Text);
                        la2 = Double.Parse(textTY2.Text);
                        la3 = Double.Parse(textTY3.Text);

                        lat = la1 * 3600 * 1000 + la2 * 60 * 1000 + la3 * 1000;

                        textTOKYOX.Text = ((int)lon).ToString();
                        textTOKYOY.Text = ((int)lat).ToString();

                        break;
                    case 2://度

                        lo1 = Double.Parse(textTX1.Text);
                        lon = lo1 * 3600 * 1000;

                        la1 = Double.Parse(textTY1.Text);
                        lat = la1 * 3600 * 1000;

                        textTOKYOX.Text = ((int)lon).ToString();
                        textTOKYOY.Text = ((int)lat).ToString();


                        break;
                    case 3://正規化座標


                        lx = Double.Parse(textTX1.Text);
                        ly = Double.Parse(textTY1.Text);

                        cv.gpconv2(lx, ly, kijyunkei, ref lon, ref lat);

                        textTOKYOX.Text = ((int)(lon * 1000)).ToString();
                        textTOKYOY.Text = ((int)(lat * 1000)).ToString();
                        break;

                    case 4://NMEA
                        lon = Double.Parse(textTX1.Text);
                        lat = Double.Parse(textTY1.Text);

                        lo1 = (int)(lon / 100) * 3600 * 1000 + (int)((lon % 100) * 60 * 1000);
                        la1 = (int)(lat / 100) * 3600 * 1000 + (int)((lat % 100) * 60 * 1000);

                        textTOKYOX.Text = lo1.ToString();
                        textTOKYOY.Text = la1.ToString();

                        break;
                    case 5://DBU
                        double DBUX = Double.Parse(textTX1.Text);
                        double DBUY = Double.Parse(textTY1.Text);

                        lo1 = (int)(DBUX*1024);
                        la1 = (int)(DBUY*1024);

                        textTOKYOX.Text = lo1.ToString();
                        textTOKYOY.Text = la1.ToString();

                        break;
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void btnChange3_Click(object sender, EventArgs e)
        {
            try
            {
                double lon = Double.Parse(textWGSX.Text);
                double lat = Double.Parse(textWGSY.Text);

                double lo1 = lon / (3600 * 1000);
                double lo2 = lon % (3600 * 1000) / (60 * 1000);
                double lo3 = lon % (60 * 1000) / 1000;

                double la1 = lat / (3600 * 1000);
                double la2 = lat % (3600 * 1000) / (60 * 1000);
                double la3 = lat % (60 * 1000) / 1000;

                double lx = 0.0;
                double ly = 0.0;
                int kijyunkei = comboWK.SelectedIndex + 1;


                switch (comboBox2.SelectedIndex)
                {
                    case 0://通算ミリ秒

                        textWX1.Text = textWGSX.Text;
                        textWY1.Text = textWGSY.Text;

                        break;
                    case 1://度分秒

                        textWX1.Text = ((int)lo1).ToString();
                        textWX2.Text = ((int)lo2).ToString();
                        textWX3.Text = lo3.ToString();

                        textWY1.Text = ((int)la1).ToString();
                        textWY2.Text = ((int)la2).ToString();
                        textWY3.Text = la3.ToString();

                        break;
                    case 2://度
                        textWX1.Text = lo1.ToString();
                        textWY1.Text = la1.ToString();

                        break;
                    case 3://正規化座標

                        cv.gpconv(lon / 1000, lat / 1000, kijyunkei, ref lx, ref ly);

                        textWX1.Text = lx.ToString();
                        textWY1.Text = ly.ToString();

                        break;
                    case 4://NMEA

                        lon = Double.Parse(textWGSX.Text);
                        lat = Double.Parse(textWGSY.Text);

                        textWX1.Text = ((int)lo1 * 100 + (int)lo2 + (lo3 / 60)).ToString();
                        textWY1.Text = ((int)la1 * 100 + (int)la2 + (la3 / 60)).ToString();
                        break;

                    case 5://DBU

                        double DBUX = lon / 1024;
                        double DBUY = lat / 1024;

                        textWX1.Text = ((int)DBUX).ToString();
                        textWY1.Text = ((int)DBUY).ToString();
                        break;

                }

            }
            catch (Exception ex)
            {

            }

        }

        private void btnChange4_Click(object sender, EventArgs e)
        {

            try
            {
                double lo1 = 0.0;
                double lo2 = 0.0;
                double lo3 = 0.0;

                double lon = 0.0;

                double la1 = 0.0;
                double la2 = 0.0;
                double la3 = 0.0;

                double lat = 0.0;

                double lx = 0.0;
                double ly = 0.0;

                int kijyunkei = comboWK.SelectedIndex + 1;

                switch (comboBox2.SelectedIndex)
                {
                    case 0://通算ミリ秒

                        textWGSX.Text = textWX1.Text;
                        textWGSY.Text = textWY1.Text;

                        break;
                    case 1://度分秒

                        lo1 = Double.Parse(textWX1.Text);
                        lo2 = Double.Parse(textWX2.Text);
                        lo3 = Double.Parse(textWX3.Text);

                        lon = lo1 * 3600 * 1000 + lo2 * 60 * 1000 + lo3 * 1000;

                        la1 = Double.Parse(textWY1.Text);
                        la2 = Double.Parse(textWY2.Text);
                        la3 = Double.Parse(textWY3.Text);

                        lat = la1 * 3600 * 1000 + la2 * 60 * 1000 + la3 * 1000;

                        textWGSX.Text = ((int)lon).ToString();
                        textWGSY.Text = ((int)lat).ToString();

                        break;
                    case 2://度

                        lo1 = Double.Parse(textWX1.Text);
                        lon = lo1 * 3600 * 1000;

                        la1 = Double.Parse(textWY1.Text);
                        lat = la1 * 3600 * 1000;

                        textWGSX.Text = ((int)lon).ToString();
                        textWGSY.Text = ((int)lat).ToString();


                        break;
                    case 3://正規化座標

                        lx = Double.Parse(textWX1.Text);
                        ly = Double.Parse(textWY1.Text);

                        cv.gpconv2(lx, ly, kijyunkei, ref lon, ref lat);

                        textWGSX.Text = ((int)(lon * 1000)).ToString();
                        textWGSY.Text = ((int)(lat * 1000)).ToString();

                        break;
                    case 4://NMEA
                        lon = Double.Parse(textWX1.Text);
                        lat = Double.Parse(textWY1.Text);

                        lo1 = (int)(lon / 100) * 3600 * 1000 + (int)((lon % 100) * 60 * 1000);
                        la1 = (int)(lat / 100) * 3600 * 1000 + (int)((lat % 100) * 60 * 1000);

                        textWGSX.Text = lo1.ToString();
                        textWGSY.Text = la1.ToString();

                        break;
                    case 5://DBU
                        double DBUX = Double.Parse(textWX1.Text);
                        double DBUY = Double.Parse(textWY1.Text);

                        lo1 = (int)(DBUX * 1024);
                        la1 = (int)(DBUY * 1024);

                        textWGSX.Text = lo1.ToString();
                        textWGSX.Text = la1.ToString();

                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textTOKYOX.Text + "," + textTOKYOY.Text;
            Clipboard.SetDataObject(text, true);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = textWGSX.Text + "," + textWGSY.Text;
            Clipboard.SetDataObject(text, true);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = textTX1.Text+" "+ textTX2.Text+" "+ textTX3.Text+","+ textTY1.Text+" " + textTY2.Text+" " + textTY3.Text;
            Clipboard.SetDataObject(text, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string text = textWX1.Text + " " + textWX2.Text + " " + textWX3.Text + "," + textWY1.Text + " " + textWY2.Text + " " + textWY3.Text;
            Clipboard.SetDataObject(text, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {


            long mx = 0;
            long my = 0;

            long zx = 0;
            long zy = 0;


            try
            {
                mx = long.Parse(textTOKYOX.Text);
                my = long.Parse(textTOKYOY.Text);

                cv.ConvJ2W(mx, my, ref zx, ref zy);


            }
            catch (Exception ex)
            {

            }

            double lon = zx / (double)(3600 * 1000);
            double lat = zy / (double)(3600 * 1000);


            string url = "https://www.google.com/maps/search/?api=1&query=" + lat.ToString()+","+ lon.ToString();

            var startInfo = new System.Diagnostics.ProcessStartInfo(url);

            System.Diagnostics.Process.Start(startInfo);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            long mx = 0;
            long my = 0;

            try
            {
                mx = long.Parse(textWGSX.Text);
                my = long.Parse(textWGSY.Text);

            }
            catch (Exception ex)
            {

            }

            double lon = mx / (double)(3600 * 1000);
            double lat = my / (double)(3600 * 1000);


            string url = "https://www.google.com/maps/search/?api=1&query=" + lat.ToString() + "," + lon.ToString();

            var startInfo = new System.Diagnostics.ProcessStartInfo(url);

            System.Diagnostics.Process.Start(startInfo);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null)
            {
                //DataFormats.Textに関連付けられたデータがあるか調べる
                if (data.GetDataPresent(DataFormats.Text))
                {
                    textPaste.Text = (string)data.GetData(DataFormats.Text);
                }
            }
        }

        private void btnPasteClear_Click(object sender, EventArgs e)
        {
            textPaste.Text = "";
        }
    }
}
