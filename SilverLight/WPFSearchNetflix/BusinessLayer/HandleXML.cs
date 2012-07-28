using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace WPFSearchNetflix
{
    public class HandleXML
    {
        //static string xmlFile = "c:\\temp\\n.xml";

        public static List<MovieTitle> parseXMLFeed (string xmlFeed)
        {
            string movieId = String.Empty;
            string movieTitle = string.Empty;
            int release_year = 0;
            string category = String.Empty;
            int runLengthTime = 0;
            float average_rating = 0;
            string boxArtLink = String.Empty;
            int itemCounter = 0;


            MovieTitle mt = null;
            List<MovieTitle> movieList = new List<MovieTitle>();

            
            XmlReader reader = XmlReader.Create(new StringReader(xmlFeed));
            while (reader.Read())
            {
                if (reader.Name == "catalog_titles")
                    continue;
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "catalog_title":
                            itemCounter++;
                            break;
                        case "id":
                            movieId = reader.ReadElementContentAsString();
                            break;
                        case "title":
                                if (true == reader.MoveToFirstAttribute())
                                    if (true == reader.MoveToNextAttribute())
                                        movieTitle = reader.Value;
                            break;
                        case "box_art":
                                if (true == reader.MoveToFirstAttribute())
                                    if (true == reader.MoveToNextAttribute())
                                        if (true == reader.MoveToNextAttribute()) 
                                            boxArtLink = reader.Value;
                            break;
                        case "release_year":
                            release_year = reader.ReadElementContentAsInt();
                            break;
                        case "category":
                            {
                                if (true == reader.MoveToFirstAttribute())
                                    if (true == reader.MoveToNextAttribute())
                                        category += reader.Value + ", ";
                            }
                            break;
                        case "runtime":
                            runLengthTime = reader.ReadElementContentAsInt();
                            runLengthTime /= 60;
                            break;
                        case "averate_rating":
                            average_rating = reader.ReadElementContentAsFloat();
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "catalog_title")
                    itemCounter++;

                if (itemCounter == 2)
                {
                    category = category.TrimEnd(' ');
                    category = category.TrimEnd(',');

                    mt = new MovieTitle(movieId, movieTitle, boxArtLink, category, release_year, runLengthTime, average_rating);
                    movieList.Add(mt);
                    itemCounter = 0;
                }
            }
            return movieList;
        }

      /* public static List<MovieTitle> parseXMLFeed(string xmlFeed, int a)
        {
            string movieId = String.Empty;
            string movieTitle = string.Empty;
            int release_year = 0;
            string category = String.Empty;
            int runLengthTime = 0;
            float average_rating = 0;
            string boxArtLink = String.Empty;

            StreamWriter sw = new StreamWriter("c:\\temp\\tempXml.xml");
            sw.Write(xmlFeed);
            sw.Flush();
            sw.Close();
            sw = null;

            XPathDocument doc = new XPathDocument(@"c:\temp\tempXml.xml");
            XPathNavigator nav = doc.CreateNavigator();

            // Compile a standard XPath expression
            XPathExpression expr;
            expr = nav.Compile("/catalog/cd/price");
            XPathNodeIterator iterator = nav.Select(expr);

            try
            {
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    listBox1.Items.Add("price: " + nav2.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }*/
    }
}
