using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ICommentService : IGenericService<CommentDto>
	{
		Task<List<CommentDto>> GetCommentsByTourIdAsync(int tourId);
		Task<List<CommentDto>> GetCommentsByUserIdAsync(Guid userId);
	}
}

