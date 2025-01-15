using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002AB RID: 683
	internal class DatabaseInitializerResolver : IDbDependencyResolver
	{
		// Token: 0x060021A0 RID: 8608 RVA: 0x0005E584 File Offset: 0x0005C784
		public virtual object GetService(Type type, object key)
		{
			Type type2 = type.TryGetElementType(typeof(IDatabaseInitializer<>));
			object obj;
			if (type2 != null && this._initializers.TryGetValue(type2, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0005E5C0 File Offset: 0x0005C7C0
		public virtual void SetInitializer(Type contextType, object initializer)
		{
			this._initializers.AddOrUpdate(contextType, initializer, (Type c, object i) => initializer);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0005E5F9 File Offset: 0x0005C7F9
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000BB8 RID: 3000
		private readonly ConcurrentDictionary<Type, object> _initializers = new ConcurrentDictionary<Type, object>();
	}
}
