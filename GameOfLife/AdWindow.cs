using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    class AdWindow : Window
    {
        private readonly DispatcherTimer adTimer;
        private int imgNmb;     // the number of the image currently shown
        private string link;    // the URL where the currently shown ad leads to
        private BitmapImage[] images = new BitmapImage[3]; // the images to be shown in the ad window
        
    
        public AdWindow(Window owner)
        {
            Random rnd = new Random();
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;
            images[0] = new BitmapImage(new Uri("ad1.jpg", UriKind.Relative));
            images[1] = new BitmapImage(new Uri("ad2.jpg", UriKind.Relative));
            images[2] = new BitmapImage(new Uri("ad3.jpg", UriKind.Relative));
            imgNmb = rnd.Next(1, 3);
            ChangeAds(this, new EventArgs());

            // Run the timer that changes the ad's image 
            adTimer = new DispatcherTimer();
            adTimer.Interval = TimeSpan.FromSeconds(3);
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link);
            Close();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            Unsubscribe();
            base.OnClosed(e);
        } 

        public void Unsubscribe()
        {
            adTimer.Tick -= ChangeAds;
            images = null;
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            
            ImageBrush myBrush = new ImageBrush();
            
            switch (imgNmb)
            {
                case 1:
                    myBrush.ImageSource = images[0];
                    Background = myBrush;
                    link = "http://example.com";
                    imgNmb++;
                    break;
                case 2:
                    myBrush.ImageSource = images[1];
                    Background = myBrush;
                    link = "http://example.com";
                    imgNmb++;
                    break;
                case 3:
                    myBrush.ImageSource = images[2];
                    Background = myBrush;
                    link = "http://example.com";
                    imgNmb = 1;
                    break;
            }
            
        }
    }
}