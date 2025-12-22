using StudentsManager.Mvc.Domain.CustomDataStructures;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Forum;

namespace StudentsManager.Mvc.Services.Forum
{
    public interface IForumService
    {
        Task<PaginatedList<ForumQuestion>> GetPaginatedListAsync(int pageIndex = 1);

        Task<List<ForumQuestion>> GetAsync(int limit = 20, int skip = 0);

        Task<ForumQuestion> SaveQuestionAsync(string question, Guid userId);

        Task<ForumComment> SaveCommentAsync(Comment input, Guid userId);

        Task<List<string>> GetSlidoQuestionsAsync(int limit = 20, int skip = 0);
    }
}
