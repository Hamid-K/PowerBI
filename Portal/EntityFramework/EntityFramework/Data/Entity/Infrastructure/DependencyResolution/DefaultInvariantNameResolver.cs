using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B1 RID: 689
	internal class DefaultInvariantNameResolver : IDbDependencyResolver
	{
		// Token: 0x060021C6 RID: 8646 RVA: 0x0005EF60 File Offset: 0x0005D160
		public virtual object GetService(Type type, object key)
		{
			if (!(type == typeof(IProviderInvariantName)))
			{
				return null;
			}
			DbProviderFactory dbProviderFactory = key as DbProviderFactory;
			if (dbProviderFactory == null)
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(DbProviderFactory).Name, typeof(IProviderInvariantName)));
			}
			return new ProviderInvariantName(dbProviderFactory.GetProviderInvariantName());
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0005EFB8 File Offset: 0x0005D1B8
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}
	}
}
