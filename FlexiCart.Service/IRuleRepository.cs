using FlexiCart.DAL;
using System.Collections.Generic;

namespace FlexiCart.Service.Interfaces
{
    public interface IRuleRepository :IRepository<PromotionRule>
    {

        IEnumerable<PromotionRule> GetFestivalPromotions();

        IEnumerable<PromotionRule> GetActiveRules();

       

    }
}
