using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IProvInsurRepository
    {
        Task<int[]> GetInsuranceCoverage(int id);
        Task<int[]> GetProviderCoverage(int id);
    }
}