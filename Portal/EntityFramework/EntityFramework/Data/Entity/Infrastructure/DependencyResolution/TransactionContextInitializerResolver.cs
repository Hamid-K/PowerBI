using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002BE RID: 702
	internal class TransactionContextInitializerResolver : IDbDependencyResolver
	{
		// Token: 0x0600220E RID: 8718 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		public object GetService(Type type, object key)
		{
			Check.NotNull<Type>(type, "type");
			Type type2 = type.TryGetElementType(typeof(IDatabaseInitializer<>));
			if (type2 != null && typeof(TransactionContext).IsAssignableFrom(type2))
			{
				return this._initializers.GetOrAdd(type2, new Func<Type, object>(this.CreateInitializerInstance));
			}
			return null;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0005FC9B File Offset: 0x0005DE9B
		private object CreateInitializerInstance(Type type)
		{
			return Activator.CreateInstance(typeof(TransactionContextInitializer<>).MakeGenericType(new Type[] { type }));
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0005FCBB File Offset: 0x0005DEBB
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000BD8 RID: 3032
		private readonly ConcurrentDictionary<Type, object> _initializers = new ConcurrentDictionary<Type, object>();
	}
}
