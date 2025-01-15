using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200104F RID: 4175
	internal static class DbData
	{
		// Token: 0x06006CFC RID: 27900 RVA: 0x001778CC File Offset: 0x00175ACC
		public static void Serialize(IDataReaderWithTableSchema reader, Stream stream)
		{
			using (DbDataWriter dbDataWriter = new DbDataWriter(stream))
			{
				dbDataWriter.WriteStartTable(reader);
				while (reader.Read())
				{
					dbDataWriter.WriteRow(reader);
				}
				dbDataWriter.WriteEndTable();
			}
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x0017791C File Offset: 0x00175B1C
		public static DbDataReaderWithTableSchema Deserialize(Stream stream)
		{
			return new StreamReaderDataReader(stream);
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x00177924 File Offset: 0x00175B24
		public static DbType MapToDbType(TypeValue type, DbType defaultType)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return DbType.Time;
			case ValueKind.Date:
				return DbType.Date;
			case ValueKind.DateTime:
				return DbType.DateTime;
			case ValueKind.DateTimeZone:
				return DbType.DateTimeOffset;
			case ValueKind.Duration:
				return DbType.Time;
			case ValueKind.Number:
				if (type.Equals(TypeValue.Int16))
				{
					return DbType.Int16;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return DbType.Int32;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return DbType.Int64;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return DbType.SByte;
				}
				if (type.Equals(TypeValue.Byte))
				{
					return DbType.Byte;
				}
				if (type.Equals(TypeValue.Single))
				{
					return DbType.Single;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return DbType.Decimal;
				}
				if (type.Equals(TypeValue.Currency))
				{
					return DbType.Currency;
				}
				return DbType.Double;
			case ValueKind.Logical:
				return DbType.Boolean;
			case ValueKind.Text:
				return DbType.String;
			case ValueKind.Binary:
				return DbType.Binary;
			default:
				return defaultType;
			}
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x00177A00 File Offset: 0x00175C00
		private static object ParseNumber(IDataReader reader, int index, Exception originalException)
		{
			DbDataReader dbDataReader = reader as DbDataReader;
			Value value = Value.Null;
			if (dbDataReader != null)
			{
				object obj = null;
				try
				{
					obj = dbDataReader.GetProviderSpecificValue(index);
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
				if (obj != null)
				{
					try
					{
						string text = obj.ToString();
						decimal num;
						if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
						{
							return num;
						}
						double num2;
						if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
						{
							return num2;
						}
						value = TextValue.New(text);
					}
					catch (Exception ex2) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex2))
					{
						value = TextValue.New(ex2.Message);
					}
				}
			}
			throw ValueException.NewDataSourceError<Message1>(Strings.ReadingFromProviderError(originalException.Message), value, originalException);
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x00177AE4 File Offset: 0x00175CE4
		public static object GetValue(IDataReader reader, int index, Func<DbException, ValueException> convertDbException = null)
		{
			object obj;
			try
			{
				if (reader.IsDBNull(index))
				{
					obj = DBNull.Value;
				}
				else
				{
					obj = reader[index];
				}
			}
			catch (OverflowException ex)
			{
				obj = DbData.ParseNumber(reader, index, ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				obj = DbData.ParseNumber(reader, index, ex2);
			}
			catch (InvalidCastException ex3)
			{
				obj = DbData.ParseNumber(reader, index, ex3);
			}
			catch (Exception ex4) when (!(ex4 is RuntimeException) && Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex4))
			{
				DbException ex5 = ex4 as DbException;
				if (ex5 != null && convertDbException != null)
				{
					throw convertDbException(ex5);
				}
				throw ValueException.NewDataSourceError<Message1>(Strings.ReadingFromProviderError(ex4.Message), Value.Null, ex4);
			}
			return obj;
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x00177BC4 File Offset: 0x00175DC4
		public static void WriteTable(Stream stream, DataTable table)
		{
			using (IDataReader dataReader = table.CreateDataReader())
			{
				DbData.Serialize(dataReader.WithTableSchema(), stream);
			}
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x00177C00 File Offset: 0x00175E00
		public static DataTable ReadTable(Stream stream)
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			using (IDataReader dataReader = DbData.Deserialize(stream))
			{
				dataTable.Load(dataReader);
			}
			return dataTable;
		}

		// Token: 0x02001050 RID: 4176
		public class CachingDbDataReader : DbDataReaderWithTableSchema
		{
			// Token: 0x06006D03 RID: 27907 RVA: 0x00177C4C File Offset: 0x00175E4C
			public CachingDbDataReader(IEngineHost host, IPersistentCache cache, OneOf<string, StructuredCacheKey> key, IDataReaderWithTableSchema reader, long count, long maxLength, Action<Action> readWrapper, bool disposeReader, bool cacheRemainderOnClose, Func<DbException, ValueException> convertDbException = null)
			{
				DbData.CachingDbDataReader <>4__this = this;
				this.host = host;
				this.reader = reader;
				this.readWrapper = readWrapper;
				this.count = count;
				this.stream = new PersistentCacheExtensions.WriteOnlyCachingStream(cache, key, maxLength, new Action(this.SetCacheDiscarded));
				this.cacheWriter = new DbDataWriter(this.stream);
				this.currentCount = 0;
				this.currentRowValues = new object[reader.FieldCount];
				this.readWrapper(delegate
				{
					<>4__this.cacheWriter.WriteStartTable(reader);
				});
				this.disposeReader = disposeReader;
				this.cacheRemainderOnClose = cacheRemainderOnClose;
				this.convertDbException = convertDbException;
			}

			// Token: 0x06006D04 RID: 27908 RVA: 0x00177D10 File Offset: 0x00175F10
			public CachingDbDataReader(IEngineHost host, IPersistentCache cache, OneOf<string, StructuredCacheKey> key, IDataReaderWithTableSchema reader, long count, long maxLength, Func<IHostTrace> tracer, bool disposeReader, bool cacheRemainderOnClose)
			{
				DbData.CachingDbDataReader <>4__this = this;
				this.host = host;
				this.reader = reader;
				this.readWrapper = delegate(Action action)
				{
					<>4__this.TraceException(tracer, action);
				};
				this.count = count;
				this.stream = new PersistentCacheExtensions.WriteOnlyCachingStream(cache, key, maxLength, null);
				this.cacheWriter = new DbDataWriter(this.stream);
				this.currentCount = 0;
				this.currentRowValues = new object[reader.FieldCount];
				this.readWrapper(delegate
				{
					<>4__this.cacheWriter.WriteStartTable(reader);
				});
				this.disposeReader = disposeReader;
				this.cacheRemainderOnClose = cacheRemainderOnClose;
				this.convertDbException = null;
			}

			// Token: 0x06006D05 RID: 27909 RVA: 0x00177DDC File Offset: 0x00175FDC
			public override void Close()
			{
				try
				{
					while (this.cacheRemainderOnClose && this.cacheWriter != null && !this.exceedsMaxCacheLength && this.Read())
					{
					}
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					this.host.LogIgnoredException(ex);
					this.stream.Discard();
				}
				if (this.disposeReader && this.reader != null)
				{
					this.reader.Dispose();
				}
				this.reader = null;
			}

			// Token: 0x17001EF6 RID: 7926
			// (get) Token: 0x06006D06 RID: 27910 RVA: 0x000091AE File Offset: 0x000073AE
			public override int Depth
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001EF7 RID: 7927
			// (get) Token: 0x06006D07 RID: 27911 RVA: 0x00177E64 File Offset: 0x00176064
			public override int FieldCount
			{
				get
				{
					return this.reader.FieldCount;
				}
			}

			// Token: 0x17001EF8 RID: 7928
			// (get) Token: 0x06006D08 RID: 27912 RVA: 0x00177E71 File Offset: 0x00176071
			public override TableSchema Schema
			{
				get
				{
					return this.reader.Schema;
				}
			}

			// Token: 0x06006D09 RID: 27913 RVA: 0x00177E7E File Offset: 0x0017607E
			public override bool GetBoolean(int ordinal)
			{
				return (bool)this.GetValue(ordinal);
			}

			// Token: 0x06006D0A RID: 27914 RVA: 0x00177E8C File Offset: 0x0017608C
			public override byte GetByte(int ordinal)
			{
				return (byte)this.GetValue(ordinal);
			}

			// Token: 0x06006D0B RID: 27915 RVA: 0x000091AE File Offset: 0x000073AE
			public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006D0C RID: 27916 RVA: 0x00177E9A File Offset: 0x0017609A
			public override char GetChar(int ordinal)
			{
				return (char)this.GetValue(ordinal);
			}

			// Token: 0x06006D0D RID: 27917 RVA: 0x000091AE File Offset: 0x000073AE
			public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006D0E RID: 27918 RVA: 0x000091AE File Offset: 0x000073AE
			public override string GetDataTypeName(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006D0F RID: 27919 RVA: 0x00177EA8 File Offset: 0x001760A8
			public override DateTime GetDateTime(int ordinal)
			{
				return (DateTime)this.GetValue(ordinal);
			}

			// Token: 0x06006D10 RID: 27920 RVA: 0x00177EB6 File Offset: 0x001760B6
			public override decimal GetDecimal(int ordinal)
			{
				return (decimal)this.GetValue(ordinal);
			}

			// Token: 0x06006D11 RID: 27921 RVA: 0x00177EC4 File Offset: 0x001760C4
			public override double GetDouble(int ordinal)
			{
				return (double)this.GetValue(ordinal);
			}

			// Token: 0x06006D12 RID: 27922 RVA: 0x000091AE File Offset: 0x000073AE
			public override IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006D13 RID: 27923 RVA: 0x00177ED2 File Offset: 0x001760D2
			public override Type GetFieldType(int ordinal)
			{
				return this.GetValue(ordinal).GetType();
			}

			// Token: 0x06006D14 RID: 27924 RVA: 0x00177EE0 File Offset: 0x001760E0
			public override float GetFloat(int ordinal)
			{
				return (float)this.GetValue(ordinal);
			}

			// Token: 0x06006D15 RID: 27925 RVA: 0x00177EEE File Offset: 0x001760EE
			public override Guid GetGuid(int ordinal)
			{
				return (Guid)this.GetValue(ordinal);
			}

			// Token: 0x06006D16 RID: 27926 RVA: 0x00177EFC File Offset: 0x001760FC
			public override short GetInt16(int ordinal)
			{
				return (short)this.GetValue(ordinal);
			}

			// Token: 0x06006D17 RID: 27927 RVA: 0x00177F0A File Offset: 0x0017610A
			public override int GetInt32(int ordinal)
			{
				return (int)this.GetValue(ordinal);
			}

			// Token: 0x06006D18 RID: 27928 RVA: 0x00177F18 File Offset: 0x00176118
			public override long GetInt64(int ordinal)
			{
				return (long)this.GetValue(ordinal);
			}

			// Token: 0x06006D19 RID: 27929 RVA: 0x00177F26 File Offset: 0x00176126
			public override string GetName(int ordinal)
			{
				return this.reader.GetName(ordinal);
			}

			// Token: 0x06006D1A RID: 27930 RVA: 0x00177F34 File Offset: 0x00176134
			public override int GetOrdinal(string name)
			{
				return this.reader.GetOrdinal(name);
			}

			// Token: 0x06006D1B RID: 27931 RVA: 0x00177F42 File Offset: 0x00176142
			public override string GetString(int ordinal)
			{
				return (string)this.GetValue(ordinal);
			}

			// Token: 0x06006D1C RID: 27932 RVA: 0x00177F50 File Offset: 0x00176150
			public override object GetValue(int ordinal)
			{
				Exception ex = this.currentRowValues[ordinal] as Exception;
				if (ex != null)
				{
					throw ex;
				}
				return this.currentRowValues[ordinal];
			}

			// Token: 0x06006D1D RID: 27933 RVA: 0x00177F78 File Offset: 0x00176178
			public override int GetValues(object[] values)
			{
				int num = Math.Min(this.currentRowValues.Length, values.Length);
				Array.Copy(this.currentRowValues, values, num);
				return num;
			}

			// Token: 0x17001EF9 RID: 7929
			// (get) Token: 0x06006D1E RID: 27934 RVA: 0x000091AE File Offset: 0x000073AE
			public override bool HasRows
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001EFA RID: 7930
			// (get) Token: 0x06006D1F RID: 27935 RVA: 0x00177FA4 File Offset: 0x001761A4
			public override bool IsClosed
			{
				get
				{
					return this.reader == null;
				}
			}

			// Token: 0x06006D20 RID: 27936 RVA: 0x00177FAF File Offset: 0x001761AF
			public override bool IsDBNull(int ordinal)
			{
				return this.currentRowValues[ordinal] == DBNull.Value;
			}

			// Token: 0x06006D21 RID: 27937 RVA: 0x000091AE File Offset: 0x000073AE
			public override bool NextResult()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006D22 RID: 27938 RVA: 0x00177FC0 File Offset: 0x001761C0
			private bool WrappedRead()
			{
				bool result = false;
				this.readWrapper(delegate
				{
					result = this.reader.Read();
					if (!result)
					{
						this.reader.Close();
					}
				});
				return result;
			}

			// Token: 0x06006D23 RID: 27939 RVA: 0x00178000 File Offset: 0x00176200
			public override bool Read()
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException();
				}
				if (this.cacheWriter == null)
				{
					return false;
				}
				bool flag2;
				try
				{
					bool flag = (long)this.currentCount < this.count && this.WrappedRead();
					if (flag)
					{
						for (int i = 0; i < this.reader.FieldCount; i++)
						{
							try
							{
								this.currentRowValues[i] = DbData.GetValue(this.reader, i, this.convertDbException);
							}
							catch (ValueException ex)
							{
								this.currentRowValues[i] = ex;
								if (this.reader.IsClosed)
								{
									throw;
								}
							}
							this.cacheWriter.WriteValue(this.currentRowValues[i]);
						}
						this.currentCount++;
					}
					else if (this.cacheWriter != null)
					{
						this.cacheWriter.Dispose();
						this.stream.Close();
						this.cacheWriter = null;
					}
					flag2 = flag;
				}
				catch (Exception)
				{
					this.cacheWriter.Dispose();
					this.stream.Discard();
					this.cacheWriter = null;
					throw;
				}
				return flag2;
			}

			// Token: 0x17001EFB RID: 7931
			// (get) Token: 0x06006D24 RID: 27940 RVA: 0x0017811C File Offset: 0x0017631C
			public override int RecordsAffected
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x17001EFC RID: 7932
			public override object this[string name]
			{
				get
				{
					return this.reader[name];
				}
			}

			// Token: 0x17001EFD RID: 7933
			public override object this[int ordinal]
			{
				get
				{
					return this.currentRowValues[ordinal];
				}
			}

			// Token: 0x06006D27 RID: 27943 RVA: 0x00178137 File Offset: 0x00176337
			protected override void Dispose(bool disposing)
			{
				if (this.reader != null && disposing && this.disposeReader)
				{
					this.reader.Dispose();
				}
				this.reader = null;
			}

			// Token: 0x06006D28 RID: 27944 RVA: 0x00178160 File Offset: 0x00176360
			private void TraceException(Func<IHostTrace> tracer, Action action)
			{
				try
				{
					action();
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = tracer())
					{
						hostTrace.Add(ex, true);
						throw;
					}
				}
			}

			// Token: 0x06006D29 RID: 27945 RVA: 0x001781B0 File Offset: 0x001763B0
			private void SetCacheDiscarded()
			{
				this.exceedsMaxCacheLength = true;
			}

			// Token: 0x04003C81 RID: 15489
			private readonly IEngineHost host;

			// Token: 0x04003C82 RID: 15490
			private readonly long count;

			// Token: 0x04003C83 RID: 15491
			private readonly PersistentCacheExtensions.WriteOnlyCachingStream stream;

			// Token: 0x04003C84 RID: 15492
			private readonly object[] currentRowValues;

			// Token: 0x04003C85 RID: 15493
			private readonly Action<Action> readWrapper;

			// Token: 0x04003C86 RID: 15494
			private readonly bool disposeReader;

			// Token: 0x04003C87 RID: 15495
			private readonly bool cacheRemainderOnClose;

			// Token: 0x04003C88 RID: 15496
			private readonly Func<DbException, ValueException> convertDbException;

			// Token: 0x04003C89 RID: 15497
			private IDataReaderWithTableSchema reader;

			// Token: 0x04003C8A RID: 15498
			private int currentCount;

			// Token: 0x04003C8B RID: 15499
			private DbDataWriter cacheWriter;

			// Token: 0x04003C8C RID: 15500
			private bool exceedsMaxCacheLength;
		}
	}
}
