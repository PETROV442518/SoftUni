namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var allGames = JsonConvert.DeserializeObject<ImportGamesDTO[]>(jsonString);
            var sb = new StringBuilder();
            var games = new List<Game>();
            foreach (var gameDto in allGames)
            {
                if (!IsValid(gameDto) || gameDto.Tags.Count == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.ParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };
                var developer = GetDeveloper(context, gameDto.Developer);
                game.Developer = developer;

                var genre = GetGenre(context, gameDto.Genre);
                game.Genre = genre;

                foreach (var currentTag in gameDto.Tags)
                {
                    var tag = GetTag(context, currentTag);
                    game.GameTags.Add(new GameTag
                    {
                        Tag = tag,
                        Game = game
                    });
                }
                games.Add(game);
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }



        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var AllUsersDto = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);
            var sb = new StringBuilder();
            var users = new List<User>();
            foreach (var userDto in AllUsersDto)
            {
                if (!IsValid(userDto) || !userDto.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                };

                var user = new User
                {
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Age = userDto.Age,
                };
                foreach (var cardDto in userDto.Cards)
                {
                    var card = new Card
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.CVC,
                        Type = Enum.Parse<CardType>(cardDto.Type)
                    };
                    user.Cards.Add(card);
                }
                users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportPurchaseDto[]), new XmlRootAttribute("Purchases"));
            var AllPurchases = (ImportPurchaseDto[])xmlSerializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();
            var purchases = new List<Purchase>();
            foreach (var purchaseDto in AllPurchases)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var isValidEnum = Enum.TryParse<PurchaseType>(purchaseDto.Type, out PurchaseType purchaseType);
                if (!isValidEnum)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var game = GetGame(context, purchaseDto.Title);
                var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);
                if (game == null || card == null)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var purchase = new Purchase
                {
                    Card = card,
                    Game = game,
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Type = Enum.Parse<PurchaseType>(purchaseDto.Type),
                    ProductKey = purchaseDto.Key
                };
                purchases.Add(purchase);
                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");

            }
            context.Purchases.AddRange(purchases);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static Game GetGame(VaporStoreDbContext context, string title)
        {
            var game = context.Games.FirstOrDefault(g => g.Name == title);
            return game;
        }

        private static Tag GetTag(VaporStoreDbContext context, string currentTag)
        {
            var tag = context.Tags.FirstOrDefault(t => t.Name == currentTag);
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = currentTag
                };
                context.Tags.Add(tag);
                context.SaveChanges();
            };

            return tag;
        }

        private static Genre GetGenre(VaporStoreDbContext context, string genre)
        {
            var realGenre = context.Genres.FirstOrDefault(g => g.Name == genre);
            if (realGenre == null)
            {
                realGenre = new Genre
                {
                    Name = genre
                };
                context.Genres.Add(realGenre);
                context.SaveChanges();
            };

            return realGenre;
        }

        private static Developer GetDeveloper(VaporStoreDbContext context, string developer)
        {
            var realDeveloper = context.Developers.FirstOrDefault(d => d.Name == developer);
            if (realDeveloper == null)
            {
                realDeveloper = new Developer
                {
                    Name = developer
                };
                context.Developers.Add(realDeveloper);
                context.SaveChanges();
            }

            return realDeveloper;
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return isValid;
        }
    }
}