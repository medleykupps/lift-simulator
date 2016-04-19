namespace LiftSimulator.Core
{
    public interface IRepository
    {
        bool AddSummaryItem(SummaryItem item);
        bool ResetSummaryItems();
    }
}