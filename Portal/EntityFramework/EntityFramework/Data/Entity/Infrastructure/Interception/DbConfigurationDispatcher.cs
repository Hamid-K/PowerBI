using System;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200027E RID: 638
	internal class DbConfigurationDispatcher
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0005B581 File Offset: 0x00059781
		public InternalDispatcher<IDbConfigurationInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0005B58C File Offset: 0x0005978C
		public virtual void Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbInterceptionContext interceptionContext)
		{
			DbConfigurationInterceptionContext clonedInterceptionContext = new DbConfigurationInterceptionContext(interceptionContext);
			this._internalDispatcher.Dispatch(delegate(IDbConfigurationInterceptor i)
			{
				i.Loaded(loadedEventArgs, clonedInterceptionContext);
			});
		}

		// Token: 0x04000B7A RID: 2938
		private readonly InternalDispatcher<IDbConfigurationInterceptor> _internalDispatcher = new InternalDispatcher<IDbConfigurationInterceptor>();
	}
}
