using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002BD RID: 701
	public class SingletonDependencyResolver<T> : IDbDependencyResolver where T : class
	{
		// Token: 0x06002209 RID: 8713 RVA: 0x0005FB6E File Offset: 0x0005DD6E
		public SingletonDependencyResolver(T singletonInstance)
			: this(singletonInstance, null)
		{
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0005FB78 File Offset: 0x0005DD78
		public SingletonDependencyResolver(T singletonInstance, object key)
		{
			Check.NotNull<T>(singletonInstance, "singletonInstance");
			this._singletonInstance = singletonInstance;
			this._keyPredicate = (object k) => key == null || object.Equals(key, k);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0005FBBD File Offset: 0x0005DDBD
		public SingletonDependencyResolver(T singletonInstance, Func<object, bool> keyPredicate)
		{
			Check.NotNull<T>(singletonInstance, "singletonInstance");
			Check.NotNull<Func<object, bool>>(keyPredicate, "keyPredicate");
			this._singletonInstance = singletonInstance;
			this._keyPredicate = keyPredicate;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0005FBEC File Offset: 0x0005DDEC
		public object GetService(Type type, object key)
		{
			return (type == typeof(T) && this._keyPredicate(key)) ? this._singletonInstance : default(T);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0005FC2F File Offset: 0x0005DE2F
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000BD6 RID: 3030
		private readonly T _singletonInstance;

		// Token: 0x04000BD7 RID: 3031
		private readonly Func<object, bool> _keyPredicate;
	}
}
