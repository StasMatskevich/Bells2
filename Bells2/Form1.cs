using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Bells2
{
    public partial class Main : Form
    {
        Pen p;
        Font tableFont;
        StringFormat tableFormat;
        float cellWidth, cellHeight;
        bool initialized;
        DateTime lastBell;
        Regex r;

        string[] lessons = new string[7]
            {
                "8:30-9:15",
                "9:25-10:10",
                "10:20-11:05",
                "11:15-12:00",
                "12:15-13:00",
                "13:15-14:00",
                "14:10-14:55"
            };

        string[] days = new string[6]
        {
                "Понедельник",
                "Вторник",
                "Среда",
                "Четверг",
                "Пятница",
                "Суббота"
        };

        string[,] classes = new string[6, 7]
        {
            { "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "" }
        };

        public Main()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            p = new Pen(Color.Black, 2);
            tableFont = new Font("Microsoft Sans Serif", 14, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableFormat = StringFormat.GenericDefault;
            tableFormat.Alignment = StringAlignment.Center;
            tableFormat.LineAlignment = StringAlignment.Center;
            initialized = false;
            r = new Regex(@"^\d{1,2}\:\d{2}\-\d{1,2}\:\d{2}$");
            LoadData();
        }

        void DrawGrid(int columns, int rows, Graphics g)
        {
            cellWidth = (tableBox.Width-2) / columns;
            cellHeight = (tableBox.Height-2) / rows;
            float x0 = 1;
            float y0 = 1;

            for (int i = 0; i <= rows; ++i)
            {
                g.DrawLine(p, x0, y0 + i * cellHeight, x0 + columns * cellWidth, y0 + i * cellHeight);
            }

            for (int i = 0; i <= columns; ++i)
            {
                g.DrawLine(p, x0 + i * cellWidth, y0, x0 + i * cellWidth, y0 + rows * cellHeight);
            }

            for (int i = 0; i < rows - 1; i++)
            {
                g.DrawString((i+1).ToString() + ") " + lessons[i], tableFont, Brushes.Black, new RectangleF(x0, y0 + cellHeight * (i + 1), cellWidth, cellHeight), tableFormat);
            }

            for (int i = 0; i < columns - 1; i++)
            {
                g.DrawString(days[i], tableFont, Brushes.Black, new RectangleF(x0 + cellWidth * (i + 1), y0, cellWidth, cellHeight), tableFormat);
            }

            int day = (DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            day--;

            int lD = 0, lC = 0;
            for (int x = 0; x < columns - 1; x++)
            {
                if (x > day) break;
                for (int y = 0; y < rows - 1; y++)
                {
                    if (x < day || DateTime.Parse(lessons[y].Substring(lessons[y].IndexOf('-') + 1)).CompareTo(DateTime.Now) <= 0)
                    {
                        g.FillRectangle(Brushes.Green, x0 + 1 + cellWidth * (x + 1), y0 + 1 + cellHeight * (y + 1), cellWidth - 2, cellHeight - 2);
                        lD = x;
                        lC = y;
                    }
                    else break;
                }
            }

            if ((lC < rows - 2 || (lC == rows-2 && lD == day-1)) && day < 6)
            {
                if (lC < rows - 2) lC++;
                else { lC = 0; lD++; }
                TimeSpan difference = (DateTime.Parse(lessons[lC].Substring(0, lessons[lC].IndexOf('-'))) - DateTime.Now);
                if (difference < new TimeSpan(0))
                {
                    difference = (DateTime.Parse(lessons[lC].Substring(lessons[lC].IndexOf('-') + 1)) - DateTime.Now);
                    TimeSpan period = (DateTime.Parse(lessons[lC].Substring(lessons[lC].IndexOf('-') + 1)) - DateTime.Parse(lessons[lC].Substring(0, lessons[lC].IndexOf('-'))));
                    float percent = 1 - (float)difference.TotalSeconds / (float)period.TotalSeconds;
                    g.FillRectangle(Brushes.Green, x0 + 1 + cellWidth * (lD + 1), y0 + 1 + cellHeight * (lC + 1), (cellWidth - 2) * percent, cellHeight - 2);
                }

                while (lC < rows - 1 && classes[lD, lC] == "") lC++;

                if (lC < rows - 1)
                {
                    DateTime nextBell = DateTime.Parse(lessons[lC].Substring(0, lessons[lC].IndexOf('-')));
                    difference = (DateTime.Parse(lessons[lC].Substring(0, lessons[lC].IndexOf('-'))) - DateTime.Now);
                    if (difference < new TimeSpan(0))
                    {
                        difference = (DateTime.Parse(lessons[lC].Substring(lessons[lC].IndexOf('-') + 1)) - DateTime.Now);
                        nextBell = DateTime.Parse(lessons[lC].Substring(lessons[lC].IndexOf('-') + 1));
                    }
                    bellTime.Text = "Звонок через: " + difference.Duration().ToString(@"hh\:mm\:ss");
                    if (!initialized)
                    {
                        lastBell = nextBell;
                        initialized = true;
                    }
                    else if (lastBell != nextBell)
                    {
                        lastBell = nextBell;
                        Bell();
                    }
                }
                else
                {
                    bellTime.Text = "Уроков нет";
                    if (day < 6 && initialized && lastBell != DateTime.Parse(lessons[6].Substring(lessons[6].IndexOf('-') + 1)))
                    {
                        lastBell = DateTime.Parse(lessons[6].Substring(lessons[6].IndexOf('-') + 1));
                        Bell();
                    }
                }

            }
            else
            {
                bellTime.Text = "Уроков нет";
                if(day < 6 && initialized && lastBell != DateTime.Parse(lessons[6].Substring(lessons[6].IndexOf('-') + 1)))
                {
                    lastBell = DateTime.Parse(lessons[6].Substring(lessons[6].IndexOf('-') + 1));
                    Bell();
                }
            }


            for (int y = 0; y < rows - 1; y++)
            {
                for (int x = 0; x < columns - 1; x++)
                {
                    if (classes[x, y] == "") continue;
                    g.DrawString(classes[x, y], tableFont, Brushes.Black, new RectangleF(x0 + cellWidth * (x + 1), y0 + cellHeight * (y + 1), cellWidth, cellHeight), tableFormat);
                }
            }


        }

        void Bell()
        {
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.sound);
            sound.Play();
        }

        private void drawer_Tick(object sender, EventArgs e)
        {
            tableBox.Invalidate();
            timeLabel.Text = "Сейчас: " + DateTime.Now.ToLongTimeString();
        }

        void DrawFrame(object sender, PaintEventArgs e)
        {
            DrawGrid(7, 8, e.Graphics);
        }

        private void tableBox_Click(object sender, EventArgs e)
        {
            int x = (int)(tableBox.PointToClient(MousePosition).X / cellWidth);
            int y = (int)(tableBox.PointToClient(MousePosition).Y / cellHeight);
            if (x > 0 && y > 0)
            {
                ClassDialog dialog = new ClassDialog(days[x-1] + Environment.NewLine + lessons[y-1] + Environment.NewLine + classes[x-1,y-1]);
                dialog.ShowDialog();
                if (dialog.succes)
                {
                    classes[x - 1, y - 1] = dialog.res;
                    initialized = false;
                    SaveData();
                }
                dialog.Dispose();
            }
            else if (x == 0 && y > 0)
            {
                TimeDialog dialog = new TimeDialog(y, lessons[y-1].Substring(0,lessons[y-1].IndexOf('-')), lessons[y-1].Substring(lessons[y-1].IndexOf('-')+1));
                dialog.ShowDialog();
                if(dialog.succes)
                {
                    lessons[y-1] = dialog.res;
                    initialized = false;
                    SaveData();
                }
                dialog.Dispose();
            }
        }

        void SaveData()
        {
            using (XmlWriter writer = XmlWriter.Create(Path.Combine(Directory.GetCurrentDirectory().ToString(), "Saves.xml")))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("data");

                writer.WriteStartElement("lessons");

                for(int i = 0; i < 7; i++)
                {
                    writer.WriteStartElement("lesson");
                    writer.WriteElementString("duration", lessons[i]);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteStartElement("classes");

                for (int x = 0; x < 6; x++)
                {
                    writer.WriteStartElement("day");
                    for (int y = 0; y < 7; y++)
                    {
                        writer.WriteStartElement("class");
                        writer.WriteElementString("object", classes[x,y]);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void copyRightLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://vk.com/stas_matskevich");
        }

        void LoadData()
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory().ToString(), "Saves.xml"))) SaveData();
            using (XmlTextReader reader = new XmlTextReader(Path.Combine(Directory.GetCurrentDirectory().ToString(), "Saves.xml")))
            {
                int sector = 0;
                int x = 0, y = 0;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "lessons")
                    {
                        x = 0;
                        y = 0;
                        sector = 1;
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "lessons") sector = 0;
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == "classes")
                    {
                        x = 0;
                        y = 0;
                        sector = 2;
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "classes") sector = 0;
                    if (sector == 1)
                    {
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            if (r.Match(reader.Value).Success)
                            {
                                lessons[x] = reader.Value;
                                x++;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка чтения времени из файла сохранения. Возможно в него были внесены некорректные изменения пользователем или сторонним программами. Если после перезапуска ошибка останется - обратитесь к разработчику");
                                reader.Close();
                                SaveData();
                                return;
                            }
                        }
                    }
                    else if (sector == 2)
                    {
                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "day") { x++; y = 0;}
                        else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "class") y++;
                        else if (reader.NodeType == XmlNodeType.Text)
                        {
                            classes[x, y] = reader.Value;
                        }
                    }
                }
            }
        }
    }
}
