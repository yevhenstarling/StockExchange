using DAL.Models;

namespace DAL.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private Queue<Purchase> purchaseQueue = new Queue<Purchase>();

        public PurchaseRepository()
        {
            //Hardcoded starting values
            purchaseQueue.Enqueue(new Purchase { Shares = 100, CostPerShare = 20, PurchaseDate = new DateTime(2023, 1, 1) });
            purchaseQueue.Enqueue(new Purchase { Shares = 150, CostPerShare = 30, PurchaseDate = new DateTime(2023, 2, 1) });
            purchaseQueue.Enqueue(new Purchase { Shares = 120, CostPerShare = 10, PurchaseDate = new DateTime(2023, 3, 1) });
        }

        //TODO: specify parameters for getting certain purchases (by userID, by shareID ... )
        public Queue<Purchase> GetPurchases()
        {
            return purchaseQueue;
        }

        //TODO: fill these methods
        public Purchase GetPurchase(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Purchase item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Purchase item)
        {
            throw new NotImplementedException();
        }

        //Dispose pattern part
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
