namespace BlazorWallAreaCalculator.Data
{
    public interface IWallService
    {
        Task<bool> WallCreate(Wall wall);
        Task<IEnumerable<Wall>> WallReadAll();
        Task<int> CountWallsByName(string WallName);
        Task<int> CountWallsByNameAndId(string WallName, int WallID);
        Task<bool> WallUpdate(Wall wall);
        Task<bool> WallDelete(Int32 WallID);
    }
}
