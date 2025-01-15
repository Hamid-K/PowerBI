using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002AA RID: 682
	internal class CompositeResolver<TFirst, TSecond> : IDbDependencyResolver where TFirst : class, IDbDependencyResolver where TSecond : class, IDbDependencyResolver
	{
		// Token: 0x0600219B RID: 8603 RVA: 0x0005E508 File Offset: 0x0005C708
		public CompositeResolver(TFirst firstResolver, TSecond secondResolver)
		{
			this._firstResolver = firstResolver;
			this._secondResolver = secondResolver;
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0005E51E File Offset: 0x0005C71E
		public TFirst First
		{
			get
			{
				return this._firstResolver;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0005E526 File Offset: 0x0005C726
		public TSecond Second
		{
			get
			{
				return this._secondResolver;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0005E52E File Offset: 0x0005C72E
		public virtual object GetService(Type type, object key)
		{
			return this._firstResolver.GetService(type, key) ?? this._secondResolver.GetService(type, key);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0005E558 File Offset: 0x0005C758
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this._firstResolver.GetServices(type, key).Concat(this._secondResolver.GetServices(type, key));
		}

		// Token: 0x04000BB6 RID: 2998
		private readonly TFirst _firstResolver;

		// Token: 0x04000BB7 RID: 2999
		private readonly TSecond _secondResolver;
	}
}
