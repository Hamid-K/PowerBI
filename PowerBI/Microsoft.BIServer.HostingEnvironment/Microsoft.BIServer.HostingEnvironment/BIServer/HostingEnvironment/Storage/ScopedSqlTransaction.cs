using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.HostingEnvironment.Storage
{
	// Token: 0x02000023 RID: 35
	public class ScopedSqlTransaction : ISqlAccess, IDisposable
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004937 File Offset: 0x00002B37
		public ScopedSqlTransaction(MeteredSqlConnection connection, string name)
		{
			this._meter = ScopeMeter.Use(string.Format("SQL", "XACT", name));
			this._connection = connection;
			this._transaction = this._connection.BeginTransaction(name);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004973 File Offset: 0x00002B73
		public void Commit()
		{
			this._transaction.Commit();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004980 File Offset: 0x00002B80
		public void Rollback()
		{
			this._transaction.Rollback();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000498D File Offset: 0x00002B8D
		public void Dispose()
		{
			this._transaction.Dispose();
			this._meter.Dispose();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000049A8 File Offset: 0x00002BA8
		public async Task<int> ExecuteAsync(string storedProcedure, object parameters)
		{
			return await this._connection.ExecuteAsync(storedProcedure, this._transaction, parameters);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000049FD File Offset: 0x00002BFD
		public Task<int> ExecuteAsync(string storedProcedure)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000049FD File Offset: 0x00002BFD
		public Task<IEnumerable<TEntityType>> QueryAsync<TEntityType>(string storedProcedure)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000049FD File Offset: 0x00002BFD
		public Task<IEnumerable<TEntityType>> QueryAsync<TEntityType>(string storedProcedure, object parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000049FD File Offset: 0x00002BFD
		public Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004A04 File Offset: 0x00002C04
		public async Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure, object parameters)
		{
			return await this._connection.QueryFirstOrDefaultAsync<TEntityType>(storedProcedure, this._transaction, parameters);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000049FD File Offset: 0x00002BFD
		public void ExecuteBatchScript(string script)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000049FD File Offset: 0x00002BFD
		public void ExecuteNonQuery(string statement, Dictionary<string, object> parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000049FD File Offset: 0x00002BFD
		public T ExecuteScalar<T>(string query, Dictionary<string, object> parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000049FD File Offset: 0x00002BFD
		public Task<DbDataReader> ExecuteReaderAsync(string storedProcedure, Dictionary<string, object> parameters, CommandBehavior commandBehavior)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000049FD File Offset: 0x00002BFD
		public SqlCommand PrepareStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000049FD File Offset: 0x00002BFD
		public SqlCommand PrepareStoredProcedure(string storedProcedureName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000049FD File Offset: 0x00002BFD
		public void ExecuteBatchScript(string script, TimeSpan individualCommandTimeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000049FD File Offset: 0x00002BFD
		public string GetStringOrNullColumn(SqlDataReader results, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000049FD File Offset: 0x00002BFD
		public byte[] GetBinaryOrNullColumn(SqlDataReader results, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000049FD File Offset: 0x00002BFD
		public string DatabaseName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000085 RID: 133
		private readonly MeteredSqlConnection _connection;

		// Token: 0x04000086 RID: 134
		private readonly SqlTransaction _transaction;

		// Token: 0x04000087 RID: 135
		private readonly IDisposable _meter;
	}
}
