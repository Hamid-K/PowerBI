using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A2F RID: 6703
	public abstract class ObjectStorage
	{
		// Token: 0x17002B18 RID: 11032
		// (get) Token: 0x0600A99C RID: 43420
		public abstract CacheSize CacheSize { get; }

		// Token: 0x17002B19 RID: 11033
		// (get) Token: 0x0600A99D RID: 43421
		public abstract long CurrentVersion { get; }

		// Token: 0x0600A99E RID: 43422
		public abstract long IncrementVersion();

		// Token: 0x0600A99F RID: 43423
		public abstract bool TryGetValue(string key, out object value, out DateTime createdAt, out long version);

		// Token: 0x0600A9A0 RID: 43424
		public abstract void CommitValue(string key, object value, long size, DateTime createdAt, long version);

		// Token: 0x0600A9A1 RID: 43425
		public abstract void Purge(long maxSize);
	}
}
