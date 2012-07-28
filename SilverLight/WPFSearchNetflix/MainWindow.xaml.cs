using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSearchNetflix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string netflixUri = "http://api-public.netflix.com";
            string netflixResource = "/catalog/titles";
            string consumerKey = "33aqvnhg4umct82rg5ptzxed";
            string consumerSecret = "SBQQmqR7R2";

            string resp = String.Empty;

            string tempSign = netflixUri + netflixResource + "?term=" + textBox1.Text + "&max_results=" + txtSliderValue.Text;

            string fullUri = getSign(tempSign, consumerKey, consumerSecret);
            WebRequest request = WebRequest.Create(fullUri);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            resp = sr.ReadToEnd();
            sr.Close();

            //StreamReader sr = new StreamReader("c:\\temp\\n.xml");
            //resp = sr.ReadToEnd();
            //sr.Close();

            List<MovieTitle> moviesList = new List<MovieTitle>();
            moviesList = HandleXML.parseXMLFeed(resp);

            StackPanel spResultChildContainer = null;
            TextBlock tbMovieTitle = null;
            MediaElement meBoxArtLink = null;
            TextBlock tbCategories = null;
            TextBlock tbReleaseYear = null;
            TextBlock tbRunLengthTime = null;
            TextBlock tbAverageRating = null;
            Border b = new Border();
            b.BorderThickness = new Thickness(2);
            b.Child = spResultChildContainer;
            
            foreach (MovieTitle loopMT in moviesList)
            {
                spResultChildContainer = new StackPanel();
                spResultChildContainer.Orientation = Orientation.Horizontal;
                spResultChildContainer.HorizontalAlignment = HorizontalAlignment.Left;
                spResultChildContainer.VerticalAlignment = VerticalAlignment.Top;
                spResultChildContainer.Width = 1000;

                meBoxArtLink = new MediaElement();
                meBoxArtLink.Source = new Uri(loopMT.boxArtLink);
                meBoxArtLink.Width = 60;
                meBoxArtLink.Height = 60;
                meBoxArtLink.Margin = new Thickness(5);

                tbAverageRating = new TextBlock();
                tbAverageRating.Text = loopMT.averageRating.ToString();
                tbAverageRating.Width = 45;
                tbAverageRating.Height = 23;
                tbAverageRating.FontSize = 15;
                tbAverageRating.Margin = new Thickness(5);
                
                tbMovieTitle = new TextBlock();
                tbMovieTitle.Text = loopMT.movieTitle;
                tbMovieTitle.Width = 300;
                tbMovieTitle.Height = 23;
                tbMovieTitle.FontSize = 15;
                tbMovieTitle.Margin = new Thickness(5);
                
                tbReleaseYear = new TextBlock();
                tbReleaseYear.Text = loopMT.releaseYear.ToString();
                tbReleaseYear.Width = 40;
                tbReleaseYear.Height = 23;
                tbReleaseYear.FontSize = 15;
                tbReleaseYear.Margin = new Thickness(5);
                
                tbRunLengthTime = new TextBlock();
                tbRunLengthTime.Text = loopMT.runLengthTime.ToString();
                tbRunLengthTime.Width = 60;
                tbRunLengthTime.Height = 23;
                tbRunLengthTime.FontSize = 15;
                tbRunLengthTime.Margin = new Thickness(5);
                
                tbCategories = new TextBlock();
                tbCategories.Text = loopMT.Categories;
                tbCategories.Width = 500;
                tbCategories.Height = 50;
                tbCategories.FontSize = 15;
                tbCategories.TextWrapping = TextWrapping.WrapWithOverflow;
                tbCategories.Margin = new Thickness(5);

                spResultChildContainer.Children.Add(meBoxArtLink);
                spResultChildContainer.Children.Add(tbAverageRating);
                spResultChildContainer.Children.Add(tbMovieTitle);
                spResultChildContainer.Children.Add(tbReleaseYear);
                spResultChildContainer.Children.Add(tbRunLengthTime);
                spResultChildContainer.Children.Add(tbCategories);

                stPanelResults.Children.Add(spResultChildContainer);

            }

        }

        static string getSign(string strUri, string consumerKey, string consumerSecret)
        {
            var uri = new Uri(strUri);
            string url, param;
            var oAuth = new OAuthBase();
            var nonce = oAuth.GenerateNonce();
            var timeStamp = oAuth.GenerateTimeStamp();

            var signature = oAuth.GenerateSignature(uri, consumerKey,
                consumerSecret, string.Empty, string.Empty, "GET", timeStamp, nonce,
                OAuthBase.SignatureTypes.HMACSHA1, out url, out param);

            return string.Format("{0}?{1}&oauth_signature={2}", url, param, signature);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            StackPanel spResultChildContainer = null;
            TextBlock tbMovieTitle = null;
            TextBlock tbCategories = null;
            TextBlock tbReleaseYear = null;
            TextBlock tbRunLengthTime = null;
            TextBlock tbAverageRating = null;
            TextBlock tbMediaHeader = null;

            tbMediaHeader = new TextBlock();
            tbMediaHeader.Text = "Image";
            tbMediaHeader.Width = 45;
            tbMediaHeader.Height = 23;
            tbMediaHeader.FontSize = 15;
            tbMediaHeader.Margin = new Thickness(5);

            tbAverageRating = new TextBlock();
            tbAverageRating.Text = "Rating";
            tbAverageRating.Width = 45;
            tbAverageRating.Height = 23;
            tbAverageRating.FontSize = 15;
            tbAverageRating.Margin = new Thickness(5);

            tbMovieTitle = new TextBlock();
            tbMovieTitle.Text = "Title";
            tbMovieTitle.Width = 300;
            tbMovieTitle.Height = 23;
            tbMovieTitle.FontSize = 15;
            tbMovieTitle.Margin = new Thickness(5);

            tbReleaseYear = new TextBlock();
            tbReleaseYear.Text = "Year";
            tbReleaseYear.Height = 23;
            tbReleaseYear.Width = 40;
            tbReleaseYear.FontSize = 15;
            tbReleaseYear.Margin = new Thickness(5);

            tbRunLengthTime = new TextBlock();
            tbRunLengthTime.Text = "Duration";
            tbRunLengthTime.Width = 60;
            tbRunLengthTime.Height = 23;
            tbRunLengthTime.FontSize = 15;
            tbRunLengthTime.Margin = new Thickness(5);

            tbCategories = new TextBlock();
            tbCategories.Text = "Categories";
            tbCategories.Height = 23;
            tbCategories.Width = 500;
            tbCategories.FontSize = 15;
            tbCategories.Margin = new Thickness(5);

            spResultChildContainer = new StackPanel();
            spResultChildContainer.Orientation = Orientation.Horizontal;
            spResultChildContainer.HorizontalAlignment = HorizontalAlignment.Left;
            spResultChildContainer.VerticalAlignment = VerticalAlignment.Top;
            spResultChildContainer.Width = 1000;

            spResultChildContainer.Children.Add(tbMediaHeader);
            spResultChildContainer.Children.Add(tbAverageRating);
            spResultChildContainer.Children.Add(tbMovieTitle);
            spResultChildContainer.Children.Add(tbReleaseYear);
            spResultChildContainer.Children.Add(tbRunLengthTime);
            spResultChildContainer.Children.Add(tbCategories);

            stPanelResults.Children.Add(spResultChildContainer);
        }
    }
}
