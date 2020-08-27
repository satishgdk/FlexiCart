using FlexiCart.Service.Interfaces;
using System;

namespace FlexiCart.Service
{
    public interface IUnitOfWork :IDisposable
    {
        IRuleRepository PromotionRules { get; set; }

        int Complete();
    }
}
