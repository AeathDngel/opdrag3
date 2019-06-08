using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.IO;
using System.Text;

namespace Opdrag_3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        private int[] pnr;
        private int[] fnr;
        private int rows;

        protected void Page_Load(object sender, EventArgs e)
        {
            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);

            Label1.Text = (memKb / 1024 / 1024) + " GB of RAM installed.";




        }
        string path = "E:/Dropbox/Zonica/Klas/ITRW316/Eksamen Projek/O3/Opdrag 3/Opdrag 3";

        protected void TestSubmit_ServerClick(object sender, EventArgs e)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("E:/ Dropbox / Zonica / Klas / ITRW316 / Eksamen Projek / O3 / Opdrag 3 / Opdrag 3/data.txt"), true))
            {
                _testData.WriteLine(fnr); // Write the file.

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            setTable(1);
        }

        public int eliminateDuplicates(int value)
        {

            Random rand = new Random();

            for (int i = 0; i < fnr.Length; i++)
            {
                if (fnr[i] == value)
                    eliminateDuplicates(rand.Next(0, 1000));
                else
                    return value;

            }
            return value;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            /*  
               }*/
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            setTable(2);
        }

        public void setTable(int x)
        {


            string fileName = "data.txt";
            string paths = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = paths.Substring(6) + "/" + fileName;
            string path2 = paths.Substring(6) + "/count.txt";

            rows = Int32.Parse(TextBox1.Text) / Int32.Parse(TextBox2.Text); //VARIABLE
            int cols = 2;
            pnr = new int[rows];
            fnr = new int[rows];

            Random rand = new Random();

            if (x == 1)
            {
                System.IO.File.WriteAllText(path2, string.Empty);
                using (FileStream fs = new FileStream(path2, FileMode.Append, FileAccess.Write))
                using (StreamWriter outputFile = new StreamWriter(fs))
                {

                    outputFile.WriteLine("-1"); // Write the file.
                }


                System.IO.File.WriteAllText(path, string.Empty);

                for (int j = 0; j < rows; j++)
                {
                    TableRow r = new TableRow();
                    for (int i = 0; i < cols; i++)
                    {
                        TableCell c = new TableCell();
                        if (i == 0)
                        {
                            pnr[i] = j;
                            c.Controls.Add(new LiteralControl(j.ToString()));
                        }
                        else
                        {
                            int num = eliminateDuplicates(rand.Next(0, 1000));
                            fnr[i] = num;
                            c.Controls.Add(new LiteralControl(num.ToString()));

                            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                            using (StreamWriter outputFile = new StreamWriter(fs))
                            {

                                outputFile.WriteLine(fnr[i]); // Write the file.
                            }



                        }
                        r.Cells.Add(c);
                    }
                    Table1.Rows.Add(r);
                }
            }
            if (x == 2)
            {

                rows = Int32.Parse(TextBox1.Text) / Int32.Parse(TextBox2.Text); //VARIABLE

                pnr = new int[rows];
                fnr = new int[rows];

                string[] readText = File.ReadAllLines(path);
                bool pageexist = false;

                for (int c = 0; c < readText.Length; c++)
                {
                    if (TextBox4.Text == readText[c])
                        pageexist = true;
                }
                //Label4.Text = readText[0];
                if (pageexist == false)
                {
                    string[] count = File.ReadAllLines(path2);

                    if (Int32.Parse(count[0]) == rows - 1)
                    {
                        count[0] = "0";
                    }
                    else
                    {
                        count[0] = (Int32.Parse(count[0]) + 1).ToString();
                    }
                    //   int counts = Int32.Parse(count[0]);

                 
                    }
                }


            }
        }
    }
}