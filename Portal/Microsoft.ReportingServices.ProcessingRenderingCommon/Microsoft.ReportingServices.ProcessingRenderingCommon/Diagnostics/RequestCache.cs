using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000082 RID: 130
	public sealed class RequestCache : IDisposable
	{
		// Token: 0x06000398 RID: 920 RVA: 0x0000B9E7 File Offset: 0x00009BE7
		private RequestCache(int cacheSlots)
		{
			this.m_totalSlots = cacheSlots;
			this.m_cache = new Dictionary<string, RequestCache.CacheObject>(this.m_totalSlots, StringComparer.Ordinal);
			this.m_refCount = 1;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000BA14 File Offset: 0x00009C14
		public static void InitializeForRequest()
		{
			if (RequestCache.m_tlc == null)
			{
				int num = 16;
				if (ProcessingContext.Configuration != null)
				{
					num = ProcessingContext.Configuration.RequestCacheSlots;
				}
				RequestCache.m_tlc = new RequestCache(num);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000BA48 File Offset: 0x00009C48
		public static void DetachFromThread()
		{
			RequestCache.m_tlc = null;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000BA50 File Offset: 0x00009C50
		public static RequestCache BindToThread(RequestCache cache)
		{
			RequestCache tlc = RequestCache.m_tlc;
			RequestCache.m_tlc = cache;
			return tlc;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000BA5D File Offset: 0x00009C5D
		public static void ReleaseReferenceAndDetach()
		{
			if (RequestCache.m_tlc != null)
			{
				RequestCache.m_tlc.Dispose();
			}
			RequestCache.DetachFromThread();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BA75 File Offset: 0x00009C75
		public static void Clear()
		{
			if (RequestCache.m_tlc == null)
			{
				return;
			}
			RequestCache.m_tlc.InstanceClear();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BA89 File Offset: 0x00009C89
		public static object Add(string key, object item)
		{
			if (RequestCache.m_tlc == null)
			{
				return item;
			}
			return RequestCache.m_tlc.InstanceAdd(key, item);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public static object Get(string key)
		{
			if (RequestCache.m_tlc != null)
			{
				return RequestCache.m_tlc.InstanceGet(key);
			}
			return null;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public static bool Remove(string key)
		{
			bool flag = false;
			if (RequestCache.m_tlc != null)
			{
				flag = RequestCache.m_tlc.InstanceRemove(key);
			}
			return flag;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000BADB File Offset: 0x00009CDB
		public static RequestCache GetCurrentAndAddReference()
		{
			if (RequestCache.m_tlc != null)
			{
				Interlocked.Increment(ref RequestCache.m_tlc.m_refCount);
			}
			return RequestCache.m_tlc;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000BAFC File Offset: 0x00009CFC
		private bool InstanceRemove(string key)
		{
			bool flag = false;
			Dictionary<string, RequestCache.CacheObject> cache = this.m_cache;
			lock (cache)
			{
				RequestCache.CacheObject cacheObject;
				if (this.m_cache.TryGetValue(key, out cacheObject))
				{
					cacheObject.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000BB50 File Offset: 0x00009D50
		private object InstanceGet(string key)
		{
			Dictionary<string, RequestCache.CacheObject> cache = this.m_cache;
			lock (cache)
			{
				RequestCache.CacheObject cacheObject;
				if (this.m_cache.TryGetValue(key, out cacheObject))
				{
					cacheObject.MarkAccessed();
					return cacheObject.RetrieveInstance();
				}
			}
			return null;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000BBAC File Offset: 0x00009DAC
		private object InstanceAdd(string key, object item)
		{
			Dictionary<string, RequestCache.CacheObject> cache = this.m_cache;
			object obj;
			lock (cache)
			{
				if (!this.CheckCapacityAndClear(1))
				{
					obj = item;
				}
				else
				{
					RequestCache.CacheObject cacheObject = new RequestCache.CacheObject(key, item);
					this.m_cache.Add(key, cacheObject);
					obj = cacheObject.RetrieveInstance();
				}
			}
			return obj;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000BC10 File Offset: 0x00009E10
		private bool CheckCapacityAndClear(int n)
		{
			if (n != 1)
			{
				throw new ArgumentOutOfRangeException("n", "only supported when n == 1");
			}
			int i = 0;
			for (i = this.m_cache.Count + n - this.m_totalSlots; i > 0; i--)
			{
				RequestCache.CacheObject cacheObject = null;
				foreach (RequestCache.CacheObject cacheObject2 in this.m_cache.Values)
				{
					if (cacheObject == null || cacheObject2.LastTimeAccessed < cacheObject.LastTimeAccessed)
					{
						cacheObject = cacheObject2;
					}
				}
				if (cacheObject == null)
				{
					break;
				}
				cacheObject.Dispose();
				this.m_cache.Remove(cacheObject.Key);
			}
			return i <= 0;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		private void InstanceClear()
		{
			if (Interlocked.Decrement(ref this.m_refCount) <= 0)
			{
				Dictionary<string, RequestCache.CacheObject> cache = this.m_cache;
				lock (cache)
				{
					foreach (RequestCache.CacheObject cacheObject in this.m_cache.Values)
					{
						cacheObject.Dispose();
					}
					this.m_cache.Clear();
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public void Dispose()
		{
			RequestCache.Clear();
			RequestCache.m_tlc = null;
		}

		// Token: 0x040001F3 RID: 499
		private readonly int m_totalSlots;

		// Token: 0x040001F4 RID: 500
		private readonly Dictionary<string, RequestCache.CacheObject> m_cache;

		// Token: 0x040001F5 RID: 501
		private int m_refCount;

		// Token: 0x040001F6 RID: 502
		[ThreadStatic]
		private static RequestCache m_tlc;

		// Token: 0x020000F0 RID: 240
		private class CacheObject : IDisposable
		{
			// Token: 0x060007C4 RID: 1988 RVA: 0x000147A9 File Offset: 0x000129A9
			public CacheObject(string key, object item)
			{
				this.m_item = item;
				this.m_key = key;
				this.m_timeConstructed = DateTime.Now;
				this.m_lastAccessed = this.m_timeConstructed;
				this.m_accessCount = 0;
			}

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000147DD File Offset: 0x000129DD
			public string Key
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_key;
				}
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x060007C6 RID: 1990 RVA: 0x000147E5 File Offset: 0x000129E5
			public DateTime TimeConstructed
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_timeConstructed;
				}
			}

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000147ED File Offset: 0x000129ED
			public DateTime LastTimeAccessed
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_lastAccessed;
				}
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000147F5 File Offset: 0x000129F5
			public int AccessCount
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_accessCount;
				}
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x000147FD File Offset: 0x000129FD
			public void MarkAccessed()
			{
				this.m_lastAccessed = DateTime.Now;
				this.m_accessCount++;
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x00014818 File Offset: 0x00012A18
			public object RetrieveInstance()
			{
				RefCountedDisposable refCountedDisposable = this.m_item as RefCountedDisposable;
				if (refCountedDisposable != null)
				{
					return refCountedDisposable.AddReference();
				}
				return this.m_item;
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x00014844 File Offset: 0x00012A44
			public void Dispose()
			{
				IDisposable disposable = this.m_item as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x040004B7 RID: 1207
			private readonly DateTime m_timeConstructed;

			// Token: 0x040004B8 RID: 1208
			private readonly object m_item;

			// Token: 0x040004B9 RID: 1209
			private readonly string m_key;

			// Token: 0x040004BA RID: 1210
			private DateTime m_lastAccessed;

			// Token: 0x040004BB RID: 1211
			private int m_accessCount;
		}
	}
}
