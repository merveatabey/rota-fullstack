using System;
using Entities;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ICommentRepository 
	{
        Task<List<CommentDto>> GetCommentsByTourIdAsync(int tourId);
        Task<List<CommentDto>> GetCommentsByUserIdAsync(Guid userId);
    }
}

