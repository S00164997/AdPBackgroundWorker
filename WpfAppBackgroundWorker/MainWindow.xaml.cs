using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppBackgroundWorker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //S00164997
        //Michael Chrystal
        public MainWindow()
        {
            //StuNo.Content = "S00164997 Michael Chrystal";
            InitializeComponent();
            
        }

        private void DoWorkButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker BGW = new BackgroundWorker();//setup background worker
           
            BGW.WorkerReportsProgress = true;//show progress

            BGW.DoWork += worker_DoWork;//do work is this method
            BGW.ProgressChanged += worker_ProgressChanged;//call this when progress changed
            
            BGW.RunWorkerCompleted += worker_RunWorkerCompleted;//when completed do this method
            BGW.RunWorkerAsync();//run background worker
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MyProgressBar.Value = e.ProgressPercentage;//update value on screen
            ProgressTextBlock.Text = (string)e.UserState;//update text block based on e.Userstate
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(0, String.Format("Percentage 0%"));//set start Userstate
            for (int i = 0; i < 11; i++)//while less than 10 sleep for 1 second and update iteration
            {
                Thread.Sleep(1000);
                //multiply by 10 to haveprogress bar the same as i.e.g I<10
                worker.ReportProgress((i ) * 10, String.Format("Percentage {0}.", i *10+"%"));
            }

            //worker.ReportProgress(100, "Done Processing.");//set to 100 for last step
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Process Finished!");//display message
            MyProgressBar.Value = 0;//reset to 0
            ProgressTextBlock.Text = "";//set to empty
        }
    }
}
