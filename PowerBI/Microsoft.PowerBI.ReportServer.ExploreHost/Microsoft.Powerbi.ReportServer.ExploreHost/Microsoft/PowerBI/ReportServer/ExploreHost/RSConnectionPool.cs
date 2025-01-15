using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000006 RID: 6
	internal sealed class RSConnectionPool : IDbConnectionPool, IDisposable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020EC File Offset: 0x000002EC
		internal RSConnectionPool()
			: this(new ConnectionPoolConfig())
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020F9 File Offset: 0x000002F9
		internal RSConnectionPool(ConnectionPoolConfig config)
		{
			this._config = config;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002119 File Offset: 0x00000319
		public void Dispose()
		{
			this.CloseConnections();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002121 File Offset: 0x00000321
		public int ConnectionCount
		{
			get
			{
				return (int)Interlocked.Read(ref this._connectionsCount);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002130 File Offset: 0x00000330
		public IDbConnection GetConnection(ConnectionKey connectionKey)
		{
			if (connectionKey == null)
			{
				throw new ArgumentNullException("connectionKey");
			}
			object obj = this._connectionsCache.Get(connectionKey.GetKeyString(), null);
			if (obj != null)
			{
				return this.FindConnection(connectionKey, (RSConnectionPool.EntryList)obj);
			}
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002170 File Offset: 0x00000370
		private IDbConnection FindConnection(ConnectionKey keyToFind, RSConnectionPool.EntryList connections)
		{
			bool flag = keyToFind.ShouldCheckIsAlive();
			while (!connections.IsEmpty && !connections.IsRemoved)
			{
				RSConnectionPool.ConnectionEntry connectionEntry;
				if (connections.TryDequeue(out connectionEntry))
				{
					Interlocked.Decrement(ref this._connectionsCount);
					if (connectionEntry.Key.Equals(keyToFind) && connectionEntry.IsActive && (!flag || connectionEntry.Connection.IsAlive))
					{
						RSConnectionTrace.Trace(TraceType.Verbose, "Connection {0} is taken from the pool. Key Buckets {1}, Connection Count {2}", new object[]
						{
							keyToFind.GetKeyString(),
							this._connectionsCache.GetCount(null),
							this.ConnectionCount
						});
						connectionEntry.Connection.IsFromPool = true;
						return connectionEntry.Connection;
					}
					connectionEntry.CloseConnection();
				}
			}
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002231 File Offset: 0x00000431
		public bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey)
		{
			if (connectionKey == null)
			{
				throw new ArgumentNullException("connectionKey");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.PoolConnectionImpl(connection, connectionKey);
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002258 File Offset: 0x00000458
		private void PoolConnectionImpl(IDbPoolableConnection connection, ConnectionKey connectionKey)
		{
			try
			{
				RSConnectionPool.EntryList entryList = (RSConnectionPool.EntryList)this._connectionsCache.AddOrGetExisting(connectionKey.GetKeyString(), new RSConnectionPool.EntryList(), new CacheItemPolicy
				{
					SlidingExpiration = this._config.ConnectionExpirationTime,
					Priority = CacheItemPriority.Default,
					RemovedCallback = delegate(CacheEntryRemovedArguments args)
					{
						RSConnectionPool.EntryList entryList2 = (RSConnectionPool.EntryList)args.CacheItem.Value;
						Interlocked.Add(ref this._connectionsCount, (long)(-(long)entryList2.Count));
						entryList2.ToList<RSConnectionPool.ConnectionEntry>().ForEach(delegate(RSConnectionPool.ConnectionEntry c)
						{
							c.CloseConnectionAsync();
						});
						RSConnectionTrace.Trace(TraceType.Verbose, "{0} idle connections of {1} are removed from the pool. Reason {2}, Stamp {3}", new object[]
						{
							entryList2.Count,
							args.CacheItem.Key,
							args.RemovedReason.ToString(),
							entryList2.Updated
						});
					}
				}, null);
				entryList = entryList ?? ((RSConnectionPool.EntryList)this._connectionsCache.Get(connectionKey.GetKeyString(), null));
				RSConnectionPool.ConnectionEntry connectionEntry = new RSConnectionPool.ConnectionEntry(connectionKey, connection);
				entryList.Enqueue(connectionEntry);
				long num = Interlocked.Increment(ref this._connectionsCount);
				entryList.Updated = DateTime.Now;
				RSConnectionTrace.Trace(TraceType.Verbose, "Connection {0} is added to the pool. Key Buckets {1}, Connection Count {2}, Stamp {3}", new object[]
				{
					connectionKey.GetKeyString(),
					this._connectionsCache.GetCount(null),
					num,
					entryList.Updated
				});
				this.EnsureLimit();
			}
			catch (Exception ex)
			{
				RSConnectionTrace.Trace(TraceType.Error, "Exception occurred pooling the connection: {0}", new object[] { ex.ToString() });
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002370 File Offset: 0x00000570
		public void CloseConnections()
		{
			this._connectionsCache.ToList<KeyValuePair<string, object>>().ForEach(delegate(KeyValuePair<string, object> pair)
			{
				this._connectionsCache.Remove(pair.Key, null);
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000238E File Offset: 0x0000058E
		private void EnsureLimit()
		{
			if (this.ConnectionCount > this._config.PoolSize && Interlocked.CompareExchange(ref this._trimming, 1, 0) == 0)
			{
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					try
					{
						this._connectionsCache.Trim(0);
						if (this.ConnectionCount > this._config.PoolSize)
						{
							RSConnectionTrace.Trace(TraceType.Verbose, "Connection pool is about to be trimmed, Key Buckets {0}", new object[] { this._connectionsCache.GetCount(null) });
							int num = this._config.PoolSize - (int)Math.Ceiling((double)(this._config.PoolSize * this._config.TrimSizeInPercent) / 100.0);
							num = Math.Max(0, this._config.PoolSize);
							Queue<KeyValuePair<string, object>> queue = new Queue<KeyValuePair<string, object>>(from pair in this._connectionsCache.ToArray<KeyValuePair<string, object>>()
								orderby ((RSConnectionPool.EntryList)pair.Value).Updated
								select pair);
							while (this.ConnectionCount > num && queue.Count > 0)
							{
								KeyValuePair<string, object> keyValuePair = queue.Dequeue();
								((RSConnectionPool.EntryList)keyValuePair.Value).IsRemoved = true;
								this._connectionsCache.Remove(keyValuePair.Key, null);
							}
							RSConnectionTrace.Trace(TraceType.Verbose, "Connection pool is trimmed due size limmit. Key Buckets {0}, Connection Count {1}", new object[]
							{
								this._connectionsCache.GetCount(null),
								this.ConnectionCount
							});
						}
					}
					catch (Exception ex)
					{
						RSConnectionTrace.Trace(TraceType.Error, "Exception occurred on trimming the pool: {0}", new object[] { ex.ToString() });
					}
					finally
					{
						Interlocked.Exchange(ref this._trimming, 0);
					}
				});
			}
		}

		// Token: 0x0400002C RID: 44
		private readonly MemoryCache _connectionsCache = new MemoryCache("RSConnectionPool", null);

		// Token: 0x0400002D RID: 45
		private readonly ConnectionPoolConfig _config;

		// Token: 0x0400002E RID: 46
		private long _connectionsCount;

		// Token: 0x0400002F RID: 47
		private int _trimming;

		// Token: 0x02000023 RID: 35
		private class ConnectionEntry
		{
			// Token: 0x060000EF RID: 239 RVA: 0x00004030 File Offset: 0x00002230
			public ConnectionEntry(ConnectionKey connKey, IDbPoolableConnection connection)
			{
				this.Key = connKey;
				this.Connection = connection;
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004046 File Offset: 0x00002246
			// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000404E File Offset: 0x0000224E
			public ConnectionKey Key { get; private set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004057 File Offset: 0x00002257
			// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000405F File Offset: 0x0000225F
			public IDbPoolableConnection Connection { get; private set; }

			// Token: 0x060000F4 RID: 244 RVA: 0x00004068 File Offset: 0x00002268
			public void CloseConnectionAsync()
			{
				if (this.IsActive)
				{
					ThreadPool.QueueUserWorkItem(delegate(object o)
					{
						this.CloseConnection();
					});
				}
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x00004084 File Offset: 0x00002284
			public void CloseConnection()
			{
				try
				{
					if (this.IsActive)
					{
						this.Connection.Close();
						this.Connection.Dispose();
						this.Connection = null;
					}
				}
				catch (Exception ex)
				{
					RSConnectionTrace.Trace(TraceType.Error, "Exception occurred closing the connection: {0}", new object[] { ex.ToString() });
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x000040E8 File Offset: 0x000022E8
			public bool IsActive
			{
				get
				{
					return this.Connection != null;
				}
			}
		}

		// Token: 0x02000024 RID: 36
		private class EntryList : ConcurrentQueue<RSConnectionPool.ConnectionEntry>
		{
			// Token: 0x060000F8 RID: 248 RVA: 0x000040FB File Offset: 0x000022FB
			public EntryList()
			{
				this.Updated = DateTime.Now;
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000410E File Offset: 0x0000230E
			// (set) Token: 0x060000FA RID: 250 RVA: 0x00004116 File Offset: 0x00002316
			public DateTime Updated { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000FB RID: 251 RVA: 0x0000411F File Offset: 0x0000231F
			// (set) Token: 0x060000FC RID: 252 RVA: 0x00004130 File Offset: 0x00002330
			public bool IsRemoved
			{
				get
				{
					return Interlocked.Read(ref this.m_isRemoved) != 0L;
				}
				set
				{
					Interlocked.Exchange(ref this.m_isRemoved, value ? 1L : 0L);
				}
			}

			// Token: 0x0400007A RID: 122
			private long m_isRemoved;
		}
	}
}
