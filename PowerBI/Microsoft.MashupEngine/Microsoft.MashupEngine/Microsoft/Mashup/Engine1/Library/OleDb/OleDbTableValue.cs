using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x0200059D RID: 1437
	internal sealed class OleDbTableValue : TableValue
	{
		// Token: 0x06002D4E RID: 11598 RVA: 0x0008A29F File Offset: 0x0008849F
		public OleDbTableValue(OleDbEnvironment environment, OleDbIdentifier identifier, TypeValue type, IEngineHost host)
		{
			this.environment = environment;
			this.identifier = identifier;
			this.type = type;
			this.host = host;
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x0008A2C4 File Offset: 0x000884C4
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x0008A2CC File Offset: 0x000884CC
		private string QualifiedName
		{
			get
			{
				return this.identifier.ToQualifiedName(this.environment.OleDbDataSourceInfo);
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0008A2E4 File Offset: 0x000884E4
		public override void TestConnection()
		{
			this.environment.TestConnection();
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0008A2F1 File Offset: 0x000884F1
		public override IPageReader GetReader()
		{
			return this.environment.OpenTable(this.QualifiedName);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0008A304 File Offset: 0x00088504
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IDataReaderWithTableSchema dataReaderWithTableSchema = null;
			IEnumerator<IValueReference> enumerator;
			try
			{
				IPersistentCache persistentCache = this.host.GetPersistentCache();
				string cacheKey = this.GetCacheKey();
				Stream stream;
				if (persistentCache.TryGetValue(cacheKey, out stream))
				{
					dataReaderWithTableSchema = DbData.Deserialize(stream);
				}
				else
				{
					dataReaderWithTableSchema = new PageReaderDataReader(this.GetReader(), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties));
					dataReaderWithTableSchema = new DbData.CachingDbDataReader(this.host, persistentCache, cacheKey, dataReaderWithTableSchema, long.MaxValue, persistentCache.MaxEntryLength, () => this.environment.Tracer.CreateTrace("ReadTable", TraceEventType.Information), true, false);
				}
				enumerator = DbDataReaderEnumerator.New(this.host, dataReaderWithTableSchema, true, this.environment.DataSourceNameString, this.Type.AsTableType.ItemType, this.environment.Resource);
			}
			catch
			{
				if (dataReaderWithTableSchema != null)
				{
					dataReaderWithTableSchema.Dispose();
				}
				throw;
			}
			return enumerator;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0008A3EC File Offset: 0x000885EC
		private string GetCacheKey()
		{
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			persistentCacheKeyBuilder.Add("OpenRowset");
			persistentCacheKeyBuilder.Add(this.environment.CacheKey);
			persistentCacheKeyBuilder.Add(this.QualifiedName);
			return persistentCacheKeyBuilder.ToString();
		}

		// Token: 0x040013CC RID: 5068
		private readonly IEngineHost host;

		// Token: 0x040013CD RID: 5069
		private readonly OleDbEnvironment environment;

		// Token: 0x040013CE RID: 5070
		private readonly OleDbIdentifier identifier;

		// Token: 0x040013CF RID: 5071
		private TypeValue type;
	}
}
