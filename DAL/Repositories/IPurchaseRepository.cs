using DAL.Models;

namespace DAL.Repositories
{
    public interface IPurchaseRepository : IDisposable
    {
        Queue<Purchase> GetPurchases();
        Purchase GetPurchase(int id);
        void Create(Purchase item);
        void Update(Purchase item);
        void Delete(int id);
        void Save();
    }
}
