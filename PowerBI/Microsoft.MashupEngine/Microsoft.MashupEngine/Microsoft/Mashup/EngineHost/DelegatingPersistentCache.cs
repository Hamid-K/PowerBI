using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200197D RID: 6525
	public abstract class DelegatingPersistentCache : PersistentCache
	{
		// Token: 0x17002A43 RID: 10819
		// (get) Token: 0x0600A58B RID: 42379
		protected abstract PersistentCache Cache { get; }

		// Token: 0x17002A44 RID: 10820
		// (get) Token: 0x0600A58C RID: 42380 RVA: 0x002242DD File Offset: 0x002224DD
		// (set) Token: 0x0600A58D RID: 42381 RVA: 0x002242EA File Offset: 0x002224EA
		public override DateTime Staleness
		{
			get
			{
				return this.Cache.Staleness;
			}
			set
			{
				this.Cache.Staleness = value;
			}
		}

		// Token: 0x17002A45 RID: 10821
		// (get) Token: 0x0600A58E RID: 42382 RVA: 0x002242F8 File Offset: 0x002224F8
		public override long MaxEntryLength
		{
			get
			{
				return this.Cache.MaxEntryLength;
			}
		}

		// Token: 0x17002A46 RID: 10822
		// (get) Token: 0x0600A58F RID: 42383 RVA: 0x00224305 File Offset: 0x00222505
		public override CacheSize CacheSize
		{
			get
			{
				return this.Cache.CacheSize;
			}
		}

		// Token: 0x17002A47 RID: 10823
		// (get) Token: 0x0600A590 RID: 42384 RVA: 0x00224312 File Offset: 0x00222512
		public override bool? UserSpecific
		{
			get
			{
				return this.Cache.UserSpecific;
			}
		}

		// Token: 0x0600A591 RID: 42385 RVA: 0x0022431F File Offset: 0x0022251F
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			return this.Cache.TryGetStorage(key, maxStaleness, minVersion, out storage);
		}

		// Token: 0x0600A592 RID: 42386 RVA: 0x00224331 File Offset: 0x00222531
		public override IStorage CreateStorage()
		{
			return this.Cache.CreateStorage();
		}

		// Token: 0x0600A593 RID: 42387 RVA: 0x0022433E File Offset: 0x0022253E
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			this.Cache.CommitStorage(key, maxVersion, storage);
		}

		// Token: 0x0600A594 RID: 42388 RVA: 0x0022434E File Offset: 0x0022254E
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return this.Cache.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
		}

		// Token: 0x0600A595 RID: 42389 RVA: 0x00224362 File Offset: 0x00222562
		public override void Purge()
		{
			this.Cache.Purge();
		}

		// Token: 0x0600A596 RID: 42390 RVA: 0x0022436F File Offset: 0x0022256F
		public override void Dispose()
		{
			this.Cache.Dispose();
		}

		// Token: 0x17002A48 RID: 10824
		// (get) Token: 0x0600A597 RID: 42391 RVA: 0x0022437C File Offset: 0x0022257C
		public override CacheVersion Current
		{
			get
			{
				return this.Cache.Current;
			}
		}

		// Token: 0x0600A598 RID: 42392 RVA: 0x00224389 File Offset: 0x00222589
		public override CacheVersion Increment()
		{
			return this.Cache.Increment();
		}
	}
}
