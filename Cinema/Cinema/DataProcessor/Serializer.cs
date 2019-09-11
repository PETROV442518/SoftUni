namespace Cinema.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies.Where(a => a.Rating >= rating && a.Projections.Any(z => z.Tickets.Sum(x => x.Customer.Id) > 0))
                .Select
                (a => new
                {
                    MovieName = a.Title,
                    Rating = $"{a.Rating:F2}",
                    TotalIncomes = $"{a.Projections.Sum(z => z.Tickets.Sum(x => x.Price)):F2}",
                    Customers = a.Projections.SelectMany(c => c.Tickets)
                    .Select(v => new
                    {
                        FirstName = v.Customer.FirstName,
                        LastName = v.Customer.LastName,
                        Balance = $"{v.Customer.Balance:F2}"
                    })
                    .OrderByDescending(f => f.Balance)
                    .ThenBy(f => f.FirstName)
                    .ThenBy(f => f.LastName)
                    .ToList()
                }
                )
                .OrderByDescending(a => double.Parse(a.Rating))
                .ThenByDescending(a => double.Parse(a.TotalIncomes))
                .Take(10)
                .ToList();
            var json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return json.ToString();
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Where(a => a.Age >= age)
              .Select(a => new ExportTopCustomerDto
              {
                  FirstName = a.FirstName,
                  LastName = a.LastName,
                  SpentMoney = $"{a.Tickets.Sum(x => x.Price):F2}",
                  SpentTime = TimeSpan.FromTicks(a.Tickets.Sum(z => z.Projection.Movie.Duration.Ticks)).ToString(@"hh\:mm\:ss")
              })
              .OrderByDescending(a => decimal.Parse(a.SpentMoney))
              .Take(10)
              .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportTopCustomerDto[]), new XmlRootAttribute("Customers"));
            var namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty,
            });

            var sb = new StringBuilder();
            xmlSerializer.Serialize(new StringWriter(sb), customers, namespaces);

            var result = sb.ToString().TrimEnd();

            return result;
        }

    }
}