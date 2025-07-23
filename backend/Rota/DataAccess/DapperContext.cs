using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Rota.DataAccess
{
	public class DapperContext
	{

		private readonly string _connectionString = "Server=localhost,1433;Database=rotadb;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";

		private IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}


		//select işlemi (tek satır/dto)
		public async Task<T> QuerySingleAsync<T>(string procAdi, DynamicParameters param = null)
		{
			using var connection = CreateConnection();
			return await connection.QuerySingleOrDefaultAsync<T>(procAdi, param, commandType: CommandType.StoredProcedure);
		}


		//select işlemi (liste)
		public async Task<IEnumerable<T>> QueryAsync<T>(string procAdi, DynamicParameters param = null)
		{
			using var connection = CreateConnection();
			return await connection.QueryAsync<T>(procAdi, param, commandType: CommandType.StoredProcedure);
		}


		
	}
}

