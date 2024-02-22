namespace BlazorWallAreaCalculator.Data
{
    public interface IDeductionService
    {
        Task<bool> DeductionCreate(Deduction deduction);
        Task<IEnumerable<Deduction>> DeductionReadAll();
        Task<IEnumerable<Deduction>> DeductionsReadByWall(int WallID);
        Task<int> CountDeductionsByName(string DeductionName);
        Task<int> CountDeductionsByNameAndId(string DeductionName, int DeductionID);
        Task<bool> DeductionUpdate(Deduction deduction);
        Task<bool> DeductionDelete(Int32 DeductionID);
    }
}
