using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Архивация_по_Шеннона___Фано
{
    /// <summary>
    /// Interaction logic for RealTime.xaml
    /// </summary>
    public partial class RealTime : Window
    {
        string _last;
        System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
        BackgroundWorker Encode;
        Compression_Data.Shannon_Fano sf = new Compression_Data.Shannon_Fano();
        public RealTime()
        {
            InitializeComponent();
            _last = txt_input.Text;
            time.Start();
            Encode = new BackgroundWorker();
            Encode.DoWork += new DoWorkEventHandler(Encode_DoWork);
            Encode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Encode_RunWorkerCompleted);
            Encode.RunWorkerAsync();
        }
        private void Encode_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(((time.ElapsedMilliseconds / 100) >= 1) && (text_input != _last))
                {
                    Dispatcher.Invoke(() =>
                    {
                        time.Reset();
                        //ShowTime();
                        txt_output.Text = sf.EncodeText(txt_input.Text, true);
                        _last = txt_input.Text;
                    });
                    return;
                }
            }
        }
        public string text_input
        {
            get
            {
                return Dispatcher.Invoke(() => { return txt_input.Text; });
            }
        }
        private void Encode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            time.Start();
            Encode.RunWorkerAsync();
        }
        #region Настройка контролов

        #region Перемещение формы за шапку
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new System.Windows.Interop.WindowInteropHelper(this).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        #endregion
        private void imgClose_MouseLeave(object sender, MouseEventArgs e)
        {
            imgClose.StretchDirection = StretchDirection.DownOnly;
        }
        private void imgClose_MouseMove(object sender, MouseEventArgs e)
        {
            imgClose.StretchDirection = StretchDirection.UpOnly;
        }
        private void imgClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            time.Reset();
            Encode.WorkerSupportsCancellation = true;
            Encode.CancelAsync();
            this.Close();
        }
        private void imgMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            imgMinimized.StretchDirection = StretchDirection.DownOnly;
        }
        private void imgMinimize_MouseMove(object sender, MouseEventArgs e)
        {
            imgMinimized.StretchDirection = StretchDirection.UpOnly;
        }
        private void imgMinimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion
        private void txt_input_KeyDown(object sender, KeyEventArgs e)
        {
            time.Restart();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            txt_output.Text += "\r\n--------------------------------------------------------------------------------------------------------------------\r\n";
            txt_output.Text += String.Format("Энтропия на 1 букву ≈ {0:0.0000000} бит\r\n{1}", sf.Efficiency,sf.Table);
        }
    }
}
