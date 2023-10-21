namespace BlazorWallAreaCalculator.Data
{
    public interface IWallService
    {
        Task<bool> WallCreate(Wall wall);
        Task<IEnumerable<Wall>> WallsReadByRoom(int RoomID);
        Task<int> CountWallsByNameAndRoom(string WallName, int RoomID);
        Task<int> CountWallsByNameAndRoomAndId(string WallName, int WallID, int RoomID);
        Task<bool> WallUpdate(Wall wall);
        Task<bool> WallDelete(Int32 WallID);
    }
}