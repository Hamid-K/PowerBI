using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.HostingEnvironment.Storage
{
	// Token: 0x02000022 RID: 34
	public sealed class MeteredSqlConnection : ISqlAccess, IDisposable
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000040B4 File Offset: 0x000022B4
		public MeteredSqlConnection(string connectionString)
		{
			this._timeMeter = new TimeMeter(MeteredSqlConnection.SqlMeter.Data);
			this._connection = new SqlConnection(connectionString);
			try
			{
				this._connection.Open();
			}
			catch (InvalidOperationException ex)
			{
				throw new Exception("ReportServerDatabaseUnavailableException", ex);
			}
			catch (SqlException ex2)
			{
				if (ex2.Number == 4060)
				{
					throw new Exception("ReportServerDatabaseLogonFailedException", ex2);
				}
				throw new Exception("ReportServerDatabaseUnavailableException", ex2);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004144 File Offset: 0x00002344
		public SqlTransaction BeginTransaction(string name)
		{
			return this._connection.BeginTransaction(name);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004154 File Offset: 0x00002354
		public async Task<int> ExecuteAsync(string storedProcedure)
		{
			int num;
			try
			{
				using (ScopeMeter.Use(new string[] { "SQL", "Exec", storedProcedure }))
				{
					IDbConnection connection = this._connection;
					object obj = null;
					IDbTransaction dbTransaction = null;
					CommandType? commandType = new CommandType?(CommandType.StoredProcedure);
					num = await connection.ExecuteAsync(storedProcedure, obj, dbTransaction, new int?(MeteredSqlConnection.DatabaseQueryTimeout), commandType);
				}
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Exception during db access", Array.Empty<object>());
				throw;
			}
			return num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041A4 File Offset: 0x000023A4
		public async Task<int> ExecuteAsync(string storedProcedure, object parameters)
		{
			return await this.ExecuteAsync(storedProcedure, null, parameters);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000041FC File Offset: 0x000023FC
		internal async Task<int> ExecuteAsync(string storedProcedure, IDbTransaction transaction, object parameters)
		{
			int num;
			try
			{
				using (ScopeMeter.Use(new string[] { "SQL", "Exec", storedProcedure }))
				{
					IDbConnection connection = this._connection;
					CommandType? commandType = new CommandType?(CommandType.StoredProcedure);
					num = await connection.ExecuteAsync(storedProcedure, parameters, transaction, new int?(MeteredSqlConnection.DatabaseQueryTimeout), commandType);
				}
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Exception during db access", Array.Empty<object>());
				throw;
			}
			return num;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000425C File Offset: 0x0000245C
		public async Task<IEnumerable<TEntityType>> QueryAsync<TEntityType>(string storedProcedure)
		{
			IEnumerable<TEntityType> enumerable;
			try
			{
				this.StartStatementMeter(storedProcedure);
				using (ScopeMeter.Use(new string[] { "SQL", "Query", storedProcedure }))
				{
					IDbConnection connection = this._connection;
					object obj = null;
					IDbTransaction dbTransaction = null;
					CommandType? commandType = new CommandType?(CommandType.StoredProcedure);
					enumerable = await connection.QueryAsync(storedProcedure, obj, dbTransaction, new int?(MeteredSqlConnection.DatabaseQueryTimeout), commandType);
				}
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Exception during db access", Array.Empty<object>());
				throw;
			}
			return enumerable;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000042AC File Offset: 0x000024AC
		public async Task<IEnumerable<TEntityType>> QueryAsync<TEntityType>(string storedProcedure, object parameters)
		{
			IEnumerable<TEntityType> enumerable;
			try
			{
				this.StartStatementMeter(storedProcedure);
				using (ScopeMeter.Use(new string[] { "SQL", storedProcedure, "async" }))
				{
					IDbConnection connection = this._connection;
					IDbTransaction dbTransaction = null;
					CommandType? commandType = new CommandType?(CommandType.StoredProcedure);
					enumerable = await connection.QueryAsync(storedProcedure, parameters, dbTransaction, new int?(MeteredSqlConnection.DatabaseQueryTimeout), commandType);
				}
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Exception during db access", Array.Empty<object>());
				throw;
			}
			return enumerable;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004304 File Offset: 0x00002504
		public async Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure)
		{
			TEntityType tentityType;
			try
			{
				using (ScopeMeter.Use(new string[] { "SQL", "Exec", storedProcedure }))
				{
					IDbConnection connection = this._connection;
					object obj = null;
					IDbTransaction dbTransaction = null;
					CommandType? commandType = new CommandType?(CommandType.StoredProcedure);
					tentityType = await connection.QueryFirstOrDefaultAsync(storedProcedure, obj, dbTransaction, new int?(MeteredSqlConnection.DatabaseQueryTimeout), commandType);
				}
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Exception during db access", Array.Empty<object>());
				throw;
			}
			return tentityType;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004354 File Offset: 0x00002554
		public async Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure, object parameters)
		{
			return await this.QueryFirstOrDefaultAsync<TEntityType>(storedProcedure, null, parameters);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000043AC File Offset: 0x000025AC
		internal async Task<TEntityType> QueryFirstOrDefaultAsync<TEntityType>(string storedProcedure, IDbTransaction transaction, object parameters)
		{
			TEntityType tentityType;
			try
			{
				using (ScopeMeter.Use(new string[] { "SQL", "Exec", storedProcedure }))
				{
					IDbConnection connection = this._connection;
					CommandType? commandType = new CommandType?(CommandType.StoredProcedure);
					tentityType = await connection.QueryFirstOrDefaultAsync(storedProcedure, parameters, transaction, new int?(MeteredSqlConnection.DatabaseQueryTimeout), commandType);
				}
			}
			catch (Exception ex)
			{
				Logger.Debug(ex, "Exception during db access", Array.Empty<object>());
				throw;
			}
			return tentityType;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004409 File Offset: 0x00002609
		public void ExecuteBatchScript(string script)
		{
			this.ExecuteBatchScript(script, MeteredSqlConnection.DefaultTimeout);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004418 File Offset: 0x00002618
		public void ExecuteBatchScript(string script, TimeSpan individualCommandTimeout)
		{
			string text = string.Format("_Batch_{0}", Guid.NewGuid());
			using (ScopeMeter.Use(new string[] { "SQL", "Batch", text }))
			{
				string text2 = "<none>";
				try
				{
					string[] array = Regex.Split(script, "^\\s*GO\\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
					for (int i = 0; i < array.Length; i++)
					{
						text2 = array[i].Trim();
						if (text2.Length > 0)
						{
							using (ScopeMeter.Use(new string[] { "SQL", text, text2 }))
							{
								using (SqlCommand sqlCommand = new SqlCommand(text2, this._connection))
								{
									sqlCommand.CommandTimeout = (int)individualCommandTimeout.TotalSeconds;
									sqlCommand.ExecuteNonQuery();
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Error(ex, "Aborting SQL batch sccript!  Error executing SQL batch script at entry\n------------\n{0}\n------------\n", new object[] { text2 });
					throw;
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004548 File Offset: 0x00002748
		public void ExecuteNonQuery(string statement, Dictionary<string, object> parameters)
		{
			try
			{
				using (ScopeMeter.Use(new string[] { "SQL", "Exec", statement }))
				{
					using (SqlCommand sqlCommand = new SqlCommand(statement, this._connection))
					{
						if (parameters != null)
						{
							foreach (KeyValuePair<string, object> keyValuePair in parameters)
							{
								sqlCommand.Parameters.Add(new SqlParameter(keyValuePair.Key, keyValuePair.Value));
							}
						}
						sqlCommand.CommandTimeout = MeteredSqlConnection.DatabaseQueryTimeout;
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error executing SQL command. Details:{0}", new object[] { ex });
				throw;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004648 File Offset: 0x00002848
		public T ExecuteScalar<T>(string query, Dictionary<string, object> parameters)
		{
			T t;
			try
			{
				using (ScopeMeter.Use(new string[] { "SQL", "Exec", query }))
				{
					using (SqlCommand sqlCommand = new SqlCommand(query, this._connection))
					{
						foreach (KeyValuePair<string, object> keyValuePair in parameters)
						{
							sqlCommand.Parameters.Add(new SqlParameter(keyValuePair.Key, keyValuePair.Value));
						}
						sqlCommand.CommandTimeout = MeteredSqlConnection.DatabaseQueryTimeout;
						t = (T)((object)sqlCommand.ExecuteScalar());
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error executing SQL command. Details:{0}", new object[] { ex });
				throw;
			}
			return t;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004748 File Offset: 0x00002948
		public async Task<DbDataReader> ExecuteReaderAsync(string storedProcedure, Dictionary<string, object> parameters, CommandBehavior commandBehavior)
		{
			DbDataReader dbDataReader;
			try
			{
				this.StartStatementMeter(storedProcedure);
				using (ScopeMeter.Use(new string[] { "SQL", "Reader", storedProcedure }))
				{
					using (SqlCommand cmd = new SqlCommand(storedProcedure, this._connection))
					{
						foreach (KeyValuePair<string, object> keyValuePair in parameters)
						{
							cmd.Parameters.Add(new SqlParameter(keyValuePair.Key, keyValuePair.Value));
						}
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandTimeout = MeteredSqlConnection.DatabaseQueryTimeout;
						dbDataReader = await cmd.ExecuteReaderAsync(commandBehavior);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error executing SQL command. Details:{0}", new object[] { ex });
				throw;
			}
			return dbDataReader;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000047A8 File Offset: 0x000029A8
		public SqlCommand PrepareStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters)
		{
			SqlCommand sqlCommand = this.PrepareStoredProcedure(storedProcedureName);
			foreach (KeyValuePair<string, object> keyValuePair in parameters)
			{
				sqlCommand.Parameters.Add(new SqlParameter(keyValuePair.Key, keyValuePair.Value));
			}
			return sqlCommand;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004818 File Offset: 0x00002A18
		public SqlCommand PrepareStoredProcedure(string storedProcedureName)
		{
			return new SqlCommand(storedProcedureName, this._connection)
			{
				CommandType = CommandType.StoredProcedure
			};
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000482D File Offset: 0x00002A2D
		public string GetStringOrNullColumn(SqlDataReader results, int index)
		{
			if (!results.IsDBNull(index))
			{
				return results.GetString(index);
			}
			return null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004844 File Offset: 0x00002A44
		public byte[] GetBinaryOrNullColumn(SqlDataReader results, int index)
		{
			SqlBinary sqlBinary = results.GetSqlBinary(index);
			if (!sqlBinary.IsNull)
			{
				return sqlBinary.Value;
			}
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000486B File Offset: 0x00002A6B
		public void Dispose()
		{
			this._connection.Close();
			this._connection.Dispose();
			this._timeMeter.Dispose();
			this.StopStatementMeter();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004894 File Offset: 0x00002A94
		private void StartStatementMeter(string storedProcedure)
		{
			this.StopStatementMeter();
			this._statementMeter = ScopeMeter.Use(new string[] { "SQL", "segment", storedProcedure });
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000048C1 File Offset: 0x00002AC1
		private void StopStatementMeter()
		{
			if (this._statementMeter != null)
			{
				this._statementMeter.Dispose();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000048D6 File Offset: 0x00002AD6
		public string DatabaseName
		{
			get
			{
				return this._connection.Database;
			}
		}

		// Token: 0x0400007E RID: 126
		private static readonly MeterCollector.MeterFactory SqlMeter = MeterCollector.Global.Factory("SQL");

		// Token: 0x0400007F RID: 127
		private static readonly int DatabaseQueryTimeout = StaticConfig.Current.GetIntOrDefault("DatabaseQueryTimeout", 120);

		// Token: 0x04000080 RID: 128
		private const int LoginFailedErrorNumber = 4060;

		// Token: 0x04000081 RID: 129
		private readonly SqlConnection _connection;

		// Token: 0x04000082 RID: 130
		private readonly TimeMeter _timeMeter;

		// Token: 0x04000083 RID: 131
		private IDisposable _statementMeter;

		// Token: 0x04000084 RID: 132
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes((double)StaticConfig.Current.GetIntOrDefault("SqlBatchTimeoutMinutes", 30));
	}
}
