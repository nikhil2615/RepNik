using System;
using System.Collections.Generic;
using System.Text;

namespace WPFSearchNetflix
{
    public class MovieTitle
    {
        public string movieId { get; set; }
        public string movieTitle { get; set; }
        public string boxArtLink { get; set; }
        public string Categories { get; set; }
        public int releaseYear { get; set; }
        public int runLengthTime { get; set; }
        public float averageRating { get; set; }

        public MovieTitle(string Movie_Id, string movie_Title, string box_Art_Link, string movie_Categories, int release_Year, int run_Length_Time, float average_rating)
        {
            movieId = Movie_Id;
            movieTitle = movie_Title;
            boxArtLink = box_Art_Link;
            Categories = movie_Categories;
            releaseYear = release_Year;
            runLengthTime = run_Length_Time;
            averageRating = average_rating;
        }
    }
}
