using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class CommentService : ICommentService
	{

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
		}

        public async Task CreateAsync(CommentDto dto)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            if (comment == null)
                throw new Exception("Comment not found");

            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            var comment = await _unitOfWork.Comments.GetAllAsync();
            return _mapper.Map<IEnumerable<CommentDto>>(comment);

        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            if (comment == null)
                throw new Exception("Comment not found");

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<List<CommentDto>> GetCommentsByTourIdAsync(int tourId)
        {
            return await _unitOfWork.CustomComments.GetCommentsByTourIdAsync(tourId);

        }

        public async Task<List<CommentDto>> GetCommentsByUserIdAsync(Guid userId)
        {
            return await _unitOfWork.CustomComments.GetCommentsByUserIdAsync(userId);   
        }

        public async Task UpdateAsync(CommentDto dto)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(dto.Id);
            if (comment == null)
                throw new Exception("Comment not found");

            _mapper.Map(dto, comment);
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveAsync();
        }
    }
}

