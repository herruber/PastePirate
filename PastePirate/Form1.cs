using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PastePirate
{
    public partial class Form1 : Form
    {

        public List<ThemeColor> selectedColors = new List<ThemeColor>();

        public List<ColorDefinition> colorDefs = new List<ColorDefinition>();
        public List<Button> buttons = new List<Button>();


        public Form1()
        {
            InitializeComponent();

            Random rand = new Random();

            for (int i = 0; i < 6; i++)
            {

                var btn = new Button();
                btn.Name = "Color" + (i + 1);
                btn.Text = btn.Name;

                btn.Click += new System.EventHandler(this.button1_Click);
                btn.Size = new Size(100, 25);
                Controls.Add(btn);
                btn.Location = new System.Drawing.Point(750, 10 + 30 * i);
                btn.BackColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                buttons.Add(btn);

                var d = new ColorDefinition();

                d = SetFromColor(d, btn.BackColor, true);

                colorDefs.Add(d);
            }

        }

        private bool ContainsColor(string line)
        {

            if (line.ToLower().Contains("color") || line.ToLower().Trim().StartsWith("c"))
            {
                return true;
            }

            return false;

        }

        private string ReplaceColor(string line, ThemeColor o, List<ThemeColor> definitions)
        {

            for (int i = 0; i < definitions.Count; i++)
            {

                if (o.Equals(definitions[i]))
                {
                    line.Replace("", "");
                }
            }

            return null;
        }

        private void original_TextChanged(object sender, EventArgs e)
        {

            string txt = original.Text;

            string[] lines = txt.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string newText = "";

            foreach (var line in lines)
            {
                string t = line;
                if (line.Length > 4)
                {
                    foreach (var d in colorDefs)
                    {

                        t = t.Replace(d.HexOld, d.HexNew);
                        t = t.Replace(d.RgbOld, d.RgbNew);

                    }


                }
                newCss.AppendText(t + "\n");

            }



        }

        public ColorDefinition SetFromHex(ColorDefinition d, string hex, bool isNew)
        {
            if (isNew)
            {
                d.RgbNew = RGBConverter(hex);
                d.HexNew = hex;
            }
            else
            {
                d.RgbOld = RGBConverter(hex);
                d.HexOld = hex;
            }

            return d;
        }

        public ColorDefinition SetFromRgb(ColorDefinition d, string rgb, bool isNew)
        {
            if (isNew)
            {
                d.RgbNew = rgb;
                d.HexNew = HexConverter(rgb);
            }
            else
            {
                d.RgbOld = rgb;
                d.HexOld = HexConverter(rgb);
            }

            return d;
        }

        public ColorDefinition SetFromColor(ColorDefinition d, Color c, bool isNew)
        {
            if (isNew)
            {
                d.RgbNew = c.R + "," + c.G + "," + c.B;
                d.HexNew = HexConverter(c);
            }
            else
            {
                d.RgbOld = c.R + "," + c.G + "," + c.B;
                d.HexOld = HexConverter(c);
            }

            return d;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {

                using (ColorDialog dlg= new ColorDialog())
                {

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {

                        Button btn = (sender as Button);
                        btn.BackColor = dlg.Color;

                        colorDefs[buttons.IndexOf(btn)] = SetFromColor(colorDefs[buttons.IndexOf(btn)], btn.BackColor, true);


                    }
                }

            }
        }

        private static string HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private static string RGBConverter(string hex)
        {
            hex = hex.Replace("#", "");

            string c1 = hex[0] +"" +hex[1];
            string c2 = hex[2] + "" + hex[3];
            string c3 = hex[4] + "" + hex[5];


            int R = int.Parse(c1, System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(c2, System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(c3, System.Globalization.NumberStyles.HexNumber);

            return R + "," + G + "," + B;
        }

        private static string HexConverter(string rgb)
        {

            string[] info = rgb.Split(',');

            int r = int.Parse(info[0]);
            int g = int.Parse(info[1]);
            int b = int.Parse(info[2]);


            return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        private void definitions_TextChanged(object sender, EventArgs e)
        {

            ColorConverter converter = new ColorConverter();
            string[] lines = definitions.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<string> colors = new List<string>();

            foreach (var line in lines)
            {

                string bajs = line.Replace("\t", " ").Replace("\r", " ");

                string[] parts = bajs.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                {
                    if (part.Contains("#"))
                    {
                        colors.Add(part);
                    }
                }

            }

            for (int i = 0; i < colors.Count; i++)
            {

                colorDefs[i] = SetFromHex(colorDefs[i], colors[i], false);


            }



        }


    }

    public class ColorDefinition
    {

        public string HexOld { get; set; }
        public string RgbOld { get; set; }

        public string HexNew { get; set; }
        public string RgbNew { get; set; }

        public ColorDefinition(string[] hex, string[] rgb)
        {
            HexOld = hex[0];
            HexNew = hex[1];

            RgbOld = rgb[0];
            RgbNew = rgb[1];
        }

        public ColorDefinition()
        {

        }

    }

    public class ThemeColor
    {

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public float A { get; set; }

        public string Hex { get; set; }
        public bool isDefinition = false;

        public ThemeColor(string rgbOrHex)
        {
            string trimmed = rgbOrHex.Trim();

            string[] colors;

            //If a color definition
            if (trimmed.StartsWith("c"))
            {
                isDefinition = true;
                colors = trimmed.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var color in colors)
                {
                    string t = color.Trim();
                    //Is a hex defintion
                    if (t.StartsWith("#"))
                    {
                        Hex = t;
                    }
                    else if(t.Contains(","))
                    {

                        string[] rgb = t.Split(',');

                        R = int.Parse(rgb[0]);
                        G = int.Parse(rgb[1]);
                        B = int.Parse(rgb[2]);

                        if (rgb.Length == 4)
                        {
                            A = float.Parse(rgb[3]);
                        }
                        else
                        {
                            A = 1f;
                        }

                    }
                }

            }
            else
            {

            }

        }

        public bool Equals(ThemeColor c)
        {

            if (R == c.R && G == c.G && B == c.B)
            {
                return true;
            }
            else if (Hex.ToLower().Equals(c.Hex.ToLower()))
            {
                return true;
            }

            return false;

        }

        public ThemeColor(string rgb, string hex)
        {

        }

    }
}
