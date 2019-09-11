namespace VaporStore.DataProcessor
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
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.ExportDtos;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var genres = context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(x => new
                {
                    Id = x.Id,
                    Genre = x.Name,
                    Games = x.Games
                    .Where(p => p.Purchases.Count > 0)
                    .Select(p => new
                    {
                        Id = p.Id,
                        Title = p.Name,
                        Developer = p.Developer.Name,
                        Tags = string.Join(", ", p.GameTags.Select(t => t.Tag.Name).ToArray()),
                        Players = p.Purchases.Count
                    })
                    .OrderByDescending(p => p.Players)
                    .ThenBy(p => p.Id)
                    .ToArray(),
                    TotalPlayers = x.Games.Sum(a => a.Purchases.Count)
                })
                .OrderByDescending(x => x.TotalPlayers)
                .ThenBy(x => x.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(genres, Newtonsoft.Json.Formatting.Indented);
            return json;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            var purchaseType = Enum.Parse<PurchaseType>(storeType);

            var users = context
                .Users
                .Select(x => new ExportUserDto
                {
                    Username = x.Username,
                    Purchases = x.Cards
                        .SelectMany(p => p.Purchases)
                        .Where(t => t.Type == purchaseType)
                        .Select(p => new ExportPurchaseDto
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new ExportGameDto
                            {
                                Genre = p.Game.Genre.Name,
                                Title = p.Game.Name,
                                Price = p.Game.Price
                            }
                        })
                        .OrderBy(d => d.Date)
                        .ToArray(),
                    TotalSpent = x.Cards.SelectMany(p => p.Purchases)
                        .Where(t => t.Type == purchaseType)
                        .Sum(p => p.Game.Price)
                })
                .Where(p => p.Purchases.Any())
                .OrderByDescending(t => t.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));
            var namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty,
            });
            var sb = new StringBuilder();
            xmlSerializer.Serialize(new StringWriter(sb), users, namespaces);
            return sb.ToString().TrimEnd();


        }
    }
}