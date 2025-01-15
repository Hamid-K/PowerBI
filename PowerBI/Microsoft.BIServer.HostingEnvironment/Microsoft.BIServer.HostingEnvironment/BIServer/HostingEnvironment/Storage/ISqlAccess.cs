using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.HostingEnvironment.Storage
{
	// Token: 0x02000021 RID: 33
	public interface ISqlAccess : IDisposable
	{
		// Token: 0x060000CC RID: 204
		Task<int> ExecuteAsync(string storedProcedure);

		// Token: 0x060000CD RID: 205
		Task<int> ExecuteAsync(string storedProcedure, object parameters);

		// Token: 0x060000CE RID: 206
		Task<IEnumerable<TEntityType>> QueryAsync<TEntityType>(string storedProcedure);

		// Token: 0x060000CF RID: 207
		Task<IEnumerable<TEntityType>> QueryAsync<TEntityType>(string storedProcedure, object parameters);

		// Token: 0x060000D0 RID: 208
		Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure);

		// Token: 0x060000D1 RID: 209
		Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure, object parameters);

		// Token: 0x060000D2 RID: 210
		void ExecuteBatchScript(string script);

		// Token: 0x060000D3 RID: 211
		void ExecuteNonQuery(string statement, Dictionary<string, object> parameters);

		// Token: 0x060000D4 RID: 212
		T ExecuteScalar<T>(string query, Dictionary<string, object> parameters);

		// Token: 0x060000D5 RID: 213
		Task<DbDataReader> ExecuteReaderAsync(string storedProcedure, Dictionary<string, object> parameters, CommandBehavior commandBehavior);

		// Token: 0x060000D6 RID: 214
		SqlCommand PrepareStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters);

		// Token: 0x060000D7 RID: 215
		SqlCommand PrepareStoredProcedure(string storedProcedureName);

		// Token: 0x060000D8 RID: 216
		void ExecuteBatchScript(string script, TimeSpan individualCommandTimeout);

		// Token: 0x060000D9 RID: 217
		string GetStringOrNullColumn(SqlDataReader results, int index);

		// Token: 0x060000DA RID: 218
		byte[] GetBinaryOrNullColumn(SqlDataReader results, int index);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DB RID: 219
		string DatabaseName { get; }
	}
}
