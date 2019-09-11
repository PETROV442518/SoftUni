namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2:f2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var MoviesDto = JsonConvert.DeserializeObject<ImportMoviesDto[]>(jsonString);
            var movies = new List<Movie>();
            var sb = new StringBuilder();
            foreach (var item in MoviesDto)
            {
                var IsGenre = Enum.TryParse<Genre>(item.Genre, out Genre genre);
                var movieTitles = movies.Select(a => a.Title).ToList();
                if (!IsValid(item) || movieTitles.Contains(item.Title) || !IsGenre)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                // var rating = double.Parse(item.Rating);
                // if(rating > 10.00 || rating < 1.00)
                // {
                //     sb.AppendLine(ErrorMessage);
                //     continue;
                // };
                var rating = $"{item.Rating}.00";


                var movie = new Movie
                {
                    Title = item.Title,
                    Genre = Enum.Parse<Genre>(item.Genre),
                    Duration = TimeSpan.Parse(item.Duration),
                    Rating = double.Parse(rating, System.Globalization.NumberStyles.Float),
                    Director = item.Director
                };
                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating));
            }
            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var HallsDto = JsonConvert.DeserializeObject<ImportHallSeats[]>(jsonString);
            var sb = new StringBuilder();
            var halls = new List<Hall>();
            var seats = new List<Seat>();
            foreach (var hallDto in HallsDto)
            {
                if (!IsValid(hallDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall
                {
                    Name = hallDto.Name,
                    Is3D = hallDto.Is3D,
                    Is4Dx = hallDto.Is4Dx
                };
                
                for (int i = 1; i <= hallDto.Seats; i++)
                {
                    var seat = new Seat
                    {
                       
                    };
                    seats.Add(seat);
                    hall.Seats.Add(seat);
                }
                
                halls.Add(hall);
                var projectionType = "";
                if (hall.Is3D == true && hall.Is4Dx == true)
                {
                    projectionType = "4Dx/3D";

                }
                else if ((hall.Is4Dx == true) && hall.Is3D == false)
                {
                    projectionType = "4Dx";
                } else if (hall.Is4Dx == false && hall.Is3D == true)
                {
                    projectionType = "3D";
                }else
                {
                    projectionType = "Normal";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
            }
            context.Seats.AddRange(seats);
            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
           var xmlSerializer = new XmlSerializer(typeof(ImportProjections[]), new XmlRootAttribute("Projections"));
           var projectionsDto = (ImportProjections[])xmlSerializer.Deserialize(new StringReader(xmlString));


            var sb = new StringBuilder();
            var projections = new List<Projection>();
            var movies = context.Movies.Select(a => a.Id).ToList();
            var halls = context.Halls.Select(a => a.Id).ToList();
            foreach (var projectionDto in projectionsDto)
            {
                
                if(!IsValid(projectionDto) || !movies.Contains(projectionDto.MovieId) || !halls.Contains(projectionDto.HallId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var projection = new Projection
                {
                    HallId = projectionDto.HallId,
                    MovieId = projectionDto.MovieId,
                    DateTime = DateTime.ParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                projections.Add(projection);
                var movieTitle = context.Movies.FirstOrDefault(m => m.Id == projection.MovieId);
                var stringDateTime = 
                   projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                sb.AppendLine(string.Format(SuccessfulImportProjection, movieTitle.Title, stringDateTime));
            }
            context.Projections.AddRange(projections);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCustomerTickets[]), new XmlRootAttribute("Customers"));
            var customersDto = (ImportCustomerTickets[])xmlSerializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();
            var customers = new List<Customer>();
            var tickets = new List<Ticket>();

            foreach (var item in customersDto)
            {
                if(!IsValid(item))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var customer = new Customer
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Age = item.Age,
                    Balance = item.Balance
                    
                };

                foreach (var tick in item.Tickets)
                {
                    var ticket = new Ticket
                    {
                        Price = tick.Price,
                        ProjectionId = tick.ProjectionId
                    };
                    tickets.Add(ticket);
                    customer.Tickets.Add(ticket);
                }
                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }
            context.Tickets.AddRange(tickets);
            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return isValid;
        }
    }
}