using System;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200028F RID: 655
	public interface IDbConfigurationInterceptor : IDbInterceptor
	{
		// Token: 0x060020CC RID: 8396
		void Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbConfigurationInterceptionContext interceptionContext);
	}
}
