namespace FlexiCart.Model.Interfaces
{
    public interface IPromotion
    {
        int RuleID { get; set; }

        string RuleName { get; set; }

        string Description { get; set; }

        bool IsActive { get; set; }
    }
}
