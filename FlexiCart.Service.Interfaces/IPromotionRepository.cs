using System.Collections.Generic;

namespace FlexiCart.Service.Interfaces
{
    public interface IPromotionRepository<Promotion>
    {
        IEnumerable<Promotion> GetFestivalPromotions();

        IEnumerable<Promotion> GetActiveRules();
    }
}
