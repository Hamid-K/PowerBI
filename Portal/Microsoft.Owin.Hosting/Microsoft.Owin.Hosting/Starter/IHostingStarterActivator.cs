using System;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000017 RID: 23
	public interface IHostingStarterActivator
	{
		// Token: 0x0600006E RID: 110
		IHostingStarter Activate(Type type);
	}
}
