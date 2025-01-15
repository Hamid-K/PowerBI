using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002A8 RID: 680
	internal class CachingDependencyResolver : IDbDependencyResolver
	{
		// Token: 0x06002195 RID: 8597 RVA: 0x0005E3DF File Offset: 0x0005C5DF
		public CachingDependencyResolver(IDbDependencyResolver underlyingResolver)
		{
			this._underlyingResolver = underlyingResolver;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0005E404 File Offset: 0x0005C604
		public virtual object GetService(Type type, object key)
		{
			return this._resolvedDependencies.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> k) => this._underlyingResolver.GetService(type, key));
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0005E454 File Offset: 0x0005C654
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this._resolvedAllDependencies.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> k) => this._underlyingResolver.GetServices(type, key));
		}

		// Token: 0x04000BB3 RID: 2995
		private readonly IDbDependencyResolver _underlyingResolver;

		// Token: 0x04000BB4 RID: 2996
		private readonly ConcurrentDictionary<Tuple<Type, object>, object> _resolvedDependencies = new ConcurrentDictionary<Tuple<Type, object>, object>();

		// Token: 0x04000BB5 RID: 2997
		private readonly ConcurrentDictionary<Tuple<Type, object>, IEnumerable<object>> _resolvedAllDependencies = new ConcurrentDictionary<Tuple<Type, object>, IEnumerable<object>>();
	}
}
