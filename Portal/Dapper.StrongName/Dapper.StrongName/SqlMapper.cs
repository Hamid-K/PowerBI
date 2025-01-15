using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;

namespace Dapper
{
	// Token: 0x0200000F RID: 15
	public static class SqlMapper
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003A8C File Offset: 0x00001C8C
		[return: Dynamic(new bool[] { false, false, true })]
		public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryAsync(typeof(SqlMapper.DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003ABF File Offset: 0x00001CBF
		[return: Dynamic(new bool[] { false, false, true })]
		public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryAsync(typeof(SqlMapper.DapperRow), command);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003AD2 File Offset: 0x00001CD2
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.First, typeof(SqlMapper.DapperRow), command);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003AE6 File Offset: 0x00001CE6
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.FirstOrDefault, typeof(SqlMapper.DapperRow), command);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003AFA File Offset: 0x00001CFA
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.Single, typeof(SqlMapper.DapperRow), command);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003B0E File Offset: 0x00001D0E
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.SingleOrDefault, typeof(SqlMapper.DapperRow), command);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003B24 File Offset: 0x00001D24
		public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryAsync(typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003B58 File Offset: 0x00001D58
		public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.First, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B8C File Offset: 0x00001D8C
		public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.FirstOrDefault, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.Single, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.SingleOrDefault, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003C28 File Offset: 0x00001E28
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.First, typeof(SqlMapper.DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003C5C File Offset: 0x00001E5C
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.FirstOrDefault, typeof(SqlMapper.DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003C90 File Offset: 0x00001E90
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.Single, typeof(SqlMapper.DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003CC4 File Offset: 0x00001EC4
		[return: Dynamic(new bool[] { false, true })]
		public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.SingleOrDefault, typeof(SqlMapper.DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return cnn.QueryAsync(type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D38 File Offset: 0x00001F38
		public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return cnn.QueryRowAsync(SqlMapper.Row.First, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D78 File Offset: 0x00001F78
		public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return cnn.QueryRowAsync(SqlMapper.Row.FirstOrDefault, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return cnn.QueryRowAsync(SqlMapper.Row.Single, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return cnn.QueryRowAsync(SqlMapper.Row.SingleOrDefault, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003E38 File Offset: 0x00002038
		public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryAsync(typeof(T), command);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E4B File Offset: 0x0000204B
		public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, CommandDefinition command)
		{
			return cnn.QueryAsync(type, command);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E55 File Offset: 0x00002055
		public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.First, type, command);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E60 File Offset: 0x00002060
		public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.First, typeof(T), command);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E74 File Offset: 0x00002074
		public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.FirstOrDefault, type, command);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E7F File Offset: 0x0000207F
		public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.FirstOrDefault, typeof(T), command);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E93 File Offset: 0x00002093
		public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.Single, type, command);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003E9E File Offset: 0x0000209E
		public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.Single, typeof(T), command);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003EB2 File Offset: 0x000020B2
		public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.SingleOrDefault, type, command);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003EBD File Offset: 0x000020BD
		public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryRowAsync(SqlMapper.Row.SingleOrDefault, typeof(T), command);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003ED4 File Offset: 0x000020D4
		private static Task<DbDataReader> ExecuteReaderWithFlagsFallbackAsync(DbCommand cmd, bool wasClosed, CommandBehavior behavior, CancellationToken cancellationToken)
		{
			Task<DbDataReader> task = cmd.ExecuteReaderAsync(SqlMapper.GetBehavior(wasClosed, behavior), cancellationToken);
			if (task.Status == TaskStatus.Faulted && SqlMapper.Settings.DisableCommandBehaviorOptimizations(behavior, task.Exception.InnerException))
			{
				return cmd.ExecuteReaderAsync(SqlMapper.GetBehavior(wasClosed, behavior), cancellationToken);
			}
			return task;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003F1C File Offset: 0x0000211C
		private static Task TryOpenAsync(this IDbConnection cnn, CancellationToken cancel)
		{
			DbConnection dbConn;
			if ((dbConn = cnn as DbConnection) != null)
			{
				return dbConn.OpenAsync(cancel);
			}
			throw new InvalidOperationException("Async operations require use of a DbConnection or an already-open IDbConnection");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003F48 File Offset: 0x00002148
		private static DbCommand TrySetupAsyncCommand(this CommandDefinition command, IDbConnection cnn, Action<IDbCommand, object> paramReader)
		{
			DbCommand dbCommand;
			if ((dbCommand = command.SetupCommand(cnn, paramReader) as DbCommand) != null)
			{
				return dbCommand;
			}
			throw new InvalidOperationException("Async operations require use of a DbConnection or an IDbConnection where .CreateCommand() returns a DbCommand");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F74 File Offset: 0x00002174
		private static async Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, Type effectiveType, CommandDefinition command)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, effectiveType, (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			bool wasClosed = cnn.State == ConnectionState.Closed;
			CancellationToken cancel = command.CancellationToken;
			IEnumerable<T> enumerable;
			using (DbCommand cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
			{
				DbDataReader reader = null;
				try
				{
					if (wasClosed)
					{
						await cnn.TryOpenAsync(cancel).ConfigureAwait(false);
					}
					reader = await SqlMapper.ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SingleResult | CommandBehavior.SequentialAccess, cancel).ConfigureAwait(false);
					SqlMapper.DeserializerState tuple = info.Deserializer;
					int hash = SqlMapper.GetColumnHash(reader, 0, -1);
					if (tuple.Func == null || tuple.Hash != hash)
					{
						if (reader.FieldCount == 0)
						{
							return Enumerable.Empty<T>();
						}
						SqlMapper.CacheInfo cacheInfo = info;
						SqlMapper.DeserializerState deserializerState = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(effectiveType, reader, 0, -1, false));
						cacheInfo.Deserializer = deserializerState;
						tuple = deserializerState;
						if (command.AddToCache)
						{
							SqlMapper.SetQueryCache(identity, info);
						}
					}
					Func<IDataReader, object> func = tuple.Func;
					if (command.Buffered)
					{
						List<T> buffer = new List<T>();
						Type convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						for (;;)
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancel).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
								int num = -1;
							}
							if (!configuredTaskAwaiter.GetResult())
							{
								break;
							}
							object val = func(reader);
							if (val == null || val is T)
							{
								buffer.Add((T)((object)val));
							}
							else
							{
								buffer.Add((T)((object)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture)));
							}
						}
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
						do
						{
							configuredTaskAwaiter3 = reader.NextResultAsync(cancel).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								await configuredTaskAwaiter3;
								configuredTaskAwaiter3 = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
								int num = -1;
							}
						}
						while (configuredTaskAwaiter3.GetResult());
						command.OnCompleted();
						enumerable = buffer;
					}
					else
					{
						wasClosed = false;
						IEnumerable<T> deferred = SqlMapper.ExecuteReaderSync<T>(reader, func, command.Parameters);
						reader = null;
						enumerable = deferred;
					}
				}
				finally
				{
					DbDataReader dbDataReader = reader;
					try
					{
					}
					finally
					{
						int num;
						if (num < 0 && dbDataReader != null)
						{
							((IDisposable)dbDataReader).Dispose();
						}
					}
					if (wasClosed)
					{
						cnn.Close();
					}
				}
			}
			return enumerable;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FCC File Offset: 0x000021CC
		private static async Task<T> QueryRowAsync<T>(this IDbConnection cnn, SqlMapper.Row row, Type effectiveType, CommandDefinition command)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, effectiveType, (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			bool wasClosed = cnn.State == ConnectionState.Closed;
			CancellationToken cancel = command.CancellationToken;
			T t;
			using (DbCommand cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
			{
				DbDataReader reader = null;
				try
				{
					if (wasClosed)
					{
						await cnn.TryOpenAsync(cancel).ConfigureAwait(false);
					}
					reader = await SqlMapper.ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, ((row & SqlMapper.Row.Single) != SqlMapper.Row.First) ? (CommandBehavior.SingleResult | CommandBehavior.SequentialAccess) : (CommandBehavior.SingleResult | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess), cancel).ConfigureAwait(false);
					T result = default(T);
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					if (await reader.ReadAsync(cancel).ConfigureAwait(false) && reader.FieldCount != 0)
					{
						SqlMapper.DeserializerState tuple = info.Deserializer;
						int hash = SqlMapper.GetColumnHash(reader, 0, -1);
						if (tuple.Func == null || tuple.Hash != hash)
						{
							SqlMapper.CacheInfo cacheInfo = info;
							SqlMapper.DeserializerState deserializerState = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(effectiveType, reader, 0, -1, false));
							cacheInfo.Deserializer = deserializerState;
							tuple = deserializerState;
							if (command.AddToCache)
							{
								SqlMapper.SetQueryCache(identity, info);
							}
						}
						object val = tuple.Func(reader);
						if (val == null || val is T)
						{
							result = (T)((object)val);
						}
						else
						{
							result = (T)((object)Convert.ChangeType(val, Nullable.GetUnderlyingType(effectiveType) ?? effectiveType, CultureInfo.InvariantCulture));
						}
						bool flag = (row & SqlMapper.Row.Single) > SqlMapper.Row.First;
						if (flag)
						{
							flag = await reader.ReadAsync(cancel).ConfigureAwait(false);
						}
						if (flag)
						{
							SqlMapper.ThrowMultipleRows(row);
						}
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
						do
						{
							configuredTaskAwaiter = reader.ReadAsync(cancel).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
								int num = -1;
							}
						}
						while (configuredTaskAwaiter.GetResult());
					}
					else if ((row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
					{
						SqlMapper.ThrowZeroRows(row);
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					do
					{
						configuredTaskAwaiter3 = reader.NextResultAsync(cancel).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							configuredTaskAwaiter3 = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							int num = -1;
						}
					}
					while (configuredTaskAwaiter3.GetResult());
					t = result;
				}
				finally
				{
					DbDataReader dbDataReader = reader;
					try
					{
					}
					finally
					{
						int num;
						if (num < 0 && dbDataReader != null)
						{
							((IDisposable)dbDataReader).Dispose();
						}
					}
					if (wasClosed)
					{
						cnn.Close();
					}
				}
			}
			return t;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000402C File Offset: 0x0000222C
		public static Task<int> ExecuteAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.ExecuteAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004058 File Offset: 0x00002258
		public static Task<int> ExecuteAsync(this IDbConnection cnn, CommandDefinition command)
		{
			object param = command.Parameters;
			IEnumerable multiExec = SqlMapper.GetMultiExec(param);
			if (multiExec != null)
			{
				return SqlMapper.ExecuteMultiImplAsync(cnn, command, multiExec);
			}
			return SqlMapper.ExecuteImplAsync(cnn, command, param);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004088 File Offset: 0x00002288
		private static async Task<int> ExecuteMultiImplAsync(IDbConnection cnn, CommandDefinition command, IEnumerable multiExec)
		{
			bool isFirst = true;
			int total = 0;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			try
			{
				if (wasClosed)
				{
					await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
				}
				SqlMapper.CacheInfo info = null;
				string masterSql = null;
				if ((command.Flags & CommandFlags.Pipelined) != CommandFlags.None)
				{
					Queue<SqlMapper.AsyncExecState> pending = new Queue<SqlMapper.AsyncExecState>(100);
					DbCommand cmd = null;
					try
					{
						foreach (object obj in multiExec)
						{
							if (isFirst)
							{
								isFirst = false;
								cmd = command.TrySetupAsyncCommand(cnn, null);
								masterSql = cmd.CommandText;
								info = SqlMapper.GetCacheInfo(new SqlMapper.Identity(command.CommandText, new CommandType?(cmd.CommandType), cnn, null, obj.GetType(), null), obj, command.AddToCache);
							}
							else if (pending.Count >= 100)
							{
								SqlMapper.AsyncExecState recycled = pending.Dequeue();
								int num = total;
								total = num + await recycled.Task.ConfigureAwait(false);
								cmd = recycled.Command;
								cmd.CommandText = masterSql;
								cmd.Parameters.Clear();
								recycled = default(SqlMapper.AsyncExecState);
							}
							else
							{
								cmd = command.TrySetupAsyncCommand(cnn, null);
							}
							info.ParamReader(cmd, obj);
							pending.Enqueue(new SqlMapper.AsyncExecState(cmd, cmd.ExecuteNonQueryAsync(command.CancellationToken)));
							cmd = null;
							obj = null;
						}
						IEnumerator enumerator = null;
						while (pending.Count != 0)
						{
							SqlMapper.AsyncExecState pair = pending.Dequeue();
							using (pair.Command)
							{
							}
							int num = total;
							total = num + await pair.Task.ConfigureAwait(false);
						}
					}
					finally
					{
						DbCommand dbCommand = cmd;
						int num2;
						try
						{
							goto IL_045E;
						}
						finally
						{
							if (num2 < 0 && dbCommand != null)
							{
								((IDisposable)dbCommand).Dispose();
							}
						}
						IL_043A:
						DbCommand command3 = pending.Dequeue().Command;
						try
						{
						}
						finally
						{
							if (num2 < 0 && command3 != null)
							{
								((IDisposable)command3).Dispose();
							}
						}
						IL_045E:
						if (pending.Count != 0)
						{
							goto IL_043A;
						}
					}
					pending = null;
					cmd = null;
				}
				else
				{
					using (DbCommand cmd = command.TrySetupAsyncCommand(cnn, null))
					{
						foreach (object obj2 in multiExec)
						{
							if (isFirst)
							{
								masterSql = cmd.CommandText;
								isFirst = false;
								info = SqlMapper.GetCacheInfo(new SqlMapper.Identity(command.CommandText, new CommandType?(cmd.CommandType), cnn, null, obj2.GetType(), null), obj2, command.AddToCache);
							}
							else
							{
								cmd.CommandText = masterSql;
								cmd.Parameters.Clear();
							}
							info.ParamReader(cmd, obj2);
							int num = total;
							total = num + await cmd.ExecuteNonQueryAsync(command.CancellationToken).ConfigureAwait(false);
						}
						IEnumerator enumerator = null;
					}
					DbCommand cmd = null;
				}
				command.OnCompleted();
				info = null;
				masterSql = null;
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
			}
			return total;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000040E0 File Offset: 0x000022E0
		private static async Task<int> ExecuteImplAsync(IDbConnection cnn, CommandDefinition command, object param)
		{
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, null, (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			bool wasClosed = cnn.State == ConnectionState.Closed;
			int num;
			using (DbCommand cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
			{
				try
				{
					if (wasClosed)
					{
						await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
					}
					int result = await cmd.ExecuteNonQueryAsync(command.CancellationToken).ConfigureAwait(false);
					command.OnCompleted();
					num = result;
				}
				finally
				{
					if (wasClosed)
					{
						cnn.Close();
					}
				}
			}
			return num;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004138 File Offset: 0x00002338
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMapAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000416C File Offset: 0x0000236C
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
		{
			return cnn.MultiMapAsync(command, map, splitOn);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004178 File Offset: 0x00002378
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMapAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000041AC File Offset: 0x000023AC
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id")
		{
			return cnn.MultiMapAsync(command, map, splitOn);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000041B8 File Offset: 0x000023B8
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMapAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000041EC File Offset: 0x000023EC
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id")
		{
			return cnn.MultiMapAsync(command, map, splitOn);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000041F8 File Offset: 0x000023F8
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMapAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000422C File Offset: 0x0000242C
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id")
		{
			return cnn.MultiMapAsync(command, map, splitOn);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004238 File Offset: 0x00002438
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMapAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000426C File Offset: 0x0000246C
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "Id")
		{
			return cnn.MultiMapAsync(command, map, splitOn);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004278 File Offset: 0x00002478
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMapAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000042AC File Offset: 0x000024AC
		public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string splitOn = "Id")
		{
			return cnn.MultiMapAsync(command, map, splitOn);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000042B8 File Offset: 0x000024B8
		private static async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Delegate map, string splitOn)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, typeof(TFirst), (param != null) ? param.GetType() : null, new Type[]
			{
				typeof(TFirst),
				typeof(TSecond),
				typeof(TThird),
				typeof(TFourth),
				typeof(TFifth),
				typeof(TSixth),
				typeof(TSeventh)
			});
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			bool wasClosed = cnn.State == ConnectionState.Closed;
			IEnumerable<TReturn> enumerable;
			try
			{
				if (wasClosed)
				{
					await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
				}
				using (DbCommand cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
				{
					using (DbDataReader reader = await SqlMapper.ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SingleResult | CommandBehavior.SequentialAccess, command.CancellationToken).ConfigureAwait(false))
					{
						if (!command.Buffered)
						{
							wasClosed = false;
						}
						IEnumerable<TReturn> results = SqlMapper.MultiMapImpl((IDbConnection)null, CommandDefinition.ForCallback(command.Parameters), map, splitOn, reader, identity, true);
						enumerable = (command.Buffered ? results.ToList<TReturn>() : results);
					}
				}
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
			}
			return enumerable;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004318 File Offset: 0x00002518
		public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken));
			return cnn.MultiMapAsync(command, types, map, splitOn);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004354 File Offset: 0x00002554
		private static async Task<IEnumerable<TReturn>> MultiMapAsync<TReturn>(this IDbConnection cnn, CommandDefinition command, Type[] types, Func<object[], TReturn> map, string splitOn)
		{
			if (types.Length < 1)
			{
				throw new ArgumentException("you must provide at least one type to deserialize");
			}
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, types[0], (param != null) ? param.GetType() : null, types);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			bool wasClosed = cnn.State == ConnectionState.Closed;
			IEnumerable<TReturn> enumerable;
			try
			{
				if (wasClosed)
				{
					await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
				}
				using (DbCommand cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
				{
					using (DbDataReader reader = await SqlMapper.ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SingleResult | CommandBehavior.SequentialAccess, command.CancellationToken).ConfigureAwait(false))
					{
						IEnumerable<TReturn> results = SqlMapper.MultiMapImpl((IDbConnection)null, default(CommandDefinition), types, map, splitOn, reader, identity, true);
						enumerable = (command.Buffered ? results.ToList<TReturn>() : results);
					}
				}
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
			}
			return enumerable;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000043BA File Offset: 0x000025BA
		private static IEnumerable<T> ExecuteReaderSync<T>(IDataReader reader, Func<IDataReader, object> func, object parameters)
		{
			using (reader)
			{
				while (reader.Read())
				{
					yield return (T)((object)func(reader));
				}
				while (reader.NextResult())
				{
				}
				SqlMapper.IParameterCallbacks parameterCallbacks = parameters as SqlMapper.IParameterCallbacks;
				if (parameterCallbacks != null)
				{
					parameterCallbacks.OnCompleted();
				}
			}
			IDataReader dataReader = null;
			yield break;
			yield break;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000043D8 File Offset: 0x000025D8
		public static Task<SqlMapper.GridReader> QueryMultipleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryMultipleAsync(new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004404 File Offset: 0x00002604
		public static async Task<SqlMapper.GridReader> QueryMultipleAsync(this IDbConnection cnn, CommandDefinition command)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, typeof(SqlMapper.GridReader), (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			DbCommand cmd = null;
			IDataReader reader = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			SqlMapper.GridReader gridReader;
			try
			{
				if (wasClosed)
				{
					await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
				}
				cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader);
				reader = await SqlMapper.ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SequentialAccess, command.CancellationToken).ConfigureAwait(false);
				SqlMapper.GridReader result = new SqlMapper.GridReader(cmd, reader, identity, command.Parameters as DynamicParameters, command.AddToCache, command.CancellationToken);
				wasClosed = false;
				gridReader = result;
			}
			catch
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
					{
						try
						{
							cmd.Cancel();
						}
						catch
						{
						}
					}
					reader.Dispose();
				}
				DbCommand dbCommand = cmd;
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				if (wasClosed)
				{
					cnn.Close();
				}
				throw;
			}
			return gridReader;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004454 File Offset: 0x00002654
		public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return SqlMapper.ExecuteReaderImplAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)), CommandBehavior.Default);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000447E File Offset: 0x0000267E
		public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.ExecuteReaderImplAsync(cnn, command, CommandBehavior.Default);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004488 File Offset: 0x00002688
		public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior)
		{
			return SqlMapper.ExecuteReaderImplAsync(cnn, command, commandBehavior);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004494 File Offset: 0x00002694
		private static async Task<IDataReader> ExecuteReaderImplAsync(IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior)
		{
			Action<IDbCommand, object> paramReader = SqlMapper.GetParameterReader(cnn, ref command);
			DbCommand cmd = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			IDataReader dataReader;
			try
			{
				cmd = command.TrySetupAsyncCommand(cnn, paramReader);
				if (wasClosed)
				{
					await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
				}
				DbDataReader reader = await SqlMapper.ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, commandBehavior, command.CancellationToken).ConfigureAwait(false);
				wasClosed = false;
				dataReader = reader;
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
				DbCommand dbCommand = cmd;
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
			return dataReader;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000044EC File Offset: 0x000026EC
		public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return SqlMapper.ExecuteScalarImplAsync<object>(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004518 File Offset: 0x00002718
		public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return SqlMapper.ExecuteScalarImplAsync<T>(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004541 File Offset: 0x00002741
		public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.ExecuteScalarImplAsync<object>(cnn, command);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000454A File Offset: 0x0000274A
		public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.ExecuteScalarImplAsync<T>(cnn, command);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004554 File Offset: 0x00002754
		private static async Task<T> ExecuteScalarImplAsync<T>(IDbConnection cnn, CommandDefinition command)
		{
			Action<IDbCommand, object> paramReader = null;
			object param = command.Parameters;
			if (param != null)
			{
				SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, null, param.GetType(), null);
				paramReader = SqlMapper.GetCacheInfo(identity, command.Parameters, command.AddToCache).ParamReader;
			}
			DbCommand cmd = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			object result;
			try
			{
				cmd = command.TrySetupAsyncCommand(cnn, paramReader);
				if (wasClosed)
				{
					await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
				}
				result = await cmd.ExecuteScalarAsync(command.CancellationToken).ConfigureAwait(false);
				command.OnCompleted();
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
				DbCommand dbCommand = cmd;
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
			return SqlMapper.Parse<T>(result);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000045A4 File Offset: 0x000027A4
		private static int GetColumnHash(IDataReader reader, int startBound = 0, int length = -1)
		{
			int max = ((length < 0) ? reader.FieldCount : (startBound + length));
			int hash = -37 * startBound + max;
			for (int i = startBound; i < max; i++)
			{
				object tmp = reader.GetName(i);
				int num = -79 * (hash * 31 + ((tmp != null) ? tmp.GetHashCode() : 0));
				Type fieldType = reader.GetFieldType(i);
				hash = num + ((fieldType != null) ? fieldType.GetHashCode() : 0);
			}
			return hash;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000097 RID: 151 RVA: 0x00004608 File Offset: 0x00002808
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x0000463C File Offset: 0x0000283C
		public static event EventHandler QueryCachePurged;

		// Token: 0x06000099 RID: 153 RVA: 0x00004670 File Offset: 0x00002870
		private static void OnQueryCachePurged()
		{
			EventHandler handler = SqlMapper.QueryCachePurged;
			if (handler != null)
			{
				handler(null, EventArgs.Empty);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004692 File Offset: 0x00002892
		private static void SetQueryCache(SqlMapper.Identity key, SqlMapper.CacheInfo value)
		{
			if (Interlocked.Increment(ref SqlMapper.collect) == 1000)
			{
				SqlMapper.CollectCacheGarbage();
			}
			SqlMapper._queryCache[key] = value;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000046B8 File Offset: 0x000028B8
		private static void CollectCacheGarbage()
		{
			try
			{
				foreach (KeyValuePair<SqlMapper.Identity, SqlMapper.CacheInfo> pair in SqlMapper._queryCache)
				{
					if (pair.Value.GetHitCount() <= 0)
					{
						SqlMapper.CacheInfo cache;
						SqlMapper._queryCache.TryRemove(pair.Key, out cache);
					}
				}
			}
			finally
			{
				Interlocked.Exchange(ref SqlMapper.collect, 0);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000473C File Offset: 0x0000293C
		private static bool TryGetQueryCache(SqlMapper.Identity key, out SqlMapper.CacheInfo value)
		{
			if (SqlMapper._queryCache.TryGetValue(key, out value))
			{
				value.RecordHit();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004759 File Offset: 0x00002959
		public static void PurgeQueryCache()
		{
			SqlMapper._queryCache.Clear();
			SqlMapper.TypeDeserializerCache.Purge();
			SqlMapper.OnQueryCachePurged();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004770 File Offset: 0x00002970
		private static void PurgeQueryCacheByType(Type type)
		{
			foreach (KeyValuePair<SqlMapper.Identity, SqlMapper.CacheInfo> entry in SqlMapper._queryCache)
			{
				if (entry.Key.type == type)
				{
					SqlMapper.CacheInfo cache;
					SqlMapper._queryCache.TryRemove(entry.Key, out cache);
				}
			}
			SqlMapper.TypeDeserializerCache.Purge(type);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000047E4 File Offset: 0x000029E4
		public static int GetCachedSQLCount()
		{
			return SqlMapper._queryCache.Count;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000047F0 File Offset: 0x000029F0
		public static IEnumerable<Tuple<string, string, int>> GetCachedSQL(int ignoreHitCountAbove = 2147483647)
		{
			IEnumerable<Tuple<string, string, int>> data = SqlMapper._queryCache.Select((KeyValuePair<SqlMapper.Identity, SqlMapper.CacheInfo> pair) => Tuple.Create<string, string, int>(pair.Key.connectionString, pair.Key.sql, pair.Value.GetHitCount()));
			if (ignoreHitCountAbove >= 2147483647)
			{
				return data;
			}
			return data.Where((Tuple<string, string, int> tuple) => tuple.Item3 <= ignoreHitCountAbove);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004858 File Offset: 0x00002A58
		public static IEnumerable<Tuple<int, int>> GetHashCollissions()
		{
			Dictionary<int, int> counts = new Dictionary<int, int>();
			foreach (SqlMapper.Identity key in SqlMapper._queryCache.Keys)
			{
				int count;
				if (!counts.TryGetValue(key.hashCode, out count))
				{
					counts.Add(key.hashCode, 1);
				}
				else
				{
					counts[key.hashCode] = count + 1;
				}
			}
			return counts.Where(delegate(KeyValuePair<int, int> pair)
			{
				KeyValuePair<int, int> keyValuePair = pair;
				return keyValuePair.Value > 1;
			}).Select(delegate(KeyValuePair<int, int> pair)
			{
				KeyValuePair<int, int> keyValuePair2 = pair;
				int key2 = keyValuePair2.Key;
				keyValuePair2 = pair;
				return Tuple.Create<int, int>(key2, keyValuePair2.Value);
			});
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004920 File Offset: 0x00002B20
		static SqlMapper()
		{
			Dictionary<Type, DbType> dictionary = new Dictionary<Type, DbType>();
			Type typeFromHandle = typeof(byte);
			dictionary[typeFromHandle] = DbType.Byte;
			Type typeFromHandle2 = typeof(sbyte);
			dictionary[typeFromHandle2] = DbType.SByte;
			Type typeFromHandle3 = typeof(short);
			dictionary[typeFromHandle3] = DbType.Int16;
			Type typeFromHandle4 = typeof(ushort);
			dictionary[typeFromHandle4] = DbType.UInt16;
			Type typeFromHandle5 = typeof(int);
			dictionary[typeFromHandle5] = DbType.Int32;
			Type typeFromHandle6 = typeof(uint);
			dictionary[typeFromHandle6] = DbType.UInt32;
			Type typeFromHandle7 = typeof(long);
			dictionary[typeFromHandle7] = DbType.Int64;
			Type typeFromHandle8 = typeof(ulong);
			dictionary[typeFromHandle8] = DbType.UInt64;
			Type typeFromHandle9 = typeof(float);
			dictionary[typeFromHandle9] = DbType.Single;
			Type typeFromHandle10 = typeof(double);
			dictionary[typeFromHandle10] = DbType.Double;
			Type typeFromHandle11 = typeof(decimal);
			dictionary[typeFromHandle11] = DbType.Decimal;
			Type typeFromHandle12 = typeof(bool);
			dictionary[typeFromHandle12] = DbType.Boolean;
			Type typeFromHandle13 = typeof(string);
			dictionary[typeFromHandle13] = DbType.String;
			Type typeFromHandle14 = typeof(char);
			dictionary[typeFromHandle14] = DbType.StringFixedLength;
			Type typeFromHandle15 = typeof(Guid);
			dictionary[typeFromHandle15] = DbType.Guid;
			Type typeFromHandle16 = typeof(DateTime);
			dictionary[typeFromHandle16] = DbType.DateTime;
			Type typeFromHandle17 = typeof(DateTimeOffset);
			dictionary[typeFromHandle17] = DbType.DateTimeOffset;
			Type typeFromHandle18 = typeof(TimeSpan);
			dictionary[typeFromHandle18] = DbType.Time;
			Type typeFromHandle19 = typeof(byte[]);
			dictionary[typeFromHandle19] = DbType.Binary;
			Type typeFromHandle20 = typeof(byte?);
			dictionary[typeFromHandle20] = DbType.Byte;
			Type typeFromHandle21 = typeof(sbyte?);
			dictionary[typeFromHandle21] = DbType.SByte;
			Type typeFromHandle22 = typeof(short?);
			dictionary[typeFromHandle22] = DbType.Int16;
			Type typeFromHandle23 = typeof(ushort?);
			dictionary[typeFromHandle23] = DbType.UInt16;
			Type typeFromHandle24 = typeof(int?);
			dictionary[typeFromHandle24] = DbType.Int32;
			Type typeFromHandle25 = typeof(uint?);
			dictionary[typeFromHandle25] = DbType.UInt32;
			Type typeFromHandle26 = typeof(long?);
			dictionary[typeFromHandle26] = DbType.Int64;
			Type typeFromHandle27 = typeof(ulong?);
			dictionary[typeFromHandle27] = DbType.UInt64;
			Type typeFromHandle28 = typeof(float?);
			dictionary[typeFromHandle28] = DbType.Single;
			Type typeFromHandle29 = typeof(double?);
			dictionary[typeFromHandle29] = DbType.Double;
			Type typeFromHandle30 = typeof(decimal?);
			dictionary[typeFromHandle30] = DbType.Decimal;
			Type typeFromHandle31 = typeof(bool?);
			dictionary[typeFromHandle31] = DbType.Boolean;
			Type typeFromHandle32 = typeof(char?);
			dictionary[typeFromHandle32] = DbType.StringFixedLength;
			Type typeFromHandle33 = typeof(Guid?);
			dictionary[typeFromHandle33] = DbType.Guid;
			Type typeFromHandle34 = typeof(DateTime?);
			dictionary[typeFromHandle34] = DbType.DateTime;
			Type typeFromHandle35 = typeof(DateTimeOffset?);
			dictionary[typeFromHandle35] = DbType.DateTimeOffset;
			Type typeFromHandle36 = typeof(TimeSpan?);
			dictionary[typeFromHandle36] = DbType.Time;
			Type typeFromHandle37 = typeof(object);
			dictionary[typeFromHandle37] = DbType.Object;
			SqlMapper.typeMap = dictionary;
			SqlMapper.ResetTypeHandlers(false);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004EB0 File Offset: 0x000030B0
		public static void ResetTypeHandlers()
		{
			SqlMapper.ResetTypeHandlers(true);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004EB8 File Offset: 0x000030B8
		private static void ResetTypeHandlers(bool clone)
		{
			SqlMapper.typeHandlers = new Dictionary<Type, SqlMapper.ITypeHandler>();
			SqlMapper.AddTypeHandlerImpl(typeof(DataTable), new DataTableHandler(), clone);
			try
			{
				SqlMapper.AddSqlDataRecordsTypeHandler(clone);
			}
			catch
			{
			}
			SqlMapper.AddTypeHandlerImpl(typeof(XmlDocument), new XmlDocumentHandler(), clone);
			SqlMapper.AddTypeHandlerImpl(typeof(XDocument), new XDocumentHandler(), clone);
			SqlMapper.AddTypeHandlerImpl(typeof(XElement), new XElementHandler(), clone);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004F40 File Offset: 0x00003140
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void AddSqlDataRecordsTypeHandler(bool clone)
		{
			SqlMapper.AddTypeHandlerImpl(typeof(IEnumerable<SqlDataRecord>), new SqlDataRecordHandler(), clone);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004F58 File Offset: 0x00003158
		public static void AddTypeMap(Type type, DbType dbType)
		{
			Dictionary<Type, DbType> snapshot = SqlMapper.typeMap;
			DbType oldValue;
			if (snapshot.TryGetValue(type, out oldValue) && oldValue == dbType)
			{
				return;
			}
			Dictionary<Type, DbType> dictionary = new Dictionary<Type, DbType>(snapshot);
			dictionary[type] = dbType;
			SqlMapper.typeMap = dictionary;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004F90 File Offset: 0x00003190
		public static void RemoveTypeMap(Type type)
		{
			Dictionary<Type, DbType> snapshot = SqlMapper.typeMap;
			if (!snapshot.ContainsKey(type))
			{
				return;
			}
			Dictionary<Type, DbType> newCopy = new Dictionary<Type, DbType>(snapshot);
			newCopy.Remove(type);
			SqlMapper.typeMap = newCopy;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004FC2 File Offset: 0x000031C2
		public static void AddTypeHandler(Type type, SqlMapper.ITypeHandler handler)
		{
			SqlMapper.AddTypeHandlerImpl(type, handler, true);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004FCC File Offset: 0x000031CC
		internal static bool HasTypeHandler(Type type)
		{
			return SqlMapper.typeHandlers.ContainsKey(type);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004FDC File Offset: 0x000031DC
		public static void AddTypeHandlerImpl(Type type, SqlMapper.ITypeHandler handler, bool clone)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type secondary = null;
			if (type.IsValueType())
			{
				Type underlying = Nullable.GetUnderlyingType(type);
				if (underlying == null)
				{
					secondary = typeof(Nullable<>).MakeGenericType(new Type[] { type });
				}
				else
				{
					secondary = type;
					type = underlying;
				}
			}
			Dictionary<Type, SqlMapper.ITypeHandler> snapshot = SqlMapper.typeHandlers;
			SqlMapper.ITypeHandler oldValue;
			if (snapshot.TryGetValue(type, out oldValue) && handler == oldValue)
			{
				return;
			}
			Dictionary<Type, SqlMapper.ITypeHandler> newCopy = (clone ? new Dictionary<Type, SqlMapper.ITypeHandler>(snapshot) : snapshot);
			typeof(SqlMapper.TypeHandlerCache<>).MakeGenericType(new Type[] { type }).GetMethod("SetHandler", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { handler });
			if (secondary != null)
			{
				typeof(SqlMapper.TypeHandlerCache<>).MakeGenericType(new Type[] { secondary }).GetMethod("SetHandler", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { handler });
			}
			if (handler == null)
			{
				newCopy.Remove(type);
				if (secondary != null)
				{
					newCopy.Remove(secondary);
				}
			}
			else
			{
				newCopy[type] = handler;
				if (secondary != null)
				{
					newCopy[secondary] = handler;
				}
			}
			SqlMapper.typeHandlers = newCopy;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005110 File Offset: 0x00003310
		public static void AddTypeHandler<T>(SqlMapper.TypeHandler<T> handler)
		{
			SqlMapper.AddTypeHandlerImpl(typeof(T), handler, true);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005124 File Offset: 0x00003324
		[Obsolete("This method is for internal use only", false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static DbType GetDbType(object value)
		{
			if (value == null || value is DBNull)
			{
				return DbType.Object;
			}
			SqlMapper.ITypeHandler handler;
			return SqlMapper.LookupDbType(value.GetType(), "n/a", false, out handler);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005154 File Offset: 0x00003354
		[Obsolete("This method is for internal use only", false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static DbType LookupDbType(Type type, string name, bool demand, out SqlMapper.ITypeHandler handler)
		{
			handler = null;
			Type nullUnderlyingType = Nullable.GetUnderlyingType(type);
			if (nullUnderlyingType != null)
			{
				type = nullUnderlyingType;
			}
			if (type.IsEnum() && !SqlMapper.typeMap.ContainsKey(type))
			{
				type = Enum.GetUnderlyingType(type);
			}
			DbType dbType;
			if (SqlMapper.typeMap.TryGetValue(type, out dbType))
			{
				return dbType;
			}
			if (type.FullName == "System.Data.Linq.Binary")
			{
				return DbType.Binary;
			}
			if (SqlMapper.typeHandlers.TryGetValue(type, out handler))
			{
				return DbType.Object;
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return (DbType)(-1);
			}
			string fullName = type.FullName;
			if (fullName == "Microsoft.SqlServer.Types.SqlGeography")
			{
				Type type2 = type;
				SqlMapper.ITypeHandler typeHandler;
				handler = (typeHandler = new SqlMapper.UdtTypeHandler("geography"));
				SqlMapper.AddTypeHandler(type2, typeHandler);
				return DbType.Object;
			}
			if (fullName == "Microsoft.SqlServer.Types.SqlGeometry")
			{
				Type type3 = type;
				SqlMapper.ITypeHandler typeHandler;
				handler = (typeHandler = new SqlMapper.UdtTypeHandler("geometry"));
				SqlMapper.AddTypeHandler(type3, typeHandler);
				return DbType.Object;
			}
			if (fullName == "Microsoft.SqlServer.Types.SqlHierarchyId")
			{
				Type type4 = type;
				SqlMapper.ITypeHandler typeHandler;
				handler = (typeHandler = new SqlMapper.UdtTypeHandler("hierarchyid"));
				SqlMapper.AddTypeHandler(type4, typeHandler);
				return DbType.Object;
			}
			if (demand)
			{
				throw new NotSupportedException(string.Concat(new string[] { "The member ", name, " of type ", type.FullName, " cannot be used as a parameter value" }));
			}
			return DbType.Object;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005291 File Offset: 0x00003491
		public static List<T> AsList<T>(this IEnumerable<T> source)
		{
			if (source != null && !(source is List<T>))
			{
				return source.ToList<T>();
			}
			return (List<T>)source;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000052AC File Offset: 0x000034AC
		public static int Execute(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken));
			return cnn.ExecuteImpl(ref command);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000052D9 File Offset: 0x000034D9
		public static int Execute(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.ExecuteImpl(ref command);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000052E4 File Offset: 0x000034E4
		public static object ExecuteScalar(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken));
			return SqlMapper.ExecuteScalarImpl<object>(cnn, ref command);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005314 File Offset: 0x00003514
		public static T ExecuteScalar<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken));
			return SqlMapper.ExecuteScalarImpl<T>(cnn, ref command);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005341 File Offset: 0x00003541
		public static object ExecuteScalar(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.ExecuteScalarImpl<object>(cnn, ref command);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000534B File Offset: 0x0000354B
		public static T ExecuteScalar<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.ExecuteScalarImpl<T>(cnn, ref command);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005355 File Offset: 0x00003555
		private static IEnumerable GetMultiExec(object param)
		{
			if (!(param is IEnumerable) || param is string || param is IEnumerable<KeyValuePair<string, object>> || param is SqlMapper.IDynamicParameters)
			{
				return null;
			}
			return (IEnumerable)param;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005380 File Offset: 0x00003580
		private static int ExecuteImpl(this IDbConnection cnn, ref CommandDefinition command)
		{
			object param = command.Parameters;
			IEnumerable multiExec = SqlMapper.GetMultiExec(param);
			SqlMapper.CacheInfo info = null;
			if (multiExec == null)
			{
				if (param != null)
				{
					SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, null, param.GetType(), null);
					info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
				}
				return SqlMapper.ExecuteCommand(cnn, ref command, (param == null) ? null : info.ParamReader);
			}
			if ((command.Flags & CommandFlags.Pipelined) != CommandFlags.None)
			{
				return SqlMapper.ExecuteMultiImplAsync(cnn, command, multiExec).Result;
			}
			bool isFirst = true;
			int total = 0;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			try
			{
				if (wasClosed)
				{
					cnn.Open();
				}
				using (IDbCommand cmd = command.SetupCommand(cnn, null))
				{
					string masterSql = null;
					foreach (object obj in multiExec)
					{
						if (isFirst)
						{
							masterSql = cmd.CommandText;
							isFirst = false;
							SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, new CommandType?(cmd.CommandType), cnn, null, obj.GetType(), null);
							info = SqlMapper.GetCacheInfo(identity, obj, command.AddToCache);
						}
						else
						{
							cmd.CommandText = masterSql;
							cmd.Parameters.Clear();
						}
						info.ParamReader(cmd, obj);
						total += cmd.ExecuteNonQuery();
					}
				}
				command.OnCompleted();
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
			}
			return total;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000551C File Offset: 0x0000371C
		public static IDataReader ExecuteReader(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken));
			IDbCommand dbcmd;
			IDataReader reader = SqlMapper.ExecuteReaderImpl(cnn, ref command, CommandBehavior.Default, out dbcmd);
			return new WrappedReader(dbcmd, reader);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005554 File Offset: 0x00003754
		public static IDataReader ExecuteReader(this IDbConnection cnn, CommandDefinition command)
		{
			IDbCommand dbcmd;
			IDataReader reader = SqlMapper.ExecuteReaderImpl(cnn, ref command, CommandBehavior.Default, out dbcmd);
			return new WrappedReader(dbcmd, reader);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005574 File Offset: 0x00003774
		public static IDataReader ExecuteReader(this IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior)
		{
			IDbCommand dbcmd;
			IDataReader reader = SqlMapper.ExecuteReaderImpl(cnn, ref command, commandBehavior, out dbcmd);
			return new WrappedReader(dbcmd, reader);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005594 File Offset: 0x00003794
		[return: Dynamic(new bool[] { false, true })]
		public static IEnumerable<dynamic> Query(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000055A5 File Offset: 0x000037A5
		[return: Dynamic]
		public static dynamic QueryFirst(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryFirst(sql, param, transaction, commandTimeout, commandType);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000055B4 File Offset: 0x000037B4
		[return: Dynamic]
		public static dynamic QueryFirstOrDefault(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000055C3 File Offset: 0x000037C3
		[return: Dynamic]
		public static dynamic QuerySingle(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QuerySingle(sql, param, transaction, commandTimeout, commandType);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000055D2 File Offset: 0x000037D2
		[return: Dynamic]
		public static dynamic QuerySingleOrDefault(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.QuerySingleOrDefault(sql, param, transaction, commandTimeout, commandType);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000055E4 File Offset: 0x000037E4
		public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken));
			IEnumerable<T> data = cnn.QueryImpl(command, typeof(T));
			if (!command.Buffered)
			{
				return data;
			}
			return data.ToList<T>();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005638 File Offset: 0x00003838
		public static T QueryFirst<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.First, ref command, typeof(T));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005670 File Offset: 0x00003870
		public static T QueryFirstOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.FirstOrDefault, ref command, typeof(T));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000056A8 File Offset: 0x000038A8
		public static T QuerySingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.Single, ref command, typeof(T));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000056E0 File Offset: 0x000038E0
		public static T QuerySingleOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.SingleOrDefault, ref command, typeof(T));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005718 File Offset: 0x00003918
		public static IEnumerable<object> Query(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken));
			IEnumerable<object> data = cnn.QueryImpl(command, type);
			if (!command.Buffered)
			{
				return data;
			}
			return data.ToList<object>();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005778 File Offset: 0x00003978
		public static object QueryFirst(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<object>(cnn, SqlMapper.Row.First, ref command, type);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000057BC File Offset: 0x000039BC
		public static object QueryFirstOrDefault(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<object>(cnn, SqlMapper.Row.FirstOrDefault, ref command, type);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005800 File Offset: 0x00003A00
		public static object QuerySingle(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<object>(cnn, SqlMapper.Row.Single, ref command, type);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005844 File Offset: 0x00003A44
		public static object QuerySingleOrDefault(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken));
			return SqlMapper.QueryRowImpl<object>(cnn, SqlMapper.Row.SingleOrDefault, ref command, type);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005888 File Offset: 0x00003A88
		public static IEnumerable<T> Query<T>(this IDbConnection cnn, CommandDefinition command)
		{
			IEnumerable<T> data = cnn.QueryImpl(command, typeof(T));
			if (!command.Buffered)
			{
				return data;
			}
			return data.ToList<T>();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000058BA File Offset: 0x00003ABA
		public static T QueryFirst<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.First, ref command, typeof(T));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000058CF File Offset: 0x00003ACF
		public static T QueryFirstOrDefault<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.FirstOrDefault, ref command, typeof(T));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000058E4 File Offset: 0x00003AE4
		public static T QuerySingle<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.Single, ref command, typeof(T));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000058F9 File Offset: 0x00003AF9
		public static T QuerySingleOrDefault<T>(this IDbConnection cnn, CommandDefinition command)
		{
			return SqlMapper.QueryRowImpl<T>(cnn, SqlMapper.Row.SingleOrDefault, ref command, typeof(T));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005910 File Offset: 0x00003B10
		public static SqlMapper.GridReader QueryMultiple(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken));
			return cnn.QueryMultipleImpl(ref command);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000593D File Offset: 0x00003B3D
		public static SqlMapper.GridReader QueryMultiple(this IDbConnection cnn, CommandDefinition command)
		{
			return cnn.QueryMultipleImpl(ref command);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005948 File Offset: 0x00003B48
		private static SqlMapper.GridReader QueryMultipleImpl(this IDbConnection cnn, ref CommandDefinition command)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, typeof(SqlMapper.GridReader), (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			IDbCommand cmd = null;
			IDataReader reader = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			SqlMapper.GridReader gridReader;
			try
			{
				if (wasClosed)
				{
					cnn.Open();
				}
				cmd = command.SetupCommand(cnn, info.ParamReader);
				reader = SqlMapper.ExecuteReaderWithFlagsFallback(cmd, wasClosed, CommandBehavior.SequentialAccess);
				SqlMapper.GridReader result = new SqlMapper.GridReader(cmd, reader, identity, command.Parameters as DynamicParameters, command.AddToCache);
				cmd = null;
				wasClosed = false;
				gridReader = result;
			}
			catch
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
					{
						try
						{
							if (cmd != null)
							{
								cmd.Cancel();
							}
						}
						catch
						{
						}
					}
					reader.Dispose();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
				if (wasClosed)
				{
					cnn.Close();
				}
				throw;
			}
			return gridReader;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005A48 File Offset: 0x00003C48
		private static IDataReader ExecuteReaderWithFlagsFallback(IDbCommand cmd, bool wasClosed, CommandBehavior behavior)
		{
			IDataReader dataReader;
			try
			{
				dataReader = cmd.ExecuteReader(SqlMapper.GetBehavior(wasClosed, behavior));
			}
			catch (ArgumentException ex)
			{
				if (!SqlMapper.Settings.DisableCommandBehaviorOptimizations(behavior, ex))
				{
					throw;
				}
				dataReader = cmd.ExecuteReader(SqlMapper.GetBehavior(wasClosed, behavior));
			}
			return dataReader;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005A94 File Offset: 0x00003C94
		private static IEnumerable<T> QueryImpl<T>(this IDbConnection cnn, CommandDefinition command, Type effectiveType)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, effectiveType, (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			IDbCommand cmd = null;
			IDataReader reader = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			try
			{
				cmd = command.SetupCommand(cnn, info.ParamReader);
				if (wasClosed)
				{
					cnn.Open();
				}
				reader = SqlMapper.ExecuteReaderWithFlagsFallback(cmd, wasClosed, CommandBehavior.SingleResult | CommandBehavior.SequentialAccess);
				wasClosed = false;
				SqlMapper.DeserializerState tuple = info.Deserializer;
				int hash = SqlMapper.GetColumnHash(reader, 0, -1);
				if (tuple.Func == null || tuple.Hash != hash)
				{
					if (reader.FieldCount == 0)
					{
						yield break;
					}
					tuple = (info.Deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(effectiveType, reader, 0, -1, false)));
					if (command.AddToCache)
					{
						SqlMapper.SetQueryCache(identity, info);
					}
				}
				Func<IDataReader, object> func = tuple.Func;
				Type convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
				while (reader.Read())
				{
					object val = func(reader);
					if (val == null || val is T)
					{
						yield return (T)((object)val);
					}
					else
					{
						yield return (T)((object)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
					}
				}
				while (reader.NextResult())
				{
				}
				reader.Dispose();
				reader = null;
				command.OnCompleted();
				func = null;
				convertToType = null;
			}
			finally
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
					{
						try
						{
							cmd.Cancel();
						}
						catch
						{
						}
					}
					reader.Dispose();
				}
				if (wasClosed)
				{
					cnn.Close();
				}
				IDbCommand dbCommand = cmd;
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005AB2 File Offset: 0x00003CB2
		private static void ThrowMultipleRows(SqlMapper.Row row)
		{
			if (row == SqlMapper.Row.Single)
			{
				SqlMapper.ErrTwoRows.Single<int>();
				return;
			}
			if (row != SqlMapper.Row.SingleOrDefault)
			{
				throw new InvalidOperationException();
			}
			SqlMapper.ErrTwoRows.SingleOrDefault<int>();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005ADB File Offset: 0x00003CDB
		private static void ThrowZeroRows(SqlMapper.Row row)
		{
			if (row == SqlMapper.Row.First)
			{
				SqlMapper.ErrZeroRows.First<int>();
				return;
			}
			if (row != SqlMapper.Row.Single)
			{
				throw new InvalidOperationException();
			}
			SqlMapper.ErrZeroRows.Single<int>();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005B04 File Offset: 0x00003D04
		private static T QueryRowImpl<T>(IDbConnection cnn, SqlMapper.Row row, ref CommandDefinition command, Type effectiveType)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, effectiveType, (param != null) ? param.GetType() : null, null);
			SqlMapper.CacheInfo info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			IDbCommand cmd = null;
			IDataReader reader = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			T t;
			try
			{
				cmd = command.SetupCommand(cnn, info.ParamReader);
				if (wasClosed)
				{
					cnn.Open();
				}
				reader = SqlMapper.ExecuteReaderWithFlagsFallback(cmd, wasClosed, ((row & SqlMapper.Row.Single) != SqlMapper.Row.First) ? (CommandBehavior.SingleResult | CommandBehavior.SequentialAccess) : (CommandBehavior.SingleResult | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess));
				wasClosed = false;
				T result = default(T);
				if (reader.Read() && reader.FieldCount != 0)
				{
					SqlMapper.DeserializerState tuple = info.Deserializer;
					int hash = SqlMapper.GetColumnHash(reader, 0, -1);
					if (tuple.Func == null || tuple.Hash != hash)
					{
						tuple = (info.Deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(effectiveType, reader, 0, -1, false)));
						if (command.AddToCache)
						{
							SqlMapper.SetQueryCache(identity, info);
						}
					}
					Func<IDataReader, object> func = tuple.Func;
					object val = func(reader);
					if (val == null || val is T)
					{
						result = (T)((object)val);
					}
					else
					{
						Type convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
						result = (T)((object)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
					}
					if ((row & SqlMapper.Row.Single) != SqlMapper.Row.First && reader.Read())
					{
						SqlMapper.ThrowMultipleRows(row);
					}
					while (reader.Read())
					{
					}
				}
				else if ((row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
				{
					SqlMapper.ThrowZeroRows(row);
				}
				while (reader.NextResult())
				{
				}
				reader.Dispose();
				reader = null;
				command.OnCompleted();
				t = result;
			}
			finally
			{
				if (reader != null)
				{
					if (!reader.IsClosed)
					{
						try
						{
							cmd.Cancel();
						}
						catch
						{
						}
					}
					reader.Dispose();
				}
				if (wasClosed)
				{
					cnn.Close();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
			}
			return t;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005CFC File Offset: 0x00003EFC
		public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMap(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005D1C File Offset: 0x00003F1C
		public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMap(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005D3C File Offset: 0x00003F3C
		public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMap(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005D5C File Offset: 0x00003F5C
		public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMap(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005D7C File Offset: 0x00003F7C
		public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMap(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005D9C File Offset: 0x00003F9C
		public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			return cnn.MultiMap(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005DBC File Offset: 0x00003FBC
		public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken));
			IEnumerable<TReturn> results = cnn.MultiMapImpl(command, types, map, splitOn, null, null, true);
			if (!buffered)
			{
				return results;
			}
			return results.ToList<TReturn>();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005E08 File Offset: 0x00004008
		private static IEnumerable<TReturn> MultiMap<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Delegate map, object param, IDbTransaction transaction, bool buffered, string splitOn, int? commandTimeout, CommandType? commandType)
		{
			CommandDefinition command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken));
			IEnumerable<TReturn> results = cnn.MultiMapImpl(command, map, splitOn, null, null, true);
			if (!buffered)
			{
				return results;
			}
			return results.ToList<TReturn>();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005E51 File Offset: 0x00004051
		private static IEnumerable<TReturn> MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Delegate map, string splitOn, IDataReader reader, SqlMapper.Identity identity, bool finalize)
		{
			object param = command.Parameters;
			SqlMapper.Identity identity2;
			if ((identity2 = identity) == null)
			{
				identity2 = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, typeof(TFirst), (param != null) ? param.GetType() : null, new Type[]
				{
					typeof(TFirst),
					typeof(TSecond),
					typeof(TThird),
					typeof(TFourth),
					typeof(TFifth),
					typeof(TSixth),
					typeof(TSeventh)
				});
			}
			identity = identity2;
			SqlMapper.CacheInfo cinfo = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			IDbCommand ownedCommand = null;
			IDataReader ownedReader = null;
			bool wasClosed = cnn != null && cnn.State == ConnectionState.Closed;
			try
			{
				if (reader == null)
				{
					ownedCommand = command.SetupCommand(cnn, cinfo.ParamReader);
					if (wasClosed)
					{
						cnn.Open();
					}
					ownedReader = SqlMapper.ExecuteReaderWithFlagsFallback(ownedCommand, wasClosed, CommandBehavior.SingleResult | CommandBehavior.SequentialAccess);
					reader = ownedReader;
				}
				SqlMapper.DeserializerState deserializer = default(SqlMapper.DeserializerState);
				int hash = SqlMapper.GetColumnHash(reader, 0, -1);
				Func<IDataReader, object>[] otherDeserializers;
				if ((deserializer = cinfo.Deserializer).Func == null || (otherDeserializers = cinfo.OtherDeserializers) == null || hash != deserializer.Hash)
				{
					Func<IDataReader, object>[] deserializers = SqlMapper.GenerateDeserializers(new Type[]
					{
						typeof(TFirst),
						typeof(TSecond),
						typeof(TThird),
						typeof(TFourth),
						typeof(TFifth),
						typeof(TSixth),
						typeof(TSeventh)
					}, splitOn, reader);
					deserializer = (cinfo.Deserializer = new SqlMapper.DeserializerState(hash, deserializers[0]));
					otherDeserializers = (cinfo.OtherDeserializers = deserializers.Skip(1).ToArray<Func<IDataReader, object>>());
					if (command.AddToCache)
					{
						SqlMapper.SetQueryCache(identity, cinfo);
					}
				}
				Func<IDataReader, TReturn> mapIt = SqlMapper.GenerateMapper<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(deserializer.Func, otherDeserializers, map);
				if (mapIt != null)
				{
					while (reader.Read())
					{
						yield return mapIt(reader);
					}
					if (finalize)
					{
						while (reader.NextResult())
						{
						}
						command.OnCompleted();
					}
				}
				mapIt = null;
			}
			finally
			{
				try
				{
					IDataReader dataReader = ownedReader;
					if (dataReader != null)
					{
						dataReader.Dispose();
					}
				}
				finally
				{
					IDbCommand dbCommand = ownedCommand;
					if (dbCommand != null)
					{
						dbCommand.Dispose();
					}
					if (wasClosed)
					{
						cnn.Close();
					}
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005E8E File Offset: 0x0000408E
		private static CommandBehavior GetBehavior(bool close, CommandBehavior @default)
		{
			return (close ? (@default | CommandBehavior.CloseConnection) : @default) & SqlMapper.Settings.AllowedCommandBehaviors;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005EA0 File Offset: 0x000040A0
		private static IEnumerable<TReturn> MultiMapImpl<TReturn>(this IDbConnection cnn, CommandDefinition command, Type[] types, Func<object[], TReturn> map, string splitOn, IDataReader reader, SqlMapper.Identity identity, bool finalize)
		{
			if (types.Length < 1)
			{
				throw new ArgumentException("you must provide at least one type to deserialize");
			}
			object param = command.Parameters;
			SqlMapper.Identity identity2;
			if ((identity2 = identity) == null)
			{
				identity2 = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, types[0], (param != null) ? param.GetType() : null, types);
			}
			identity = identity2;
			SqlMapper.CacheInfo cinfo = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			IDbCommand ownedCommand = null;
			IDataReader ownedReader = null;
			bool wasClosed = cnn != null && cnn.State == ConnectionState.Closed;
			try
			{
				if (reader == null)
				{
					ownedCommand = command.SetupCommand(cnn, cinfo.ParamReader);
					if (wasClosed)
					{
						cnn.Open();
					}
					ownedReader = SqlMapper.ExecuteReaderWithFlagsFallback(ownedCommand, wasClosed, CommandBehavior.SingleResult | CommandBehavior.SequentialAccess);
					reader = ownedReader;
				}
				int hash = SqlMapper.GetColumnHash(reader, 0, -1);
				SqlMapper.DeserializerState deserializer;
				Func<IDataReader, object>[] otherDeserializers;
				if ((deserializer = cinfo.Deserializer).Func == null || (otherDeserializers = cinfo.OtherDeserializers) == null || hash != deserializer.Hash)
				{
					Func<IDataReader, object>[] deserializers = SqlMapper.GenerateDeserializers(types, splitOn, reader);
					deserializer = (cinfo.Deserializer = new SqlMapper.DeserializerState(hash, deserializers[0]));
					otherDeserializers = (cinfo.OtherDeserializers = deserializers.Skip(1).ToArray<Func<IDataReader, object>>());
					SqlMapper.SetQueryCache(identity, cinfo);
				}
				Func<IDataReader, TReturn> mapIt = SqlMapper.GenerateMapper<TReturn>(types.Length, deserializer.Func, otherDeserializers, map);
				if (mapIt != null)
				{
					while (reader.Read())
					{
						yield return mapIt(reader);
					}
					if (finalize)
					{
						while (reader.NextResult())
						{
						}
						command.OnCompleted();
					}
				}
				mapIt = null;
			}
			finally
			{
				try
				{
					IDataReader dataReader = ownedReader;
					if (dataReader != null)
					{
						dataReader.Dispose();
					}
				}
				finally
				{
					IDbCommand dbCommand = ownedCommand;
					if (dbCommand != null)
					{
						dbCommand.Dispose();
					}
					if (wasClosed)
					{
						cnn.Close();
					}
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005EF0 File Offset: 0x000040F0
		private static Func<IDataReader, TReturn> GenerateMapper<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<IDataReader, object> deserializer, Func<IDataReader, object>[] otherDeserializers, object map)
		{
			switch (otherDeserializers.Length)
			{
			case 1:
				return (IDataReader r) => ((Func<TFirst, TSecond, TReturn>)map)((TFirst)((object)deserializer(r)), (TSecond)((object)otherDeserializers[0](r)));
			case 2:
				return (IDataReader r) => ((Func<TFirst, TSecond, TThird, TReturn>)map)((TFirst)((object)deserializer(r)), (TSecond)((object)otherDeserializers[0](r)), (TThird)((object)otherDeserializers[1](r)));
			case 3:
				return (IDataReader r) => ((Func<TFirst, TSecond, TThird, TFourth, TReturn>)map)((TFirst)((object)deserializer(r)), (TSecond)((object)otherDeserializers[0](r)), (TThird)((object)otherDeserializers[1](r)), (TFourth)((object)otherDeserializers[2](r)));
			case 4:
				return (IDataReader r) => ((Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>)map)((TFirst)((object)deserializer(r)), (TSecond)((object)otherDeserializers[0](r)), (TThird)((object)otherDeserializers[1](r)), (TFourth)((object)otherDeserializers[2](r)), (TFifth)((object)otherDeserializers[3](r)));
			case 5:
				return (IDataReader r) => ((Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>)map)((TFirst)((object)deserializer(r)), (TSecond)((object)otherDeserializers[0](r)), (TThird)((object)otherDeserializers[1](r)), (TFourth)((object)otherDeserializers[2](r)), (TFifth)((object)otherDeserializers[3](r)), (TSixth)((object)otherDeserializers[4](r)));
			case 6:
				return (IDataReader r) => ((Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>)map)((TFirst)((object)deserializer(r)), (TSecond)((object)otherDeserializers[0](r)), (TThird)((object)otherDeserializers[1](r)), (TFourth)((object)otherDeserializers[2](r)), (TFifth)((object)otherDeserializers[3](r)), (TSixth)((object)otherDeserializers[4](r)), (TSeventh)((object)otherDeserializers[5](r)));
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005F98 File Offset: 0x00004198
		private static Func<IDataReader, TReturn> GenerateMapper<TReturn>(int length, Func<IDataReader, object> deserializer, Func<IDataReader, object>[] otherDeserializers, Func<object[], TReturn> map)
		{
			return delegate(IDataReader r)
			{
				object[] objects = new object[length];
				objects[0] = deserializer(r);
				for (int i = 1; i < length; i++)
				{
					objects[i] = otherDeserializers[i - 1](r);
				}
				return map(objects);
			};
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005FD4 File Offset: 0x000041D4
		private static Func<IDataReader, object>[] GenerateDeserializers(Type[] types, string splitOn, IDataReader reader)
		{
			List<Func<IDataReader, object>> deserializers = new List<Func<IDataReader, object>>();
			string[] splits = (from s in splitOn.Split(new char[] { ',' })
				select s.Trim()).ToArray<string>();
			bool isMultiSplit = splits.Length > 1;
			if (types[0] == typeof(object))
			{
				bool first = true;
				int currentPos = 0;
				int splitIdx = 0;
				string currentSplit = splits[splitIdx];
				foreach (Type type in types)
				{
					if (type == typeof(SqlMapper.DontMap))
					{
						break;
					}
					int splitPoint = SqlMapper.GetNextSplitDynamic(currentPos, currentSplit, reader);
					if (isMultiSplit && splitIdx < splits.Length - 1)
					{
						currentSplit = splits[++splitIdx];
					}
					deserializers.Add(SqlMapper.GetDeserializer(type, reader, currentPos, splitPoint - currentPos, !first));
					currentPos = splitPoint;
					first = false;
				}
			}
			else
			{
				int currentPos2 = reader.FieldCount;
				int splitIdx2 = splits.Length - 1;
				string currentSplit2 = splits[splitIdx2];
				for (int typeIdx = types.Length - 1; typeIdx >= 0; typeIdx--)
				{
					Type type2 = types[typeIdx];
					if (!(type2 == typeof(SqlMapper.DontMap)))
					{
						int splitPoint2 = 0;
						if (typeIdx > 0)
						{
							splitPoint2 = SqlMapper.GetNextSplit(currentPos2, currentSplit2, reader);
							if (isMultiSplit && splitIdx2 > 0)
							{
								currentSplit2 = splits[--splitIdx2];
							}
						}
						deserializers.Add(SqlMapper.GetDeserializer(type2, reader, splitPoint2, currentPos2 - splitPoint2, typeIdx > 0));
						currentPos2 = splitPoint2;
					}
				}
				deserializers.Reverse();
			}
			return deserializers.ToArray();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000615C File Offset: 0x0000435C
		private static int GetNextSplitDynamic(int startIdx, string splitOn, IDataReader reader)
		{
			if (startIdx == reader.FieldCount)
			{
				throw SqlMapper.MultiMapException(reader);
			}
			if (splitOn == "*")
			{
				return ++startIdx;
			}
			for (int i = startIdx + 1; i < reader.FieldCount; i++)
			{
				if (string.Equals(splitOn, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return reader.FieldCount;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000061B8 File Offset: 0x000043B8
		private static int GetNextSplit(int startIdx, string splitOn, IDataReader reader)
		{
			if (splitOn == "*")
			{
				return --startIdx;
			}
			for (int i = startIdx - 1; i > 0; i--)
			{
				if (string.Equals(splitOn, reader.GetName(i), StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			throw SqlMapper.MultiMapException(reader);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006200 File Offset: 0x00004400
		private static SqlMapper.CacheInfo GetCacheInfo(SqlMapper.Identity identity, object exampleParameters, bool addToCache)
		{
			SqlMapper.CacheInfo info;
			if (!SqlMapper.TryGetQueryCache(identity, out info))
			{
				if (SqlMapper.GetMultiExec(exampleParameters) != null)
				{
					throw new InvalidOperationException("An enumerable sequence of parameters (arrays, lists, etc) is not allowed in this context");
				}
				info = new SqlMapper.CacheInfo();
				if (identity.parametersType != null)
				{
					Action<IDbCommand, object> reader;
					if (exampleParameters is SqlMapper.IDynamicParameters)
					{
						reader = delegate(IDbCommand cmd, object obj)
						{
							((SqlMapper.IDynamicParameters)obj).AddParameters(cmd, identity);
						};
					}
					else if (exampleParameters is IEnumerable<KeyValuePair<string, object>>)
					{
						reader = delegate(IDbCommand cmd, object obj)
						{
							SqlMapper.IDynamicParameters mapped = new DynamicParameters(obj);
							mapped.AddParameters(cmd, identity);
						};
					}
					else
					{
						IList<SqlMapper.LiteralToken> literals = SqlMapper.GetLiteralTokens(identity.sql);
						reader = SqlMapper.CreateParamInfoGenerator(identity, false, true, literals);
					}
					if (identity.commandType != null)
					{
						CommandType? commandType = identity.commandType;
						CommandType commandType2 = CommandType.Text;
						if (!((commandType.GetValueOrDefault() == commandType2) & (commandType != null)))
						{
							goto IL_0106;
						}
					}
					if (SqlMapper.ShouldPassByPosition(identity.sql))
					{
						Action<IDbCommand, object> tail = reader;
						reader = delegate(IDbCommand cmd, object obj)
						{
							tail(cmd, obj);
							SqlMapper.PassByPosition(cmd);
						};
					}
					IL_0106:
					info.ParamReader = reader;
				}
				if (addToCache)
				{
					SqlMapper.SetQueryCache(identity, info);
				}
			}
			return info;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000632A File Offset: 0x0000452A
		private static bool ShouldPassByPosition(string sql)
		{
			return sql != null && sql.IndexOf('?') >= 0 && SqlMapper.pseudoPositional.IsMatch(sql);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006348 File Offset: 0x00004548
		private static void PassByPosition(IDbCommand cmd)
		{
			if (cmd.Parameters.Count == 0)
			{
				return;
			}
			Dictionary<string, IDbDataParameter> parameters = new Dictionary<string, IDbDataParameter>(StringComparer.Ordinal);
			foreach (object obj in cmd.Parameters)
			{
				IDbDataParameter param = (IDbDataParameter)obj;
				if (!string.IsNullOrEmpty(param.ParameterName))
				{
					parameters[param.ParameterName] = param;
				}
			}
			HashSet<string> consumed = new HashSet<string>(StringComparer.Ordinal);
			bool firstMatch = true;
			cmd.CommandText = SqlMapper.pseudoPositional.Replace(cmd.CommandText, delegate(Match match)
			{
				string key = match.Groups[1].Value;
				if (!consumed.Add(key))
				{
					throw new InvalidOperationException("When passing parameters by position, each parameter can only be referenced once");
				}
				IDbDataParameter param2;
				if (parameters.TryGetValue(key, out param2))
				{
					if (firstMatch)
					{
						firstMatch = false;
						cmd.Parameters.Clear();
					}
					cmd.Parameters.Add(param2);
					parameters.Remove(key);
					consumed.Add(key);
					return "?";
				}
				return match.Value;
			});
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006434 File Offset: 0x00004634
		private static Func<IDataReader, object> GetDeserializer(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
		{
			if (type == typeof(object) || type == typeof(SqlMapper.DapperRow))
			{
				return SqlMapper.GetDapperRowDeserializer(reader, startBound, length, returnNullIfFirstMissing);
			}
			Type underlyingType = null;
			if (SqlMapper.typeMap.ContainsKey(type) || type.IsEnum() || type.FullName == "System.Data.Linq.Binary" || (type.IsValueType() && (underlyingType = Nullable.GetUnderlyingType(type)) != null && underlyingType.IsEnum()))
			{
				return SqlMapper.GetStructDeserializer(type, underlyingType ?? type, startBound);
			}
			SqlMapper.ITypeHandler handler;
			if (SqlMapper.typeHandlers.TryGetValue(type, out handler))
			{
				return SqlMapper.GetHandlerDeserializer(handler, type, startBound);
			}
			return SqlMapper.GetTypeDeserializer(type, reader, startBound, length, returnNullIfFirstMissing);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000064EC File Offset: 0x000046EC
		private static Func<IDataReader, object> GetHandlerDeserializer(SqlMapper.ITypeHandler handler, Type type, int startBound)
		{
			return (IDataReader reader) => handler.Parse(type, reader.GetValue(startBound));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006520 File Offset: 0x00004720
		private static Exception MultiMapException(IDataRecord reader)
		{
			bool hasFields = false;
			try
			{
				hasFields = reader != null && reader.FieldCount != 0;
			}
			catch
			{
			}
			if (hasFields)
			{
				return new ArgumentException("When using the multi-mapping APIs ensure you set the splitOn param if you have keys other than Id", "splitOn");
			}
			return new InvalidOperationException("No columns were selected");
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006574 File Offset: 0x00004774
		internal static Func<IDataReader, object> GetDapperRowDeserializer(IDataRecord reader, int startBound, int length, bool returnNullIfFirstMissing)
		{
			int fieldCount = reader.FieldCount;
			if (length == -1)
			{
				length = fieldCount - startBound;
			}
			if (fieldCount <= startBound)
			{
				throw SqlMapper.MultiMapException(reader);
			}
			int effectiveFieldCount = Math.Min(fieldCount - startBound, length);
			SqlMapper.DapperTable table = null;
			return delegate(IDataReader r)
			{
				if (table == null)
				{
					string[] names = new string[effectiveFieldCount];
					for (int i = 0; i < effectiveFieldCount; i++)
					{
						names[i] = r.GetName(i + startBound);
					}
					table = new SqlMapper.DapperTable(names);
				}
				object[] values = new object[effectiveFieldCount];
				if (returnNullIfFirstMissing)
				{
					values[0] = r.GetValue(startBound);
					if (values[0] is DBNull)
					{
						return null;
					}
				}
				if (startBound == 0)
				{
					for (int j = 0; j < values.Length; j++)
					{
						object val = r.GetValue(j);
						values[j] = ((val is DBNull) ? null : val);
					}
				}
				else
				{
					int begin = (returnNullIfFirstMissing ? 1 : 0);
					for (int iter = begin; iter < effectiveFieldCount; iter++)
					{
						object obj = r.GetValue(iter + startBound);
						values[iter] = ((obj is DBNull) ? null : obj);
					}
				}
				return new SqlMapper.DapperRow(table, values);
			};
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000065E4 File Offset: 0x000047E4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is for internal use only", false)]
		public static char ReadChar(object value)
		{
			if (value == null || value is DBNull)
			{
				throw new ArgumentNullException("value");
			}
			string s = value as string;
			if (s == null || s.Length != 1)
			{
				throw new ArgumentException("A single-character was expected", "value");
			}
			return s[0];
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006634 File Offset: 0x00004834
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is for internal use only", false)]
		public static char? ReadNullableChar(object value)
		{
			if (value == null || value is DBNull)
			{
				return null;
			}
			string s = value as string;
			if (s == null || s.Length != 1)
			{
				throw new ArgumentException("A single-character was expected", "value");
			}
			return new char?(s[0]);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006688 File Offset: 0x00004888
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is for internal use only", true)]
		public static IDbDataParameter FindOrAddParameter(IDataParameterCollection parameters, IDbCommand command, string name)
		{
			IDbDataParameter result;
			if (parameters.Contains(name))
			{
				result = (IDbDataParameter)parameters[name];
			}
			else
			{
				result = command.CreateParameter();
				result.ParameterName = name;
				parameters.Add(result);
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000066C4 File Offset: 0x000048C4
		internal static int GetListPaddingExtraCount(int count)
		{
			if (count <= 5)
			{
				return 0;
			}
			if (count < 0)
			{
				return 0;
			}
			int padFactor;
			if (count <= 150)
			{
				padFactor = 10;
			}
			else if (count <= 750)
			{
				padFactor = 50;
			}
			else if (count <= 2000)
			{
				padFactor = 100;
			}
			else if (count <= 2070)
			{
				padFactor = 10;
			}
			else
			{
				if (count <= 2100)
				{
					return 0;
				}
				padFactor = 200;
			}
			int intoBlock = count % padFactor;
			if (intoBlock != 0)
			{
				return padFactor - intoBlock;
			}
			return 0;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000672D File Offset: 0x0000492D
		private static string GetInListRegex(string name, bool byPosition)
		{
			if (!byPosition)
			{
				return "([?@:]" + Regex.Escape(name) + ")(?!\\w)(\\s+(?i)unknown(?-i))?";
			}
			return "(\\?)" + Regex.Escape(name) + "\\?(?!\\w)(\\s+(?i)unknown(?-i))?";
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006760 File Offset: 0x00004960
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is for internal use only", false)]
		public static void PackListParameters(IDbCommand command, string namePrefix, object value)
		{
			if (FeatureSupport.Get(command.Connection).Arrays)
			{
				IDbDataParameter arrayParm = command.CreateParameter();
				arrayParm.Value = SqlMapper.SanitizeParameterValue(value);
				arrayParm.ParameterName = namePrefix;
				command.Parameters.Add(arrayParm);
				return;
			}
			bool byPosition = SqlMapper.ShouldPassByPosition(command.CommandText);
			IEnumerable list = value as IEnumerable;
			int count = 0;
			bool isString = value is IEnumerable<string>;
			bool isDbString = value is IEnumerable<DbString>;
			DbType dbType = DbType.AnsiString;
			int splitAt = SqlMapper.Settings.InListStringSplitCount;
			bool viaSplit = splitAt >= 0 && SqlMapper.TryStringSplit(ref list, splitAt, namePrefix, command, byPosition);
			if (list != null && !viaSplit)
			{
				object lastValue = null;
				foreach (object item in list)
				{
					int num = count + 1;
					count = num;
					if (num == 1)
					{
						if (item == null)
						{
							throw new NotSupportedException("The first item in a list-expansion cannot be null");
						}
						if (!isDbString)
						{
							SqlMapper.ITypeHandler handler;
							dbType = SqlMapper.LookupDbType(item.GetType(), "", true, out handler);
						}
					}
					string nextName = namePrefix + count.ToString();
					if (isDbString && item is DbString)
					{
						DbString str = item as DbString;
						str.AddParameter(command, nextName);
					}
					else
					{
						IDbDataParameter listParam = command.CreateParameter();
						listParam.ParameterName = nextName;
						if (isString)
						{
							listParam.Size = 4000;
							if (item != null && ((string)item).Length > 4000)
							{
								listParam.Size = -1;
							}
						}
						object tmp = (listParam.Value = SqlMapper.SanitizeParameterValue(item));
						if (tmp != null && !(tmp is DBNull))
						{
							lastValue = tmp;
						}
						if (listParam.DbType != dbType)
						{
							listParam.DbType = dbType;
						}
						command.Parameters.Add(listParam);
					}
				}
				if (SqlMapper.Settings.PadListExpansions && !isDbString && lastValue != null)
				{
					int padCount = SqlMapper.GetListPaddingExtraCount(count);
					for (int i = 0; i < padCount; i++)
					{
						int num = count;
						count = num + 1;
						IDbDataParameter padParam = command.CreateParameter();
						padParam.ParameterName = namePrefix + count.ToString();
						if (isString)
						{
							padParam.Size = 4000;
						}
						padParam.DbType = dbType;
						padParam.Value = lastValue;
						command.Parameters.Add(padParam);
					}
				}
			}
			if (!viaSplit)
			{
				string regexIncludingUnknown = SqlMapper.GetInListRegex(namePrefix, byPosition);
				if (count == 0)
				{
					command.CommandText = Regex.Replace(command.CommandText, regexIncludingUnknown, delegate(Match match)
					{
						string variableName = match.Groups[1].Value;
						if (match.Groups[2].Success)
						{
							return match.Value;
						}
						return "(SELECT " + variableName + " WHERE 1 = 0)";
					}, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
					IDbDataParameter dummyParam = command.CreateParameter();
					dummyParam.ParameterName = namePrefix;
					dummyParam.Value = DBNull.Value;
					command.Parameters.Add(dummyParam);
					return;
				}
				command.CommandText = Regex.Replace(command.CommandText, regexIncludingUnknown, delegate(Match match)
				{
					string variableName2 = match.Groups[1].Value;
					if (match.Groups[2].Success)
					{
						string suffix = match.Groups[2].Value;
						StringBuilder sb = SqlMapper.GetStringBuilder().Append(variableName2).Append(1)
							.Append(suffix);
						for (int j = 2; j <= count; j++)
						{
							sb.Append(',').Append(variableName2).Append(j)
								.Append(suffix);
						}
						return sb.__ToStringRecycle();
					}
					StringBuilder sb2 = SqlMapper.GetStringBuilder().Append('(').Append(variableName2);
					if (!byPosition)
					{
						sb2.Append(1);
					}
					for (int k = 2; k <= count; k++)
					{
						sb2.Append(',').Append(variableName2);
						if (!byPosition)
						{
							sb2.Append(k);
						}
					}
					return sb2.Append(')').__ToStringRecycle();
				}, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006AA4 File Offset: 0x00004CA4
		private static bool TryStringSplit(ref IEnumerable list, int splitAt, string namePrefix, IDbCommand command, bool byPosition)
		{
			if (list == null || splitAt < 0)
			{
				return false;
			}
			IEnumerable enumerable = list;
			if (enumerable != null)
			{
				IEnumerable<int> enumerable2;
				if ((enumerable2 = enumerable as IEnumerable<int>) != null)
				{
					IEnumerable<int> m = enumerable2;
					return SqlMapper.TryStringSplit<int>(ref m, splitAt, namePrefix, command, "int", byPosition, delegate(StringBuilder sb, int i)
					{
						sb.Append(i.ToString(CultureInfo.InvariantCulture));
					});
				}
				IEnumerable<long> enumerable3;
				if ((enumerable3 = enumerable as IEnumerable<long>) != null)
				{
					IEnumerable<long> j = enumerable3;
					return SqlMapper.TryStringSplit<long>(ref j, splitAt, namePrefix, command, "bigint", byPosition, delegate(StringBuilder sb, long i)
					{
						sb.Append(i.ToString(CultureInfo.InvariantCulture));
					});
				}
				IEnumerable<short> enumerable4;
				if ((enumerable4 = enumerable as IEnumerable<short>) != null)
				{
					IEnumerable<short> k = enumerable4;
					return SqlMapper.TryStringSplit<short>(ref k, splitAt, namePrefix, command, "smallint", byPosition, delegate(StringBuilder sb, short i)
					{
						sb.Append(i.ToString(CultureInfo.InvariantCulture));
					});
				}
				IEnumerable<byte> enumerable5;
				if ((enumerable5 = enumerable as IEnumerable<byte>) != null)
				{
					IEnumerable<byte> l = enumerable5;
					return SqlMapper.TryStringSplit<byte>(ref l, splitAt, namePrefix, command, "tinyint", byPosition, delegate(StringBuilder sb, byte i)
					{
						sb.Append(i.ToString(CultureInfo.InvariantCulture));
					});
				}
			}
			return false;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006BC8 File Offset: 0x00004DC8
		private static bool TryStringSplit<T>(ref IEnumerable<T> list, int splitAt, string namePrefix, IDbCommand command, string colType, bool byPosition, Action<StringBuilder, T> append)
		{
			ICollection<T> typed = list as ICollection<T>;
			if (typed == null)
			{
				typed = list.ToList<T>();
				list = typed;
			}
			if (typed.Count < splitAt)
			{
				return false;
			}
			string varName = null;
			string regexIncludingUnknown = SqlMapper.GetInListRegex(namePrefix, byPosition);
			string sql = Regex.Replace(command.CommandText, regexIncludingUnknown, delegate(Match match)
			{
				string variableName = match.Groups[1].Value;
				if (match.Groups[2].Success)
				{
					return match.Value;
				}
				varName = variableName;
				return string.Concat(new string[] { "(select cast([value] as ", colType, ") from string_split(", variableName, ",','))" });
			}, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
			if (varName == null)
			{
				return false;
			}
			command.CommandText = sql;
			IDbDataParameter concatenatedParam = command.CreateParameter();
			concatenatedParam.ParameterName = namePrefix;
			concatenatedParam.DbType = DbType.AnsiString;
			concatenatedParam.Size = -1;
			string val;
			using (IEnumerator<T> iter = typed.GetEnumerator())
			{
				if (iter.MoveNext())
				{
					StringBuilder sb = SqlMapper.GetStringBuilder();
					append(sb, iter.Current);
					while (iter.MoveNext())
					{
						T t = iter.Current;
						append(sb.Append(','), t);
					}
					val = sb.ToString();
				}
				else
				{
					val = "";
				}
			}
			concatenatedParam.Value = val;
			command.Parameters.Add(concatenatedParam);
			return true;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006CF8 File Offset: 0x00004EF8
		[Obsolete("This method is for internal use only", false)]
		public static object SanitizeParameterValue(object value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			if (value is Enum)
			{
				TypeCode typeCode;
				if (value is IConvertible)
				{
					typeCode = ((IConvertible)value).GetTypeCode();
				}
				else
				{
					typeCode = TypeExtensions.GetTypeCode(Enum.GetUnderlyingType(value.GetType()));
				}
				switch (typeCode)
				{
				case TypeCode.SByte:
					return (sbyte)value;
				case TypeCode.Byte:
					return (byte)value;
				case TypeCode.Int16:
					return (short)value;
				case TypeCode.UInt16:
					return (ushort)value;
				case TypeCode.Int32:
					return (int)value;
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (long)value;
				case TypeCode.UInt64:
					return (ulong)value;
				}
			}
			return value;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006DCC File Offset: 0x00004FCC
		private static IEnumerable<PropertyInfo> FilterParameters(IEnumerable<PropertyInfo> parameters, string sql)
		{
			List<PropertyInfo> list = new List<PropertyInfo>(16);
			foreach (PropertyInfo p in parameters)
			{
				if (Regex.IsMatch(sql, "[?@:]" + p.Name + "([^\\p{L}\\p{N}_]+|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant))
				{
					list.Add(p);
				}
			}
			return list;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006E40 File Offset: 0x00005040
		public static void ReplaceLiterals(this SqlMapper.IParameterLookup parameters, IDbCommand command)
		{
			IList<SqlMapper.LiteralToken> tokens = SqlMapper.GetLiteralTokens(command.CommandText);
			if (tokens.Count != 0)
			{
				SqlMapper.ReplaceLiterals(parameters, command, tokens);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006E6C File Offset: 0x0000506C
		[Obsolete("This method is for internal use only")]
		public static string Format(object value)
		{
			if (value == null)
			{
				return "null";
			}
			switch (TypeExtensions.GetTypeCode(value.GetType()))
			{
			case TypeCode.DBNull:
				return "null";
			case TypeCode.Boolean:
				if (!(bool)value)
				{
					return "0";
				}
				return "1";
			case TypeCode.SByte:
				return ((sbyte)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Byte:
				return ((byte)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Int16:
				return ((short)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.UInt16:
				return ((ushort)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Int32:
				return ((int)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.UInt32:
				return ((uint)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Int64:
				return ((long)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.UInt64:
				return ((ulong)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Single:
				return ((float)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Double:
				return ((double)value).ToString(CultureInfo.InvariantCulture);
			case TypeCode.Decimal:
				return ((decimal)value).ToString(CultureInfo.InvariantCulture);
			}
			IEnumerable multiExec = SqlMapper.GetMultiExec(value);
			if (multiExec == null)
			{
				throw new NotSupportedException("The type '" + value.GetType().Name + "' is not supported for SQL literals.");
			}
			StringBuilder sb = null;
			bool first = true;
			foreach (object subval in multiExec)
			{
				if (first)
				{
					sb = SqlMapper.GetStringBuilder().Append('(');
					first = false;
				}
				else
				{
					sb.Append(',');
				}
				sb.Append(SqlMapper.Format(subval));
			}
			if (first)
			{
				return "(select null where 1=0)";
			}
			return sb.Append(')').__ToStringRecycle();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007090 File Offset: 0x00005290
		internal static void ReplaceLiterals(SqlMapper.IParameterLookup parameters, IDbCommand command, IList<SqlMapper.LiteralToken> tokens)
		{
			string sql = command.CommandText;
			foreach (SqlMapper.LiteralToken token in tokens)
			{
				object value = parameters[token.Member];
				string text = SqlMapper.Format(value);
				sql = sql.Replace(token.Token, text);
			}
			command.CommandText = sql;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007108 File Offset: 0x00005308
		internal static IList<SqlMapper.LiteralToken> GetLiteralTokens(string sql)
		{
			if (string.IsNullOrEmpty(sql))
			{
				return SqlMapper.LiteralToken.None;
			}
			if (!SqlMapper.literalTokens.IsMatch(sql))
			{
				return SqlMapper.LiteralToken.None;
			}
			MatchCollection matches = SqlMapper.literalTokens.Matches(sql);
			HashSet<string> found = new HashSet<string>(StringComparer.Ordinal);
			List<SqlMapper.LiteralToken> list = new List<SqlMapper.LiteralToken>(matches.Count);
			foreach (object obj in matches)
			{
				Match match = (Match)obj;
				string token = match.Value;
				if (found.Add(match.Value))
				{
					list.Add(new SqlMapper.LiteralToken(token, match.Groups[1].Value));
				}
			}
			if (list.Count != 0)
			{
				return list;
			}
			return SqlMapper.LiteralToken.None;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000071E8 File Offset: 0x000053E8
		public static Action<IDbCommand, object> CreateParamInfoGenerator(SqlMapper.Identity identity, bool checkForDuplicates, bool removeUnused)
		{
			return SqlMapper.CreateParamInfoGenerator(identity, checkForDuplicates, removeUnused, SqlMapper.GetLiteralTokens(identity.sql));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000071FD File Offset: 0x000053FD
		private static bool IsValueTuple(Type type)
		{
			return type != null && type.IsValueType() && type.FullName.StartsWith("System.ValueTuple`", StringComparison.Ordinal);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007220 File Offset: 0x00005420
		private static List<SqlMapper.IMemberMap> GetValueTupleMembers(Type type, string[] names)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			List<SqlMapper.IMemberMap> result = new List<SqlMapper.IMemberMap>(names.Length);
			for (int i = 0; i < names.Length; i++)
			{
				FieldInfo field = null;
				string name = "Item" + (i + 1).ToString(CultureInfo.InvariantCulture);
				foreach (FieldInfo test in fields)
				{
					if (test.Name == name)
					{
						field = test;
						break;
					}
				}
				result.Add((field == null) ? null : new SimpleMemberMap(string.IsNullOrWhiteSpace(names[i]) ? name : names[i], field));
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000072CC File Offset: 0x000054CC
		internal static Action<IDbCommand, object> CreateParamInfoGenerator(SqlMapper.Identity identity, bool checkForDuplicates, bool removeUnused, IList<SqlMapper.LiteralToken> literals)
		{
			Type type = identity.parametersType;
			if (SqlMapper.IsValueTuple(type))
			{
				throw new NotSupportedException("ValueTuple should not be used for parameters - the language-level names are not available to use as parameter names, and it adds unnecessary boxing");
			}
			bool filterParams = false;
			if (removeUnused && identity.commandType.GetValueOrDefault(CommandType.Text) == CommandType.Text)
			{
				filterParams = !SqlMapper.smellsLikeOleDb.IsMatch(identity.sql);
			}
			DynamicMethod dm = new DynamicMethod("ParamInfo" + Guid.NewGuid().ToString(), null, new Type[]
			{
				typeof(IDbCommand),
				typeof(object)
			}, type, true);
			ILGenerator il = dm.GetILGenerator();
			bool isStruct = type.IsValueType();
			bool haveInt32Arg = false;
			il.Emit(OpCodes.Ldarg_1);
			if (isStruct)
			{
				il.DeclareLocal(type.MakeByRefType());
				il.Emit(OpCodes.Unbox, type);
			}
			else
			{
				il.DeclareLocal(type);
				il.Emit(OpCodes.Castclass, type);
			}
			il.Emit(OpCodes.Stloc_0);
			il.Emit(OpCodes.Ldarg_0);
			il.EmitCall(OpCodes.Callvirt, typeof(IDbCommand).GetProperty("Parameters").GetGetMethod(), null);
			PropertyInfo[] allTypeProps = type.GetProperties();
			List<PropertyInfo> propsList = new List<PropertyInfo>(allTypeProps.Length);
			foreach (PropertyInfo p in allTypeProps)
			{
				if (p.GetIndexParameters().Length == 0)
				{
					propsList.Add(p);
				}
			}
			ConstructorInfo[] ctors = type.GetConstructors();
			IEnumerable<PropertyInfo> props = null;
			ParameterInfo[] ctorParams;
			if (ctors.Length == 1 && propsList.Count == (ctorParams = ctors[0].GetParameters()).Length)
			{
				bool ok = true;
				for (int j = 0; j < propsList.Count; j++)
				{
					if (!string.Equals(propsList[j].Name, ctorParams[j].Name, StringComparison.OrdinalIgnoreCase))
					{
						ok = false;
						break;
					}
				}
				if (ok)
				{
					props = propsList;
				}
				else
				{
					Dictionary<string, int> positionByName = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
					foreach (ParameterInfo param in ctorParams)
					{
						positionByName[param.Name] = param.Position;
					}
					if (positionByName.Count == propsList.Count)
					{
						int[] positions = new int[propsList.Count];
						ok = true;
						for (int k = 0; k < propsList.Count; k++)
						{
							int pos;
							if (!positionByName.TryGetValue(propsList[k].Name, out pos))
							{
								ok = false;
								break;
							}
							positions[k] = pos;
						}
						if (ok)
						{
							props = propsList.ToArray();
							Array.Sort<int, PropertyInfo>(positions, (PropertyInfo[])props);
						}
					}
				}
			}
			if (props == null)
			{
				propsList.Sort(new SqlMapper.PropertyInfoByNameComparer());
				props = propsList;
			}
			if (filterParams)
			{
				props = SqlMapper.FilterParameters(props, identity.sql);
			}
			OpCode callOpCode = (isStruct ? OpCodes.Call : OpCodes.Callvirt);
			foreach (PropertyInfo prop in props)
			{
				if (typeof(SqlMapper.ICustomQueryParameter).IsAssignableFrom(prop.PropertyType))
				{
					il.Emit(OpCodes.Ldloc_0);
					il.Emit(callOpCode, prop.GetGetMethod());
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Ldstr, prop.Name);
					il.EmitCall(OpCodes.Callvirt, prop.PropertyType.GetMethod("AddParameter"), null);
				}
				else
				{
					SqlMapper.ITypeHandler handler;
					DbType dbType = SqlMapper.LookupDbType(prop.PropertyType, prop.Name, true, out handler);
					if (dbType == (DbType)(-1))
					{
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldstr, prop.Name);
						il.Emit(OpCodes.Ldloc_0);
						il.Emit(callOpCode, prop.GetGetMethod());
						if (prop.PropertyType.IsValueType())
						{
							il.Emit(OpCodes.Box, prop.PropertyType);
						}
						il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("PackListParameters"), null);
					}
					else
					{
						il.Emit(OpCodes.Dup);
						il.Emit(OpCodes.Ldarg_0);
						if (checkForDuplicates)
						{
							il.Emit(OpCodes.Ldstr, prop.Name);
							il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("FindOrAddParameter"), null);
						}
						else
						{
							il.EmitCall(OpCodes.Callvirt, typeof(IDbCommand).GetMethod("CreateParameter"), null);
							il.Emit(OpCodes.Dup);
							il.Emit(OpCodes.Ldstr, prop.Name);
							il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("ParameterName").GetSetMethod(), null);
						}
						if (dbType != DbType.Time && handler == null)
						{
							il.Emit(OpCodes.Dup);
							if (dbType == DbType.Object && prop.PropertyType == typeof(object))
							{
								il.Emit(OpCodes.Ldloc_0);
								il.Emit(callOpCode, prop.GetGetMethod());
								il.Emit(OpCodes.Call, typeof(SqlMapper).GetMethod("GetDbType", BindingFlags.Static | BindingFlags.Public));
							}
							else
							{
								SqlMapper.EmitInt32(il, (int)dbType);
							}
							il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("DbType").GetSetMethod(), null);
						}
						il.Emit(OpCodes.Dup);
						SqlMapper.EmitInt32(il, 1);
						il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("Direction").GetSetMethod(), null);
						il.Emit(OpCodes.Dup);
						il.Emit(OpCodes.Ldloc_0);
						il.Emit(callOpCode, prop.GetGetMethod());
						bool checkForNull;
						if (prop.PropertyType.IsValueType())
						{
							Type propType = prop.PropertyType;
							Type nullType = Nullable.GetUnderlyingType(propType);
							bool callSanitize = false;
							if ((nullType ?? propType).IsEnum())
							{
								if (nullType != null)
								{
									checkForNull = (callSanitize = true);
								}
								else
								{
									checkForNull = false;
									switch (TypeExtensions.GetTypeCode(Enum.GetUnderlyingType(propType)))
									{
									case TypeCode.SByte:
										propType = typeof(sbyte);
										break;
									case TypeCode.Byte:
										propType = typeof(byte);
										break;
									case TypeCode.Int16:
										propType = typeof(short);
										break;
									case TypeCode.UInt16:
										propType = typeof(ushort);
										break;
									case TypeCode.Int32:
										propType = typeof(int);
										break;
									case TypeCode.UInt32:
										propType = typeof(uint);
										break;
									case TypeCode.Int64:
										propType = typeof(long);
										break;
									case TypeCode.UInt64:
										propType = typeof(ulong);
										break;
									}
								}
							}
							else
							{
								checkForNull = nullType != null;
							}
							il.Emit(OpCodes.Box, propType);
							if (callSanitize)
							{
								checkForNull = false;
								il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("SanitizeParameterValue"), null);
							}
						}
						else
						{
							checkForNull = true;
						}
						if (checkForNull)
						{
							if ((dbType == DbType.String || dbType == DbType.AnsiString) && !haveInt32Arg)
							{
								il.DeclareLocal(typeof(int));
								haveInt32Arg = true;
							}
							il.Emit(OpCodes.Dup);
							Label notNull = il.DefineLabel();
							Label? allDone = ((dbType == DbType.String || dbType == DbType.AnsiString) ? new Label?(il.DefineLabel()) : null);
							il.Emit(OpCodes.Brtrue_S, notNull);
							il.Emit(OpCodes.Pop);
							il.Emit(OpCodes.Ldsfld, typeof(DBNull).GetField("Value"));
							if (dbType == DbType.String || dbType == DbType.AnsiString)
							{
								SqlMapper.EmitInt32(il, 0);
								il.Emit(OpCodes.Stloc_1);
							}
							if (allDone != null)
							{
								il.Emit(OpCodes.Br_S, allDone.Value);
							}
							il.MarkLabel(notNull);
							if (prop.PropertyType == typeof(string))
							{
								il.Emit(OpCodes.Dup);
								il.EmitCall(OpCodes.Callvirt, typeof(string).GetProperty("Length").GetGetMethod(), null);
								SqlMapper.EmitInt32(il, 4000);
								il.Emit(OpCodes.Cgt);
								Label isLong = il.DefineLabel();
								Label lenDone = il.DefineLabel();
								il.Emit(OpCodes.Brtrue_S, isLong);
								SqlMapper.EmitInt32(il, 4000);
								il.Emit(OpCodes.Br_S, lenDone);
								il.MarkLabel(isLong);
								SqlMapper.EmitInt32(il, -1);
								il.MarkLabel(lenDone);
								il.Emit(OpCodes.Stloc_1);
							}
							if (prop.PropertyType.FullName == "System.Data.Linq.Binary")
							{
								il.EmitCall(OpCodes.Callvirt, prop.PropertyType.GetMethod("ToArray", BindingFlags.Instance | BindingFlags.Public), null);
							}
							if (allDone != null)
							{
								il.MarkLabel(allDone.Value);
							}
						}
						if (handler != null)
						{
							il.Emit(OpCodes.Call, typeof(SqlMapper.TypeHandlerCache<>).MakeGenericType(new Type[] { prop.PropertyType }).GetMethod("SetValue"));
						}
						else
						{
							il.EmitCall(OpCodes.Callvirt, typeof(IDataParameter).GetProperty("Value").GetSetMethod(), null);
						}
						if (prop.PropertyType == typeof(string))
						{
							Label endOfSize = il.DefineLabel();
							il.Emit(OpCodes.Ldloc_1);
							il.Emit(OpCodes.Brfalse_S, endOfSize);
							il.Emit(OpCodes.Dup);
							il.Emit(OpCodes.Ldloc_1);
							il.EmitCall(OpCodes.Callvirt, typeof(IDbDataParameter).GetProperty("Size").GetSetMethod(), null);
							il.MarkLabel(endOfSize);
						}
						if (checkForDuplicates)
						{
							il.Emit(OpCodes.Pop);
						}
						else
						{
							il.EmitCall(OpCodes.Callvirt, typeof(IList).GetMethod("Add"), null);
							il.Emit(OpCodes.Pop);
						}
					}
				}
			}
			il.Emit(OpCodes.Pop);
			if (literals.Count != 0 && propsList != null)
			{
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Ldarg_0);
				PropertyInfo cmdText = typeof(IDbCommand).GetProperty("CommandText");
				il.EmitCall(OpCodes.Callvirt, cmdText.GetGetMethod(), null);
				Dictionary<Type, LocalBuilder> locals = null;
				LocalBuilder local = null;
				foreach (SqlMapper.LiteralToken literal in literals)
				{
					PropertyInfo exact = null;
					PropertyInfo fallback = null;
					string huntName = literal.Member;
					for (int l = 0; l < propsList.Count; l++)
					{
						string thisName = propsList[l].Name;
						if (string.Equals(thisName, huntName, StringComparison.OrdinalIgnoreCase))
						{
							fallback = propsList[l];
							if (string.Equals(thisName, huntName, StringComparison.Ordinal))
							{
								exact = fallback;
								break;
							}
						}
					}
					PropertyInfo prop2 = exact ?? fallback;
					if (prop2 != null)
					{
						il.Emit(OpCodes.Ldstr, literal.Token);
						il.Emit(OpCodes.Ldloc_0);
						il.EmitCall(callOpCode, prop2.GetGetMethod(), null);
						Type propType2 = prop2.PropertyType;
						TypeCode typeCode = TypeExtensions.GetTypeCode(propType2);
						if (typeCode != TypeCode.Boolean)
						{
							if (typeCode - TypeCode.SByte > 10)
							{
								if (propType2.IsValueType())
								{
									il.Emit(OpCodes.Box, propType2);
								}
								il.EmitCall(OpCodes.Call, SqlMapper.format, null);
							}
							else
							{
								MethodInfo convert = SqlMapper.GetToString(typeCode);
								if (local == null || local.LocalType != propType2)
								{
									if (locals == null)
									{
										locals = new Dictionary<Type, LocalBuilder>();
										local = null;
									}
									else if (!locals.TryGetValue(propType2, out local))
									{
										local = null;
									}
									if (local == null)
									{
										local = il.DeclareLocal(propType2);
										locals.Add(propType2, local);
									}
								}
								il.Emit(OpCodes.Stloc, local);
								il.Emit(OpCodes.Ldloca, local);
								il.EmitCall(OpCodes.Call, SqlMapper.InvariantCulture, null);
								il.EmitCall(OpCodes.Call, convert, null);
							}
						}
						else
						{
							Label ifTrue = il.DefineLabel();
							Label allDone2 = il.DefineLabel();
							il.Emit(OpCodes.Brtrue_S, ifTrue);
							il.Emit(OpCodes.Ldstr, "0");
							il.Emit(OpCodes.Br_S, allDone2);
							il.MarkLabel(ifTrue);
							il.Emit(OpCodes.Ldstr, "1");
							il.MarkLabel(allDone2);
						}
						il.EmitCall(OpCodes.Callvirt, SqlMapper.StringReplace, null);
					}
				}
				il.EmitCall(OpCodes.Callvirt, cmdText.GetSetMethod(), null);
			}
			il.Emit(OpCodes.Ret);
			return (Action<IDbCommand, object>)dm.CreateDelegate(typeof(Action<IDbCommand, object>));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007F94 File Offset: 0x00006194
		private static MethodInfo GetToString(TypeCode typeCode)
		{
			MethodInfo method;
			if (!SqlMapper.toStrings.TryGetValue(typeCode, out method))
			{
				return null;
			}
			return method;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007FB4 File Offset: 0x000061B4
		private static int ExecuteCommand(IDbConnection cnn, ref CommandDefinition command, Action<IDbCommand, object> paramReader)
		{
			IDbCommand cmd = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			int num;
			try
			{
				cmd = command.SetupCommand(cnn, paramReader);
				if (wasClosed)
				{
					cnn.Open();
				}
				int result = cmd.ExecuteNonQuery();
				command.OnCompleted();
				num = result;
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
			}
			return num;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008014 File Offset: 0x00006214
		private static T ExecuteScalarImpl<T>(IDbConnection cnn, ref CommandDefinition command)
		{
			Action<IDbCommand, object> paramReader = null;
			object param = command.Parameters;
			if (param != null)
			{
				SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, null, param.GetType(), null);
				paramReader = SqlMapper.GetCacheInfo(identity, command.Parameters, command.AddToCache).ParamReader;
			}
			IDbCommand cmd = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			object result;
			try
			{
				cmd = command.SetupCommand(cnn, paramReader);
				if (wasClosed)
				{
					cnn.Open();
				}
				result = cmd.ExecuteScalar();
				command.OnCompleted();
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
				if (cmd != null)
				{
					cmd.Dispose();
				}
			}
			return SqlMapper.Parse<T>(result);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000080BC File Offset: 0x000062BC
		private static IDataReader ExecuteReaderImpl(IDbConnection cnn, ref CommandDefinition command, CommandBehavior commandBehavior, out IDbCommand cmd)
		{
			Action<IDbCommand, object> paramReader = SqlMapper.GetParameterReader(cnn, ref command);
			cmd = null;
			bool wasClosed = cnn.State == ConnectionState.Closed;
			bool disposeCommand = true;
			IDataReader dataReader;
			try
			{
				cmd = command.SetupCommand(cnn, paramReader);
				if (wasClosed)
				{
					cnn.Open();
				}
				IDataReader reader = SqlMapper.ExecuteReaderWithFlagsFallback(cmd, wasClosed, commandBehavior);
				wasClosed = false;
				disposeCommand = false;
				dataReader = reader;
			}
			finally
			{
				if (wasClosed)
				{
					cnn.Close();
				}
				if (cmd != null && disposeCommand)
				{
					cmd.Dispose();
				}
			}
			return dataReader;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008134 File Offset: 0x00006334
		private static Action<IDbCommand, object> GetParameterReader(IDbConnection cnn, ref CommandDefinition command)
		{
			object param = command.Parameters;
			IEnumerable multiExec = SqlMapper.GetMultiExec(param);
			SqlMapper.CacheInfo info = null;
			if (multiExec != null)
			{
				throw new NotSupportedException("MultiExec is not supported by ExecuteReader");
			}
			if (param != null)
			{
				SqlMapper.Identity identity = new SqlMapper.Identity(command.CommandText, command.CommandType, cnn, null, param.GetType(), null);
				info = SqlMapper.GetCacheInfo(identity, param, command.AddToCache);
			}
			return (info != null) ? info.ParamReader : null;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000819C File Offset: 0x0000639C
		private static Func<IDataReader, object> GetStructDeserializer(Type type, Type effectiveType, int index)
		{
			if (type == typeof(char))
			{
				return (IDataReader r) => SqlMapper.ReadChar(r.GetValue(index));
			}
			if (type == typeof(char?))
			{
				return (IDataReader r) => SqlMapper.ReadNullableChar(r.GetValue(index));
			}
			if (type.FullName == "System.Data.Linq.Binary")
			{
				return (IDataReader r) => Activator.CreateInstance(type, new object[] { r.GetValue(index) });
			}
			if (effectiveType.IsEnum())
			{
				return delegate(IDataReader r)
				{
					object val = r.GetValue(index);
					if (val is float || val is double || val is decimal)
					{
						val = Convert.ChangeType(val, Enum.GetUnderlyingType(effectiveType), CultureInfo.InvariantCulture);
					}
					if (!(val is DBNull))
					{
						return Enum.ToObject(effectiveType, val);
					}
					return null;
				};
			}
			SqlMapper.ITypeHandler handler;
			if (SqlMapper.typeHandlers.TryGetValue(type, out handler))
			{
				return delegate(IDataReader r)
				{
					object val2 = r.GetValue(index);
					if (!(val2 is DBNull))
					{
						return handler.Parse(type, val2);
					}
					return null;
				};
			}
			return delegate(IDataReader r)
			{
				object val3 = r.GetValue(index);
				if (!(val3 is DBNull))
				{
					return val3;
				}
				return null;
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000827C File Offset: 0x0000647C
		private static T Parse<T>(object value)
		{
			if (value == null || value is DBNull)
			{
				return default(T);
			}
			if (value is T)
			{
				return (T)((object)value);
			}
			Type type = typeof(T);
			type = Nullable.GetUnderlyingType(type) ?? type;
			if (type.IsEnum())
			{
				if (value is float || value is double || value is decimal)
				{
					value = Convert.ChangeType(value, Enum.GetUnderlyingType(type), CultureInfo.InvariantCulture);
				}
				return (T)((object)Enum.ToObject(type, value));
			}
			SqlMapper.ITypeHandler handler;
			if (SqlMapper.typeHandlers.TryGetValue(type, out handler))
			{
				return (T)((object)handler.Parse(type, value));
			}
			return (T)((object)Convert.ChangeType(value, type, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008334 File Offset: 0x00006534
		public static SqlMapper.ITypeMap GetTypeMap(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			SqlMapper.ITypeMap map = (SqlMapper.ITypeMap)SqlMapper._typeMaps[type];
			if (map == null)
			{
				Hashtable typeMaps = SqlMapper._typeMaps;
				lock (typeMaps)
				{
					map = (SqlMapper.ITypeMap)SqlMapper._typeMaps[type];
					if (map == null)
					{
						map = SqlMapper.TypeMapProvider(type);
						SqlMapper._typeMaps[type] = map;
					}
				}
			}
			return map;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000083C4 File Offset: 0x000065C4
		public static void SetTypeMap(Type type, SqlMapper.ITypeMap map)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (map == null || map is DefaultTypeMap)
			{
				Hashtable typeMaps = SqlMapper._typeMaps;
				lock (typeMaps)
				{
					SqlMapper._typeMaps.Remove(type);
					goto IL_006E;
				}
			}
			Hashtable typeMaps2 = SqlMapper._typeMaps;
			lock (typeMaps2)
			{
				SqlMapper._typeMaps[type] = map;
			}
			IL_006E:
			SqlMapper.PurgeQueryCacheByType(type);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008464 File Offset: 0x00006664
		public static Func<IDataReader, object> GetTypeDeserializer(Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false)
		{
			return SqlMapper.TypeDeserializerCache.GetReader(type, reader, startBound, length, returnNullIfFirstMissing);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008474 File Offset: 0x00006674
		private static LocalBuilder GetTempLocal(ILGenerator il, ref Dictionary<Type, LocalBuilder> locals, Type type, bool initAndLoad)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			locals = locals ?? new Dictionary<Type, LocalBuilder>();
			LocalBuilder found;
			if (!locals.TryGetValue(type, out found))
			{
				found = il.DeclareLocal(type);
				locals.Add(type, found);
			}
			if (initAndLoad)
			{
				il.Emit(OpCodes.Ldloca, (short)found.LocalIndex);
				il.Emit(OpCodes.Initobj, type);
				il.Emit(OpCodes.Ldloca, (short)found.LocalIndex);
				il.Emit(OpCodes.Ldobj, type);
			}
			return found;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008500 File Offset: 0x00006700
		private static Func<IDataReader, object> GetTypeDeserializerImpl(Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false)
		{
			Type returnType = (type.IsValueType() ? typeof(object) : type);
			DynamicMethod dm = new DynamicMethod("Deserialize" + Guid.NewGuid().ToString(), returnType, new Type[] { typeof(IDataReader) }, type, true);
			ILGenerator il = dm.GetILGenerator();
			il.DeclareLocal(typeof(int));
			il.DeclareLocal(type);
			il.Emit(OpCodes.Ldc_I4_0);
			il.Emit(OpCodes.Stloc_0);
			if (length == -1)
			{
				length = reader.FieldCount - startBound;
			}
			if (reader.FieldCount <= startBound)
			{
				throw SqlMapper.MultiMapException(reader);
			}
			string[] names = (from i in Enumerable.Range(startBound, length)
				select reader.GetName(i)).ToArray<string>();
			SqlMapper.ITypeMap typeMap = SqlMapper.GetTypeMap(type);
			int index = startBound;
			ConstructorInfo specializedConstructor = null;
			bool supportInitialize = false;
			Dictionary<Type, LocalBuilder> structLocals = null;
			if (type.IsValueType())
			{
				il.Emit(OpCodes.Ldloca_S, 1);
				il.Emit(OpCodes.Initobj, type);
			}
			else
			{
				Type[] types = new Type[length];
				for (int k = startBound; k < startBound + length; k++)
				{
					types[k - startBound] = reader.GetFieldType(k);
				}
				ConstructorInfo explicitConstr = typeMap.FindExplicitConstructor();
				if (explicitConstr != null)
				{
					ParameterInfo[] consPs = explicitConstr.GetParameters();
					foreach (ParameterInfo p in consPs)
					{
						if (!p.ParameterType.IsValueType())
						{
							il.Emit(OpCodes.Ldnull);
						}
						else
						{
							SqlMapper.GetTempLocal(il, ref structLocals, p.ParameterType, true);
						}
					}
					il.Emit(OpCodes.Newobj, explicitConstr);
					il.Emit(OpCodes.Stloc_1);
					supportInitialize = typeof(ISupportInitialize).IsAssignableFrom(type);
					if (supportInitialize)
					{
						il.Emit(OpCodes.Ldloc_1);
						il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod("BeginInit"), null);
					}
				}
				else
				{
					ConstructorInfo ctor = typeMap.FindConstructor(names, types);
					if (ctor == null)
					{
						string proposedTypes = "(" + string.Join(", ", types.Select((Type t, int i) => t.FullName + " " + names[i]).ToArray<string>()) + ")";
						throw new InvalidOperationException(string.Concat(new string[] { "A parameterless default constructor or one matching signature ", proposedTypes, " is required for ", type.FullName, " materialization" }));
					}
					if (ctor.GetParameters().Length == 0)
					{
						il.Emit(OpCodes.Newobj, ctor);
						il.Emit(OpCodes.Stloc_1);
						supportInitialize = typeof(ISupportInitialize).IsAssignableFrom(type);
						if (supportInitialize)
						{
							il.Emit(OpCodes.Ldloc_1);
							il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod("BeginInit"), null);
						}
					}
					else
					{
						specializedConstructor = ctor;
					}
				}
			}
			il.BeginExceptionBlock();
			if (type.IsValueType())
			{
				il.Emit(OpCodes.Ldloca_S, 1);
			}
			else if (specializedConstructor == null)
			{
				il.Emit(OpCodes.Ldloc_1);
			}
			List<SqlMapper.IMemberMap> members = (SqlMapper.IsValueTuple(type) ? SqlMapper.GetValueTupleMembers(type, names) : ((specializedConstructor != null) ? names.Select((string n) => typeMap.GetConstructorParameter(specializedConstructor, n)) : names.Select((string n) => typeMap.GetMember(n))).ToList<SqlMapper.IMemberMap>());
			bool first = true;
			Label allDone = il.DefineLabel();
			int enumDeclareLocal = -1;
			int valueCopyLocal = il.DeclareLocal(typeof(object)).LocalIndex;
			bool applyNullSetting = SqlMapper.Settings.ApplyNullValues;
			foreach (SqlMapper.IMemberMap item in members)
			{
				if (item != null)
				{
					if (specializedConstructor == null)
					{
						il.Emit(OpCodes.Dup);
					}
					Label isDbNullLabel = il.DefineLabel();
					Label finishLabel = il.DefineLabel();
					il.Emit(OpCodes.Ldarg_0);
					SqlMapper.EmitInt32(il, index);
					il.Emit(OpCodes.Dup);
					il.Emit(OpCodes.Stloc_0);
					il.Emit(OpCodes.Callvirt, SqlMapper.getItem);
					il.Emit(OpCodes.Dup);
					SqlMapper.StoreLocal(il, valueCopyLocal);
					Type colType = reader.GetFieldType(index);
					Type memberType = item.MemberType;
					if (memberType == typeof(char) || memberType == typeof(char?))
					{
						il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod((memberType == typeof(char)) ? "ReadChar" : "ReadNullableChar", BindingFlags.Static | BindingFlags.Public), null);
					}
					else
					{
						il.Emit(OpCodes.Dup);
						il.Emit(OpCodes.Isinst, typeof(DBNull));
						il.Emit(OpCodes.Brtrue_S, isDbNullLabel);
						Type nullUnderlyingType = Nullable.GetUnderlyingType(memberType);
						Type unboxType = ((nullUnderlyingType != null && nullUnderlyingType.IsEnum()) ? nullUnderlyingType : memberType);
						if (unboxType.IsEnum())
						{
							Type numericType = Enum.GetUnderlyingType(unboxType);
							if (colType == typeof(string))
							{
								if (enumDeclareLocal == -1)
								{
									enumDeclareLocal = il.DeclareLocal(typeof(string)).LocalIndex;
								}
								il.Emit(OpCodes.Castclass, typeof(string));
								SqlMapper.StoreLocal(il, enumDeclareLocal);
								il.Emit(OpCodes.Ldtoken, unboxType);
								il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
								SqlMapper.LoadLocal(il, enumDeclareLocal);
								il.Emit(OpCodes.Ldc_I4_1);
								il.EmitCall(OpCodes.Call, SqlMapper.enumParse, null);
								il.Emit(OpCodes.Unbox_Any, unboxType);
							}
							else
							{
								SqlMapper.FlexibleConvertBoxedFromHeadOfStack(il, colType, unboxType, numericType);
							}
							if (nullUnderlyingType != null)
							{
								il.Emit(OpCodes.Newobj, memberType.GetConstructor(new Type[] { nullUnderlyingType }));
							}
						}
						else if (memberType.FullName == "System.Data.Linq.Binary")
						{
							il.Emit(OpCodes.Unbox_Any, typeof(byte[]));
							il.Emit(OpCodes.Newobj, memberType.GetConstructor(new Type[] { typeof(byte[]) }));
						}
						else
						{
							TypeCode dataTypeCode = TypeExtensions.GetTypeCode(colType);
							TypeCode unboxTypeCode = TypeExtensions.GetTypeCode(unboxType);
							bool hasTypeHandler;
							if ((hasTypeHandler = SqlMapper.typeHandlers.ContainsKey(unboxType)) || colType == unboxType || dataTypeCode == unboxTypeCode || dataTypeCode == TypeExtensions.GetTypeCode(nullUnderlyingType))
							{
								if (hasTypeHandler)
								{
									il.EmitCall(OpCodes.Call, typeof(SqlMapper.TypeHandlerCache<>).MakeGenericType(new Type[] { unboxType }).GetMethod("Parse"), null);
								}
								else
								{
									il.Emit(OpCodes.Unbox_Any, unboxType);
								}
							}
							else
							{
								SqlMapper.FlexibleConvertBoxedFromHeadOfStack(il, colType, nullUnderlyingType ?? unboxType, null);
								if (nullUnderlyingType != null)
								{
									il.Emit(OpCodes.Newobj, unboxType.GetConstructor(new Type[] { nullUnderlyingType }));
								}
							}
						}
					}
					if (specializedConstructor == null)
					{
						if (item.Property != null)
						{
							il.Emit(type.IsValueType() ? OpCodes.Call : OpCodes.Callvirt, DefaultTypeMap.GetPropertySetter(item.Property, type));
						}
						else
						{
							il.Emit(OpCodes.Stfld, item.Field);
						}
					}
					il.Emit(OpCodes.Br_S, finishLabel);
					il.MarkLabel(isDbNullLabel);
					if (specializedConstructor != null)
					{
						il.Emit(OpCodes.Pop);
						if (item.MemberType.IsValueType())
						{
							int localIndex = il.DeclareLocal(item.MemberType).LocalIndex;
							SqlMapper.LoadLocalAddress(il, localIndex);
							il.Emit(OpCodes.Initobj, item.MemberType);
							SqlMapper.LoadLocal(il, localIndex);
						}
						else
						{
							il.Emit(OpCodes.Ldnull);
						}
					}
					else if (applyNullSetting && (!memberType.IsValueType() || Nullable.GetUnderlyingType(memberType) != null))
					{
						il.Emit(OpCodes.Pop);
						if (memberType.IsValueType())
						{
							SqlMapper.GetTempLocal(il, ref structLocals, memberType, true);
						}
						else
						{
							il.Emit(OpCodes.Ldnull);
						}
						if (item.Property != null)
						{
							il.Emit(type.IsValueType() ? OpCodes.Call : OpCodes.Callvirt, DefaultTypeMap.GetPropertySetter(item.Property, type));
						}
						else
						{
							il.Emit(OpCodes.Stfld, item.Field);
						}
					}
					else
					{
						il.Emit(OpCodes.Pop);
						il.Emit(OpCodes.Pop);
					}
					if (first && returnNullIfFirstMissing)
					{
						il.Emit(OpCodes.Pop);
						il.Emit(OpCodes.Ldnull);
						il.Emit(OpCodes.Stloc_1);
						il.Emit(OpCodes.Br, allDone);
					}
					il.MarkLabel(finishLabel);
				}
				first = false;
				index++;
			}
			if (type.IsValueType())
			{
				il.Emit(OpCodes.Pop);
			}
			else
			{
				if (specializedConstructor != null)
				{
					il.Emit(OpCodes.Newobj, specializedConstructor);
				}
				il.Emit(OpCodes.Stloc_1);
				if (supportInitialize)
				{
					il.Emit(OpCodes.Ldloc_1);
					il.EmitCall(OpCodes.Callvirt, typeof(ISupportInitialize).GetMethod("EndInit"), null);
				}
			}
			il.MarkLabel(allDone);
			il.BeginCatchBlock(typeof(Exception));
			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ldarg_0);
			SqlMapper.LoadLocal(il, valueCopyLocal);
			il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod("ThrowDataException"), null);
			il.EndExceptionBlock();
			il.Emit(OpCodes.Ldloc_1);
			if (type.IsValueType())
			{
				il.Emit(OpCodes.Box, type);
			}
			il.Emit(OpCodes.Ret);
			Type funcType = Expression.GetFuncType(new Type[]
			{
				typeof(IDataReader),
				returnType
			});
			return (Func<IDataReader, object>)dm.CreateDelegate(funcType);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008F74 File Offset: 0x00007174
		private static void FlexibleConvertBoxedFromHeadOfStack(ILGenerator il, Type from, Type to, Type via)
		{
			if (from == (via ?? to))
			{
				il.Emit(OpCodes.Unbox_Any, to);
				return;
			}
			MethodInfo op;
			if ((op = SqlMapper.GetOperator(from, to)) != null)
			{
				il.Emit(OpCodes.Unbox_Any, from);
				il.Emit(OpCodes.Call, op);
				return;
			}
			bool handled = false;
			OpCode opCode = default(OpCode);
			TypeCode typeCode = TypeExtensions.GetTypeCode(from);
			if (typeCode == TypeCode.Boolean || typeCode - TypeCode.SByte <= 9)
			{
				handled = true;
				switch (TypeExtensions.GetTypeCode(via ?? to))
				{
				case TypeCode.Boolean:
				case TypeCode.Int32:
					opCode = OpCodes.Conv_Ovf_I4;
					goto IL_00FE;
				case TypeCode.SByte:
					opCode = OpCodes.Conv_Ovf_I1;
					goto IL_00FE;
				case TypeCode.Byte:
					opCode = OpCodes.Conv_Ovf_I1_Un;
					goto IL_00FE;
				case TypeCode.Int16:
					opCode = OpCodes.Conv_Ovf_I2;
					goto IL_00FE;
				case TypeCode.UInt16:
					opCode = OpCodes.Conv_Ovf_I2_Un;
					goto IL_00FE;
				case TypeCode.UInt32:
					opCode = OpCodes.Conv_Ovf_I4_Un;
					goto IL_00FE;
				case TypeCode.Int64:
					opCode = OpCodes.Conv_Ovf_I8;
					goto IL_00FE;
				case TypeCode.UInt64:
					opCode = OpCodes.Conv_Ovf_I8_Un;
					goto IL_00FE;
				case TypeCode.Single:
					opCode = OpCodes.Conv_R4;
					goto IL_00FE;
				case TypeCode.Double:
					opCode = OpCodes.Conv_R8;
					goto IL_00FE;
				}
				handled = false;
			}
			IL_00FE:
			if (handled)
			{
				il.Emit(OpCodes.Unbox_Any, from);
				il.Emit(opCode);
				if (to == typeof(bool))
				{
					il.Emit(OpCodes.Ldc_I4_0);
					il.Emit(OpCodes.Ceq);
					il.Emit(OpCodes.Ldc_I4_0);
					il.Emit(OpCodes.Ceq);
					return;
				}
			}
			else
			{
				il.Emit(OpCodes.Ldtoken, via ?? to);
				il.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);
				il.EmitCall(OpCodes.Call, typeof(Convert).GetMethod("ChangeType", new Type[]
				{
					typeof(object),
					typeof(Type)
				}), null);
				il.Emit(OpCodes.Unbox_Any, to);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009154 File Offset: 0x00007354
		private static MethodInfo GetOperator(Type from, Type to)
		{
			if (to == null)
			{
				return null;
			}
			MethodInfo[] fromMethods;
			MethodInfo methodInfo;
			MethodInfo[] toMethods;
			if ((methodInfo = SqlMapper.ResolveOperator(fromMethods = from.GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")) == null && (methodInfo = SqlMapper.ResolveOperator(toMethods = to.GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")) == null)
			{
				methodInfo = SqlMapper.ResolveOperator(fromMethods, from, to, "op_Explicit") ?? SqlMapper.ResolveOperator(toMethods, from, to, "op_Explicit");
			}
			return methodInfo;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000091C0 File Offset: 0x000073C0
		private static MethodInfo ResolveOperator(MethodInfo[] methods, Type from, Type to, string name)
		{
			for (int i = 0; i < methods.Length; i++)
			{
				if (!(methods[i].Name != name) && !(methods[i].ReturnType != to))
				{
					ParameterInfo[] args = methods[i].GetParameters();
					if (args.Length == 1 && !(args[0].ParameterType != from))
					{
						return methods[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009220 File Offset: 0x00007420
		private static void LoadLocal(ILGenerator il, int index)
		{
			if (index < 0 || index >= 32767)
			{
				throw new ArgumentNullException("index");
			}
			switch (index)
			{
			case 0:
				il.Emit(OpCodes.Ldloc_0);
				return;
			case 1:
				il.Emit(OpCodes.Ldloc_1);
				return;
			case 2:
				il.Emit(OpCodes.Ldloc_2);
				return;
			case 3:
				il.Emit(OpCodes.Ldloc_3);
				return;
			default:
				if (index <= 255)
				{
					il.Emit(OpCodes.Ldloc_S, (byte)index);
					return;
				}
				il.Emit(OpCodes.Ldloc, (short)index);
				return;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000092B0 File Offset: 0x000074B0
		private static void StoreLocal(ILGenerator il, int index)
		{
			if (index < 0 || index >= 32767)
			{
				throw new ArgumentNullException("index");
			}
			switch (index)
			{
			case 0:
				il.Emit(OpCodes.Stloc_0);
				return;
			case 1:
				il.Emit(OpCodes.Stloc_1);
				return;
			case 2:
				il.Emit(OpCodes.Stloc_2);
				return;
			case 3:
				il.Emit(OpCodes.Stloc_3);
				return;
			default:
				if (index <= 255)
				{
					il.Emit(OpCodes.Stloc_S, (byte)index);
					return;
				}
				il.Emit(OpCodes.Stloc, (short)index);
				return;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000933F File Offset: 0x0000753F
		private static void LoadLocalAddress(ILGenerator il, int index)
		{
			if (index < 0 || index >= 32767)
			{
				throw new ArgumentNullException("index");
			}
			if (index <= 255)
			{
				il.Emit(OpCodes.Ldloca_S, (byte)index);
				return;
			}
			il.Emit(OpCodes.Ldloca, (short)index);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000937C File Offset: 0x0000757C
		[Obsolete("This method is for internal use only", false)]
		public static void ThrowDataException(Exception ex, int index, IDataReader reader, object value)
		{
			Exception toThrow;
			try
			{
				string name = "(n/a)";
				string formattedValue = "(n/a)";
				if (reader != null && index >= 0 && index < reader.FieldCount)
				{
					name = reader.GetName(index);
					try
					{
						if (value == null || value is DBNull)
						{
							formattedValue = "<null>";
						}
						else
						{
							formattedValue = Convert.ToString(value) + " - " + TypeExtensions.GetTypeCode(value.GetType());
						}
					}
					catch (Exception valEx)
					{
						formattedValue = valEx.Message;
					}
				}
				toThrow = new DataException(string.Format("Error parsing column {0} ({1}={2})", index, name, formattedValue), ex);
			}
			catch
			{
				toThrow = new DataException(ex.Message, ex);
			}
			throw toThrow;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009438 File Offset: 0x00007638
		private static void EmitInt32(ILGenerator il, int value)
		{
			switch (value)
			{
			case -1:
				il.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				il.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				il.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				il.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				il.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				il.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				il.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				il.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				il.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				il.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				if (value >= -128 && value <= 127)
				{
					il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
					return;
				}
				il.Emit(OpCodes.Ldc_I4, value);
				return;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00009513 File Offset: 0x00007713
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000951A File Offset: 0x0000771A
		public static IEqualityComparer<string> ConnectionStringComparer
		{
			get
			{
				return SqlMapper.connectionStringComparer;
			}
			set
			{
				SqlMapper.connectionStringComparer = value ?? StringComparer.Ordinal;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000952B File Offset: 0x0000772B
		public static SqlMapper.ICustomQueryParameter AsTableValuedParameter(this DataTable table, string typeName = null)
		{
			return new TableValuedParameter(table, typeName);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009534 File Offset: 0x00007734
		public static void SetTypeName(this DataTable table, string typeName)
		{
			if (table != null)
			{
				if (string.IsNullOrEmpty(typeName))
				{
					table.ExtendedProperties.Remove("dapper:TypeName");
					return;
				}
				table.ExtendedProperties["dapper:TypeName"] = typeName;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009563 File Offset: 0x00007763
		public static string GetTypeName(this DataTable table)
		{
			return ((table != null) ? table.ExtendedProperties["dapper:TypeName"] : null) as string;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009580 File Offset: 0x00007780
		public static SqlMapper.ICustomQueryParameter AsTableValuedParameter(this IEnumerable<SqlDataRecord> list, string typeName = null)
		{
			return new SqlDataRecordListTVPParameter(list, typeName);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000958C File Offset: 0x0000778C
		private static StringBuilder GetStringBuilder()
		{
			StringBuilder tmp = SqlMapper.perThreadStringBuilderCache;
			if (tmp != null)
			{
				SqlMapper.perThreadStringBuilderCache = null;
				tmp.Length = 0;
				return tmp;
			}
			return new StringBuilder();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000095B8 File Offset: 0x000077B8
		private static string __ToStringRecycle(this StringBuilder obj)
		{
			if (obj == null)
			{
				return "";
			}
			string s = obj.ToString();
			SqlMapper.perThreadStringBuilderCache = SqlMapper.perThreadStringBuilderCache ?? obj;
			return s;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000095E5 File Offset: 0x000077E5
		public static IEnumerable<T> Parse<T>(this IDataReader reader)
		{
			if (reader.Read())
			{
				Type effectiveType = typeof(T);
				Func<IDataReader, object> deser = SqlMapper.GetDeserializer(effectiveType, reader, 0, -1, false);
				Type convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
				do
				{
					object val = deser(reader);
					if (val == null || val is T)
					{
						yield return (T)((object)val);
					}
					else
					{
						yield return (T)((object)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
					}
				}
				while (reader.Read());
				deser = null;
				convertToType = null;
			}
			yield break;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000095F5 File Offset: 0x000077F5
		public static IEnumerable<object> Parse(this IDataReader reader, Type type)
		{
			if (reader.Read())
			{
				Func<IDataReader, object> deser = SqlMapper.GetDeserializer(type, reader, 0, -1, false);
				do
				{
					yield return deser(reader);
				}
				while (reader.Read());
				deser = null;
			}
			yield break;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000960C File Offset: 0x0000780C
		[return: Dynamic(new bool[] { false, true })]
		public static IEnumerable<dynamic> Parse(this IDataReader reader)
		{
			if (reader.Read())
			{
				Func<IDataReader, object> deser = SqlMapper.GetDapperRowDeserializer(reader, 0, -1, false);
				do
				{
					yield return deser(reader);
				}
				while (reader.Read());
				deser = null;
			}
			yield break;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000961C File Offset: 0x0000781C
		public static Func<IDataReader, object> GetRowParser(this IDataReader reader, Type type, int startIndex = 0, int length = -1, bool returnNullIfFirstMissing = false)
		{
			return SqlMapper.GetDeserializer(type, reader, startIndex, length, returnNullIfFirstMissing);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000962C File Offset: 0x0000782C
		public static Func<IDataReader, T> GetRowParser<T>(this IDataReader reader, Type concreteType = null, int startIndex = 0, int length = -1, bool returnNullIfFirstMissing = false)
		{
			concreteType = concreteType ?? typeof(T);
			Func<IDataReader, object> func = SqlMapper.GetDeserializer(concreteType, reader, startIndex, length, returnNullIfFirstMissing);
			if (concreteType.IsValueType())
			{
				return (IDataReader _) => (T)((object)func(_));
			}
			return (Func<IDataReader, T>)func;
		}

		// Token: 0x04000033 RID: 51
		private static readonly ConcurrentDictionary<SqlMapper.Identity, SqlMapper.CacheInfo> _queryCache = new ConcurrentDictionary<SqlMapper.Identity, SqlMapper.CacheInfo>();

		// Token: 0x04000034 RID: 52
		private const int COLLECT_PER_ITEMS = 1000;

		// Token: 0x04000035 RID: 53
		private const int COLLECT_HIT_COUNT_MIN = 0;

		// Token: 0x04000036 RID: 54
		private static int collect;

		// Token: 0x04000037 RID: 55
		private static Dictionary<Type, DbType> typeMap;

		// Token: 0x04000038 RID: 56
		private static Dictionary<Type, SqlMapper.ITypeHandler> typeHandlers;

		// Token: 0x04000039 RID: 57
		internal const string LinqBinary = "System.Data.Linq.Binary";

		// Token: 0x0400003A RID: 58
		private const string ObsoleteInternalUsageOnly = "This method is for internal use only";

		// Token: 0x0400003B RID: 59
		private static readonly int[] ErrTwoRows = new int[2];

		// Token: 0x0400003C RID: 60
		private static readonly int[] ErrZeroRows = new int[0];

		// Token: 0x0400003D RID: 61
		private static readonly Regex smellsLikeOleDb = new Regex("(?<![\\p{L}\\p{N}@_])[?@:](?![\\p{L}\\p{N}@_])", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400003E RID: 62
		private static readonly Regex literalTokens = new Regex("(?<![\\p{L}\\p{N}_])\\{=([\\p{L}\\p{N}_]+)\\}", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400003F RID: 63
		private static readonly Regex pseudoPositional = new Regex("\\?([\\p{L}_][\\p{L}\\p{N}_]*)\\?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000040 RID: 64
		internal static readonly MethodInfo format = typeof(SqlMapper).GetMethod("Format", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x04000041 RID: 65
		private static readonly Dictionary<TypeCode, MethodInfo> toStrings = new Type[]
		{
			typeof(bool),
			typeof(sbyte),
			typeof(byte),
			typeof(ushort),
			typeof(short),
			typeof(uint),
			typeof(int),
			typeof(ulong),
			typeof(long),
			typeof(float),
			typeof(double),
			typeof(decimal)
		}.ToDictionary((Type x) => TypeExtensions.GetTypeCode(x), (Type x) => x.GetPublicInstanceMethod("ToString", new Type[] { typeof(IFormatProvider) }));

		// Token: 0x04000042 RID: 66
		private static readonly MethodInfo StringReplace = typeof(string).GetPublicInstanceMethod("Replace", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04000043 RID: 67
		private static readonly MethodInfo InvariantCulture = typeof(CultureInfo).GetProperty("InvariantCulture", BindingFlags.Static | BindingFlags.Public).GetGetMethod();

		// Token: 0x04000044 RID: 68
		private static readonly MethodInfo enumParse = typeof(Enum).GetMethod("Parse", new Type[]
		{
			typeof(Type),
			typeof(string),
			typeof(bool)
		});

		// Token: 0x04000045 RID: 69
		private static readonly MethodInfo getItem = (from p in typeof(IDataRecord).GetProperties(BindingFlags.Instance | BindingFlags.Public)
			where p.GetIndexParameters().Length != 0 && p.GetIndexParameters()[0].ParameterType == typeof(int)
			select p.GetGetMethod()).First<MethodInfo>();

		// Token: 0x04000046 RID: 70
		public static Func<Type, SqlMapper.ITypeMap> TypeMapProvider = (Type type) => new DefaultTypeMap(type);

		// Token: 0x04000047 RID: 71
		private static readonly Hashtable _typeMaps = new Hashtable();

		// Token: 0x04000048 RID: 72
		private static IEqualityComparer<string> connectionStringComparer = StringComparer.Ordinal;

		// Token: 0x04000049 RID: 73
		private const string DataTableTypeNameKey = "dapper:TypeName";

		// Token: 0x0400004A RID: 74
		[ThreadStatic]
		private static StringBuilder perThreadStringBuilderCache;

		// Token: 0x02000021 RID: 33
		private struct AsyncExecState
		{
			// Token: 0x06000194 RID: 404 RVA: 0x00009F12 File Offset: 0x00008112
			public AsyncExecState(DbCommand command, Task<int> task)
			{
				this.Command = command;
				this.Task = task;
			}

			// Token: 0x04000072 RID: 114
			public readonly DbCommand Command;

			// Token: 0x04000073 RID: 115
			public readonly Task<int> Task;
		}

		// Token: 0x02000022 RID: 34
		private class CacheInfo
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000195 RID: 405 RVA: 0x00009F22 File Offset: 0x00008122
			// (set) Token: 0x06000196 RID: 406 RVA: 0x00009F2A File Offset: 0x0000812A
			public SqlMapper.DeserializerState Deserializer { get; set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00009F33 File Offset: 0x00008133
			// (set) Token: 0x06000198 RID: 408 RVA: 0x00009F3B File Offset: 0x0000813B
			public Func<IDataReader, object>[] OtherDeserializers { get; set; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000199 RID: 409 RVA: 0x00009F44 File Offset: 0x00008144
			// (set) Token: 0x0600019A RID: 410 RVA: 0x00009F4C File Offset: 0x0000814C
			public Action<IDbCommand, object> ParamReader { get; set; }

			// Token: 0x0600019B RID: 411 RVA: 0x00009F55 File Offset: 0x00008155
			public int GetHitCount()
			{
				return Interlocked.CompareExchange(ref this.hitCount, 0, 0);
			}

			// Token: 0x0600019C RID: 412 RVA: 0x00009F64 File Offset: 0x00008164
			public void RecordHit()
			{
				Interlocked.Increment(ref this.hitCount);
			}

			// Token: 0x04000077 RID: 119
			private int hitCount;
		}

		// Token: 0x02000023 RID: 35
		private class PropertyInfoByNameComparer : IComparer<PropertyInfo>
		{
			// Token: 0x0600019E RID: 414 RVA: 0x00009F7A File Offset: 0x0000817A
			public int Compare(PropertyInfo x, PropertyInfo y)
			{
				return string.CompareOrdinal(x.Name, y.Name);
			}
		}

		// Token: 0x02000024 RID: 36
		[Flags]
		internal enum Row
		{
			// Token: 0x04000079 RID: 121
			First = 0,
			// Token: 0x0400007A RID: 122
			FirstOrDefault = 1,
			// Token: 0x0400007B RID: 123
			Single = 2,
			// Token: 0x0400007C RID: 124
			SingleOrDefault = 3
		}

		// Token: 0x02000025 RID: 37
		private sealed class DapperRow : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IReadOnlyDictionary<string, object>, IReadOnlyCollection<KeyValuePair<string, object>>
		{
			// Token: 0x060001A0 RID: 416 RVA: 0x00009F95 File Offset: 0x00008195
			public DapperRow(SqlMapper.DapperTable table, object[] values)
			{
				if (table == null)
				{
					throw new ArgumentNullException("table");
				}
				this.table = table;
				if (values == null)
				{
					throw new ArgumentNullException("values");
				}
				this.values = values;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009FCC File Offset: 0x000081CC
			int ICollection<KeyValuePair<string, object>>.Count
			{
				get
				{
					int count = 0;
					for (int i = 0; i < this.values.Length; i++)
					{
						if (!(this.values[i] is SqlMapper.DapperRow.DeadValue))
						{
							count++;
						}
					}
					return count;
				}
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x0000A004 File Offset: 0x00008204
			public bool TryGetValue(string key, out object value)
			{
				int index = this.table.IndexOfName(key);
				if (index < 0)
				{
					value = null;
					return false;
				}
				value = ((index < this.values.Length) ? this.values[index] : null);
				if (value is SqlMapper.DapperRow.DeadValue)
				{
					value = null;
					return false;
				}
				return true;
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x0000A050 File Offset: 0x00008250
			public override string ToString()
			{
				StringBuilder sb = SqlMapper.GetStringBuilder().Append("{DapperRow");
				foreach (KeyValuePair<string, object> kv in this)
				{
					object value = kv.Value;
					sb.Append(", ").Append(kv.Key);
					if (value != null)
					{
						sb.Append(" = '").Append(kv.Value).Append('\'');
					}
					else
					{
						sb.Append(" = NULL");
					}
				}
				return sb.Append('}').__ToStringRecycle();
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x0000A100 File Offset: 0x00008300
			DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
			{
				return new SqlMapper.DapperRowMetaObject(parameter, BindingRestrictions.Empty, this);
			}

			// Token: 0x060001A5 RID: 421 RVA: 0x0000A10E File Offset: 0x0000830E
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				string[] names = this.table.FieldNames;
				int num;
				for (int i = 0; i < names.Length; i = num + 1)
				{
					object value = ((i < this.values.Length) ? this.values[i] : null);
					if (!(value is SqlMapper.DapperRow.DeadValue))
					{
						yield return new KeyValuePair<string, object>(names[i], value);
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x0000A11D File Offset: 0x0000831D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x0000A128 File Offset: 0x00008328
			void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
			{
				((IDictionary<string, object>)this).Add(item.Key, item.Value);
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x0000A14C File Offset: 0x0000834C
			void ICollection<KeyValuePair<string, object>>.Clear()
			{
				for (int i = 0; i < this.values.Length; i++)
				{
					this.values[i] = SqlMapper.DapperRow.DeadValue.Default;
				}
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x0000A17C File Offset: 0x0000837C
			bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
			{
				object value;
				return this.TryGetValue(item.Key, out value) && object.Equals(value, item.Value);
			}

			// Token: 0x060001AA RID: 426 RVA: 0x0000A1AC File Offset: 0x000083AC
			void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
			{
				foreach (KeyValuePair<string, object> kv in this)
				{
					array[arrayIndex++] = kv;
				}
			}

			// Token: 0x060001AB RID: 427 RVA: 0x0000A1FC File Offset: 0x000083FC
			bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
			{
				return ((IDictionary<string, object>)this).Remove(item.Key);
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A218 File Offset: 0x00008418
			bool ICollection<KeyValuePair<string, object>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060001AD RID: 429 RVA: 0x0000A21C File Offset: 0x0000841C
			bool IDictionary<string, object>.ContainsKey(string key)
			{
				int index = this.table.IndexOfName(key);
				return index >= 0 && index < this.values.Length && !(this.values[index] is SqlMapper.DapperRow.DeadValue);
			}

			// Token: 0x060001AE RID: 430 RVA: 0x0000A257 File Offset: 0x00008457
			void IDictionary<string, object>.Add(string key, object value)
			{
				this.SetValue(key, value, true);
			}

			// Token: 0x060001AF RID: 431 RVA: 0x0000A264 File Offset: 0x00008464
			bool IDictionary<string, object>.Remove(string key)
			{
				int index = this.table.IndexOfName(key);
				if (index < 0 || index >= this.values.Length || this.values[index] is SqlMapper.DapperRow.DeadValue)
				{
					return false;
				}
				this.values[index] = SqlMapper.DapperRow.DeadValue.Default;
				return true;
			}

			// Token: 0x17000036 RID: 54
			object IDictionary<string, object>.this[string key]
			{
				get
				{
					object val;
					this.TryGetValue(key, out val);
					return val;
				}
				set
				{
					this.SetValue(key, value, false);
				}
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x0000A2D0 File Offset: 0x000084D0
			public object SetValue(string key, object value)
			{
				return this.SetValue(key, value, false);
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x0000A2DC File Offset: 0x000084DC
			private object SetValue(string key, object value, bool isAdd)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int index = this.table.IndexOfName(key);
				if (index < 0)
				{
					index = this.table.AddField(key);
				}
				else if (isAdd && index < this.values.Length && !(this.values[index] is SqlMapper.DapperRow.DeadValue))
				{
					throw new ArgumentException("An item with the same key has already been added", "key");
				}
				int oldLength = this.values.Length;
				if (oldLength <= index)
				{
					Array.Resize<object>(ref this.values, this.table.FieldCount);
					for (int i = oldLength; i < this.values.Length; i++)
					{
						this.values[i] = SqlMapper.DapperRow.DeadValue.Default;
					}
				}
				this.values[index] = value;
				return value;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000A393 File Offset: 0x00008593
			ICollection<string> IDictionary<string, object>.Keys
			{
				get
				{
					return this.Select((KeyValuePair<string, object> kv) => kv.Key).ToArray<string>();
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000A3BF File Offset: 0x000085BF
			ICollection<object> IDictionary<string, object>.Values
			{
				get
				{
					return this.Select((KeyValuePair<string, object> kv) => kv.Value).ToArray<object>();
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000A3EB File Offset: 0x000085EB
			int IReadOnlyCollection<KeyValuePair<string, object>>.Count
			{
				get
				{
					return this.values.Count((object t) => !(t is SqlMapper.DapperRow.DeadValue));
				}
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x0000A418 File Offset: 0x00008618
			bool IReadOnlyDictionary<string, object>.ContainsKey(string key)
			{
				int index = this.table.IndexOfName(key);
				return index >= 0 && index < this.values.Length && !(this.values[index] is SqlMapper.DapperRow.DeadValue);
			}

			// Token: 0x1700003A RID: 58
			object IReadOnlyDictionary<string, object>.this[string key]
			{
				get
				{
					object val;
					this.TryGetValue(key, out val);
					return val;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A470 File Offset: 0x00008670
			IEnumerable<string> IReadOnlyDictionary<string, object>.Keys
			{
				get
				{
					return this.Select((KeyValuePair<string, object> kv) => kv.Key);
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A497 File Offset: 0x00008697
			IEnumerable<object> IReadOnlyDictionary<string, object>.Values
			{
				get
				{
					return this.Select((KeyValuePair<string, object> kv) => kv.Value);
				}
			}

			// Token: 0x0400007D RID: 125
			private readonly SqlMapper.DapperTable table;

			// Token: 0x0400007E RID: 126
			private object[] values;

			// Token: 0x02000059 RID: 89
			private sealed class DeadValue
			{
				// Token: 0x060002D0 RID: 720 RVA: 0x0000F76E File Offset: 0x0000D96E
				private DeadValue()
				{
				}

				// Token: 0x0400019C RID: 412
				public static readonly SqlMapper.DapperRow.DeadValue Default = new SqlMapper.DapperRow.DeadValue();
			}
		}

		// Token: 0x02000026 RID: 38
		private sealed class DapperRowMetaObject : DynamicMetaObject
		{
			// Token: 0x060001BB RID: 443 RVA: 0x0000A4BE File Offset: 0x000086BE
			public DapperRowMetaObject(Expression expression, BindingRestrictions restrictions)
				: base(expression, restrictions)
			{
			}

			// Token: 0x060001BC RID: 444 RVA: 0x0000A4C8 File Offset: 0x000086C8
			public DapperRowMetaObject(Expression expression, BindingRestrictions restrictions, object value)
				: base(expression, restrictions, value)
			{
			}

			// Token: 0x060001BD RID: 445 RVA: 0x0000A4D4 File Offset: 0x000086D4
			private DynamicMetaObject CallMethod(MethodInfo method, Expression[] parameters)
			{
				return new DynamicMetaObject(Expression.Call(Expression.Convert(base.Expression, base.LimitType), method, parameters), BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType));
			}

			// Token: 0x060001BE RID: 446 RVA: 0x0000A514 File Offset: 0x00008714
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				Expression[] parameters = new Expression[] { Expression.Constant(binder.Name) };
				return this.CallMethod(SqlMapper.DapperRowMetaObject.getValueMethod, parameters);
			}

			// Token: 0x060001BF RID: 447 RVA: 0x0000A544 File Offset: 0x00008744
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				Expression[] parameters = new Expression[] { Expression.Constant(binder.Name) };
				return this.CallMethod(SqlMapper.DapperRowMetaObject.getValueMethod, parameters);
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x0000A574 File Offset: 0x00008774
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				Expression[] parameters = new Expression[]
				{
					Expression.Constant(binder.Name),
					value.Expression
				};
				return this.CallMethod(SqlMapper.DapperRowMetaObject.setValueMethod, parameters);
			}

			// Token: 0x0400007F RID: 127
			private static readonly MethodInfo getValueMethod = typeof(IDictionary<string, object>).GetProperty("Item").GetGetMethod();

			// Token: 0x04000080 RID: 128
			private static readonly MethodInfo setValueMethod = typeof(SqlMapper.DapperRow).GetMethod("SetValue", new Type[]
			{
				typeof(string),
				typeof(object)
			});
		}

		// Token: 0x02000027 RID: 39
		private sealed class DapperTable
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000A614 File Offset: 0x00008814
			internal string[] FieldNames
			{
				get
				{
					return this.fieldNames;
				}
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x0000A61C File Offset: 0x0000881C
			public DapperTable(string[] fieldNames)
			{
				if (fieldNames == null)
				{
					throw new ArgumentNullException("fieldNames");
				}
				this.fieldNames = fieldNames;
				this.fieldNameLookup = new Dictionary<string, int>(fieldNames.Length, StringComparer.Ordinal);
				for (int i = fieldNames.Length - 1; i >= 0; i--)
				{
					string key = fieldNames[i];
					if (key != null)
					{
						this.fieldNameLookup[key] = i;
					}
				}
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x0000A67C File Offset: 0x0000887C
			internal int IndexOfName(string name)
			{
				int result;
				if (name == null || !this.fieldNameLookup.TryGetValue(name, out result))
				{
					return -1;
				}
				return result;
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x0000A6A0 File Offset: 0x000088A0
			internal int AddField(string name)
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				if (this.fieldNameLookup.ContainsKey(name))
				{
					throw new InvalidOperationException("Field already exists: " + name);
				}
				int oldLen = this.fieldNames.Length;
				Array.Resize<string>(ref this.fieldNames, oldLen + 1);
				this.fieldNames[oldLen] = name;
				this.fieldNameLookup[name] = oldLen;
				return oldLen;
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x0000A708 File Offset: 0x00008908
			internal bool FieldExists(string key)
			{
				return key != null && this.fieldNameLookup.ContainsKey(key);
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A71B File Offset: 0x0000891B
			public int FieldCount
			{
				get
				{
					return this.fieldNames.Length;
				}
			}

			// Token: 0x04000081 RID: 129
			private string[] fieldNames;

			// Token: 0x04000082 RID: 130
			private readonly Dictionary<string, int> fieldNameLookup;
		}

		// Token: 0x02000028 RID: 40
		private struct DeserializerState
		{
			// Token: 0x060001C8 RID: 456 RVA: 0x0000A725 File Offset: 0x00008925
			public DeserializerState(int hash, Func<IDataReader, object> func)
			{
				this.Hash = hash;
				this.Func = func;
			}

			// Token: 0x04000083 RID: 131
			public readonly int Hash;

			// Token: 0x04000084 RID: 132
			public readonly Func<IDataReader, object> Func;
		}

		// Token: 0x02000029 RID: 41
		private class DontMap
		{
		}

		// Token: 0x0200002A RID: 42
		public class GridReader : IDisposable
		{
			// Token: 0x060001CA RID: 458 RVA: 0x0000A73D File Offset: 0x0000893D
			internal GridReader(IDbCommand command, IDataReader reader, SqlMapper.Identity identity, DynamicParameters dynamicParams, bool addToCache, CancellationToken cancel)
				: this(command, reader, identity, dynamicParams, addToCache)
			{
				this.cancel = cancel;
			}

			// Token: 0x060001CB RID: 459 RVA: 0x0000A754 File Offset: 0x00008954
			[return: Dynamic(new bool[] { false, false, true })]
			public Task<IEnumerable<dynamic>> ReadAsync(bool buffered = true)
			{
				return this.ReadAsyncImpl<object>(typeof(SqlMapper.DapperRow), buffered);
			}

			// Token: 0x060001CC RID: 460 RVA: 0x0000A767 File Offset: 0x00008967
			[return: Dynamic(new bool[] { false, true })]
			public Task<dynamic> ReadFirstAsync()
			{
				return this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.First);
			}

			// Token: 0x060001CD RID: 461 RVA: 0x0000A77A File Offset: 0x0000897A
			[return: Dynamic(new bool[] { false, true })]
			public Task<dynamic> ReadFirstOrDefaultAsync()
			{
				return this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.FirstOrDefault);
			}

			// Token: 0x060001CE RID: 462 RVA: 0x0000A78D File Offset: 0x0000898D
			[return: Dynamic(new bool[] { false, true })]
			public Task<dynamic> ReadSingleAsync()
			{
				return this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.Single);
			}

			// Token: 0x060001CF RID: 463 RVA: 0x0000A7A0 File Offset: 0x000089A0
			[return: Dynamic(new bool[] { false, true })]
			public Task<dynamic> ReadSingleOrDefaultAsync()
			{
				return this.ReadRowAsyncImpl<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.SingleOrDefault);
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x0000A7B3 File Offset: 0x000089B3
			public Task<IEnumerable<object>> ReadAsync(Type type, bool buffered = true)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadAsyncImpl<object>(type, buffered);
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x0000A7D1 File Offset: 0x000089D1
			public Task<object> ReadFirstAsync(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.First);
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x0000A7EF File Offset: 0x000089EF
			public Task<object> ReadFirstOrDefaultAsync(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.FirstOrDefault);
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x0000A80D File Offset: 0x00008A0D
			public Task<object> ReadSingleAsync(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.Single);
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x0000A82B File Offset: 0x00008A2B
			public Task<object> ReadSingleOrDefaultAsync(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRowAsyncImpl<object>(type, SqlMapper.Row.SingleOrDefault);
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x0000A849 File Offset: 0x00008A49
			public Task<IEnumerable<T>> ReadAsync<T>(bool buffered = true)
			{
				return this.ReadAsyncImpl<T>(typeof(T), buffered);
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x0000A85C File Offset: 0x00008A5C
			public Task<T> ReadFirstAsync<T>()
			{
				return this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.First);
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x0000A86F File Offset: 0x00008A6F
			public Task<T> ReadFirstOrDefaultAsync<T>()
			{
				return this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.FirstOrDefault);
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x0000A882 File Offset: 0x00008A82
			public Task<T> ReadSingleAsync<T>()
			{
				return this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.Single);
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x0000A895 File Offset: 0x00008A95
			public Task<T> ReadSingleOrDefaultAsync<T>()
			{
				return this.ReadRowAsyncImpl<T>(typeof(T), SqlMapper.Row.SingleOrDefault);
			}

			// Token: 0x060001DA RID: 474 RVA: 0x0000A8A8 File Offset: 0x00008AA8
			private async Task NextResultAsync()
			{
				bool flag = await ((DbDataReader)this.reader).NextResultAsync(this.cancel).ConfigureAwait(false);
				if (flag)
				{
					this.readCount++;
					this.gridIndex++;
					this.IsConsumed = false;
				}
				else
				{
					this.reader.Dispose();
					this.reader = null;
					SqlMapper.IParameterCallbacks parameterCallbacks = this.callbacks;
					if (parameterCallbacks != null)
					{
						parameterCallbacks.OnCompleted();
					}
					this.Dispose();
				}
			}

			// Token: 0x060001DB RID: 475 RVA: 0x0000A8F0 File Offset: 0x00008AF0
			private Task<IEnumerable<T>> ReadAsyncImpl<T>(Type type, bool buffered)
			{
				if (this.reader == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
				}
				if (this.IsConsumed)
				{
					throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
				}
				SqlMapper.Identity typedIdentity = this.identity.ForGrid(type, this.gridIndex);
				SqlMapper.CacheInfo cache = SqlMapper.GetCacheInfo(typedIdentity, null, this.addToCache);
				SqlMapper.DeserializerState deserializer = cache.Deserializer;
				int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
				if (deserializer.Func == null || deserializer.Hash != hash)
				{
					deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, this.reader, 0, -1, false));
					cache.Deserializer = deserializer;
				}
				this.IsConsumed = true;
				if (buffered && this.reader is DbDataReader)
				{
					return this.ReadBufferedAsync<T>(this.gridIndex, deserializer.Func);
				}
				IEnumerable<T> result = this.ReadDeferred<T>(this.gridIndex, deserializer.Func, type);
				if (buffered)
				{
					result = result.ToList<T>();
				}
				return Task.FromResult<IEnumerable<T>>(result);
			}

			// Token: 0x060001DC RID: 476 RVA: 0x0000A9E8 File Offset: 0x00008BE8
			private Task<T> ReadRowAsyncImpl<T>(Type type, SqlMapper.Row row)
			{
				DbDataReader dbReader;
				if ((dbReader = this.reader as DbDataReader) != null)
				{
					return this.ReadRowAsyncImplViaDbReader<T>(dbReader, type, row);
				}
				return Task.FromResult<T>(this.ReadRow<T>(type, row));
			}

			// Token: 0x060001DD RID: 477 RVA: 0x0000AA1C File Offset: 0x00008C1C
			private async Task<T> ReadRowAsyncImplViaDbReader<T>(DbDataReader reader, Type type, SqlMapper.Row row)
			{
				if (reader == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
				}
				if (this.IsConsumed)
				{
					throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
				}
				this.IsConsumed = true;
				T result = default(T);
				bool flag = await reader.ReadAsync(this.cancel).ConfigureAwait(false);
				if (flag && reader.FieldCount != 0)
				{
					SqlMapper.CacheInfo cache = SqlMapper.GetCacheInfo(this.identity.ForGrid(type, this.gridIndex), null, this.addToCache);
					SqlMapper.DeserializerState deserializer = cache.Deserializer;
					int hash = SqlMapper.GetColumnHash(reader, 0, -1);
					if (deserializer.Func == null || deserializer.Hash != hash)
					{
						deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, reader, 0, -1, false));
						cache.Deserializer = deserializer;
					}
					result = (T)((object)deserializer.Func(reader));
					bool flag2 = (row & SqlMapper.Row.Single) > SqlMapper.Row.First;
					if (flag2)
					{
						flag2 = await reader.ReadAsync(this.cancel).ConfigureAwait(false);
					}
					if (flag2)
					{
						SqlMapper.ThrowMultipleRows(row);
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					do
					{
						configuredTaskAwaiter = reader.ReadAsync(this.cancel).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
					}
					while (configuredTaskAwaiter.GetResult());
				}
				else if ((row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
				{
					SqlMapper.ThrowZeroRows(row);
				}
				await this.NextResultAsync().ConfigureAwait(false);
				return result;
			}

			// Token: 0x060001DE RID: 478 RVA: 0x0000AA7C File Offset: 0x00008C7C
			private async Task<IEnumerable<T>> ReadBufferedAsync<T>(int index, Func<IDataReader, object> deserializer)
			{
				object obj = null;
				int num = 0;
				IEnumerable<T> enumerable;
				try
				{
					DbDataReader reader = (DbDataReader)this.reader;
					List<T> buffer = new List<T>();
					for (;;)
					{
						bool flag = index == this.gridIndex;
						bool flag2 = flag;
						if (flag2)
						{
							bool flag3 = await reader.ReadAsync(this.cancel).ConfigureAwait(false);
							flag2 = flag3;
						}
						if (!flag2)
						{
							break;
						}
						buffer.Add((T)((object)deserializer(reader)));
					}
					enumerable = buffer;
					num = 1;
				}
				catch (object obj)
				{
				}
				if (index == this.gridIndex)
				{
					await this.NextResultAsync().ConfigureAwait(false);
				}
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				IEnumerable<T> enumerable2;
				if (num == 1)
				{
					enumerable2 = enumerable;
				}
				else
				{
					obj = null;
					enumerable = null;
				}
				return enumerable2;
			}

			// Token: 0x060001DF RID: 479 RVA: 0x0000AAD1 File Offset: 0x00008CD1
			internal GridReader(IDbCommand command, IDataReader reader, SqlMapper.Identity identity, SqlMapper.IParameterCallbacks callbacks, bool addToCache)
			{
				this.Command = command;
				this.reader = reader;
				this.identity = identity;
				this.callbacks = callbacks;
				this.addToCache = addToCache;
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x0000AAFE File Offset: 0x00008CFE
			[return: Dynamic(new bool[] { false, true })]
			public IEnumerable<dynamic> Read(bool buffered = true)
			{
				return this.ReadImpl<object>(typeof(SqlMapper.DapperRow), buffered);
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x0000AB11 File Offset: 0x00008D11
			[return: Dynamic]
			public dynamic ReadFirst()
			{
				return this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.First);
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x0000AB24 File Offset: 0x00008D24
			[return: Dynamic]
			public dynamic ReadFirstOrDefault()
			{
				return this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.FirstOrDefault);
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x0000AB37 File Offset: 0x00008D37
			[return: Dynamic]
			public dynamic ReadSingle()
			{
				return this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.Single);
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x0000AB4A File Offset: 0x00008D4A
			[return: Dynamic]
			public dynamic ReadSingleOrDefault()
			{
				return this.ReadRow<object>(typeof(SqlMapper.DapperRow), SqlMapper.Row.SingleOrDefault);
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x0000AB5D File Offset: 0x00008D5D
			public IEnumerable<T> Read<T>(bool buffered = true)
			{
				return this.ReadImpl<T>(typeof(T), buffered);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x0000AB70 File Offset: 0x00008D70
			public T ReadFirst<T>()
			{
				return this.ReadRow<T>(typeof(T), SqlMapper.Row.First);
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x0000AB83 File Offset: 0x00008D83
			public T ReadFirstOrDefault<T>()
			{
				return this.ReadRow<T>(typeof(T), SqlMapper.Row.FirstOrDefault);
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x0000AB96 File Offset: 0x00008D96
			public T ReadSingle<T>()
			{
				return this.ReadRow<T>(typeof(T), SqlMapper.Row.Single);
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x0000ABA9 File Offset: 0x00008DA9
			public T ReadSingleOrDefault<T>()
			{
				return this.ReadRow<T>(typeof(T), SqlMapper.Row.SingleOrDefault);
			}

			// Token: 0x060001EA RID: 490 RVA: 0x0000ABBC File Offset: 0x00008DBC
			public IEnumerable<object> Read(Type type, bool buffered = true)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadImpl<object>(type, buffered);
			}

			// Token: 0x060001EB RID: 491 RVA: 0x0000ABDA File Offset: 0x00008DDA
			public object ReadFirst(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRow<object>(type, SqlMapper.Row.First);
			}

			// Token: 0x060001EC RID: 492 RVA: 0x0000ABF8 File Offset: 0x00008DF8
			public object ReadFirstOrDefault(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRow<object>(type, SqlMapper.Row.FirstOrDefault);
			}

			// Token: 0x060001ED RID: 493 RVA: 0x0000AC16 File Offset: 0x00008E16
			public object ReadSingle(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRow<object>(type, SqlMapper.Row.Single);
			}

			// Token: 0x060001EE RID: 494 RVA: 0x0000AC34 File Offset: 0x00008E34
			public object ReadSingleOrDefault(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.ReadRow<object>(type, SqlMapper.Row.SingleOrDefault);
			}

			// Token: 0x060001EF RID: 495 RVA: 0x0000AC54 File Offset: 0x00008E54
			private IEnumerable<T> ReadImpl<T>(Type type, bool buffered)
			{
				if (this.reader == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
				}
				if (this.IsConsumed)
				{
					throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
				}
				SqlMapper.Identity typedIdentity = this.identity.ForGrid(type, this.gridIndex);
				SqlMapper.CacheInfo cache = SqlMapper.GetCacheInfo(typedIdentity, null, this.addToCache);
				SqlMapper.DeserializerState deserializer = cache.Deserializer;
				int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
				if (deserializer.Func == null || deserializer.Hash != hash)
				{
					deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, this.reader, 0, -1, false));
					cache.Deserializer = deserializer;
				}
				this.IsConsumed = true;
				IEnumerable<T> result = this.ReadDeferred<T>(this.gridIndex, deserializer.Func, type);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<T>();
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x0000AD24 File Offset: 0x00008F24
			private T ReadRow<T>(Type type, SqlMapper.Row row)
			{
				if (this.reader == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName, "The reader has been disposed; this can happen after all data has been consumed");
				}
				if (this.IsConsumed)
				{
					throw new InvalidOperationException("Query results must be consumed in the correct order, and each result can only be consumed once");
				}
				this.IsConsumed = true;
				T result = default(T);
				if (this.reader.Read() && this.reader.FieldCount != 0)
				{
					SqlMapper.Identity typedIdentity = this.identity.ForGrid(type, this.gridIndex);
					SqlMapper.CacheInfo cache = SqlMapper.GetCacheInfo(typedIdentity, null, this.addToCache);
					SqlMapper.DeserializerState deserializer = cache.Deserializer;
					int hash = SqlMapper.GetColumnHash(this.reader, 0, -1);
					if (deserializer.Func == null || deserializer.Hash != hash)
					{
						deserializer = new SqlMapper.DeserializerState(hash, SqlMapper.GetDeserializer(type, this.reader, 0, -1, false));
						cache.Deserializer = deserializer;
					}
					object val = deserializer.Func(this.reader);
					if (val == null || val is T)
					{
						result = (T)((object)val);
					}
					else
					{
						Type convertToType = Nullable.GetUnderlyingType(type) ?? type;
						result = (T)((object)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
					}
					if ((row & SqlMapper.Row.Single) != SqlMapper.Row.First && this.reader.Read())
					{
						SqlMapper.ThrowMultipleRows(row);
					}
					while (this.reader.Read())
					{
					}
				}
				else if ((row & SqlMapper.Row.FirstOrDefault) == SqlMapper.Row.First)
				{
					SqlMapper.ThrowZeroRows(row);
				}
				this.NextResult();
				return result;
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x0000AE7D File Offset: 0x0000907D
			private IEnumerable<TReturn> MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Delegate func, string splitOn)
			{
				SqlMapper.Identity identity = this.identity.ForGrid(typeof(TReturn), new Type[]
				{
					typeof(TFirst),
					typeof(TSecond),
					typeof(TThird),
					typeof(TFourth),
					typeof(TFifth),
					typeof(TSixth),
					typeof(TSeventh)
				}, this.gridIndex);
				this.IsConsumed = true;
				try
				{
					foreach (TReturn r in SqlMapper.MultiMapImpl((IDbConnection)null, default(CommandDefinition), func, splitOn, this.reader, identity, false))
					{
						yield return r;
					}
					IEnumerator<TReturn> enumerator = null;
				}
				finally
				{
					this.NextResult();
				}
				yield break;
				yield break;
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x0000AE9B File Offset: 0x0000909B
			private IEnumerable<TReturn> MultiReadInternal<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn)
			{
				SqlMapper.Identity identity = this.identity.ForGrid(typeof(TReturn), types, this.gridIndex);
				try
				{
					foreach (TReturn r in SqlMapper.MultiMapImpl((IDbConnection)null, default(CommandDefinition), types, map, splitOn, this.reader, identity, false))
					{
						yield return r;
					}
					IEnumerator<TReturn> enumerator = null;
				}
				finally
				{
					this.NextResult();
				}
				yield break;
				yield break;
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x0000AEC0 File Offset: 0x000090C0
			public IEnumerable<TReturn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TFirst, TSecond, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x0000AEE4 File Offset: 0x000090E4
			public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> func, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TFirst, TSecond, TThird, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x0000AF08 File Offset: 0x00009108
			public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> func, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, SqlMapper.DontMap, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x0000AF2C File Offset: 0x0000912C
			public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, SqlMapper.DontMap, SqlMapper.DontMap, TReturn>(func, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x0000AF50 File Offset: 0x00009150
			public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, SqlMapper.DontMap, TReturn>(func, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x0000AF74 File Offset: 0x00009174
			public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(func, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x0000AF98 File Offset: 0x00009198
			public IEnumerable<TReturn> Read<TReturn>(Type[] types, Func<object[], TReturn> map, string splitOn = "id", bool buffered = true)
			{
				IEnumerable<TReturn> result = this.MultiReadInternal<TReturn>(types, map, splitOn);
				if (!buffered)
				{
					return result;
				}
				return result.ToList<TReturn>();
			}

			// Token: 0x060001FA RID: 506 RVA: 0x0000AFBD File Offset: 0x000091BD
			private IEnumerable<T> ReadDeferred<T>(int index, Func<IDataReader, object> deserializer, Type effectiveType)
			{
				try
				{
					Type convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
					while (index == this.gridIndex && this.reader.Read())
					{
						object val = deserializer(this.reader);
						if (val == null || val is T)
						{
							yield return (T)((object)val);
						}
						else
						{
							yield return (T)((object)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
						}
					}
					convertToType = null;
				}
				finally
				{
					if (index == this.gridIndex)
					{
						this.NextResult();
					}
				}
				yield break;
				yield break;
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060001FB RID: 507 RVA: 0x0000AFE2 File Offset: 0x000091E2
			// (set) Token: 0x060001FC RID: 508 RVA: 0x0000AFEA File Offset: 0x000091EA
			public bool IsConsumed { get; private set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001FD RID: 509 RVA: 0x0000AFF3 File Offset: 0x000091F3
			// (set) Token: 0x060001FE RID: 510 RVA: 0x0000AFFB File Offset: 0x000091FB
			public IDbCommand Command { get; set; }

			// Token: 0x060001FF RID: 511 RVA: 0x0000B004 File Offset: 0x00009204
			private void NextResult()
			{
				if (this.reader.NextResult())
				{
					this.readCount++;
					this.gridIndex++;
					this.IsConsumed = false;
					return;
				}
				this.reader.Dispose();
				this.reader = null;
				SqlMapper.IParameterCallbacks parameterCallbacks = this.callbacks;
				if (parameterCallbacks != null)
				{
					parameterCallbacks.OnCompleted();
				}
				this.Dispose();
			}

			// Token: 0x06000200 RID: 512 RVA: 0x0000B06C File Offset: 0x0000926C
			public void Dispose()
			{
				if (this.reader != null)
				{
					if (!this.reader.IsClosed)
					{
						IDbCommand command = this.Command;
						if (command != null)
						{
							command.Cancel();
						}
					}
					this.reader.Dispose();
					this.reader = null;
				}
				if (this.Command != null)
				{
					this.Command.Dispose();
					this.Command = null;
				}
			}

			// Token: 0x04000085 RID: 133
			private readonly CancellationToken cancel;

			// Token: 0x04000086 RID: 134
			private IDataReader reader;

			// Token: 0x04000087 RID: 135
			private readonly SqlMapper.Identity identity;

			// Token: 0x04000088 RID: 136
			private readonly bool addToCache;

			// Token: 0x04000089 RID: 137
			private int gridIndex;

			// Token: 0x0400008A RID: 138
			private int readCount;

			// Token: 0x0400008B RID: 139
			private readonly SqlMapper.IParameterCallbacks callbacks;
		}

		// Token: 0x0200002B RID: 43
		public interface ICustomQueryParameter
		{
			// Token: 0x06000201 RID: 513
			void AddParameter(IDbCommand command, string name);
		}

		// Token: 0x0200002C RID: 44
		public class Identity : IEquatable<SqlMapper.Identity>
		{
			// Token: 0x06000202 RID: 514 RVA: 0x0000B0CB File Offset: 0x000092CB
			internal SqlMapper.Identity ForGrid(Type primaryType, int gridIndex)
			{
				return new SqlMapper.Identity(this.sql, this.commandType, this.connectionString, primaryType, this.parametersType, null, gridIndex);
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000B0ED File Offset: 0x000092ED
			internal SqlMapper.Identity ForGrid(Type primaryType, Type[] otherTypes, int gridIndex)
			{
				return new SqlMapper.Identity(this.sql, this.commandType, this.connectionString, primaryType, this.parametersType, otherTypes, gridIndex);
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000B10F File Offset: 0x0000930F
			public SqlMapper.Identity ForDynamicParameters(Type type)
			{
				return new SqlMapper.Identity(this.sql, this.commandType, this.connectionString, this.type, type, null, -1);
			}

			// Token: 0x06000205 RID: 517 RVA: 0x0000B131 File Offset: 0x00009331
			internal Identity(string sql, CommandType? commandType, IDbConnection connection, Type type, Type parametersType, Type[] otherTypes)
				: this(sql, commandType, connection.ConnectionString, type, parametersType, otherTypes, 0)
			{
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000B148 File Offset: 0x00009348
			private Identity(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType, Type[] otherTypes, int gridIndex)
			{
				this.sql = sql;
				this.commandType = commandType;
				this.connectionString = connectionString;
				this.type = type;
				this.parametersType = parametersType;
				this.gridIndex = gridIndex;
				this.hashCode = 17;
				this.hashCode = this.hashCode * 23 + commandType.GetHashCode();
				this.hashCode = this.hashCode * 23 + gridIndex.GetHashCode();
				this.hashCode = this.hashCode * 23 + ((sql != null) ? sql.GetHashCode() : 0);
				this.hashCode = this.hashCode * 23 + ((type != null) ? type.GetHashCode() : 0);
				if (otherTypes != null)
				{
					foreach (Type t in otherTypes)
					{
						this.hashCode = this.hashCode * 23 + ((t != null) ? t.GetHashCode() : 0);
					}
				}
				this.hashCode = this.hashCode * 23 + ((connectionString == null) ? 0 : SqlMapper.connectionStringComparer.GetHashCode(connectionString));
				this.hashCode = this.hashCode * 23 + ((parametersType != null) ? parametersType.GetHashCode() : 0);
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000B272 File Offset: 0x00009472
			public override bool Equals(object obj)
			{
				return this.Equals(obj as SqlMapper.Identity);
			}

			// Token: 0x06000208 RID: 520 RVA: 0x0000B280 File Offset: 0x00009480
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x06000209 RID: 521 RVA: 0x0000B288 File Offset: 0x00009488
			public bool Equals(SqlMapper.Identity other)
			{
				if (other != null && this.gridIndex == other.gridIndex && this.type == other.type && this.sql == other.sql)
				{
					CommandType? commandType = this.commandType;
					CommandType? commandType2 = other.commandType;
					if (((commandType.GetValueOrDefault() == commandType2.GetValueOrDefault()) & (commandType != null == (commandType2 != null))) && SqlMapper.connectionStringComparer.Equals(this.connectionString, other.connectionString))
					{
						return this.parametersType == other.parametersType;
					}
				}
				return false;
			}

			// Token: 0x0400008E RID: 142
			public readonly string sql;

			// Token: 0x0400008F RID: 143
			public readonly CommandType? commandType;

			// Token: 0x04000090 RID: 144
			public readonly int hashCode;

			// Token: 0x04000091 RID: 145
			public readonly int gridIndex;

			// Token: 0x04000092 RID: 146
			public readonly Type type;

			// Token: 0x04000093 RID: 147
			public readonly string connectionString;

			// Token: 0x04000094 RID: 148
			public readonly Type parametersType;
		}

		// Token: 0x0200002D RID: 45
		public interface IDynamicParameters
		{
			// Token: 0x0600020A RID: 522
			void AddParameters(IDbCommand command, SqlMapper.Identity identity);
		}

		// Token: 0x0200002E RID: 46
		public interface IMemberMap
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600020B RID: 523
			string ColumnName { get; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600020C RID: 524
			Type MemberType { get; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600020D RID: 525
			PropertyInfo Property { get; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600020E RID: 526
			FieldInfo Field { get; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x0600020F RID: 527
			ParameterInfo Parameter { get; }
		}

		// Token: 0x0200002F RID: 47
		public interface IParameterCallbacks : SqlMapper.IDynamicParameters
		{
			// Token: 0x06000210 RID: 528
			void OnCompleted();
		}

		// Token: 0x02000030 RID: 48
		public interface IParameterLookup : SqlMapper.IDynamicParameters
		{
			// Token: 0x17000046 RID: 70
			object this[string name] { get; }
		}

		// Token: 0x02000031 RID: 49
		public interface ITypeHandler
		{
			// Token: 0x06000212 RID: 530
			void SetValue(IDbDataParameter parameter, object value);

			// Token: 0x06000213 RID: 531
			object Parse(Type destinationType, object value);
		}

		// Token: 0x02000032 RID: 50
		public interface ITypeMap
		{
			// Token: 0x06000214 RID: 532
			ConstructorInfo FindConstructor(string[] names, Type[] types);

			// Token: 0x06000215 RID: 533
			ConstructorInfo FindExplicitConstructor();

			// Token: 0x06000216 RID: 534
			SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName);

			// Token: 0x06000217 RID: 535
			SqlMapper.IMemberMap GetMember(string columnName);
		}

		// Token: 0x02000033 RID: 51
		internal class Link<TKey, TValue> where TKey : class
		{
			// Token: 0x06000218 RID: 536 RVA: 0x0000B32E File Offset: 0x0000952E
			public static bool TryGet(SqlMapper.Link<TKey, TValue> link, TKey key, out TValue value)
			{
				while (link != null)
				{
					if (key == link.Key)
					{
						value = link.Value;
						return true;
					}
					link = link.Tail;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x06000219 RID: 537 RVA: 0x0000B368 File Offset: 0x00009568
			public static bool TryAdd(ref SqlMapper.Link<TKey, TValue> head, TKey key, ref TValue value)
			{
				TValue found;
				for (;;)
				{
					SqlMapper.Link<TKey, TValue> snapshot = Interlocked.CompareExchange<SqlMapper.Link<TKey, TValue>>(ref head, null, null);
					if (SqlMapper.Link<TKey, TValue>.TryGet(snapshot, key, out found))
					{
						break;
					}
					SqlMapper.Link<TKey, TValue> newNode = new SqlMapper.Link<TKey, TValue>(key, value, snapshot);
					if (Interlocked.CompareExchange<SqlMapper.Link<TKey, TValue>>(ref head, newNode, snapshot) == snapshot)
					{
						return true;
					}
				}
				value = found;
				return false;
			}

			// Token: 0x0600021A RID: 538 RVA: 0x0000B3B3 File Offset: 0x000095B3
			private Link(TKey key, TValue value, SqlMapper.Link<TKey, TValue> tail)
			{
				this.Key = key;
				this.Value = value;
				this.Tail = tail;
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600021B RID: 539 RVA: 0x0000B3D0 File Offset: 0x000095D0
			public TKey Key { get; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B3D8 File Offset: 0x000095D8
			public TValue Value { get; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600021D RID: 541 RVA: 0x0000B3E0 File Offset: 0x000095E0
			public SqlMapper.Link<TKey, TValue> Tail { get; }
		}

		// Token: 0x02000034 RID: 52
		internal struct LiteralToken
		{
			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B3E8 File Offset: 0x000095E8
			public string Token { get; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600021F RID: 543 RVA: 0x0000B3F0 File Offset: 0x000095F0
			public string Member { get; }

			// Token: 0x06000220 RID: 544 RVA: 0x0000B3F8 File Offset: 0x000095F8
			internal LiteralToken(string token, string member)
			{
				this.Token = token;
				this.Member = member;
			}

			// Token: 0x0400009A RID: 154
			internal static readonly IList<SqlMapper.LiteralToken> None = new SqlMapper.LiteralToken[0];
		}

		// Token: 0x02000035 RID: 53
		public static class Settings
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B415 File Offset: 0x00009615
			// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B41C File Offset: 0x0000961C
			internal static CommandBehavior AllowedCommandBehaviors { get; private set; } = ~CommandBehavior.SingleResult;

			// Token: 0x06000224 RID: 548 RVA: 0x0000B424 File Offset: 0x00009624
			private static void SetAllowedCommandBehaviors(CommandBehavior behavior, bool enabled)
			{
				if (enabled)
				{
					SqlMapper.Settings.AllowedCommandBehaviors |= behavior;
					return;
				}
				SqlMapper.Settings.AllowedCommandBehaviors &= ~behavior;
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000225 RID: 549 RVA: 0x0000B443 File Offset: 0x00009643
			// (set) Token: 0x06000226 RID: 550 RVA: 0x0000B44F File Offset: 0x0000964F
			public static bool UseSingleResultOptimization
			{
				get
				{
					return (SqlMapper.Settings.AllowedCommandBehaviors & CommandBehavior.SingleResult) > CommandBehavior.Default;
				}
				set
				{
					SqlMapper.Settings.SetAllowedCommandBehaviors(CommandBehavior.SingleResult, value);
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000227 RID: 551 RVA: 0x0000B458 File Offset: 0x00009658
			// (set) Token: 0x06000228 RID: 552 RVA: 0x0000B464 File Offset: 0x00009664
			public static bool UseSingleRowOptimization
			{
				get
				{
					return (SqlMapper.Settings.AllowedCommandBehaviors & CommandBehavior.SingleRow) > CommandBehavior.Default;
				}
				set
				{
					SqlMapper.Settings.SetAllowedCommandBehaviors(CommandBehavior.SingleRow, value);
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000B46D File Offset: 0x0000966D
			internal static bool DisableCommandBehaviorOptimizations(CommandBehavior behavior, Exception ex)
			{
				if (SqlMapper.Settings.AllowedCommandBehaviors == ~CommandBehavior.SingleResult && (behavior & (CommandBehavior.SingleResult | CommandBehavior.SingleRow)) != CommandBehavior.Default && (ex.Message.Contains("SingleResult") || ex.Message.Contains("SingleRow")))
				{
					SqlMapper.Settings.SetAllowedCommandBehaviors(CommandBehavior.SingleResult | CommandBehavior.SingleRow, false);
					return true;
				}
				return false;
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000B4AD File Offset: 0x000096AD
			static Settings()
			{
				SqlMapper.Settings.SetDefaults();
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000B4C4 File Offset: 0x000096C4
			public static void SetDefaults()
			{
				SqlMapper.Settings.CommandTimeout = null;
				SqlMapper.Settings.ApplyNullValues = false;
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600022C RID: 556 RVA: 0x0000B4E5 File Offset: 0x000096E5
			// (set) Token: 0x0600022D RID: 557 RVA: 0x0000B4EC File Offset: 0x000096EC
			public static int? CommandTimeout { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0000B4F4 File Offset: 0x000096F4
			// (set) Token: 0x0600022F RID: 559 RVA: 0x0000B4FB File Offset: 0x000096FB
			public static bool ApplyNullValues { get; set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B503 File Offset: 0x00009703
			// (set) Token: 0x06000231 RID: 561 RVA: 0x0000B50A File Offset: 0x0000970A
			public static bool PadListExpansions { get; set; }

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000B512 File Offset: 0x00009712
			// (set) Token: 0x06000233 RID: 563 RVA: 0x0000B519 File Offset: 0x00009719
			public static int InListStringSplitCount { get; set; } = -1;

			// Token: 0x0400009B RID: 155
			private const CommandBehavior DefaultAllowedCommandBehaviors = ~CommandBehavior.SingleResult;
		}

		// Token: 0x02000036 RID: 54
		private class TypeDeserializerCache
		{
			// Token: 0x06000234 RID: 564 RVA: 0x0000B521 File Offset: 0x00009721
			private TypeDeserializerCache(Type type)
			{
				this.type = type;
			}

			// Token: 0x06000235 RID: 565 RVA: 0x0000B53C File Offset: 0x0000973C
			internal static void Purge(Type type)
			{
				Hashtable hashtable = SqlMapper.TypeDeserializerCache.byType;
				lock (hashtable)
				{
					SqlMapper.TypeDeserializerCache.byType.Remove(type);
				}
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000B580 File Offset: 0x00009780
			internal static void Purge()
			{
				Hashtable hashtable = SqlMapper.TypeDeserializerCache.byType;
				lock (hashtable)
				{
					SqlMapper.TypeDeserializerCache.byType.Clear();
				}
			}

			// Token: 0x06000237 RID: 567 RVA: 0x0000B5C4 File Offset: 0x000097C4
			internal static Func<IDataReader, object> GetReader(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
			{
				SqlMapper.TypeDeserializerCache found = (SqlMapper.TypeDeserializerCache)SqlMapper.TypeDeserializerCache.byType[type];
				if (found == null)
				{
					Hashtable hashtable = SqlMapper.TypeDeserializerCache.byType;
					lock (hashtable)
					{
						found = (SqlMapper.TypeDeserializerCache)SqlMapper.TypeDeserializerCache.byType[type];
						if (found == null)
						{
							found = (SqlMapper.TypeDeserializerCache.byType[type] = new SqlMapper.TypeDeserializerCache(type));
						}
					}
				}
				return found.GetReader(reader, startBound, length, returnNullIfFirstMissing);
			}

			// Token: 0x06000238 RID: 568 RVA: 0x0000B644 File Offset: 0x00009844
			private Func<IDataReader, object> GetReader(IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
			{
				if (length < 0)
				{
					length = reader.FieldCount - startBound;
				}
				int hash = SqlMapper.GetColumnHash(reader, startBound, length);
				if (returnNullIfFirstMissing)
				{
					hash *= -27;
				}
				SqlMapper.TypeDeserializerCache.DeserializerKey key = new SqlMapper.TypeDeserializerCache.DeserializerKey(hash, startBound, length, returnNullIfFirstMissing, reader, false);
				Dictionary<SqlMapper.TypeDeserializerCache.DeserializerKey, Func<IDataReader, object>> dictionary = this.readers;
				Func<IDataReader, object> deser;
				lock (dictionary)
				{
					if (this.readers.TryGetValue(key, out deser))
					{
						return deser;
					}
				}
				deser = SqlMapper.GetTypeDeserializerImpl(this.type, reader, startBound, length, returnNullIfFirstMissing);
				key = new SqlMapper.TypeDeserializerCache.DeserializerKey(hash, startBound, length, returnNullIfFirstMissing, reader, true);
				Dictionary<SqlMapper.TypeDeserializerCache.DeserializerKey, Func<IDataReader, object>> dictionary2 = this.readers;
				Func<IDataReader, object> func;
				lock (dictionary2)
				{
					func = (this.readers[key] = deser);
				}
				return func;
			}

			// Token: 0x040000A1 RID: 161
			private static readonly Hashtable byType = new Hashtable();

			// Token: 0x040000A2 RID: 162
			private readonly Type type;

			// Token: 0x040000A3 RID: 163
			private readonly Dictionary<SqlMapper.TypeDeserializerCache.DeserializerKey, Func<IDataReader, object>> readers = new Dictionary<SqlMapper.TypeDeserializerCache.DeserializerKey, Func<IDataReader, object>>();

			// Token: 0x02000062 RID: 98
			private struct DeserializerKey : IEquatable<SqlMapper.TypeDeserializerCache.DeserializerKey>
			{
				// Token: 0x06000302 RID: 770 RVA: 0x000106D8 File Offset: 0x0000E8D8
				public DeserializerKey(int hashCode, int startBound, int length, bool returnNullIfFirstMissing, IDataReader reader, bool copyDown)
				{
					this.hashCode = hashCode;
					this.startBound = startBound;
					this.length = length;
					this.returnNullIfFirstMissing = returnNullIfFirstMissing;
					if (copyDown)
					{
						this.reader = null;
						this.names = new string[length];
						this.types = new Type[length];
						int index = startBound;
						for (int i = 0; i < length; i++)
						{
							this.names[i] = reader.GetName(index);
							this.types[i] = reader.GetFieldType(index++);
						}
						return;
					}
					this.reader = reader;
					this.names = null;
					this.types = null;
				}

				// Token: 0x06000303 RID: 771 RVA: 0x0001076E File Offset: 0x0000E96E
				public override int GetHashCode()
				{
					return this.hashCode;
				}

				// Token: 0x06000304 RID: 772 RVA: 0x00010778 File Offset: 0x0000E978
				public override string ToString()
				{
					if (this.names != null)
					{
						return string.Join(", ", this.names);
					}
					if (this.reader != null)
					{
						StringBuilder sb = new StringBuilder();
						int index = this.startBound;
						for (int i = 0; i < this.length; i++)
						{
							if (i != 0)
							{
								sb.Append(", ");
							}
							sb.Append(this.reader.GetName(index++));
						}
						return sb.ToString();
					}
					return base.ToString();
				}

				// Token: 0x06000305 RID: 773 RVA: 0x00010801 File Offset: 0x0000EA01
				public override bool Equals(object obj)
				{
					return obj is SqlMapper.TypeDeserializerCache.DeserializerKey && this.Equals((SqlMapper.TypeDeserializerCache.DeserializerKey)obj);
				}

				// Token: 0x06000306 RID: 774 RVA: 0x0001081C File Offset: 0x0000EA1C
				public bool Equals(SqlMapper.TypeDeserializerCache.DeserializerKey other)
				{
					if (this.hashCode != other.hashCode || this.startBound != other.startBound || this.length != other.length || this.returnNullIfFirstMissing != other.returnNullIfFirstMissing)
					{
						return false;
					}
					int i = 0;
					while (i < this.length)
					{
						string[] array = this.names;
						string text;
						if ((text = ((array != null) ? array[i] : null)) == null)
						{
							IDataReader dataReader = this.reader;
							text = ((dataReader != null) ? dataReader.GetName(this.startBound + i) : null);
						}
						string[] array2 = other.names;
						string text2;
						if ((text2 = ((array2 != null) ? array2[i] : null)) == null)
						{
							IDataReader dataReader2 = other.reader;
							text2 = ((dataReader2 != null) ? dataReader2.GetName(this.startBound + i) : null);
						}
						if (!(text != text2))
						{
							Type[] array3 = this.types;
							Type type;
							if ((type = ((array3 != null) ? array3[i] : null)) == null)
							{
								IDataReader dataReader3 = this.reader;
								type = ((dataReader3 != null) ? dataReader3.GetFieldType(this.startBound + i) : null);
							}
							Type[] array4 = other.types;
							Type type2;
							if ((type2 = ((array4 != null) ? array4[i] : null)) == null)
							{
								IDataReader dataReader4 = other.reader;
								type2 = ((dataReader4 != null) ? dataReader4.GetFieldType(this.startBound + i) : null);
							}
							if (!(type != type2))
							{
								i++;
								continue;
							}
						}
						return false;
					}
					return true;
				}

				// Token: 0x040001E0 RID: 480
				private readonly int startBound;

				// Token: 0x040001E1 RID: 481
				private readonly int length;

				// Token: 0x040001E2 RID: 482
				private readonly bool returnNullIfFirstMissing;

				// Token: 0x040001E3 RID: 483
				private readonly IDataReader reader;

				// Token: 0x040001E4 RID: 484
				private readonly string[] names;

				// Token: 0x040001E5 RID: 485
				private readonly Type[] types;

				// Token: 0x040001E6 RID: 486
				private readonly int hashCode;
			}
		}

		// Token: 0x02000037 RID: 55
		public abstract class TypeHandler<T> : SqlMapper.ITypeHandler
		{
			// Token: 0x0600023A RID: 570
			public abstract void SetValue(IDbDataParameter parameter, T value);

			// Token: 0x0600023B RID: 571
			public abstract T Parse(object value);

			// Token: 0x0600023C RID: 572 RVA: 0x0000B734 File Offset: 0x00009934
			void SqlMapper.ITypeHandler.SetValue(IDbDataParameter parameter, object value)
			{
				if (value is DBNull)
				{
					parameter.Value = value;
					return;
				}
				this.SetValue(parameter, (T)((object)value));
			}

			// Token: 0x0600023D RID: 573 RVA: 0x0000B753 File Offset: 0x00009953
			object SqlMapper.ITypeHandler.Parse(Type destinationType, object value)
			{
				return this.Parse(value);
			}
		}

		// Token: 0x02000038 RID: 56
		public abstract class StringTypeHandler<T> : SqlMapper.TypeHandler<T>
		{
			// Token: 0x0600023F RID: 575
			protected abstract T Parse(string xml);

			// Token: 0x06000240 RID: 576
			protected abstract string Format(T xml);

			// Token: 0x06000241 RID: 577 RVA: 0x0000B769 File Offset: 0x00009969
			public override void SetValue(IDbDataParameter parameter, T value)
			{
				parameter.Value = ((value == null) ? DBNull.Value : this.Format(value));
			}

			// Token: 0x06000242 RID: 578 RVA: 0x0000B788 File Offset: 0x00009988
			public override T Parse(object value)
			{
				if (value == null || value is DBNull)
				{
					return default(T);
				}
				return this.Parse((string)value);
			}
		}

		// Token: 0x02000039 RID: 57
		[Obsolete("This method is for internal use only", false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static class TypeHandlerCache<T>
		{
			// Token: 0x06000244 RID: 580 RVA: 0x0000B7BE File Offset: 0x000099BE
			[Obsolete("This method is for internal use only", true)]
			public static T Parse(object value)
			{
				return (T)((object)SqlMapper.TypeHandlerCache<T>.handler.Parse(typeof(T), value));
			}

			// Token: 0x06000245 RID: 581 RVA: 0x0000B7DA File Offset: 0x000099DA
			[Obsolete("This method is for internal use only", true)]
			public static void SetValue(IDbDataParameter parameter, object value)
			{
				SqlMapper.TypeHandlerCache<T>.handler.SetValue(parameter, value);
			}

			// Token: 0x06000246 RID: 582 RVA: 0x0000B7E8 File Offset: 0x000099E8
			internal static void SetHandler(SqlMapper.ITypeHandler handler)
			{
				SqlMapper.TypeHandlerCache<T>.handler = handler;
			}

			// Token: 0x040000A4 RID: 164
			private static SqlMapper.ITypeHandler handler;
		}

		// Token: 0x0200003A RID: 58
		public class UdtTypeHandler : SqlMapper.ITypeHandler
		{
			// Token: 0x06000247 RID: 583 RVA: 0x0000B7F0 File Offset: 0x000099F0
			public UdtTypeHandler(string udtTypeName)
			{
				if (string.IsNullOrEmpty(udtTypeName))
				{
					throw new ArgumentException("Cannot be null or empty", udtTypeName);
				}
				this.udtTypeName = udtTypeName;
			}

			// Token: 0x06000248 RID: 584 RVA: 0x0000B813 File Offset: 0x00009A13
			object SqlMapper.ITypeHandler.Parse(Type destinationType, object value)
			{
				if (!(value is DBNull))
				{
					return value;
				}
				return null;
			}

			// Token: 0x06000249 RID: 585 RVA: 0x0000B820 File Offset: 0x00009A20
			void SqlMapper.ITypeHandler.SetValue(IDbDataParameter parameter, object value)
			{
				parameter.Value = SqlMapper.SanitizeParameterValue(value);
				if (parameter is SqlParameter && !(value is DBNull))
				{
					((SqlParameter)parameter).SqlDbType = SqlDbType.Udt;
					((SqlParameter)parameter).UdtTypeName = this.udtTypeName;
				}
			}

			// Token: 0x040000A5 RID: 165
			private readonly string udtTypeName;
		}
	}
}
