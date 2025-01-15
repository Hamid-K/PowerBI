using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000FE RID: 254
	internal sealed class CustomProviderProxy : ICustomProvider, IDisposable
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001B946 File Offset: 0x00019B46
		public CustomProviderProxy(string logSource, ICustomProvider coreProvider)
		{
			if (logSource == null)
			{
				throw new ArgumentNullException("logSource");
			}
			if (coreProvider == null)
			{
				throw new ArgumentNullException("coreProvider");
			}
			this._logSource = logSource;
			this._coreProvider = coreProvider;
			this._logEndTxnOperation = false;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001B97F File Offset: 0x00019B7F
		private string LogSource
		{
			get
			{
				return this._logSource;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001B988 File Offset: 0x00019B88
		private TResult PerformOperation<TResult>(Func<TResult> func)
		{
			TResult result = default(TResult);
			this.PerformOperation(delegate
			{
				result = func();
			});
			return result;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		private void PerformOperation(Action action)
		{
			long num = Interlocked.Increment(ref this._providerCallCount);
			if (num % 100L == 0L)
			{
				CacheEventHelper.WriteInformationToSink(this.LogSource, "CallCount = {0}", new object[] { num });
			}
			try
			{
				action();
			}
			catch (Exception ex)
			{
				Exception e = ex;
				CacheEventHelper.WriteErrorToSink(this.LogSource, "Exception = {0}", new object[] { e });
				EventLogProvider.EventWriteExternalStoreFailure(this.LogSource, e.ToString());
				if (e is ConfigStoreException)
				{
					throw;
				}
				ThreadPool.QueueUserWorkItem(delegate
				{
					throw new ConfigStoreException(e.Message, e);
				});
				throw new ConfigStoreException(e.Message, e);
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		private void LogRowOperation(string operation, object transactionContext, string type, string key, byte[] data, long version)
		{
			if (!"Nodes".Equals(type) && !"Nodes".Equals(key))
			{
				int hashCode = transactionContext.GetHashCode();
				this._logEndTxnOperation = true;
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					StringBuilder stringBuilder = new StringBuilder(256);
					if (data != null)
					{
						foreach (byte b in data)
						{
							stringBuilder.Append(b);
							stringBuilder.Append(';');
						}
					}
					string text = stringBuilder.ToString();
					CacheEventHelper.WriteInformationToSink(this.LogSource, "Row {0} :: TxcHash {1}, TypeKey [{2}, {3}], Version {4}, Data {5}", new object[] { operation, hashCode, type, key, version, text });
					return;
				}
				CacheEventHelper.WriteInformationToSink(this.LogSource, "Row {0} :: TxcHash {1}, TypeKey [{2}, {3}], Version {4}", new object[] { operation, hashCode, type, key, version });
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001BBC3 File Offset: 0x00019DC3
		public bool IsInitialized()
		{
			return this.PerformOperation<bool>(() => this._coreProvider.IsInitialized());
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001BBD7 File Offset: 0x00019DD7
		public void Initialize()
		{
			this.PerformOperation(delegate
			{
				this._coreProvider.Initialize();
			});
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001BBEB File Offset: 0x00019DEB
		public void TestConnection()
		{
			this.PerformOperation(delegate
			{
				this._coreProvider.TestConnection();
			});
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001BC00 File Offset: 0x00019E00
		public object AddUser(string machine, string user)
		{
			return this.PerformOperation<object>(() => this._coreProvider.AddUser(machine, user));
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001BC3C File Offset: 0x00019E3C
		public void RemoveUser(string machine, string user, object state)
		{
			this.PerformOperation(delegate
			{
				this._coreProvider.RemoveUser(machine, user, state);
			});
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001BC7D File Offset: 0x00019E7D
		public void Cleanup()
		{
			this.PerformOperation(delegate
			{
				this._coreProvider.Cleanup();
			});
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001BC91 File Offset: 0x00019E91
		public Version RetrieveStoreVersion()
		{
			return this.PerformOperation<Version>(() => this._coreProvider.RetrieveStoreVersion());
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		public void Open(string connectionString)
		{
			this.PerformOperation(delegate
			{
				this._coreProvider.Open(connectionString);
			});
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001BCDB File Offset: 0x00019EDB
		public object BeginTransaction()
		{
			return this.PerformOperation<object>(() => this._coreProvider.BeginTransaction());
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001BCF0 File Offset: 0x00019EF0
		public void EndTransaction(object transactionContext, bool rollback)
		{
			if (this._logEndTxnOperation)
			{
				CacheEventHelper.WriteInformationToSink(this.LogSource, "Transaction {0} :: TxcHash {1}", new object[]
				{
					rollback ? "Rollback" : "Commit",
					transactionContext.GetHashCode()
				});
				this._logEndTxnOperation = false;
			}
			this.PerformOperation(delegate
			{
				this._coreProvider.EndTransaction(transactionContext, rollback);
			});
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public byte[] GetValue(object transactionContext, string type, string key)
		{
			return this.PerformOperation<byte[]>(() => this._coreProvider.GetValue(transactionContext, type, key));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public ConfigStoreEntry GetEntry(object transactionContext, string type, string key)
		{
			return this.PerformOperation<ConfigStoreEntry>(() => this._coreProvider.GetEntry(transactionContext, type, key));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001BE04 File Offset: 0x0001A004
		public ICollection<ConfigStoreEntry> GetEntries(object transactionContext, string type)
		{
			return this.PerformOperation<ICollection<ConfigStoreEntry>>(() => this._coreProvider.GetEntries(transactionContext, type));
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001BE40 File Offset: 0x0001A040
		public bool Insert(object transactionContext, string type, string key, byte[] data, long version)
		{
			this.LogRowOperation("Insert", transactionContext, type, key, data, version);
			return this.PerformOperation<bool>(() => this._coreProvider.Insert(transactionContext, type, key, data, version));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		public bool Update(object transactionContext, string type, string key, byte[] data, long oldVersion)
		{
			this.LogRowOperation("Update", transactionContext, type, key, data, oldVersion);
			return this.PerformOperation<bool>(() => this._coreProvider.Update(transactionContext, type, key, data, oldVersion));
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001BF38 File Offset: 0x0001A138
		public bool Delete(object transactionContext, string type, string key, long oldVersion)
		{
			this.LogRowOperation("Delete", transactionContext, type, key, null, oldVersion);
			return this.PerformOperation<bool>(() => this._coreProvider.Delete(transactionContext, type, key, oldVersion));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public void Delete(object transactionContext, string type)
		{
			this.LogRowOperation("Delete", transactionContext, type, null, null, -1L);
			this.PerformOperation(delegate
			{
				this._coreProvider.Delete(transactionContext, type);
			});
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C000 File Offset: 0x0001A200
		public DateTime GetStoreUtcTime(object transactionContext)
		{
			return this.PerformOperation<DateTime>(() => this._coreProvider.GetStoreUtcTime(transactionContext));
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001C033 File Offset: 0x0001A233
		public void Dispose()
		{
			this._coreProvider.Dispose();
		}

		// Token: 0x04000609 RID: 1545
		private const string NodeType = "Nodes";

		// Token: 0x0400060A RID: 1546
		private readonly string _logSource;

		// Token: 0x0400060B RID: 1547
		private readonly ICustomProvider _coreProvider;

		// Token: 0x0400060C RID: 1548
		private long _providerCallCount;

		// Token: 0x0400060D RID: 1549
		private bool _logEndTxnOperation;
	}
}
