namespace BlazorWallAreaCalculator.Data
{
    public interface IProjectService
    {
        Task<bool> ProjectCreate(Project project);
        Task<IEnumerable<Project>> ProjectReadAll();
        Task<int> CountProjectsByName(string ProjectName);
        Task<int> CountProjectsByNameAndId(string ProjectName, int ProjectID);
        Task<bool> ProjectUpdate(Project project);
        Task<bool> ProjectDelete(Int32 ProjectID);
    }
}
