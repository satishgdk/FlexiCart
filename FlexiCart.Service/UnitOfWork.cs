using FlexiCart.DAL;
using FlexiCart.Service.Interfaces;


namespace FlexiCart.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingCartDBEntities _context;

        public UnitOfWork(ShoppingCartDBEntities context)
        {
            _context = context;
            PromotionRules = new RuleRepository(_context);
        }

        public IRuleRepository PromotionRules { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
