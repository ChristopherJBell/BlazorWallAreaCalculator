namespace BlazorWallAreaCalculator.Data
{
    public interface IRoomService
    {
        Task<bool> RoomCreate(Room room);
        Task<IEnumerable<Room>> RoomsReadByProject(int ProjectID);
        Task<int> CountRoomsByNameAndProject(string RoomName, int ProjectID);
        Task<int> CountRoomsByNameAndProjectAndId(string RoomName, int RoomID, int ProjectID);
        Task<bool> RoomUpdate(Room room);
        Task<bool> RoomDelete(Int32 RoomID);
    }
}
