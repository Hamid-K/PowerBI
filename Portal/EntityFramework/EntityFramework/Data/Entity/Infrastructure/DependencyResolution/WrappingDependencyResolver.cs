using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002C0 RID: 704
	internal class WrappingDependencyResolver<TService> : IDbDependencyResolver
	{
		// Token: 0x06002217 RID: 8727 RVA: 0x0005FE3C File Offset: 0x0005E03C
		public WrappingDependencyResolver(IDbDependencyResolver snapshot, Func<TService, object, TService> serviceWrapper)
		{
			this._snapshot = snapshot;
			this._serviceWrapper = serviceWrapper;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0005FE52 File Offset: 0x0005E052
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(TService)))
			{
				return null;
			}
			return this._serviceWrapper(this._snapshot.GetService(key), key);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0005FE88 File Offset: 0x0005E088
		public IEnumerable<object> GetServices(Type type, object key)
		{
			if (!(type == typeof(TService)))
			{
				return Enumerable.Empty<object>();
			}
			return (IEnumerable<object>)(from s in this._snapshot.GetServices(key)
				select this._serviceWrapper(s, key));
		}

		// Token: 0x04000BDC RID: 3036
		private readonly IDbDependencyResolver _snapshot;

		// Token: 0x04000BDD RID: 3037
		private readonly Func<TService, object, TService> _serviceWrapper;
	}
}
