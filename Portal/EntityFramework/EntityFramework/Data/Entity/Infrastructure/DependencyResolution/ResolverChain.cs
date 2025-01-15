using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002BB RID: 699
	internal class ResolverChain : IDbDependencyResolver
	{
		// Token: 0x060021FD RID: 8701 RVA: 0x0005F7AD File Offset: 0x0005D9AD
		public virtual void Add(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._resolvers.Push(resolver);
			this._resolversSnapshot = this._resolvers.ToArray();
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x0005F7DA File Offset: 0x0005D9DA
		public virtual IEnumerable<IDbDependencyResolver> Resolvers
		{
			get
			{
				return this._resolversSnapshot.Reverse<IDbDependencyResolver>();
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0005F7EC File Offset: 0x0005D9EC
		public virtual object GetService(Type type, object key)
		{
			return this._resolversSnapshot.Select((IDbDependencyResolver r) => r.GetService(type, key)).FirstOrDefault((object s) => s != null);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0005F84C File Offset: 0x0005DA4C
		public virtual IEnumerable<object> GetServices(Type type, object key)
		{
			return this._resolversSnapshot.SelectMany((IDbDependencyResolver r) => r.GetServices(type, key));
		}

		// Token: 0x04000BD0 RID: 3024
		private readonly ConcurrentStack<IDbDependencyResolver> _resolvers = new ConcurrentStack<IDbDependencyResolver>();

		// Token: 0x04000BD1 RID: 3025
		private volatile IDbDependencyResolver[] _resolversSnapshot = new IDbDependencyResolver[0];
	}
}
