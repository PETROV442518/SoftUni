using System.Collections.Generic;
using Panda.Models;
using Panda.Services.Dtos;

namespace Panda.Services
{
    public interface IPackagesServices
    {
        List<PackageIndexInfo> GetPackagesByStatus(PackageStatus packageStatus);
    }
}
