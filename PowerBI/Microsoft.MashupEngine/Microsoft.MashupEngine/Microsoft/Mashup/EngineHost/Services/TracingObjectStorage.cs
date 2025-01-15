using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B46 RID: 6982
	internal sealed class TracingObjectStorage : ObjectStorage
	{
		// Token: 0x0600AEB3 RID: 44723 RVA: 0x0023C697 File Offset: 0x0023A897
		public TracingObjectStorage(ObjectStorage storage, IEvaluationConstants evaluationConstants, string identity)
		{
			this.storage = storage;
			this.cacheStats = new CacheStats(identity, evaluationConstants, 100, 1000);
		}

		// Token: 0x17002BD1 RID: 11217
		// (get) Token: 0x0600AEB4 RID: 44724 RVA: 0x0023C6BA File Offset: 0x0023A8BA
		public override CacheSize CacheSize
		{
			get
			{
				return this.storage.CacheSize;
			}
		}

		// Token: 0x17002BD2 RID: 11218
		// (get) Token: 0x0600AEB5 RID: 44725 RVA: 0x0023C6C7 File Offset: 0x0023A8C7
		public override long CurrentVersion
		{
			get
			{
				return this.storage.CurrentVersion;
			}
		}

		// Token: 0x0600AEB6 RID: 44726 RVA: 0x0023C6D4 File Offset: 0x0023A8D4
		public override long IncrementVersion()
		{
			return this.storage.IncrementVersion();
		}

		// Token: 0x0600AEB7 RID: 44727 RVA: 0x0023C6E4 File Offset: 0x0023A8E4
		public override bool TryGetValue(string key, out object value, out DateTime createdAt, out long version)
		{
			bool flag = this.storage.TryGetValue(key, out value, out createdAt, out version);
			this.cacheStats.Access(flag);
			return flag;
		}

		// Token: 0x0600AEB8 RID: 44728 RVA: 0x0023C70F File Offset: 0x0023A90F
		public override void CommitValue(string key, object value, long size, DateTime createdAt, long version)
		{
			this.storage.CommitValue(key, value, size, createdAt, version);
			this.cacheStats.Size(this.storage.CacheSize);
		}

		// Token: 0x0600AEB9 RID: 44729 RVA: 0x0023C739 File Offset: 0x0023A939
		public override void Purge(long maxSize)
		{
			this.storage.Purge(maxSize);
		}

		// Token: 0x04005A13 RID: 23059
		private const int traceFrequency = 100;

		// Token: 0x04005A14 RID: 23060
		private const int resetFrequency = 1000;

		// Token: 0x04005A15 RID: 23061
		private readonly ObjectStorage storage;

		// Token: 0x04005A16 RID: 23062
		private readonly CacheStats cacheStats;
	}
}
