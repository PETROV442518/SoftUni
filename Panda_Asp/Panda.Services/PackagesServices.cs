using System.Collections.Generic;
using System.Linq;
using Panda.Data;
using Panda.Models;
using Panda.Services.Dtos;

namespace Panda.Services
{
    public class PackagesServices : IPackagesServices
    {
        private readonly PandaDbContext _context;

        public PackagesServices(PandaDbContext context)
        {
            this._context = context;
        }
        public List<PackageIndexInfo> GetPackagesByStatus(PackageStatus packageStatus)
        {
            List<Package> packages = this._context.Packages.Where(s => s.Status == packageStatus ).ToList();
            List<PackageIndexInfo> Dtos = new List<PackageIndexInfo>();
            foreach (var p in packages)
            {
                PackageIndexInfo pack = new PackageIndexInfo
                {
                    Id = p.Id,
                    Description = p.Description,
                    RecipientId = p.RecipientId
                };
                Dtos.Add(pack);
            }
            return Dtos;
        }

        
    }
}
