using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F9 RID: 4345
	internal sealed class PageCachingDataReader : IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x0600719B RID: 29083 RVA: 0x00186B44 File Offset: 0x00184D44
		public static IDataReaderWithTableSchema New(IEngineHost engineHost, IPersistentCache cache, int maxPageSize, OneOf<string, StructuredCacheKey> baseKey, RowRange range, Func<RowRange, IDataReaderWithTableSchema> readerProvider, IResource resource = null)
		{
			RowRange rowRange = RowRange.All;
			long num = range.SkipCount.Value % (long)maxPageSize;
			long num2 = range.SkipCount.Value - num;
			rowRange = rowRange.Skip(new RowCount(num2));
			if (!range.TakeCount.IsInfinite)
			{
				long num3 = range.TakeCount.Value + num;
				long num4 = num3 % (long)maxPageSize;
				long num5 = ((num4 > 0L || num3 == 0L) ? (num3 + (long)maxPageSize - num4) : num3);
				rowRange = rowRange.Take(new RowCount(num5));
			}
			return new SkipTakeDataReader(new PageCachingDataReader(engineHost, cache, maxPageSize, baseKey, rowRange, readerProvider, resource), RowRange.All.Skip(new RowCount(num)).Take(range.TakeCount));
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x00186C14 File Offset: 0x00184E14
		private PageCachingDataReader(IEngineHost engineHost, IPersistentCache cache, int maxPageSize, OneOf<string, StructuredCacheKey> baseKey, RowRange range, Func<RowRange, IDataReaderWithTableSchema> readerProvider, IResource resource)
		{
			this.engineHost = engineHost;
			this.cache = cache;
			this.maxPageSize = maxPageSize;
			this.baseKey = baseKey.AppendPart(maxPageSize.ToString(CultureInfo.InvariantCulture));
			this.range = range;
			this.readerProvider = readerProvider;
			this.resource = resource;
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x0600719D RID: 29085 RVA: 0x00186C6D File Offset: 0x00184E6D
		public bool IsClosed
		{
			get
			{
				return this.isDone;
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x0600719E RID: 29086 RVA: 0x00002105 File Offset: 0x00000305
		public int RecordsAffected
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x0600719F RID: 29087 RVA: 0x00186C75 File Offset: 0x00184E75
		public int Depth
		{
			get
			{
				return this.pageReader.Depth;
			}
		}

		// Token: 0x17001FE1 RID: 8161
		public object this[string name]
		{
			get
			{
				return this.pageReader[name];
			}
		}

		// Token: 0x17001FE2 RID: 8162
		public object this[int i]
		{
			get
			{
				return this.pageReader[i];
			}
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x000091AE File Offset: 0x000073AE
		public bool NextResult()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x060071A3 RID: 29091 RVA: 0x00186C9E File Offset: 0x00184E9E
		public TableSchema Schema
		{
			get
			{
				this.EnsureMetadataRead();
				return this.schema;
			}
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x00186CAC File Offset: 0x00184EAC
		public bool Read()
		{
			bool flag;
			try
			{
				if (this.isDone)
				{
					flag = false;
				}
				else
				{
					if (this.pageReader == null)
					{
						this.GetNextPage();
					}
					if (!this.pageReader.Read())
					{
						if (this.fetchedFromPage < (long)this.maxPageSize)
						{
							this.isDone = true;
						}
						this.pageReader.Dispose();
						this.pageReader = null;
						if (this.cacheStream != null)
						{
							this.cacheStream.Dispose();
						}
						flag = this.Read();
					}
					else
					{
						this.fetchedFromPage += 1L;
						this.rowCount += 1L;
						flag = true;
					}
				}
			}
			catch (Exception)
			{
				if (this.cacheStream != null)
				{
					this.cacheStream.Discard();
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x00186D70 File Offset: 0x00184F70
		public void Close()
		{
			if (this.pageReader != null)
			{
				try
				{
					while (this.pageReader.Read())
					{
					}
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = TracingService.CreateTrace(this.engineHost, "Engine/IO/Db/PageCachingDataReader/Close", TraceEventType.Information, this.resource))
					{
						hostTrace.Add(ex, true);
					}
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					this.engineHost.LogIgnoredException(ex);
					this.cacheStream.Discard();
				}
				this.pageReader.Dispose();
				this.pageReader = null;
			}
			if (this.cacheStream != null)
			{
				this.cacheStream.Dispose();
				this.cacheStream = null;
			}
			if (this.dataSourceReader != null)
			{
				this.dataSourceReader.Dispose();
				this.dataSourceReader = null;
			}
			this.isDone = true;
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x00186E50 File Offset: 0x00185050
		[Obsolete]
		public DataTable GetSchemaTable()
		{
			return this.Schema.ToDataTable();
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x00186E5D File Offset: 0x0018505D
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x060071A8 RID: 29096 RVA: 0x00186E65 File Offset: 0x00185065
		public int FieldCount
		{
			get
			{
				this.EnsureMetadataRead();
				return this.fieldCount;
			}
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x00186E73 File Offset: 0x00185073
		public bool GetBoolean(int i)
		{
			return this.pageReader.GetBoolean(i);
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x00186E81 File Offset: 0x00185081
		public byte GetByte(int i)
		{
			return this.pageReader.GetByte(i);
		}

		// Token: 0x060071AB RID: 29099 RVA: 0x00186E8F File Offset: 0x0018508F
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return this.pageReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x00186EA3 File Offset: 0x001850A3
		public char GetChar(int i)
		{
			return this.pageReader.GetChar(i);
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x00186EB1 File Offset: 0x001850B1
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return this.pageReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x00186EC5 File Offset: 0x001850C5
		public IDataReader GetData(int i)
		{
			return this.pageReader.GetData(i);
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x00186ED3 File Offset: 0x001850D3
		public string GetDataTypeName(int i)
		{
			return this.pageReader.GetDataTypeName(i);
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x00186EE1 File Offset: 0x001850E1
		public DateTime GetDateTime(int i)
		{
			return this.pageReader.GetDateTime(i);
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x00186EEF File Offset: 0x001850EF
		public decimal GetDecimal(int i)
		{
			return this.pageReader.GetDecimal(i);
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x00186EFD File Offset: 0x001850FD
		public double GetDouble(int i)
		{
			return this.pageReader.GetDouble(i);
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x00186F0B File Offset: 0x0018510B
		public Type GetFieldType(int i)
		{
			return this.pageReader.GetFieldType(i);
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x00186F19 File Offset: 0x00185119
		public float GetFloat(int i)
		{
			return this.pageReader.GetFloat(i);
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x00186F27 File Offset: 0x00185127
		public Guid GetGuid(int i)
		{
			return this.pageReader.GetGuid(i);
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x00186F35 File Offset: 0x00185135
		public short GetInt16(int i)
		{
			return this.pageReader.GetInt16(i);
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x00186F43 File Offset: 0x00185143
		public int GetInt32(int i)
		{
			return this.pageReader.GetInt32(i);
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x00186F51 File Offset: 0x00185151
		public long GetInt64(int i)
		{
			return this.pageReader.GetInt64(i);
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x00186F5F File Offset: 0x0018515F
		public string GetName(int i)
		{
			return this.pageReader.GetName(i);
		}

		// Token: 0x060071BA RID: 29114 RVA: 0x00186F6D File Offset: 0x0018516D
		public int GetOrdinal(string name)
		{
			return this.pageReader.GetOrdinal(name);
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x00186F7B File Offset: 0x0018517B
		public string GetString(int i)
		{
			return this.pageReader.GetString(i);
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x00186F89 File Offset: 0x00185189
		public object GetValue(int i)
		{
			return this.pageReader.GetValue(i);
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x00186F97 File Offset: 0x00185197
		public int GetValues(object[] values)
		{
			return this.pageReader.GetValues(values);
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x00186FA5 File Offset: 0x001851A5
		public bool IsDBNull(int i)
		{
			return this.pageReader.IsDBNull(i);
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x00186FB3 File Offset: 0x001851B3
		private void EnsureMetadataRead()
		{
			if (this.schema == null)
			{
				this.GetNextPage();
			}
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x00186FC4 File Offset: 0x001851C4
		private void GetNextPage()
		{
			long num = this.range.SkipCount.Value + this.rowCount;
			OneOf<string, StructuredCacheKey> oneOf = this.baseKey.AppendPart(num.ToString(CultureInfo.InvariantCulture));
			Stream stream;
			if (this.dataSourceReader == null && this.cache.TryGetValue(oneOf, out stream))
			{
				this.pageReader = DbData.Deserialize(stream);
			}
			else
			{
				if (this.dataSourceReader == null)
				{
					RowRange rowRange = RowRange.All.Skip(new RowCount(num));
					if (!this.range.TakeCount.IsInfinite)
					{
						rowRange = rowRange.Take(new RowCount(this.range.TakeCount.Value - this.rowCount));
					}
					this.dataSourceReader = this.readerProvider(rowRange);
				}
				this.cacheStream = new PersistentCacheExtensions.WriteOnlyCachingStream(this.cache, oneOf, this.cache.MaxEntryLength, null);
				this.pageReader = new StreamWriterDataReader(this.dataSourceReader, this.cacheStream, (long)this.maxPageSize);
			}
			if (this.schema == null)
			{
				this.schema = this.pageReader.Schema;
				this.fieldCount = this.pageReader.FieldCount;
			}
			this.fetchedFromPage = 0L;
		}

		// Token: 0x04003ED4 RID: 16084
		private const string keySeparator = "/";

		// Token: 0x04003ED5 RID: 16085
		private readonly IEngineHost engineHost;

		// Token: 0x04003ED6 RID: 16086
		private readonly IPersistentCache cache;

		// Token: 0x04003ED7 RID: 16087
		private readonly int maxPageSize;

		// Token: 0x04003ED8 RID: 16088
		private readonly OneOf<string, StructuredCacheKey> baseKey;

		// Token: 0x04003ED9 RID: 16089
		private readonly RowRange range;

		// Token: 0x04003EDA RID: 16090
		private readonly Func<RowRange, IDataReaderWithTableSchema> readerProvider;

		// Token: 0x04003EDB RID: 16091
		private readonly IResource resource;

		// Token: 0x04003EDC RID: 16092
		private bool isDone;

		// Token: 0x04003EDD RID: 16093
		private long rowCount;

		// Token: 0x04003EDE RID: 16094
		private long fetchedFromPage;

		// Token: 0x04003EDF RID: 16095
		private IDataReaderWithTableSchema pageReader;

		// Token: 0x04003EE0 RID: 16096
		private IDataReaderWithTableSchema dataSourceReader;

		// Token: 0x04003EE1 RID: 16097
		private PersistentCacheExtensions.WriteOnlyCachingStream cacheStream;

		// Token: 0x04003EE2 RID: 16098
		private TableSchema schema;

		// Token: 0x04003EE3 RID: 16099
		private int fieldCount;
	}
}
