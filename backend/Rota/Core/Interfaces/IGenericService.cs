using System;
namespace Rota.Core.Interfaces
{
	public interface IGenericService<TDto> where TDto : class
	{
		Task<IEnumerable<TDto>> GetAllAsync();
		Task<TDto> GetByIdAsync(int id);
		Task CreateAsync(TDto dto);
		Task UpdateAsync(TDto dto);
		Task DeleteAsync(int id);
	}
}

