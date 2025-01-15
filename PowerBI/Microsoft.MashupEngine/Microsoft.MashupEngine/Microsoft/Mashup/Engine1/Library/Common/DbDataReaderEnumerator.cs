using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001054 RID: 4180
	internal class DbDataReaderEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06006D31 RID: 27953 RVA: 0x0017822D File Offset: 0x0017642D
		public DbDataReaderEnumerator(IDataReader reader, bool hasRows, RecordTypeValue recordType, string dataSourceName, Func<DbException, IList<RecordKeyDefinition>> errorDetailsMarshaller = null)
			: this(reader, TableSchema.FromDataReader(reader), hasRows, recordType, dataSourceName, null, errorDetailsMarshaller)
		{
		}

		// Token: 0x06006D32 RID: 27954 RVA: 0x00178243 File Offset: 0x00176443
		public DbDataReaderEnumerator(IDataReader reader, bool hasRows, RecordTypeValue recordType, string dataSourceName, Func<Type, TypeValue, Func<IDataReader, int, Value>> getRetrievalFunction, Func<DbException, IList<RecordKeyDefinition>> errorDetailsMarshaller = null)
			: this(reader, TableSchema.FromDataReader(reader), hasRows, recordType, dataSourceName, getRetrievalFunction, errorDetailsMarshaller)
		{
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x0017825C File Offset: 0x0017645C
		private DbDataReaderEnumerator(IDataReader reader, TableSchema schema, bool hasRows, RecordTypeValue recordType, string dataSourceName, Func<Type, TypeValue, Func<IDataReader, int, Value>> getRetrievalFunction, Func<DbException, IList<RecordKeyDefinition>> errorDetailsMarshaller)
		{
			this.dataSourceName = dataSourceName;
			this.reader = reader;
			this.recordKeys = recordType.Fields.Keys;
			int length = this.recordKeys.Length;
			this.valueMarshallers = new Func<IDataReader, int, Value>[length];
			this.errorDetailsMarshaller = errorDetailsMarshaller ?? new Func<DbException, IList<RecordKeyDefinition>>(DbExceptionInfo.GetDetails);
			getRetrievalFunction = getRetrievalFunction ?? new Func<Type, TypeValue, Func<IDataReader, int, Value>>(ValueMarshaller.GetRetrievalFunction);
			if (hasRows)
			{
				if (schema.ColumnCount > 0)
				{
					for (int i = 0; i < length; i++)
					{
						Type dataType = schema.GetColumn(i).DataType;
						this.valueMarshallers[i] = ((dataType == null) ? new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveObjectAsValue) : getRetrievalFunction(dataType, recordType.Fields[i]["Type"].AsType));
					}
					return;
				}
				for (int j = 0; j < length; j++)
				{
					this.valueMarshallers[j] = new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveObjectAsValue);
				}
			}
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x06006D34 RID: 27956 RVA: 0x00178360 File Offset: 0x00176560
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06006D36 RID: 27958 RVA: 0x00178368 File Offset: 0x00176568
		public void Dispose()
		{
			this.reader.Close();
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06006D37 RID: 27959 RVA: 0x00178375 File Offset: 0x00176575
		public IValueReference Current
		{
			get
			{
				if (this.current == null)
				{
					this.current = DbDataReaderEnumerator.CreateRecordValue(this.reader, this.recordKeys, this.valueMarshallers, this.dataSourceName, this.errorDetailsMarshaller);
				}
				return this.current;
			}
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x001783AE File Offset: 0x001765AE
		public bool MoveNext()
		{
			this.current = null;
			return this.reader.Read();
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x001783C4 File Offset: 0x001765C4
		private static RecordValue CreateRecordValue(IDataReader reader, Keys keys, Func<IDataReader, int, Value>[] valueMarshallers, string dataSourceName, Func<DbException, IList<RecordKeyDefinition>> errorDetailsMarshaller)
		{
			TableSchema tableSchema = null;
			IValueReference[] array = new IValueReference[keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					if (reader.IsDBNull(i))
					{
						array[i] = Value.Null;
					}
					else
					{
						try
						{
							array[i] = valueMarshallers[i](reader, i);
						}
						catch (InvalidCastException ex)
						{
							if (tableSchema == null)
							{
								tableSchema = TableSchema.FromDataReader(reader);
							}
							array[i] = new ExceptionValueReference(ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage(dataSourceName, Strings.ValueException_CastTypeMismatch_Complex(reader.GetValue(i).GetType().FullName, tableSchema.GetColumn(i).DataType.FullName)), Value.Null, ex));
						}
					}
				}
				catch (ValueException ex2)
				{
					array[i] = new ExceptionValueReference(ex2);
				}
				catch (Exception ex3)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex3) || ex3 is RuntimeException)
					{
						throw;
					}
					DbException ex4 = ex3 as DbException;
					if (ex4 != null)
					{
						array[i] = new ExceptionValueReference(ValueException.NewDataSourceError(ex4.Message, RecordBuilder.ToRecord(errorDetailsMarshaller(ex4)), null));
					}
					else
					{
						array[i] = new ExceptionValueReference(ValueException.NewDataSourceError<Message1>(Strings.ReadingFromProviderError(ex3.Message), Value.Null, ex3));
					}
				}
			}
			return RecordValue.New(keys, array);
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x00178514 File Offset: 0x00176714
		public static IEnumerator<IValueReference> New(IEngineHost host, IDataReader reader, bool hasRows, string dataSourceName, RecordTypeValue itemType, IResource resource)
		{
			TableSchema tableSchema = TableSchema.FromDataReader(reader);
			if (itemType.Fields.Keys.Length != tableSchema.ColumnCount && CacheConsistencyService.VerifyCacheConsistency(host))
			{
				throw DataSourceException.NewDataSourceChanged(host, dataSourceName, resource);
			}
			return new DbDataReaderEnumerator(reader, tableSchema, hasRows, itemType, dataSourceName, null, null);
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x00178560 File Offset: 0x00176760
		public static IEnumerator<IValueReference> New(IEngineHost host, DbDataReader dbDataReader, string dataSourceName, RecordTypeValue itemType, IResource resource)
		{
			bool flag = dbDataReader.IsClosed || dbDataReader.HasRows;
			TableSchema tableSchema;
			if (flag)
			{
				tableSchema = TableSchema.FromDataReader(dbDataReader);
				if (itemType.Fields.Keys.Length != tableSchema.ColumnCount && CacheConsistencyService.VerifyCacheConsistency(host))
				{
					throw DataSourceException.NewDataSourceChanged(host, dataSourceName, resource);
				}
			}
			else
			{
				tableSchema = new TableSchema(0);
			}
			return new DbDataReaderEnumerator(dbDataReader, tableSchema, flag, itemType, dataSourceName, null, null);
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x001785C8 File Offset: 0x001767C8
		public static IEnumerator<IValueReference> New(DbEnvironment environment, DbDataReader dbDataReader, bool hasRows, RecordTypeValue itemType)
		{
			TableSchema tableSchema = TableSchema.FromDataReader(dbDataReader);
			if (itemType.Fields.Keys.Length != tableSchema.ColumnCount && CacheConsistencyService.VerifyCacheConsistency(environment.Host))
			{
				throw DataSourceException.NewDataSourceChanged(environment.Host, environment.DataSourceNameString, environment.Resource);
			}
			return new DbDataReaderEnumerator(dbDataReader, tableSchema, hasRows, itemType, environment.DataSourceNameString, null, new Func<DbException, IList<RecordKeyDefinition>>(environment.GetDbExceptionDetails));
		}

		// Token: 0x04003C94 RID: 15508
		private readonly IDataReader reader;

		// Token: 0x04003C95 RID: 15509
		private readonly Keys recordKeys;

		// Token: 0x04003C96 RID: 15510
		private readonly Func<IDataReader, int, Value>[] valueMarshallers;

		// Token: 0x04003C97 RID: 15511
		private readonly string dataSourceName;

		// Token: 0x04003C98 RID: 15512
		private readonly Func<DbException, IList<RecordKeyDefinition>> errorDetailsMarshaller;

		// Token: 0x04003C99 RID: 15513
		private IValueReference current;
	}
}
