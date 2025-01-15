using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000083 RID: 131
	internal struct RequestCacheItem
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x0000BD79 File Offset: 0x00009F79
		internal RequestCacheItem(string cacheKey)
		{
			this.m_cacheKey = cacheKey;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000BD82 File Offset: 0x00009F82
		internal RequestCacheItem(string arg0, string arg1)
		{
			this.m_cacheKey = arg0 + "`(" + arg1;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000BD96 File Offset: 0x00009F96
		internal RequestCacheItem(string arg0, string arg1, string arg2)
		{
			this.m_cacheKey = string.Concat(new string[] { arg0, "`(", arg1, "`(", arg2 });
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000BDC5 File Offset: 0x00009FC5
		internal RequestCacheItem(string arg0, string arg1, string arg2, string arg3)
		{
			this.m_cacheKey = string.Concat(new string[] { arg0, "`(", arg1, "`(", arg2, "`(", arg3 });
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000BE01 File Offset: 0x0000A001
		internal string CacheKey
		{
			get
			{
				return this.m_cacheKey;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000BE09 File Offset: 0x0000A009
		internal T Commit<T>(T value)
		{
			RequestCache.Add(this.m_cacheKey, value);
			return value;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000BE1E File Offset: 0x0000A01E
		internal void Commit<T, K>(T arg0, K arg1)
		{
			RequestCache.Add(this.m_cacheKey, new object[] { arg0, arg1 });
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000BE44 File Offset: 0x0000A044
		internal T GetValue<T>() where T : class
		{
			return RequestCache.Get(this.m_cacheKey) as T;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000BE5C File Offset: 0x0000A05C
		internal bool TryGetValue<T, K>(out T arg0, out K arg1)
		{
			object[] array = RequestCache.Get(this.m_cacheKey) as object[];
			if (array == null)
			{
				arg0 = default(T);
				arg1 = default(K);
				return false;
			}
			arg0 = (T)((object)array[0]);
			arg1 = (K)((object)array[1]);
			return true;
		}

		// Token: 0x040001F7 RID: 503
		private const string Separator = "`(";

		// Token: 0x040001F8 RID: 504
		private readonly string m_cacheKey;
	}
}
