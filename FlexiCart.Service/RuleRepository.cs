
using FlexiCart.DAL;
using FlexiCart.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexiCart.Service
{
    public class RuleRepository  :Repository<PromotionRule> , IRuleRepository
    {
        protected readonly ShoppingCartDBEntities dbContext;

        public RuleRepository (ShoppingCartDBEntities shoppingDBContext) :base(shoppingDBContext)
        {
            dbContext = shoppingDBContext;
        }

        public IEnumerable<PromotionRule> GetActiveRules()
        {
            return dbContext.PromotionRules.Where(p => p.IsActive == true);
        }

        public IEnumerable<PromotionRule> GetFestivalPromotions()
        {
            throw new NotImplementedException();
        }
    }
}
