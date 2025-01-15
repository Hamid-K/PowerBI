using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000BC RID: 188
	internal class TdsParserSessionPool
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x0002B494 File Offset: 0x00029694
		internal TdsParserSessionPool(TdsParser parser)
		{
			this._parser = parser;
			this._cache = new List<TdsParserStateObject>();
			this._freeStateObjects = new TdsParserStateObject[10];
			this._freeStateObjectCount = 0;
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserSessionPool.ctor|ADV> {0} created session pool for parser {1}", this.ObjectID, parser.ObjectID);
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0002B4F8 File Offset: 0x000296F8
		private bool IsDisposed
		{
			get
			{
				return this._freeStateObjects == null;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0002B503 File Offset: 0x00029703
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002B50C File Offset: 0x0002970C
		internal void Deactivate()
		{
			using (TryEventScope.Create<int, int>("<sc.TdsParserSessionPool.Deactivate|ADV> {0} deactivating cachedCount={1}", this.ObjectID, this._cachedCount))
			{
				List<TdsParserStateObject> cache = this._cache;
				lock (cache)
				{
					for (int i = this._cache.Count - 1; i >= 0; i--)
					{
						TdsParserStateObject tdsParserStateObject = this._cache[i];
						if (tdsParserStateObject != null && tdsParserStateObject.IsOrphaned)
						{
							SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserSessionPool.Deactivate|ADV> {0} reclaiming session {1}", this.ObjectID, tdsParserStateObject.ObjectID);
							this.PutSession(tdsParserStateObject);
						}
					}
				}
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002B5C8 File Offset: 0x000297C8
		internal void Dispose()
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserSessionPool.Dispose|ADV> {0} disposing cachedCount={1}", this.ObjectID, this._cachedCount);
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = 0; i < this._freeStateObjectCount; i++)
				{
					if (this._freeStateObjects[i] != null)
					{
						this._freeStateObjects[i].Dispose();
					}
				}
				this._freeStateObjects = null;
				this._freeStateObjectCount = 0;
				for (int j = 0; j < this._cache.Count; j++)
				{
					if (this._cache[j] != null)
					{
						if (this._cache[j].IsOrphaned)
						{
							this._cache[j].Dispose();
						}
						else
						{
							this._cache[j].DecrementPendingCallbacks(false);
						}
					}
				}
				this._cache.Clear();
				this._cachedCount = 0;
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002B6C4 File Offset: 0x000298C4
		internal TdsParserStateObject GetSession(object owner)
		{
			List<TdsParserStateObject> cache = this._cache;
			TdsParserStateObject tdsParserStateObject;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					throw ADP.ClosedConnectionError();
				}
				if (this._freeStateObjectCount > 0)
				{
					this._freeStateObjectCount--;
					tdsParserStateObject = this._freeStateObjects[this._freeStateObjectCount];
					this._freeStateObjects[this._freeStateObjectCount] = null;
				}
				else
				{
					tdsParserStateObject = this._parser.CreateSession();
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserSessionPool.CreateSession|ADV> {0} adding session {1} to pool", this.ObjectID, tdsParserStateObject.ObjectID);
					this._cache.Add(tdsParserStateObject);
					this._cachedCount = this._cache.Count;
				}
				tdsParserStateObject.Activate(owner);
			}
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.TdsParserSessionPool.GetSession|ADV> {0} using session {1}", this.ObjectID, tdsParserStateObject.ObjectID);
			return tdsParserStateObject;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002B7A8 File Offset: 0x000299A8
		internal void PutSession(TdsParserStateObject session)
		{
			bool flag = session.Deactivate();
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					session.Dispose();
				}
				else if (flag && this._freeStateObjectCount < 10)
				{
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, int>("<sc.TdsParserSessionPool.PutSession|ADV> {0} keeping session {1} cachedCount={2}", this.ObjectID, session.ObjectID, this._cachedCount);
					this._freeStateObjects[this._freeStateObjectCount] = session;
					this._freeStateObjectCount++;
				}
				else
				{
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, int>("<sc.TdsParserSessionPool.PutSession|ADV> {0} disposing session {1} cachedCount={2}", this.ObjectID, session.ObjectID, this._cachedCount);
					bool flag3 = this._cache.Remove(session);
					this._cachedCount = this._cache.Count;
					session.Dispose();
				}
				session.RemoveOwner();
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0002B894 File Offset: 0x00029A94
		internal int ActiveSessionsCount
		{
			get
			{
				return this._cachedCount - this._freeStateObjectCount;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002B8A4 File Offset: 0x00029AA4
		internal string TraceString()
		{
			return string.Format(null, "(ObjID={0}, free={1}, cached={2}, total={3})", new object[]
			{
				this._objectID,
				(this._freeStateObjects == null) ? "(null)" : this._freeStateObjectCount.ToString(null),
				this._cachedCount,
				this._cache.Count
			});
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0002B910 File Offset: 0x00029B10
		internal void BestEffortCleanup()
		{
			for (int i = 0; i < this._cache.Count; i++)
			{
				TdsParserStateObject tdsParserStateObject = this._cache[i];
				if (tdsParserStateObject != null)
				{
					SNIHandle handle = tdsParserStateObject.Handle;
					if (handle != null)
					{
						handle.Dispose();
					}
				}
			}
		}

		// Token: 0x040005E2 RID: 1506
		private const int MaxInactiveCount = 10;

		// Token: 0x040005E3 RID: 1507
		private static int s_objectTypeCount;

		// Token: 0x040005E4 RID: 1508
		private readonly int _objectID = Interlocked.Increment(ref TdsParserSessionPool.s_objectTypeCount);

		// Token: 0x040005E5 RID: 1509
		private readonly TdsParser _parser;

		// Token: 0x040005E6 RID: 1510
		private readonly List<TdsParserStateObject> _cache;

		// Token: 0x040005E7 RID: 1511
		private int _cachedCount;

		// Token: 0x040005E8 RID: 1512
		private TdsParserStateObject[] _freeStateObjects;

		// Token: 0x040005E9 RID: 1513
		private int _freeStateObjectCount;
	}
}
