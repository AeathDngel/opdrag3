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

        private int valueReturn = 0;
        private int[] pnr;
        private int[] fnr;
        private int rows;
        private long memKb;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            GetPhysicallyInstalledSystemMemory(out memKb);

            Label1.Text = (memKb / 1024 / 1024) + " GB of RAM installed. | " + memKb + " kB or RAM installed";




        }

        protected void TestSubmit_ServerClick(object sender, EventArgs e)
        {
         
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            setTable(1);
        }

        public void eliminateDuplicates(int value)
        {

            Random rand = new Random();
            int count = 0;
            for (int i = 0; i < fnr.Length; i++)
            {
                if (fnr[i] == value)
                    count++;



            }

            if (count > 0)
            {
                eliminateDuplicates(rand.Next(0, 1000));

            }
            else if (count == 0)
            {
                // MessageBox.Show(value.ToString());
                valueReturn = value;
            }

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            setTable(2);
        }

        public void setTable(int x)
        {
            GetPhysicallyInstalledSystemMemory(out memKb);

            if (Int32.Parse(TextBox1.Text) > memKb)
            {
                Label4.Text = "You allocated to much memory to reserve!";
                return;
            }
            else if(Int32.Parse(TextBox1.Text) <= 0)
            {
                Label4.Text = "You can't allocate no memory to reserve!";
                return;
            }

            if (Int32.Parse(TextBox2.Text) > Int32.Parse(TextBox1.Text))
            {
                Label4.Text = "Your page frame size can't be bigger than your allocated reserve memory!!";
                return;
            }

            Label4.Text = "";

            string fileName = "data.txt";
            string paths = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string path = paths.Substring(6) + "/" + fileName;
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
                            eliminateDuplicates(rand.Next(0, 1000));
                            int num = valueReturn;
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

                    readText[Int32.Parse(count[0])] = TextBox4.Text;

                    System.IO.File.WriteAllText(path2, string.Empty);

                    using (FileStream fs = new FileStream(path2, FileMode.Append, FileAccess.Write))
                    using (StreamWriter outputFile = new StreamWriter(fs))
                    {

                        outputFile.WriteLine(count[0]); // Write the file.
                    }

                    System.IO.File.WriteAllText(path, string.Empty);

                    for (int l = 0; l < rows; l++)
                    {
                        TableRow r = new TableRow();
                        for (int m = 0; m < cols; m++)
                        {
                            TableCell c = new TableCell();
                            if (m == 0)
                            {

                                c.Controls.Add(new LiteralControl(l.ToString()));
                            }
                            else
                            {

                                c.Controls.Add(new LiteralControl(readText[l].ToString()));

                                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                                using (StreamWriter outputFile = new StreamWriter(fs))
                                {

                                    outputFile.WriteLine(readText[l]); // Write the file.
                                }



                            }
                            r.Cells.Add(c);
                        }
                        Table1.Rows.Add(r);
                    }
                }
                else
                {

                    Label4.Text = "Page already exists";
                    for (int l = 0; l < rows; l++)
                    {
                        TableRow r = new TableRow();
                        for (int m = 0; m < cols; m++)
                        {
                            TableCell c = new TableCell();
                            if (m == 0)
                            {

                                c.Controls.Add(new LiteralControl(l.ToString()));
                            }
                            else
                            {

                                c.Controls.Add(new LiteralControl(readText[l].ToString()));

                                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                                using (StreamWriter outputFile = new StreamWriter(fs))
                                {

                                    outputFile.WriteLine(readText[l]); // Write the file.
                                }

                                

                            }
                            r.Cells.Add(c);
                        }
                        Table1.Rows.Add(r);
                    }
                }


            }
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

         
        }
    }
}