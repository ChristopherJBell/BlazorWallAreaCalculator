namespace BlazorWallAreaCalculator.Data
{
    public interface IProjectService
    {
        Task<bool> ProjectCreate(Project project);
        Task<IEnumerable<Project>> ProjectReadAll();
        Task<bool> ProjectUpdate(Project project);
        Task<bool> ProjectDelete(Int32 ProjectID);
    }
}
