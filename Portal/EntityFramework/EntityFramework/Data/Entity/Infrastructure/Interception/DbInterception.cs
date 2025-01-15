using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000285 RID: 645
	public static class DbInterception
	{
		// Token: 0x06002081 RID: 8321 RVA: 0x0005C191 File Offset: 0x0005A391
		public static void Add(IDbInterceptor interceptor)
		{
			Check.NotNull<IDbInterceptor>(interceptor, "interceptor");
			DbInterception._dispatchers.Value.AddInterceptor(interceptor);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0005C1AF File Offset: 0x0005A3AF
		public static void Remove(IDbInterceptor interceptor)
		{
			Check.NotNull<IDbInterceptor>(interceptor, "interceptor");
			DbInterception._dispatchers.Value.RemoveInterceptor(interceptor);
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0005C1CD File Offset: 0x0005A3CD
		public static DbDispatchers Dispatch
		{
			get
			{
				return DbInterception._dispatchers.Value;
			}
		}

		// Token: 0x04000B83 RID: 2947
		private static readonly Lazy<DbDispatchers> _dispatchers = new Lazy<DbDispatchers>(() => new DbDispatchers());
	}
}
