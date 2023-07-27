using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.WaveFormRenderer;
using System.Drawing.Imaging;
using NAudio.Wave;
using System.IO;
using NAudio.Utils;
using System.Threading;
using NAudio.Wave.SampleProviders;
namespace AudioMerger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static string inputfilepath1, inputfilepath2;
        private static MemoryStream outputStream1 = new MemoryStream();
        private static MemoryStream outputStream2 = new MemoryStream();
        private static MemoryStream outputStream3 = new MemoryStream();
        private static double start1 = 0, end1 = 0;
        private static double starttemp1 = 0, endtemp1 = 0, start1X = 0, end1X = 0;
        private static double start2 = 0, end2 = 0;
        private static double starttemp2 = 0, endtemp2 = 0, start2X = 0, end2X = 0;
        private static WaveOutEvent player1 = new WaveOutEvent();
        private static WaveOutEvent player2 = new WaveOutEvent();
        private static WaveOutEvent player3 = new WaveOutEvent();
        private static StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings();
        private static WaveFormRenderer renderer = new WaveFormRenderer();
        private static MediaFoundationReader mediafundationreader1, mediafundationreader2;
        private static Image image1, image2;
        public static Brush brush = (Brush)Brushes.MediumPurple;
        public static string buttonpressed1, buttonpressed2, buttonpressed3;
        private static WaveFileReader reader1, reader2, reader3;
        private static bool temp1 = false, temp2 = false, closed = false, setprogresssong1 = false, setprogresssong2 = false, setprogresssong3 = false;
        public static double totaltime1, currenttime1, totaltime2, currenttime2, totaltime3, currenttime3;
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Task.Run(() => Start());
        }
        public void Start()
        {
            while (!closed)
            {
                SetProgressSong1();
                SetProgressSong2();
                SetProgressSong3();
                System.Threading.Thread.Sleep(40);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                inputfilepath1 = op.FileName;
                mediafundationreader1 = new MediaFoundationReader(inputfilepath1);
                myRendererSettings.Width = 1040;
                myRendererSettings.TopHeight = 60;
                myRendererSettings.BottomHeight = 60;
                MediaFoundationReader reader = new MediaFoundationReader(inputfilepath1);
                image1 = renderer.Render(reader, myRendererSettings);
                pictureBox1.BackgroundImage = image1;
                start1X = 0;
                starttemp1 = 0;
                end1X = 0;
                endtemp1 = 0;
                temp1 = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            player1.Pause();
            buttonpressed1 = "pause";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (buttonpressed1 == "pause")
            {
                player1.Play();
            }
            else
            {
                mediafundationreader1.Position = 0;
                outputStream1 = new MemoryStream();
                using (var waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(outputStream1), mediafundationreader1.WaveFormat))
                {
                    byte[] buffer = new byte[mediafundationreader1.WaveFormat.AverageBytesPerSecond];
                    int read;
                    while ((read = mediafundationreader1.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        waveFileWriter.Write(buffer, 0, read);
                    }
                }
                outputStream1.GetBuffer();
                outputStream1.Position = 0;
                reader1 = new WaveFileReader(outputStream1);
                player1.Init(reader1);
                player1.Play();
                totaltime1 = reader1.TotalTime.TotalSeconds;
                textBox6.Text = totaltime1.ToString();
                trackBar1.Value = 0;
            }
            buttonpressed1 = "play";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                player1.Stop();
            }
            catch { }
            buttonpressed1 = "stop";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                inputfilepath2 = op.FileName;
                mediafundationreader2 = new MediaFoundationReader(inputfilepath2);
                myRendererSettings.Width = 1040;
                myRendererSettings.TopHeight = 60;
                myRendererSettings.BottomHeight = 60;
                MediaFoundationReader reader = new MediaFoundationReader(inputfilepath2);
                image2 = renderer.Render(reader, myRendererSettings);
                pictureBox2.BackgroundImage = image2;
                start2X = 0;
                starttemp2 = 0;
                end2X = 0;
                endtemp2 = 0;
                temp2 = false;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            player2.Pause();
            buttonpressed2 = "pause";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (buttonpressed2 == "pause")
            {
                player2.Play();
            }
            else
            {
                mediafundationreader2.Position = 0;
                outputStream2 = new MemoryStream();
                using (var waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(outputStream2), mediafundationreader2.WaveFormat))
                {
                    byte[] buffer = new byte[mediafundationreader2.WaveFormat.AverageBytesPerSecond];
                    int read;
                    while ((read = mediafundationreader2.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        waveFileWriter.Write(buffer, 0, read);
                    }
                }
                outputStream2.GetBuffer();
                outputStream2.Position = 0;
                reader2 = new WaveFileReader(outputStream2);
                player2.Init(reader2);
                player2.Play();
                totaltime2 = reader2.TotalTime.TotalSeconds;
                textBox12.Text = totaltime2.ToString();
                trackBar2.Value = 0;
            }
            buttonpressed2 = "play";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                player2.Stop();
            }
            catch { }
            buttonpressed2 = "stop";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            player3.Pause();
            buttonpressed3 = "pause";
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (buttonpressed3 == "pause")
            {
                player3.Play();
            }
            else
            {
                TimeSpan delayDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox1.Text));
                TimeSpan delayDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox7.Text));
                TimeSpan skipDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox2.Text));
                TimeSpan skipDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox8.Text));
                TimeSpan takeDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox3.Text));
                TimeSpan takeDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox9.Text));
                TimeSpan silenceDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox4.Text));
                TimeSpan silenceDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox10.Text));
                reader1.Position = 0;
                reader2.Position = 0;
                OffsetSampleProvider trimmed1 = new OffsetSampleProvider(reader1.ToSampleProvider())
                {
                    DelayBy = delayDuration1,
                    SkipOver = skipDuration1,
                    Take = takeDuration1,
                    LeadOut = silenceDuration1
                };
                OffsetSampleProvider trimmed2 = new OffsetSampleProvider(reader2.ToSampleProvider())
                {
                    DelayBy = delayDuration2,
                    SkipOver = skipDuration2,
                    Take = takeDuration2,
                    LeadOut = silenceDuration2
                };
                var sources = new[]
                {
                    trimmed1,
                    trimmed2
                };
                var mixingSampleProvider = new MixingSampleProvider(sources);
                var waveProvider = mixingSampleProvider.ToWaveProvider();
                outputStream3 = new MemoryStream();
                using (var waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(outputStream3), waveProvider.WaveFormat))
                {
                    byte[] buffer = new byte[waveProvider.WaveFormat.AverageBytesPerSecond];
                    int read;
                    while ((read = waveProvider.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        waveFileWriter.Write(buffer, 0, read);
                    }
                }
                myRendererSettings.Width = 1040;
                myRendererSettings.TopHeight = 60;
                myRendererSettings.BottomHeight = 60;
                MediaFoundationReader streamreader = new StreamMediaFoundationReader(outputStream3);
                Image image3 = renderer.Render(streamreader, myRendererSettings);
                pictureBox3.BackgroundImage = image3;
                outputStream3.GetBuffer();
                outputStream3.Position = 0;
                reader3 = new WaveFileReader(outputStream3);
                player3.Init(reader3);
                player3.Play();
                totaltime3 = reader3.TotalTime.TotalSeconds;
                trackBar3.Value = 0;
            }
            buttonpressed3 = "play";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                player3.Stop();
            }
            catch { }
            buttonpressed3 = "stop";
        }
        private void button12_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "All Files(*.*)|*.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                TimeSpan delayDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox1.Text));
                TimeSpan delayDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox7.Text));
                TimeSpan skipDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox2.Text));
                TimeSpan skipDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox8.Text));
                TimeSpan takeDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox3.Text));
                TimeSpan takeDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox9.Text));
                TimeSpan silenceDuration1 = TimeSpan.FromSeconds(Convert.ToSingle(textBox4.Text));
                TimeSpan silenceDuration2 = TimeSpan.FromSeconds(Convert.ToSingle(textBox10.Text));
                reader1.Position = 0;
                reader2.Position = 0;
                OffsetSampleProvider trimmed1 = new OffsetSampleProvider(reader1.ToSampleProvider())
                {
                    DelayBy = delayDuration1,
                    SkipOver = skipDuration1,
                    Take = takeDuration1,
                    LeadOut = silenceDuration1
                };
                OffsetSampleProvider trimmed2 = new OffsetSampleProvider(reader2.ToSampleProvider())
                {
                    DelayBy = delayDuration2,
                    SkipOver = skipDuration2,
                    Take = takeDuration2,
                    LeadOut = silenceDuration2
                };
                var sources = new[]
                {
                    trimmed1,
                    trimmed2
                };
                var mixingSampleProvider = new MixingSampleProvider(sources);
                var waveProvider = mixingSampleProvider.ToWaveProvider();
                WaveFileWriter.CreateWaveFile(sf.FileName, waveProvider);
                myRendererSettings.Width = 1040;
                myRendererSettings.TopHeight = 60;
                myRendererSettings.BottomHeight = 60;
                MediaFoundationReader reader = new MediaFoundationReader(sf.FileName);
                Image image3 = renderer.Render(reader, myRendererSettings);
                pictureBox3.BackgroundImage = image3;
            }
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            reader1.CurrentTime = TimeSpan.FromSeconds(trackBar1.Value / 10000f * totaltime1);
            setprogresssong1 = false;
        }
        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            setprogresssong1 = true;
        }
        private void SetProgressSong1()
        {
            try
            {
                if (!setprogresssong1)
                {
                    currenttime1 = reader1.CurrentTime.TotalSeconds;
                    textBox5.Text = currenttime1.ToString();
                    trackBar1.Value = (int)(currenttime1 * 10000f / totaltime1);
                }
            }
            catch { }
        }
        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            reader2.CurrentTime = TimeSpan.FromSeconds(trackBar2.Value / 10000f * totaltime2);
            setprogresssong2 = false;
        }
        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            setprogresssong2 = true;
        }
        private void SetProgressSong2()
        {
            try
            {
                if (!setprogresssong2)
                {
                    currenttime2 = reader2.CurrentTime.TotalSeconds;
                    textBox11.Text = currenttime2.ToString();
                    trackBar2.Value = (int)(currenttime2 * 10000f / totaltime2);
                }
            }
            catch { }
        }
        private void trackBar3_MouseUp(object sender, MouseEventArgs e)
        {
            reader3.CurrentTime = TimeSpan.FromSeconds(trackBar3.Value / 10000f * totaltime3);
            setprogresssong3 = false;
        }
        private void trackBar3_MouseDown(object sender, MouseEventArgs e)
        {
            setprogresssong3 = true;
        }
        private void SetProgressSong3()
        {
            try
            {
                if (!setprogresssong3)
                {
                    currenttime3 = reader3.CurrentTime.TotalSeconds;
                    trackBar3.Value = (int)(currenttime3 * 10000f / totaltime3);
                }
            }
            catch { }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!temp1)
            {
                start1X = e.X;
                starttemp1 = (double)e.X / (double)pictureBox1.ClientSize.Width * mediafundationreader1.TotalTime.TotalSeconds;
                temp1 = true;
            }
            else
            {
                end1X = e.X;
                endtemp1 = (double)e.X / (double)pictureBox1.ClientSize.Width * mediafundationreader1.TotalTime.TotalSeconds;
                temp1 = false;
            }
            if (starttemp1 <= endtemp1)
            {
                start1 = Convert.ToSingle(starttemp1);
                end1 = Convert.ToSingle(endtemp1);
            }
            else
            {
                start1 = Convert.ToSingle(endtemp1);
                end1 = Convert.ToSingle(starttemp1);
            }
            textBox2.Text = start1.ToString();
            textBox3.Text = end1.ToString();
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Cross;
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!temp2)
            {
                start2X = e.X;
                starttemp2 = (double)e.X / (double)pictureBox2.ClientSize.Width * mediafundationreader2.TotalTime.TotalSeconds;
                temp2 = true;
            }
            else
            {
                end2X = e.X;
                endtemp2 = (double)e.X / (double)pictureBox2.ClientSize.Width * mediafundationreader2.TotalTime.TotalSeconds;
                temp2 = false;
            }
            if (starttemp2 <= endtemp2)
            {
                start2 = Convert.ToSingle(starttemp2);
                end2 = Convert.ToSingle(endtemp2);
            }
            else
            {
                start2 = Convert.ToSingle(endtemp2);
                end2 = Convert.ToSingle(starttemp2);
            }
            textBox8.Text = start2.ToString();
            textBox9.Text = end2.ToString();
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Cursor = Cursors.Cross;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            start1X = Convert.ToSingle(textBox2.Text) * (double)pictureBox1.ClientSize.Width / mediafundationreader1.TotalTime.TotalSeconds;
            end1X = Convert.ToSingle(textBox3.Text) * (double)pictureBox1.ClientSize.Width / mediafundationreader1.TotalTime.TotalSeconds;
            Bitmap bmp = new Bitmap(image1);
            Graphics graphics = Graphics.FromImage(bmp as Image);
            graphics.FillRectangle(brush, Convert.ToSingle(start1X) - 1, 0, 1, this.pictureBox1.Height);
            graphics.FillRectangle(brush, Convert.ToSingle(end1X) - 1, 0, 1, this.pictureBox1.Height);
            this.pictureBox1.BackgroundImage = bmp;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            start1X = Convert.ToSingle(textBox2.Text) * (double)pictureBox1.ClientSize.Width / mediafundationreader1.TotalTime.TotalSeconds;
            end1X = Convert.ToSingle(textBox3.Text) * (double)pictureBox1.ClientSize.Width / mediafundationreader1.TotalTime.TotalSeconds;
            Bitmap bmp = new Bitmap(image1);
            Graphics graphics = Graphics.FromImage(bmp as Image);
            graphics.FillRectangle(brush, Convert.ToSingle(start1X) - 1, 0, 1, this.pictureBox1.Height);
            graphics.FillRectangle(brush, Convert.ToSingle(end1X) - 1, 0, 1, this.pictureBox1.Height);
            this.pictureBox1.BackgroundImage = bmp;
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            start2X = Convert.ToSingle(textBox8.Text) * (double)pictureBox2.ClientSize.Width / mediafundationreader2.TotalTime.TotalSeconds;
            end2X = Convert.ToSingle(textBox9.Text) * (double)pictureBox2.ClientSize.Width / mediafundationreader2.TotalTime.TotalSeconds;
            Bitmap bmp = new Bitmap(image2);
            Graphics graphics = Graphics.FromImage(bmp as Image);
            graphics.FillRectangle(brush, Convert.ToSingle(start2X) - 1, 0, 1, this.pictureBox2.Height);
            graphics.FillRectangle(brush, Convert.ToSingle(end2X) - 1, 0, 1, this.pictureBox2.Height);
            this.pictureBox2.BackgroundImage = bmp;
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            start2X = Convert.ToSingle(textBox8.Text) * (double)pictureBox2.ClientSize.Width / mediafundationreader2.TotalTime.TotalSeconds;
            end2X = Convert.ToSingle(textBox9.Text) * (double)pictureBox2.ClientSize.Width / mediafundationreader2.TotalTime.TotalSeconds;
            Bitmap bmp = new Bitmap(image2);
            Graphics graphics = Graphics.FromImage(bmp as Image);
            graphics.FillRectangle(brush, Convert.ToSingle(start2X) - 1, 0, 1, this.pictureBox2.Height);
            graphics.FillRectangle(brush, Convert.ToSingle(end2X) - 1, 0, 1, this.pictureBox2.Height);
            this.pictureBox2.BackgroundImage = bmp;
        }
    }
}