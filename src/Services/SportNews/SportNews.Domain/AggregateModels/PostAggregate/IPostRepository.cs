using SportNews.Domain.SeedWork;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.AggregateModels.PostAggregate
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(string id);
        Task<PagedList<Post>> GetPostsPagingAsync(string postId, string searchKeyword, int pageIndex, int pageSize);
    }
}
