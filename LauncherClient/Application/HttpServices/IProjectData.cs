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

		[Post("/Apps")]
		Task<Guid> CreateProject(CreateProjectDTO createProjectDTO);

		[Put("/Apps")]
		Task UpdateProject(ProjectDTO ProjectDTO);

		[Delete("/Apps/{id}")]
		Task DeleteProject(Guid id);

		[Multipart]
		[Post("/Releases/addRelease/{id}")]
		Task<ReleaseAssemblyDTO> UploadFile(Guid? id, [AliasAs("file")] StreamPart stream);

		[Get("/Releases/downloadLastRelease/{id}")]
		Task<HttpContent> DownloadLastRelease(Guid id);

		[Get("/Releases/downloadRelease/{id}")]
		Task<HttpContent> DownloadRelease(Guid id);

		

	}
}
