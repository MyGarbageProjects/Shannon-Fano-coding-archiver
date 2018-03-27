using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Animation;
using System.ComponentModel;
using System.IO;

namespace Compression_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Shannon_Fano SF = new Shannon_Fano();
        string link;
        public MainWindow()
        {
            InitializeComponent();
            #region Запуск анимаци
            //Animation<Rectangle> anim = new Animation<Rectangle>(MainGrid);
            //anim.AddElement(anim_back_rect);
            //anim.Start(false);
            Animation<Rectangle> anim = new Animation<Rectangle>(MainGrid);
            anim.AddElement(anim_back_rect);
            anim.Start(false);
            #endregion
        }
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            //dlg.Filter = radio_enc.IsChecked == true ? "Document (*.txt)|*.txt| Images (*.jpg)|*.jpg" : "Shannon-Fano document (*.txtsf)|*.txtsf| Shannon-Fano Images (*.jpgsf)|*.jpgsf";
            dlg.Filter = radio_enc.IsChecked == true ? "Document (*.txt)|*.txt" : "Shannon-Fano document (*.txtsf)|*.txtsf";
            dlg.InitialDirectory = Environment.CurrentDirectory;

            if (dlg.ShowDialog() == true)
            {
                lbl_path.Content = "..." + dlg.InitialDirectory.Remove(0, dlg.InitialDirectory.LastIndexOf("\\")) + "\\" + dlg.SafeFileName;
                if (new FileInfo(dlg.FileName).Length > 6291456)
                    Log.Text += "------------------------------------------------\r\nК сожалению алгоритм не достаточно опитален,по этой причине,программа не может шифровать файлы больше ≈ 6 МБ\r\n";
                else
                {
                    #region Что делаем
                    if (radio_enc.IsChecked == true)
                    {
                        progressBar.Value = 0;
                        progressBar.IsIndeterminate = true;
                        #region Кодировка текста 
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += new DoWorkEventHandler(Start_EncodeText);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Complete_EncodeText);
                        worker.RunWorkerAsync(dlg);
                        #endregion
                    }
                    else
                    {
                        progressBar.Value = 0;
                        progressBar.IsIndeterminate = true;
                        #region Раскодировка текста
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += new DoWorkEventHandler(Start_DecodeText);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Complete_DecodeText);
                        worker.RunWorkerAsync(dlg);
                        #endregion
                    }
                    #endregion
                }
            }
        }

        private void Start_EncodeText(object sender, DoWorkEventArgs e)
        {
            #region Настройка контролов
            Dispatcher.Invoke(() =>
            {
                start_btn.IsEnabled = false;
                radio_dec.IsEnabled = false;
                radio_enc.IsEnabled = false;
            }, System.Windows.Threading.DispatcherPriority.Background);
            #endregion
            SF.EncodeText(dlg.FileName.Remove(dlg.FileName.LastIndexOf("\\")+1), dlg.SafeFileName, dlg.FileName.Remove(dlg.FileName.LastIndexOf("\\") + 1), true);//SF.EncodeText(dlg.InitialDirectory + "\\", dlg.SafeFileName, dlg.InitialDirectory + "\\", true);
        }
        private void Complete_EncodeText(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Запись в лог
            FileInfo _buffer = new FileInfo(dlg.FileName);
            FileInfo _buffer2 = new FileInfo(dlg.FileName + "sf");
            Log.Text += "------------------------------------------------\r\n";
            Log.Text += String.Format("Фаил успешно закодирован.\r\nИмя файла: {0}\r\nБыло: {1} КБ - Cтало: {2} КБ\r\nПроцент сжатия состовляет {3}%\r\n(вместе с таблицой)\r\nЭнтропия на 1 букву ≈ {4:0.0000000} бит\r\n",
                dlg.SafeFileName + "sf", _buffer.Length/1024, _buffer2.Length/1024, 100 - ((_buffer2.Length * 100) / _buffer.Length), SF.Efficiency);
            #endregion
            lbl_path.Foreground = new SolidColorBrush(Colors.RoyalBlue);
            #region Настройка контролов
            progressBar.Value = 100;
            progressBar.IsIndeterminate = false;
            start_btn.IsEnabled = true;
            radio_dec.IsEnabled = true;
            radio_enc.IsEnabled = true;
            #endregion
        }

        private void Start_DecodeText(object sender, DoWorkEventArgs e)
        {
            #region Настройка контролов
            Dispatcher.Invoke(() =>
            {
                start_btn.IsEnabled = false;
                radio_dec.IsEnabled = false;
                radio_enc.IsEnabled = false;
            }, System.Windows.Threading.DispatcherPriority.Background);
            #endregion
            SF.DecodeText(dlg.FileName.Remove(dlg.FileName.LastIndexOf("\\") + 1), dlg.SafeFileName, dlg.FileName.Remove(dlg.FileName.LastIndexOf("\\") + 1));
        }
        private void Complete_DecodeText(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Запись в лог
            string fileName = dlg.SafeFileName.Remove(dlg.SafeFileName.Length - 2);
            Log.Text += "------------------------------------------------\r\n";
            Log.Text += string.Format("Фаил успешно раскодирован.\r\nИмя файла: {0}\r\nБыло: {1} КБ- Cтало: {2} КБ\r\n", fileName, new FileInfo(dlg.FileName).Length/1024, new FileInfo(dlg.FileName.Remove(dlg.FileName.LastIndexOf("\\") + 1)+fileName).Length / 1024);
            #endregion
            lbl_path.Foreground = new SolidColorBrush(Colors.RoyalBlue);
            #region Настройка контролов
            progressBar.Value = 100;
            progressBar.IsIndeterminate = false;
            start_btn.IsEnabled = true;
            radio_dec.IsEnabled = true;
            radio_enc.IsEnabled = true;
            #endregion
        }

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
        #region Настройка контролов
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
            Application.Current.Shutdown();
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
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            if (info != null)
                info.Show();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Архивация_по_Шеннона___Фано.RealTime mode2 = new Архивация_по_Шеннона___Фано.RealTime();
            if (mode2 != null)
                mode2.ShowDialog();
        }
        private string Link
        {
            set { link = value; }
            get { return link; }
        }
        private void lbl_path_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_path.Foreground = new SolidColorBrush(Color.FromRgb(150, 150, 150));
        }
        private void lbl_path_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_path.Foreground = new SolidColorBrush(Colors.RoyalBlue);
        }
        private void lbl_path_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Link != "")
                System.Diagnostics.Process.Start(Link);
        }
        #endregion
    }
}
