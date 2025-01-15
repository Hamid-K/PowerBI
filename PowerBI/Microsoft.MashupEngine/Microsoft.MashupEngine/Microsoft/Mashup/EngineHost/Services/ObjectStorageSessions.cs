using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A30 RID: 6704
	public class ObjectStorageSessions
	{
		// Token: 0x0600A9A3 RID: 43427 RVA: 0x00230EC0 File Offset: 0x0022F0C0
		public ObjectStorageSessions(string identity, long maxTotalSize)
		{
			this.syncRoot = new object();
			this.maxTotalSize = maxTotalSize;
			this.cacheStats = new CacheStats(identity, null, 10, 100);
			this.sessions = new LruCache<string, ObjectStorageSessions.Entry>(new Func<bool>(this.ShouldPurge), new Action<string, ObjectStorageSessions.Entry>(this.OnSessionRemoved));
		}

		// Token: 0x17002B1A RID: 11034
		// (get) Token: 0x0600A9A4 RID: 43428 RVA: 0x00230F19 File Offset: 0x0022F119
		public long MaxTotalSize
		{
			get
			{
				return this.maxTotalSize;
			}
		}

		// Token: 0x0600A9A5 RID: 43429 RVA: 0x00230F24 File Offset: 0x0022F124
		public ObjectStorageSessions.Session NewSession(string session, Func<ObjectStorage> storageCtor)
		{
			object obj = this.syncRoot;
			ObjectStorageSessions.Session session2;
			lock (obj)
			{
				ObjectStorageSessions.Entry entry;
				if (this.sessions.TryGetValue(session, out entry))
				{
					this.cacheStats.Access(true);
				}
				else
				{
					this.cacheStats.Access(false);
					entry = new ObjectStorageSessions.Entry
					{
						storage = storageCtor()
					};
					this.sessions.Add(session, entry);
					this.currentTotalSize += 24L;
					this.cacheStats.Size(new CacheSize(this.sessions.Count, this.currentTotalSize));
				}
				session2 = new ObjectStorageSessions.NotifyingSession(this, session, entry);
			}
			return session2;
		}

		// Token: 0x0600A9A6 RID: 43430 RVA: 0x00230FE4 File Offset: 0x0022F1E4
		private bool ShouldPurge()
		{
			return this.currentTotalSize > this.maxTotalSize;
		}

		// Token: 0x0600A9A7 RID: 43431 RVA: 0x00230FF4 File Offset: 0x0022F1F4
		private void OnSessionRemoved(string session, ObjectStorageSessions.Entry entry)
		{
			this.currentTotalSize -= entry.lastSize;
			this.currentTotalSize -= 24L;
		}

		// Token: 0x0600A9A8 RID: 43432 RVA: 0x0023101C File Offset: 0x0022F21C
		private void OnSessionDisposed(string session, ObjectStorageSessions.Entry entry)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.sessions.ContainsKey(session))
				{
					long totalSize = entry.storage.CacheSize.TotalSize;
					this.currentTotalSize += totalSize - entry.lastSize;
					entry.lastSize = totalSize;
				}
			}
		}

		// Token: 0x04005831 RID: 22577
		private const int traceFrequency = 10;

		// Token: 0x04005832 RID: 22578
		private const int resetFrequency = 100;

		// Token: 0x04005833 RID: 22579
		private readonly object syncRoot;

		// Token: 0x04005834 RID: 22580
		private readonly long maxTotalSize;

		// Token: 0x04005835 RID: 22581
		private readonly CacheStats cacheStats;

		// Token: 0x04005836 RID: 22582
		private LruCache<string, ObjectStorageSessions.Entry> sessions;

		// Token: 0x04005837 RID: 22583
		private long currentTotalSize;

		// Token: 0x02001A31 RID: 6705
		public abstract class Session : IDisposable
		{
			// Token: 0x17002B1B RID: 11035
			// (get) Token: 0x0600A9A9 RID: 43433
			public abstract ObjectStorage Storage { get; }

			// Token: 0x0600A9AA RID: 43434
			public abstract void Dispose();
		}

		// Token: 0x02001A32 RID: 6706
		private sealed class NotifyingSession : ObjectStorageSessions.Session
		{
			// Token: 0x0600A9AC RID: 43436 RVA: 0x00231094 File Offset: 0x0022F294
			public NotifyingSession(ObjectStorageSessions sessions, string session, ObjectStorageSessions.Entry entry)
			{
				this.sessions = sessions;
				this.session = session;
				this.entry = entry;
			}

			// Token: 0x17002B1C RID: 11036
			// (get) Token: 0x0600A9AD RID: 43437 RVA: 0x002310B1 File Offset: 0x0022F2B1
			public override ObjectStorage Storage
			{
				get
				{
					return this.entry.storage;
				}
			}

			// Token: 0x0600A9AE RID: 43438 RVA: 0x002310BE File Offset: 0x0022F2BE
			public override void Dispose()
			{
				this.sessions.OnSessionDisposed(this.session, this.entry);
			}

			// Token: 0x04005838 RID: 22584
			private readonly ObjectStorageSessions sessions;

			// Token: 0x04005839 RID: 22585
			private readonly string session;

			// Token: 0x0400583A RID: 22586
			private readonly ObjectStorageSessions.Entry entry;
		}

		// Token: 0x02001A33 RID: 6707
		private class Entry
		{
			// Token: 0x0400583B RID: 22587
			public const int Size = 24;

			// Token: 0x0400583C RID: 22588
			public ObjectStorage storage;

			// Token: 0x0400583D RID: 22589
			public long lastSize;
		}
	}
}
