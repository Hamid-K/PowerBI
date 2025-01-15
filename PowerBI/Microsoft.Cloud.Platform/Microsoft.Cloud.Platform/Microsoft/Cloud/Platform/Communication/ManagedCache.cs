using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004AD RID: 1197
	public class ManagedCache<TKey, TValue>
	{
		// Token: 0x060024AF RID: 9391 RVA: 0x000833B8 File Offset: 0x000815B8
		public ManagedCache(Func<object[], TValue> createHandler, Action<TValue> destroyHandler, Predicate<TValue> checkStateHandler)
		{
			this.m_cache = new Dictionary<TKey, TValue>();
			this.m_cacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
			this.m_createHandler = createHandler;
			this.m_destroyHandler = destroyHandler;
			this.m_checkStateHandler = checkStateHandler;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000833EC File Offset: 0x000815EC
		public TValue Get([NotNull] TKey key, params object[] createParams)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TKey>(key, "Key");
			bool flag = false;
			bool flag2 = false;
			TValue tvalue;
			TValue tvalue2;
			for (;;)
			{
				flag = false;
				using (this.AcquireLock(flag2))
				{
					if (this.m_cache.TryGetValue(key, out tvalue))
					{
						if (this.m_checkStateHandler(tvalue))
						{
							return tvalue;
						}
						flag = true;
					}
					if (!flag2)
					{
						flag2 = true;
						continue;
					}
					if (flag)
					{
						this.m_cache.Remove(key);
					}
					tvalue2 = this.m_createHandler(createParams);
					this.m_cache.Add(key, tvalue2);
				}
				break;
			}
			if (flag)
			{
				this.m_destroyHandler(tvalue);
			}
			return tvalue2;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0008349C File Offset: 0x0008169C
		public void Remove(TKey key, TValue value)
		{
			using (this.AcquireLock(true))
			{
				TValue tvalue;
				if (!this.m_cache.TryGetValue(key, out tvalue))
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Key '{0}' of value '{1}' was already removed from cache", new object[] { key, value });
					return;
				}
				if (value != tvalue)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "There is a key '{0}' in cache but it is associated with a different value then expected.", new object[] { key });
					return;
				}
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Removing key '{0}' and value '{1}' from cache", new object[] { key, value });
				this.m_cache.Remove(key);
			}
			this.m_destroyHandler(value);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00083578 File Offset: 0x00081778
		public void RemoveAll()
		{
			using (this.AcquireLock(true))
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this.m_cache)
				{
					this.m_destroyHandler(keyValuePair.Value);
				}
				this.m_cache.Clear();
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00083604 File Offset: 0x00081804
		private IDisposable AcquireLock(bool isWritter)
		{
			if (isWritter)
			{
				return new WriterLock(this.m_cacheLock);
			}
			return new ReaderLock(this.m_cacheLock);
		}

		// Token: 0x04000CE8 RID: 3304
		private Dictionary<TKey, TValue> m_cache;

		// Token: 0x04000CE9 RID: 3305
		private ReaderWriterLockSlim m_cacheLock;

		// Token: 0x04000CEA RID: 3306
		private Func<object[], TValue> m_createHandler;

		// Token: 0x04000CEB RID: 3307
		private Action<TValue> m_destroyHandler;

		// Token: 0x04000CEC RID: 3308
		private Predicate<TValue> m_checkStateHandler;
	}
}
