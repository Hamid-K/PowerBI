using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.HostingEnvironment.Storage
{
	// Token: 0x02000024 RID: 36
	public class ReferenceSqlAccess : ISqlAccess, IDisposable
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004A59 File Offset: 0x00002C59
		public static ISqlAccess UseButDoNotDispose(ISqlAccess referenceSqlAccess)
		{
			return new ReferenceSqlAccess(referenceSqlAccess);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004A61 File Offset: 0x00002C61
		private ReferenceSqlAccess(ISqlAccess sqlAccess)
		{
			this._sqlAccess = sqlAccess;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004A70 File Offset: 0x00002C70
		Task<int> ISqlAccess.ExecuteAsync(string storedProcedure)
		{
			return this._sqlAccess.ExecuteAsync(storedProcedure);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004A7E File Offset: 0x00002C7E
		Task<int> ISqlAccess.ExecuteAsync(string storedProcedure, object parameters)
		{
			return this._sqlAccess.ExecuteAsync(storedProcedure, parameters);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004A8D File Offset: 0x00002C8D
		Task<IEnumerable<TEntityType>> ISqlAccess.QueryAsync<TEntityType>(string storedProcedure)
		{
			return this._sqlAccess.QueryAsync<TEntityType>(storedProcedure);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004A9B File Offset: 0x00002C9B
		Task<IEnumerable<TEntityType>> ISqlAccess.QueryAsync<TEntityType>(string storedProcedure, object parameters)
		{
			return this._sqlAccess.QueryAsync<TEntityType>(storedProcedure, parameters);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004AAA File Offset: 0x00002CAA
		Task<TEntityType> ISqlAccess.QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure)
		{
			return this._sqlAccess.QueryFirstOrDefaultAsync<TEntityType>(storedProcedure);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004AB8 File Offset: 0x00002CB8
		Task<TEntityType> ISqlAccess.QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure, object parameters)
		{
			return this._sqlAccess.QueryFirstOrDefaultAsync<TEntityType>(storedProcedure, parameters);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004AC7 File Offset: 0x00002CC7
		void ISqlAccess.ExecuteBatchScript(string script)
		{
			this._sqlAccess.ExecuteBatchScript(script);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004AD5 File Offset: 0x00002CD5
		void ISqlAccess.ExecuteNonQuery(string statement, Dictionary<string, object> parameters)
		{
			this._sqlAccess.ExecuteNonQuery(statement, parameters);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004AE4 File Offset: 0x00002CE4
		T ISqlAccess.ExecuteScalar<T>(string query, Dictionary<string, object> parameters)
		{
			return this._sqlAccess.ExecuteScalar<T>(query, parameters);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004AF3 File Offset: 0x00002CF3
		Task<DbDataReader> ISqlAccess.ExecuteReaderAsync(string storedProcedure, Dictionary<string, object> parameters, CommandBehavior commandBehavior)
		{
			return this._sqlAccess.ExecuteReaderAsync(storedProcedure, parameters, commandBehavior);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004B03 File Offset: 0x00002D03
		SqlCommand ISqlAccess.PrepareStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters)
		{
			return this._sqlAccess.PrepareStoredProcedure(storedProcedureName, parameters);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004B12 File Offset: 0x00002D12
		SqlCommand ISqlAccess.PrepareStoredProcedure(string storedProcedureName)
		{
			return this._sqlAccess.PrepareStoredProcedure(storedProcedureName);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004B20 File Offset: 0x00002D20
		void ISqlAccess.ExecuteBatchScript(string script, TimeSpan individualCommandTimeout)
		{
			this._sqlAccess.ExecuteBatchScript(script, individualCommandTimeout);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004B2F File Offset: 0x00002D2F
		string ISqlAccess.GetStringOrNullColumn(SqlDataReader results, int index)
		{
			return this._sqlAccess.GetStringOrNullColumn(results, index);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004B3E File Offset: 0x00002D3E
		byte[] ISqlAccess.GetBinaryOrNullColumn(SqlDataReader results, int index)
		{
			return this._sqlAccess.GetBinaryOrNullColumn(results, index);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004B4D File Offset: 0x00002D4D
		string ISqlAccess.DatabaseName
		{
			get
			{
				return this._sqlAccess.DatabaseName;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003749 File Offset: 0x00001949
		public void Dispose()
		{
		}

		// Token: 0x04000088 RID: 136
		private readonly ISqlAccess _sqlAccess;
	}
}
