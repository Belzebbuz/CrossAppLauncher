using LauncherClient.Data.Models;
using Refit;

namespace LauncherClient.ApplicationLayer.HttpServices
{
	[Headers("Authorization: Bearer")]
	public interface IProjectData
	{
		[Get("/Apps")]
		Task<List<ProjectDTO>> GetAllProjects();

		[Get("/Apps/{id}")]
		Task<ProjectDTO> GetProjectById(Guid id);

		[Get("/Apps/{isWinService}/{name}")]
		Task<List<ProjectDTO>> GetProjectsByFilter(bool isWinService, string name);

		[Get("/Apps/filter/{isWinService}")]
		Task<List<ProjectDTO>> GetProjectsByIsWinService(bool isWinService);

		[Multipart]
		[Post("/Apps/{id}/addRelease")]
		[Headers("Authorization: Bearer")]
		Task<ReleaseAssemblyDTO> UploadFile(Guid? id, [AliasAs("file")] StreamPart stream);

		[Get("/Apps/downloadRelease/{id}")]
		[Headers("Authorization: Bearer")]
		Task<HttpContent> DownloadLastRelease(Guid id);

		[Post("/Apps")]
		[Headers("Authorization: Bearer")]
		Task<Guid> CreateProject(CreateProjectDTO createProjectDTO);

	}
}
