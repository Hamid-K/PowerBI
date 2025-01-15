using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200109A RID: 4250
	internal class DbValueBuilder : ValueBuilderBase
	{
		// Token: 0x06006F2F RID: 28463 RVA: 0x0017F758 File Offset: 0x0017D958
		public DbValueBuilder(DbEnvironment environment, DbQueryPlan queryPlan, Func<DbDataReaderWithTableSchema, DbDataReaderWithTableSchema> readerWrapper = null)
			: this(environment, queryPlan, 0, null, false, readerWrapper)
		{
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x0017F779 File Offset: 0x0017D979
		private DbValueBuilder(DbEnvironment environment, DbQueryPlan queryPlan, int skip, int? take, bool count, Func<DbDataReaderWithTableSchema, DbDataReaderWithTableSchema> readerWrapper = null)
			: base(environment, queryPlan, skip, take, count)
		{
			this.readerWrapper = readerWrapper;
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06006F31 RID: 28465 RVA: 0x0017F790 File Offset: 0x0017D990
		public DbEnvironment DbEnvironment
		{
			get
			{
				return (DbEnvironment)base.Environment;
			}
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x06006F32 RID: 28466 RVA: 0x0017F79D File Offset: 0x0017D99D
		public DbQueryPlan DbQueryPlan
		{
			get
			{
				return (DbQueryPlan)base.QueryPlan;
			}
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x06006F33 RID: 28467 RVA: 0x0017F7AC File Offset: 0x0017D9AC
		public DbQueryPlan CompleteDbQueryPlan
		{
			get
			{
				if (this.completeDbQueryPlan == null)
				{
					if (base.Take != null || base.Skip > 0)
					{
						RowCount rowCount = ((base.Take != null) ? new RowCount((long)base.Take.Value) : RowCount.Infinite);
						this.completeDbQueryPlan = this.CreatePagingQueryPlan(new RowCount((long)base.Skip), rowCount, false, false);
					}
					else
					{
						this.completeDbQueryPlan = this.DbQueryPlan;
					}
				}
				return this.completeDbQueryPlan;
			}
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x0017F838 File Offset: 0x0017DA38
		private DbQueryPlan CreatePagingQueryPlan(RowCount skip, RowCount take, bool count, bool order)
		{
			SqlQueryExpression sqlQueryExpression = this.DbEnvironment.NewAstCreator(null, null).CreatePagingQuery(this.DbQueryPlan.Query, (this.DbQueryPlan.Type.TypeKind == ValueKind.Table) ? DbValueBuilder.GetColumnNames(this.DbQueryPlan.Type.AsTableType) : null, order ? DbValueBuilder.GetSortColumnNames(this.DbQueryPlan.Type.AsTableType) : null, skip.Value, take.IsInfinite ? null : new long?(take.Value));
			if (count)
			{
				sqlQueryExpression = this.DbEnvironment.NewAstCreator(null, null).CreateCountQuery(sqlQueryExpression);
			}
			return new DbQueryPlan(base.Type, sqlQueryExpression, this.DbQueryPlan.Options, this.DbEnvironment.SqlSettings);
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x0017F90C File Offset: 0x0017DB0C
		public override long CreateCountOverEnumerator()
		{
			DbValueBuilder dbValueBuilder = new DbValueBuilder(this.DbEnvironment, this.DbQueryPlan, base.Skip, base.Take, true, this.readerWrapper);
			DbQueryPlan countQueryPlan = dbValueBuilder.CreatePagingQueryPlan(new RowCount((long)base.Skip), (base.Take != null) ? new RowCount((long)base.Take.Value) : RowCount.Infinite, true, false);
			TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(Keys.New("$Item")));
			TableValue tableValue;
			if (this.DbEnvironment.TryExecuteCommand(tableTypeValue, () => countQueryPlan.ExternalQuery, out tableValue))
			{
				return tableValue[0].AsRecord[0].AsNumber.AsInteger64;
			}
			long num;
			using (DbValueBuilder.ConnectionReader connectionReader = new DbValueBuilder.ConnectionReader(this.DbEnvironment, this.readerWrapper))
			{
				using (DbDataReaderWithTableSchema dbDataReaderWithTableSchema = connectionReader.Execute(countQueryPlan.ExternalQuery))
				{
					if (!dbDataReaderWithTableSchema.Read())
					{
						throw new InvalidOperationException();
					}
					num = Convert.ToInt64(dbDataReaderWithTableSchema[0], CultureInfo.InvariantCulture);
				}
			}
			return num;
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x0017FA5C File Offset: 0x0017DC5C
		public override Value GetSingleValue()
		{
			DbQueryPlan dbQueryPlan = new DbValueBuilder(this.DbEnvironment, this.DbQueryPlan, 0, null, false, this.readerWrapper).CreatePagingQueryPlan(RowCount.Zero, RowCount.Infinite, false, false);
			Value value;
			using (DbValueBuilder.ConnectionReader connectionReader = new DbValueBuilder.ConnectionReader(this.DbEnvironment, this.readerWrapper))
			{
				using (DbDataReaderWithTableSchema dbDataReaderWithTableSchema = connectionReader.Execute(dbQueryPlan.ExternalQuery))
				{
					if (!this.DbEnvironment.ConvertDbExceptions<bool>(new Func<bool>(dbDataReaderWithTableSchema.Read)))
					{
						throw new InvalidOperationException();
					}
					value = ValueMarshaller.MarshalFromClr(dbDataReaderWithTableSchema[0]);
				}
			}
			return value;
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x0017FB20 File Offset: 0x0017DD20
		protected override ValueBuilderBase CreatePagingValueBuilder(Query originalQuery, int skipCount, int? takeCount)
		{
			if (skipCount <= 0)
			{
				int? take = base.Take;
				int? num = takeCount;
				if ((take.GetValueOrDefault() == num.GetValueOrDefault()) & (take != null == (num != null)))
				{
					return this;
				}
			}
			return new DbValueBuilder(this.DbEnvironment, this.DbQueryPlan, skipCount, takeCount, false, this.readerWrapper);
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x0017FB7C File Offset: 0x0017DD7C
		public override bool TryGetReader(out IPageReader reader)
		{
			bool flag = true;
			OleDbClient oleDbClient;
			if (this.DbEnvironment.TryGetOleDbClient(out oleDbClient) && base.Skip == 0 && base.Take == null)
			{
				flag = false;
			}
			DbQueryPlan dbQueryPlan = this.CreatePagingQueryPlan(new RowCount((long)base.Skip), (base.Take != null) ? new RowCount((long)base.Take.Value) : RowCount.Infinite, false, flag);
			string externalQuery = dbQueryPlan.ExternalQuery;
			if (oleDbClient != null)
			{
				this.DbQueryPlan.Type.AsTableType.GetPrimaryKey();
				Type[] columnTypes = DbValueBuilder.GetColumnTypes(this.DbQueryPlan.Type.AsTableType);
				reader = oleDbClient.ExecuteCommand(columnTypes, externalQuery).AfterDispose(new Action(oleDbClient.Dispose));
			}
			else
			{
				reader = this.GetReader(externalQuery);
			}
			string[] array = DbValueBuilder.MitigatedColumnNamesVisitor.Visit(dbQueryPlan.Query);
			if (array != null)
			{
				reader = new ColumnRenamePageReader(reader, array);
			}
			return true;
		}

		// Token: 0x06006F39 RID: 28473 RVA: 0x0017FC70 File Offset: 0x0017DE70
		private IPageReader GetReader(string commandText)
		{
			TableValue tableValue;
			if (this.DbEnvironment.TryExecuteCommand(base.Type.AsTableType, () => commandText, out tableValue))
			{
				return tableValue.GetReader();
			}
			DbValueBuilder.ConnectionReader connectionReader = new DbValueBuilder.ConnectionReader(this.DbEnvironment, this.readerWrapper);
			return new DataReaderPageReader(new DisposingDataReader(new DbValueBuilder.AdoNetDataReader(connectionReader.Execute(commandText), this.DbQueryPlan.Type.AsTableType.ItemType, this.DbEnvironment), connectionReader), new DataReaderPageReader.ExceptionPropertyGetter(this.DbEnvironment.TryGetPageReaderExceptionProperties));
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x0017FD14 File Offset: 0x0017DF14
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			RowCount skip = new RowCount((long)base.Skip);
			RowCount take = ((base.Take != null) ? new RowCount((long)base.Take.Value) : RowCount.Infinite);
			TableValue tableValue;
			if (this.DbEnvironment.TryExecuteCommand(base.Type.AsTableType, () => this.GetCommand(skip, take).CommandText, out tableValue))
			{
				return tableValue.GetEnumerator();
			}
			bool flag = this.DbEnvironment.SupportsPaging(base.QueryPlan.Type.AsTableType);
			DbValueBuilder.ConnectionReader connectionReader = new DbValueBuilder.ConnectionReader(this.DbEnvironment, this.readerWrapper);
			RowCount take2 = take;
			RowCount rowCount = take2;
			int? num = new int?(4096);
			RowCount skip2;
			RowCount zero;
			if (!flag)
			{
				skip2 = skip;
				zero = RowCount.Zero;
				num = null;
				if (!rowCount.IsInfinite)
				{
					rowCount += skip2;
				}
			}
			else
			{
				zero = new RowCount((long)(base.Skip / 4096 * 4096));
				skip2 = new RowCount((long)base.Skip - zero.Value);
				if (!take2.IsInfinite)
				{
					rowCount = new RowCount((take2.Value + skip2.Value + 4096L - 1L) / 4096L * 4096L);
				}
			}
			IEnumerator<IValueReference> enumerator = new DbValueBuilder.PagingEnumerator(connectionReader, this.DbEnvironment, base.QueryPlan.Type.AsTableType, zero, num, rowCount, this.DbEnvironment.Host.QueryService<ICacheSets>().Data.PersistentCache, new Func<RowCount, RowCount, DbValueBuilder.NativeCommand>(this.GetCommand));
			if (!skip2.IsZero || !take2.IsInfinite)
			{
				enumerator = new SkipTakeEnumerator<IValueReference>(enumerator, skip2, take2);
			}
			return enumerator;
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x0017FEE3 File Offset: 0x0017E0E3
		private DbValueBuilder.NativeCommand GetCommand(RowCount skip, RowCount take)
		{
			return new DbValueBuilder.NativeCommand(this.CreatePagingQueryPlan(skip, take, false, true).ExternalQuery);
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x0017FEFC File Offset: 0x0017E0FC
		private static string GetKey(string connectionCacheKey, DbValueBuilder.NativeCommand command, long offset, int? count)
		{
			if (command.CacheKey == null)
			{
				return null;
			}
			return PersistentCacheKey.DatabasePage.Qualify(connectionCacheKey ?? string.Empty, command.CacheKey, string.Format(CultureInfo.InvariantCulture, "({0},{1})", offset, count));
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x0017FF50 File Offset: 0x0017E150
		private static string[] GetColumnNames(TableTypeValue type)
		{
			RecordTypeValue itemType = type.ItemType;
			string[] array = new string[itemType.Fields.Keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = itemType.Fields.Keys[i];
			}
			return array;
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0017FFA0 File Offset: 0x0017E1A0
		private static string[] GetSortColumnNames(TableTypeValue type)
		{
			TableKey primaryKey = type.GetPrimaryKey();
			if (primaryKey != null)
			{
				string[] array = new string[primaryKey.Columns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = type.ItemType.Fields.Keys[primaryKey.Columns[i]];
				}
				return array;
			}
			return null;
		}

		// Token: 0x06006F3F RID: 28479 RVA: 0x0017FFF8 File Offset: 0x0017E1F8
		private static Type[] GetColumnTypes(TableTypeValue type)
		{
			RecordValue fields = type.ItemType.Fields;
			Type[] array = new Type[fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = fields[i]["Type"].AsType.NonNullable.ToClrType();
			}
			return array;
		}

		// Token: 0x04003DA9 RID: 15785
		private DbQueryPlan completeDbQueryPlan;

		// Token: 0x04003DAA RID: 15786
		private readonly Func<DbDataReaderWithTableSchema, DbDataReaderWithTableSchema> readerWrapper;

		// Token: 0x04003DAB RID: 15787
		public const int PageSize = 4096;

		// Token: 0x0200109B RID: 4251
		public class PagingEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06006F40 RID: 28480 RVA: 0x00180050 File Offset: 0x0017E250
			public PagingEnumerator(DbValueBuilder.ConnectionReader connectionReader, DbEnvironment environment, TableTypeValue tableType, RowCount skip, int? pageSize, RowCount take, IPersistentCache cache, Func<RowCount, RowCount, DbValueBuilder.NativeCommand> getCommandFunction)
			{
				this.connectionReader = connectionReader;
				this.environment = environment;
				this.tableType = tableType;
				this.skip = skip;
				this.pageSize = pageSize;
				this.take = take;
				this.paging = true;
				this.cache = cache;
				this.getCommandFunction = getCommandFunction;
				this.cacheKey = this.getCommandFunction(RowCount.Zero, RowCount.Infinite);
				this.wrapper = this.environment.CreateReaderWrapper(this.cacheKey.CacheKey, false);
			}

			// Token: 0x06006F41 RID: 28481 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x17001F59 RID: 8025
			// (get) Token: 0x06006F42 RID: 28482 RVA: 0x001800DF File Offset: 0x0017E2DF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001F5A RID: 8026
			// (get) Token: 0x06006F43 RID: 28483 RVA: 0x001800E7 File Offset: 0x0017E2E7
			public IValueReference Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06006F44 RID: 28484 RVA: 0x001800F4 File Offset: 0x0017E2F4
			public bool MoveNext()
			{
				if (this.paging)
				{
					while (!this.take.IsZero)
					{
						if (this.enumerator == null)
						{
							if (!this.TryGetPage(this.skip.Value, this.pageSize, out this.enumerator))
							{
								this.enumerator = this.GetRows();
								this.paging = false;
								goto IL_00D1;
							}
							this.count = 0;
						}
						if (this.enumerator.MoveNext())
						{
							this.take = RowCount.op_Decrement(this.take);
							this.skip = RowCount.op_Increment(this.skip);
							this.count++;
							return true;
						}
						this.enumerator.Dispose();
						this.enumerator = null;
						int num = this.count;
						int? num2 = this.pageSize;
						if (!((num == num2.GetValueOrDefault()) & (num2 != null)))
						{
							return false;
						}
					}
					return false;
				}
				IL_00D1:
				return this.enumerator.MoveNext();
			}

			// Token: 0x06006F45 RID: 28485 RVA: 0x001801DD File Offset: 0x0017E3DD
			public void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
				this.connectionReader.Dispose();
			}

			// Token: 0x06006F46 RID: 28486 RVA: 0x00180204 File Offset: 0x0017E404
			public bool TryGetPage(long offset, int? count, out IEnumerator<IValueReference> page)
			{
				string key = DbValueBuilder.GetKey(this.environment.CacheKey, this.cacheKey, offset, count);
				Stream stream;
				if (key != null && this.cache.TryGetValue(key, out stream))
				{
					DbDataReaderWithTableSchema dbDataReaderWithTableSchema = DbData.Deserialize(stream);
					try
					{
						page = DbDataReaderEnumerator.New(this.environment, dbDataReaderWithTableSchema, dbDataReaderWithTableSchema.HasRows, this.tableType.ItemType);
						return true;
					}
					catch
					{
						dbDataReaderWithTableSchema.Dispose();
						throw;
					}
				}
				page = null;
				return false;
			}

			// Token: 0x06006F47 RID: 28487 RVA: 0x0018028C File Offset: 0x0017E48C
			private IEnumerator<IValueReference> GetRows()
			{
				RowCount zero;
				RowCount zero2;
				if (this.environment.SupportsSkip(this.tableType))
				{
					zero = RowCount.Zero;
					zero2 = this.skip;
				}
				else
				{
					zero = this.skip;
					zero2 = RowCount.Zero;
				}
				DbValueBuilder.NativeCommand nativeCommand = this.getCommandFunction(zero2, this.take + zero);
				DbValueBuilder.NativeCommand nativeCommand2 = ((this.pageSize == null) ? nativeCommand : this.cacheKey);
				DbDataReaderWithTableSchema dbDataReaderWithTableSchema = null;
				IEnumerator<IValueReference> enumerator2;
				try
				{
					dbDataReaderWithTableSchema = this.connectionReader.Execute(nativeCommand, this.wrapper);
					IEnumerator<IValueReference> enumerator;
					if (nativeCommand2.CacheKey == null)
					{
						enumerator = DbDataReaderEnumerator.New(this.environment, dbDataReaderWithTableSchema, dbDataReaderWithTableSchema.HasRows, this.tableType.ItemType);
					}
					else
					{
						enumerator = new DbValueBuilder.PageCachingEnumerator(this.connectionReader.DbCommand, dbDataReaderWithTableSchema, this.environment, this.tableType.ItemType, this.cache, this.environment.CacheKey, nativeCommand2, zero2.Value, this.pageSize);
					}
					dbDataReaderWithTableSchema = null;
					enumerator2 = new SkipTakeEnumerator<IValueReference>(enumerator, zero, RowCount.Infinite);
				}
				finally
				{
					if (dbDataReaderWithTableSchema != null)
					{
						dbDataReaderWithTableSchema.Dispose();
					}
				}
				return enumerator2;
			}

			// Token: 0x04003DAC RID: 15788
			private readonly DbValueBuilder.ConnectionReader connectionReader;

			// Token: 0x04003DAD RID: 15789
			private readonly DbEnvironment environment;

			// Token: 0x04003DAE RID: 15790
			private readonly Func<RowCount, RowCount, DbValueBuilder.NativeCommand> getCommandFunction;

			// Token: 0x04003DAF RID: 15791
			private readonly TableTypeValue tableType;

			// Token: 0x04003DB0 RID: 15792
			private readonly int? pageSize;

			// Token: 0x04003DB1 RID: 15793
			private readonly IPersistentCache cache;

			// Token: 0x04003DB2 RID: 15794
			private readonly DbValueBuilder.NativeCommand cacheKey;

			// Token: 0x04003DB3 RID: 15795
			private readonly DbEnvironment.DbReaderWrapper wrapper;

			// Token: 0x04003DB4 RID: 15796
			private RowCount skip;

			// Token: 0x04003DB5 RID: 15797
			private RowCount take;

			// Token: 0x04003DB6 RID: 15798
			private bool paging;

			// Token: 0x04003DB7 RID: 15799
			private IEnumerator<IValueReference> enumerator;

			// Token: 0x04003DB8 RID: 15800
			private int count;
		}

		// Token: 0x0200109C RID: 4252
		private class PageCachingEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06006F48 RID: 28488 RVA: 0x001803B4 File Offset: 0x0017E5B4
			public PageCachingEnumerator(DbCommand dbCommand, DbDataReaderWithTableSchema reader, DbEnvironment environment, RecordTypeValue recordType, IPersistentCache cache, string connectionCacheKey, DbValueBuilder.NativeCommand command, long offset, int? pageSize)
			{
				this.dbCommand = dbCommand;
				this.reader = reader;
				this.recordType = recordType;
				this.environment = environment;
				this.cache = cache;
				this.connectionCacheKey = connectionCacheKey;
				this.command = command;
				this.offset = offset;
				this.pageSize = pageSize;
				this.enumerator = null;
			}

			// Token: 0x17001F5B RID: 8027
			// (get) Token: 0x06006F49 RID: 28489 RVA: 0x00180413 File Offset: 0x0017E613
			public IValueReference Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x17001F5C RID: 8028
			// (get) Token: 0x06006F4A RID: 28490 RVA: 0x00180420 File Offset: 0x0017E620
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006F4B RID: 28491 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06006F4C RID: 28492 RVA: 0x00180428 File Offset: 0x0017E628
			public void Dispose()
			{
				this.ResetEnumerator();
				if (!this.reader.IsClosed)
				{
					try
					{
						this.environment.AbortCommand(this.dbCommand, this.reader);
						this.reader.Close();
					}
					catch (DbException ex)
					{
						using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("PageCachingEnumerator/Dispose", TraceEventType.Information))
						{
							this.environment.TraceException(hostTrace, ex);
						}
					}
				}
			}

			// Token: 0x06006F4D RID: 28493 RVA: 0x001804BC File Offset: 0x0017E6BC
			public bool MoveNext()
			{
				while (this.enumerator == null || !this.enumerator.MoveNext())
				{
					if (this.reader.IsClosed)
					{
						this.ResetEnumerator();
						return false;
					}
					string key = DbValueBuilder.GetKey(this.connectionCacheKey, this.command, this.offset, this.pageSize);
					int? num = this.pageSize;
					long num2 = ((num != null) ? ((long)num.GetValueOrDefault()) : long.MaxValue);
					DbDataReaderWithTableSchema dbDataReaderWithTableSchema = new DbData.CachingDbDataReader(this.environment.Host, this.cache, key, this.reader, num2, this.cache.MaxEntryLength, new Action<Action>(this.WrapWithTracingAndDbExceptionHandling), false, true, new Func<DbException, ValueException>(this.environment.CreateValueException));
					this.offset += num2;
					this.ResetEnumerator();
					try
					{
						this.enumerator = DbDataReaderEnumerator.New(this.environment, dbDataReaderWithTableSchema, this.reader.HasRows, this.recordType);
					}
					catch
					{
						dbDataReaderWithTableSchema.Dispose();
						throw;
					}
				}
				return true;
			}

			// Token: 0x06006F4E RID: 28494 RVA: 0x001805E4 File Offset: 0x0017E7E4
			private void ResetEnumerator()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
			}

			// Token: 0x06006F4F RID: 28495 RVA: 0x00180600 File Offset: 0x0017E800
			private void WrapWithTracingAndDbExceptionHandling(Action action)
			{
				try
				{
					action();
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("PageCachingEnumerator", TraceEventType.Information))
					{
						this.environment.TraceException(hostTrace, ex);
						if (ex is IOException)
						{
							throw DataSourceException.NewDataSourceError(this.environment.Host, ex.Message, this.environment.Resource, null, ex.InnerException);
						}
						if (ex is DbException)
						{
							throw this.environment.CreateValueException((DbException)ex);
						}
						throw;
					}
				}
			}

			// Token: 0x04003DB9 RID: 15801
			private DbDataReaderWithTableSchema reader;

			// Token: 0x04003DBA RID: 15802
			private readonly DbCommand dbCommand;

			// Token: 0x04003DBB RID: 15803
			private readonly RecordTypeValue recordType;

			// Token: 0x04003DBC RID: 15804
			private readonly DbEnvironment environment;

			// Token: 0x04003DBD RID: 15805
			private readonly int? pageSize;

			// Token: 0x04003DBE RID: 15806
			private long offset;

			// Token: 0x04003DBF RID: 15807
			private readonly IPersistentCache cache;

			// Token: 0x04003DC0 RID: 15808
			private readonly string connectionCacheKey;

			// Token: 0x04003DC1 RID: 15809
			private readonly DbValueBuilder.NativeCommand command;

			// Token: 0x04003DC2 RID: 15810
			private IEnumerator<IValueReference> enumerator;
		}

		// Token: 0x0200109D RID: 4253
		public class ConnectionReader : IDisposable
		{
			// Token: 0x06006F50 RID: 28496 RVA: 0x001806B0 File Offset: 0x0017E8B0
			public ConnectionReader(DbEnvironment environment, Func<DbDataReaderWithTableSchema, DbDataReaderWithTableSchema> readerWrapper = null)
			{
				this.host = environment.Host;
				this.connection = environment.CreateConnection();
				this.environment = environment;
				this.readerWrapper = readerWrapper;
			}

			// Token: 0x17001F5D RID: 8029
			// (get) Token: 0x06006F51 RID: 28497 RVA: 0x001806DE File Offset: 0x0017E8DE
			public DbCommand DbCommand
			{
				get
				{
					return this.command;
				}
			}

			// Token: 0x06006F52 RID: 28498 RVA: 0x001806E6 File Offset: 0x0017E8E6
			private void Open()
			{
				if (this.connection.State == ConnectionState.Closed)
				{
					this.connection.Open(this.environment);
				}
			}

			// Token: 0x06006F53 RID: 28499 RVA: 0x00180706 File Offset: 0x0017E906
			public void RegisterForCleanup()
			{
				ILifetimeService lifetimeService = this.host.QueryService<ILifetimeService>();
				if (lifetimeService == null)
				{
					return;
				}
				lifetimeService.Register(this);
			}

			// Token: 0x06006F54 RID: 28500 RVA: 0x0018071E File Offset: 0x0017E91E
			public void Dispose()
			{
				this.CloseReader();
				DbCommand dbCommand = this.command;
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				DbConnection dbConnection = this.connection;
				if (dbConnection != null)
				{
					dbConnection.Close();
				}
				ILifetimeService lifetimeService = this.host.QueryService<ILifetimeService>();
				if (lifetimeService == null)
				{
					return;
				}
				lifetimeService.Unregister(this);
			}

			// Token: 0x06006F55 RID: 28501 RVA: 0x00180760 File Offset: 0x0017E960
			public DbDataReaderWithTableSchema Execute(string commandText)
			{
				DbEnvironment.DbReaderWrapper dbReaderWrapper = this.environment.CreateReaderWrapper(null, false);
				return this.Execute(commandText, CommandBehavior.Default, Value.Null, dbReaderWrapper);
			}

			// Token: 0x06006F56 RID: 28502 RVA: 0x00180789 File Offset: 0x0017E989
			public DbDataReaderWithTableSchema Execute(DbValueBuilder.NativeCommand command, DbEnvironment.DbReaderWrapper wrapper)
			{
				return this.Execute(command.CommandText, CommandBehavior.Default, command.Parameters, wrapper);
			}

			// Token: 0x06006F57 RID: 28503 RVA: 0x001807A4 File Offset: 0x0017E9A4
			public virtual DbDataReaderWithTableSchema Execute(string commandText, CommandBehavior commandBehavior, Value parameters, DbEnvironment.DbReaderWrapper wrapper)
			{
				this.CloseReader();
				this.CreateCommand(commandText, parameters);
				this.reader = this.environment.ConvertDbExceptions<ProgressDbDataReader>(delegate
				{
					ProgressDbDataReader progressDbDataReader;
					try
					{
						IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, this.environment.Resource.Kind, ProgressDbDataSource.GetDataSource(this.command.Connection));
						using (new ProgressRequest(hostProgress))
						{
							this.Open();
							DbDataReaderWithTableSchema dbDataReaderWithTableSchema = this.command.ExecuteReader(commandBehavior).WithTableSchema();
							dbDataReaderWithTableSchema = wrapper.Wrap(dbDataReaderWithTableSchema);
							if (this.readerWrapper != null)
							{
								dbDataReaderWithTableSchema = this.readerWrapper(dbDataReaderWithTableSchema);
							}
							progressDbDataReader = new ProgressDbDataReader(dbDataReaderWithTableSchema, hostProgress);
						}
					}
					catch (Exception ex)
					{
						using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("ExecuteReader", TraceEventType.Information))
						{
							this.environment.TraceException(hostTrace, ex);
							throw;
						}
					}
					return progressDbDataReader;
				});
				return this.reader;
			}

			// Token: 0x06006F58 RID: 28504 RVA: 0x00180800 File Offset: 0x0017EA00
			private void CreateCommand(string commandText, Value parameters)
			{
				DbCommand dbCommand = this.command;
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
				DbCommand dbCommand2 = this.connection.CreateCommand();
				dbCommand2.CommandText = commandText;
				dbCommand2.CommandTimeout = this.environment.CommandTimeout.GetValueOrDefault(600);
				this.command = this.environment.ParameterTypeMap.AddParameters(dbCommand2, parameters);
			}

			// Token: 0x06006F59 RID: 28505 RVA: 0x00180868 File Offset: 0x0017EA68
			private void CloseReader()
			{
				if (this.reader != null)
				{
					try
					{
						this.environment.AbortCommand(this.command, this.reader);
					}
					catch (DbException ex)
					{
						using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("CloseReader", TraceEventType.Information))
						{
							this.environment.TraceException(hostTrace, ex);
						}
					}
					this.reader = null;
				}
			}

			// Token: 0x04003DC3 RID: 15811
			private const int DefaultCommandTimeoutInSeconds = 600;

			// Token: 0x04003DC4 RID: 15812
			private readonly IEngineHost host;

			// Token: 0x04003DC5 RID: 15813
			private readonly DbEnvironment environment;

			// Token: 0x04003DC6 RID: 15814
			private readonly DbConnection connection;

			// Token: 0x04003DC7 RID: 15815
			private readonly Func<DbDataReaderWithTableSchema, DbDataReaderWithTableSchema> readerWrapper;

			// Token: 0x04003DC8 RID: 15816
			protected DbDataReaderWithTableSchema reader;

			// Token: 0x04003DC9 RID: 15817
			private DbCommand command;
		}

		// Token: 0x0200109F RID: 4255
		public class SingletonReader : DbValueBuilder.ConnectionReader
		{
			// Token: 0x06006F5C RID: 28508 RVA: 0x00180A14 File Offset: 0x0017EC14
			public SingletonReader(DbEnvironment environment)
				: base(environment, null)
			{
			}

			// Token: 0x06006F5D RID: 28509 RVA: 0x00180A1E File Offset: 0x0017EC1E
			public override DbDataReaderWithTableSchema Execute(string commandText, CommandBehavior commandBehavior, Value parameters, DbEnvironment.DbReaderWrapper wrapper)
			{
				if (this.reader == null)
				{
					return base.Execute(commandText, commandBehavior, parameters, wrapper);
				}
				return this.reader;
			}
		}

		// Token: 0x020010A0 RID: 4256
		public class AdoNetDataReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x06006F5E RID: 28510 RVA: 0x00180A3C File Offset: 0x0017EC3C
			public AdoNetDataReader(IDataReaderWithTableSchema reader, RecordTypeValue rowType, DbEnvironment dbEnvironment)
				: base(reader)
			{
				this.dbEnvironment = dbEnvironment;
				this.uniqueNames = rowType.Fields.Keys;
				this.columnMappings = this.GetColumnMappings(reader, rowType);
				this.types = new Type[this.columnMappings.Length];
				for (int i = 0; i < this.types.Length; i++)
				{
					this.types[i] = DbValueBuilder.AdoNetDataReader.GetType(reader.GetFieldType(i), this.columnMappings[i]);
				}
			}

			// Token: 0x06006F5F RID: 28511 RVA: 0x00180AB8 File Offset: 0x0017ECB8
			private DbValueBuilder.AdoNetDataReader.ColumnMapping[] GetColumnMappings(IDataReader reader, RecordTypeValue rowType)
			{
				if (reader.FieldCount != rowType.Fields.Count)
				{
					using (IHostTrace hostTrace = this.dbEnvironment.Tracer.CreateTrace("GetColumnMappings", TraceEventType.Error))
					{
						hostTrace.AddArray("FieldNames", rowType.Fields.Keys, true);
						hostTrace.AddDataTable("SchemaTable", reader.GetSchemaTable(), true);
					}
					throw new InvalidOperationException("Field count mismatch when mapping column types");
				}
				DbValueBuilder.AdoNetDataReader.ColumnMapping[] array = new DbValueBuilder.AdoNetDataReader.ColumnMapping[rowType.Fields.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = DbValueBuilder.AdoNetDataReader.GetColumnMapping(reader.GetFieldType(i), reader.GetDataTypeName(i), rowType.Fields[i]["Type"].AsType.TypeKind);
				}
				return array;
			}

			// Token: 0x06006F60 RID: 28512 RVA: 0x00180B98 File Offset: 0x0017ED98
			private static DbValueBuilder.AdoNetDataReader.ColumnMapping GetColumnMapping(Type sourceType, string originalTypeName, ValueKind targetType)
			{
				if (sourceType == typeof(int) && targetType == ValueKind.Logical)
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.Int32AsLogical;
				}
				if (sourceType == typeof(long) && targetType == ValueKind.Duration)
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.Int64AsTimeSpan;
				}
				if (sourceType == typeof(TimeSpan) && targetType == ValueKind.Time)
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.TimeSpanAsTime;
				}
				if (sourceType == typeof(DateTime) && targetType == ValueKind.Date)
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.DateTimeAsDate;
				}
				if (sourceType == typeof(Guid) && targetType == ValueKind.Text)
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.GuidAsString;
				}
				if (targetType == ValueKind.None)
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.UnsupportedAsException;
				}
				if (!(sourceType == typeof(decimal)))
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.Default;
				}
				if (originalTypeName == "smallmoney" || originalTypeName == "money")
				{
					return DbValueBuilder.AdoNetDataReader.ColumnMapping.DecimalAsCurrency;
				}
				return DbValueBuilder.AdoNetDataReader.ColumnMapping.DecimalAsNumeric;
			}

			// Token: 0x06006F61 RID: 28513 RVA: 0x00180C58 File Offset: 0x0017EE58
			private static Type GetType(Type type, DbValueBuilder.AdoNetDataReader.ColumnMapping columnMapping)
			{
				switch (columnMapping)
				{
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.Default:
					return type;
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.Int32AsLogical:
					return typeof(bool);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.DateTimeAsDate:
					return typeof(Date);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.TimeSpanAsTime:
					return typeof(Time);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.Int64AsTimeSpan:
					return typeof(TimeSpan);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.GuidAsString:
					return typeof(string);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.DecimalAsNumeric:
					return typeof(Number);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.DecimalAsCurrency:
					return typeof(Currency);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.UnsupportedAsException:
					return typeof(UnsupportedType);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17001F5E RID: 8030
			// (get) Token: 0x06006F62 RID: 28514 RVA: 0x00180CF0 File Offset: 0x0017EEF0
			public override TableSchema Schema
			{
				get
				{
					TableSchema schema = base.Schema;
					TableSchema tableSchema = new TableSchema(schema.ColumnCount);
					int columnCount = schema.ColumnCount;
					if (columnCount != this.uniqueNames.Length || columnCount != this.types.Length)
					{
						using (IHostTrace hostTrace = this.dbEnvironment.Tracer.CreateTrace("GetSchemaTable", TraceEventType.Error))
						{
							hostTrace.AddArray("UniqueNames", this.uniqueNames, true);
							hostTrace.AddArray("Types", this.types.Select((Type t) => t.ToString()), false);
							hostTrace.AddSchema("SchemaTable", schema, true);
						}
						throw new InvalidOperationException("Mismatch in Table Descriptions");
					}
					for (int i = 0; i < columnCount; i++)
					{
						tableSchema.AddColumn(this.uniqueNames[i], this.types[i], schema.GetColumn(i).Nullable);
					}
					return tableSchema;
				}
			}

			// Token: 0x17001F5F RID: 8031
			// (get) Token: 0x06006F63 RID: 28515 RVA: 0x00180E04 File Offset: 0x0017F004
			public override int FieldCount
			{
				get
				{
					return this.columnMappings.Length;
				}
			}

			// Token: 0x06006F64 RID: 28516 RVA: 0x00180E0E File Offset: 0x0017F00E
			public override Type GetFieldType(int i)
			{
				return this.types[i];
			}

			// Token: 0x06006F65 RID: 28517 RVA: 0x00180E18 File Offset: 0x0017F018
			public override bool GetBoolean(int i)
			{
				if (this.columnMappings[i] == DbValueBuilder.AdoNetDataReader.ColumnMapping.Int32AsLogical)
				{
					return base.Reader.GetInt32(i) != 0;
				}
				return base.Reader.GetBoolean(i);
			}

			// Token: 0x06006F66 RID: 28518 RVA: 0x00180E44 File Offset: 0x0017F044
			public override string GetString(int i)
			{
				if (this.columnMappings[i] == DbValueBuilder.AdoNetDataReader.ColumnMapping.GuidAsString)
				{
					return base.Reader.GetGuid(i).ToString();
				}
				return base.Reader.GetString(i);
			}

			// Token: 0x06006F67 RID: 28519 RVA: 0x00180E83 File Offset: 0x0017F083
			private Number GetNumeric(int i)
			{
				return this.dbEnvironment.GetNumeric(base.Reader, i);
			}

			// Token: 0x06006F68 RID: 28520 RVA: 0x00180E97 File Offset: 0x0017F097
			private Currency GetCurrency(int i)
			{
				return new Currency(base.Reader.GetDecimal(i));
			}

			// Token: 0x06006F69 RID: 28521 RVA: 0x00180EAA File Offset: 0x0017F0AA
			private Exception GetUnsupportedTypeException(int i)
			{
				return new NotSupportedException(Strings.UnsupportedClrType(base.Reader.GetValue(i).GetType()));
			}

			// Token: 0x17001F60 RID: 8032
			public override object this[string name]
			{
				get
				{
					return this.GetValue(this.GetOrdinal(name));
				}
			}

			// Token: 0x17001F61 RID: 8033
			public override object this[int i]
			{
				get
				{
					return this.GetValue(i);
				}
			}

			// Token: 0x06006F6C RID: 28524 RVA: 0x00180ECC File Offset: 0x0017F0CC
			public override object GetValue(int i)
			{
				DbValueBuilder.AdoNetDataReader.ColumnMapping columnMapping = this.columnMappings[i];
				if (columnMapping == DbValueBuilder.AdoNetDataReader.ColumnMapping.Default)
				{
					return base.Reader.GetValue(i);
				}
				if (base.Reader.IsDBNull(i))
				{
					return DBNull.Value;
				}
				switch (columnMapping)
				{
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.Int32AsLogical:
					return this.GetBoolean(i);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.DateTimeAsDate:
					return new Date(this.GetDateTime(i));
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.TimeSpanAsTime:
					return new Time((TimeSpan)base.Reader.GetValue(i));
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.Int64AsTimeSpan:
					return new TimeSpan(this.GetInt64(i));
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.GuidAsString:
					return this.GetString(i);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.DecimalAsNumeric:
					return this.GetNumeric(i);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.DecimalAsCurrency:
					return this.GetCurrency(i);
				case DbValueBuilder.AdoNetDataReader.ColumnMapping.UnsupportedAsException:
					throw this.GetUnsupportedTypeException(i);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x04003DCD RID: 15821
			private DbEnvironment dbEnvironment;

			// Token: 0x04003DCE RID: 15822
			private Keys uniqueNames;

			// Token: 0x04003DCF RID: 15823
			private DbValueBuilder.AdoNetDataReader.ColumnMapping[] columnMappings;

			// Token: 0x04003DD0 RID: 15824
			private Type[] types;

			// Token: 0x020010A1 RID: 4257
			private enum ColumnMapping
			{
				// Token: 0x04003DD2 RID: 15826
				Default,
				// Token: 0x04003DD3 RID: 15827
				Int32AsLogical,
				// Token: 0x04003DD4 RID: 15828
				DateTimeAsDate,
				// Token: 0x04003DD5 RID: 15829
				TimeSpanAsTime,
				// Token: 0x04003DD6 RID: 15830
				Int64AsTimeSpan,
				// Token: 0x04003DD7 RID: 15831
				GuidAsString,
				// Token: 0x04003DD8 RID: 15832
				DecimalAsNumeric,
				// Token: 0x04003DD9 RID: 15833
				DecimalAsCurrency,
				// Token: 0x04003DDA RID: 15834
				UnsupportedAsException
			}
		}

		// Token: 0x020010A3 RID: 4259
		public struct NativeCommand
		{
			// Token: 0x06006F70 RID: 28528 RVA: 0x00180FB8 File Offset: 0x0017F1B8
			public NativeCommand(string commandText)
			{
				this = new DbValueBuilder.NativeCommand(commandText, Value.Null);
			}

			// Token: 0x06006F71 RID: 28529 RVA: 0x00180FC8 File Offset: 0x0017F1C8
			public NativeCommand(string commandText, Value parameters)
			{
				this.commandText = commandText;
				this.parameters = parameters;
				if (this.parameters.IsNull)
				{
					this.cacheKey = this.commandText;
					return;
				}
				string text = this.parameters.CreateCacheKey();
				if (text == null)
				{
					this.cacheKey = null;
					return;
				}
				PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
				persistentCacheKeyBuilder.Add(this.commandText);
				persistentCacheKeyBuilder.Add(text);
				this.cacheKey = persistentCacheKeyBuilder.ToString();
			}

			// Token: 0x17001F62 RID: 8034
			// (get) Token: 0x06006F72 RID: 28530 RVA: 0x00181039 File Offset: 0x0017F239
			public string CommandText
			{
				get
				{
					return this.commandText;
				}
			}

			// Token: 0x17001F63 RID: 8035
			// (get) Token: 0x06006F73 RID: 28531 RVA: 0x00181041 File Offset: 0x0017F241
			public Value Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x17001F64 RID: 8036
			// (get) Token: 0x06006F74 RID: 28532 RVA: 0x00181049 File Offset: 0x0017F249
			public string CacheKey
			{
				get
				{
					return this.cacheKey;
				}
			}

			// Token: 0x04003DDD RID: 15837
			private readonly string commandText;

			// Token: 0x04003DDE RID: 15838
			private readonly Value parameters;

			// Token: 0x04003DDF RID: 15839
			private readonly string cacheKey;
		}

		// Token: 0x020010A4 RID: 4260
		private class MitigatedColumnNamesVisitor : SqlQueryExpressionVisitor<string[]>
		{
			// Token: 0x06006F75 RID: 28533 RVA: 0x00181054 File Offset: 0x0017F254
			protected override string[] VisitQuerySpecification(QuerySpecification querySpecification)
			{
				string[] array = null;
				for (int i = 0; i < querySpecification.SelectItems.Count; i++)
				{
					SelectItem selectItem = querySpecification.SelectItems[i];
					if (selectItem.Name != null && selectItem.Name.IsMitigated)
					{
						if (array == null)
						{
							array = new string[querySpecification.SelectItems.Count];
						}
						array[i] = selectItem.Name.OriginalName;
					}
				}
				return array;
			}

			// Token: 0x06006F76 RID: 28534 RVA: 0x001810BE File Offset: 0x0017F2BE
			protected override string[] VisitBinaryQueryOperation(BinaryQueryOperation queryOperation)
			{
				return this.VisitSqlQueryExpression(queryOperation.Right);
			}

			// Token: 0x06006F77 RID: 28535 RVA: 0x001810CC File Offset: 0x0017F2CC
			public static string[] Visit(SqlQueryExpression queryExpression)
			{
				return new DbValueBuilder.MitigatedColumnNamesVisitor().VisitSqlQueryExpression(queryExpression);
			}
		}
	}
}
