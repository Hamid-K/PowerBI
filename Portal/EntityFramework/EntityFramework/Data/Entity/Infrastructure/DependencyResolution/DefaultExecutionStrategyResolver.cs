using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B0 RID: 688
	internal class DefaultExecutionStrategyResolver : IDbDependencyResolver
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(Func<IDbExecutionStrategy>)))
			{
				return null;
			}
			Check.NotNull<object>(key, "key");
			if (!(key is ExecutionStrategyKey))
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(ExecutionStrategyKey).Name, "Func<IExecutionStrategy>"));
			}
			return new Func<IDbExecutionStrategy>(() => new DefaultExecutionStrategy());
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0005EF4B File Offset: 0x0005D14B
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}
	}
}
