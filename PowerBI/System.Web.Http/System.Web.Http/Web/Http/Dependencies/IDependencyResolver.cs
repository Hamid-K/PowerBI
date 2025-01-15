using System;

namespace System.Web.Http.Dependencies
{
	// Token: 0x02000089 RID: 137
	public interface IDependencyResolver : IDependencyScope, IDisposable
	{
		// Token: 0x06000364 RID: 868
		IDependencyScope BeginScope();
	}
}
