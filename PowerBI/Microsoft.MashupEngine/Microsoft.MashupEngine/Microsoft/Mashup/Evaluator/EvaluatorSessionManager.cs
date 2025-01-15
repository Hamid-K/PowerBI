using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CA1 RID: 7329
	internal sealed class EvaluatorSessionManager
	{
		// Token: 0x0600B63E RID: 46654 RVA: 0x0024FFC8 File Offset: 0x0024E1C8
		public EvaluatorSessionManager(TimeSpan ttl)
		{
			this.syncRoot = new object();
			this.ttl = ttl;
			this.unreferenced = new LinkedList<EvaluatorSession>();
			this.sessions = new Dictionary<string, EvaluatorSessionManager.EvaluatorSessionEntry>();
		}

		// Token: 0x0600B63F RID: 46655 RVA: 0x0024FFF8 File Offset: 0x0024E1F8
		public IDisposable Reserve(string id)
		{
			object obj = this.syncRoot;
			IDisposable disposable;
			lock (obj)
			{
				this.TrimExpiredSessions();
				EvaluatorSessionManager.EvaluatorSessionEntry evaluatorSessionEntry;
				if (this.sessions.TryGetValue(id, out evaluatorSessionEntry))
				{
					evaluatorSessionEntry.Session.AddRef();
					if (evaluatorSessionEntry.UnreferencedNode != null)
					{
						this.unreferenced.Remove(evaluatorSessionEntry.UnreferencedNode);
						evaluatorSessionEntry.UnreferencedNode = null;
					}
				}
				else
				{
					EvaluatorSession evaluatorSession = new EvaluatorSession(id);
					evaluatorSession.AddRef();
					evaluatorSessionEntry = new EvaluatorSessionManager.EvaluatorSessionEntry(evaluatorSession);
					this.sessions[id] = evaluatorSessionEntry;
				}
				disposable = new EvaluatorSessionManager.SessionLease(this, evaluatorSessionEntry);
			}
			return disposable;
		}

		// Token: 0x0600B640 RID: 46656 RVA: 0x002500A0 File Offset: 0x0024E2A0
		public bool TryGetSession(string id, out EvaluatorSession session)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				EvaluatorSessionManager.EvaluatorSessionEntry evaluatorSessionEntry;
				if (this.sessions.TryGetValue(id, out evaluatorSessionEntry))
				{
					session = evaluatorSessionEntry.Session;
					flag2 = true;
				}
				else
				{
					session = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600B641 RID: 46657 RVA: 0x002500FC File Offset: 0x0024E2FC
		private void Release(EvaluatorSessionManager.EvaluatorSessionEntry entry)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (entry.Session.DecreaseRef() == 0)
				{
					entry.UnreferencedNode = this.unreferenced.AddLast(entry.Session);
				}
			}
		}

		// Token: 0x0600B642 RID: 46658 RVA: 0x0025015C File Offset: 0x0024E35C
		private void TrimExpiredSessions()
		{
			DateTime dateTime = DateTime.UtcNow.SafeAdd(-this.ttl);
			while (this.unreferenced.Count != 0)
			{
				LinkedListNode<EvaluatorSession> first = this.unreferenced.First;
				if (first.Value.LastRecentlyUsed >= dateTime)
				{
					break;
				}
				this.sessions.Remove(first.Value.ID);
				this.unreferenced.Remove(first);
			}
		}

		// Token: 0x04005D1A RID: 23834
		private readonly object syncRoot;

		// Token: 0x04005D1B RID: 23835
		private readonly TimeSpan ttl;

		// Token: 0x04005D1C RID: 23836
		private readonly LinkedList<EvaluatorSession> unreferenced;

		// Token: 0x04005D1D RID: 23837
		private readonly Dictionary<string, EvaluatorSessionManager.EvaluatorSessionEntry> sessions;

		// Token: 0x02001CA2 RID: 7330
		private sealed class SessionLease : IDisposable
		{
			// Token: 0x0600B643 RID: 46659 RVA: 0x002501D0 File Offset: 0x0024E3D0
			public SessionLease(EvaluatorSessionManager manager, EvaluatorSessionManager.EvaluatorSessionEntry entry)
			{
				this.manager = manager;
				this.entry = entry;
			}

			// Token: 0x0600B644 RID: 46660 RVA: 0x002501E6 File Offset: 0x0024E3E6
			public void Dispose()
			{
				if (this.manager != null)
				{
					this.manager.Release(this.entry);
					this.manager = null;
					this.entry = null;
				}
			}

			// Token: 0x04005D1E RID: 23838
			private EvaluatorSessionManager manager;

			// Token: 0x04005D1F RID: 23839
			private EvaluatorSessionManager.EvaluatorSessionEntry entry;
		}

		// Token: 0x02001CA3 RID: 7331
		private sealed class EvaluatorSessionEntry
		{
			// Token: 0x0600B645 RID: 46661 RVA: 0x0025020F File Offset: 0x0024E40F
			public EvaluatorSessionEntry(EvaluatorSession session)
			{
				this.session = session;
			}

			// Token: 0x17002D7C RID: 11644
			// (get) Token: 0x0600B646 RID: 46662 RVA: 0x0025021E File Offset: 0x0024E41E
			public EvaluatorSession Session
			{
				get
				{
					return this.session;
				}
			}

			// Token: 0x17002D7D RID: 11645
			// (get) Token: 0x0600B647 RID: 46663 RVA: 0x00250226 File Offset: 0x0024E426
			// (set) Token: 0x0600B648 RID: 46664 RVA: 0x0025022E File Offset: 0x0024E42E
			public LinkedListNode<EvaluatorSession> UnreferencedNode { get; set; }

			// Token: 0x04005D20 RID: 23840
			private readonly EvaluatorSession session;
		}
	}
}
