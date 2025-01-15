using System;
using Owin;

namespace Microsoft.Owin.Hosting.ServerFactory
{
	// Token: 0x0200001E RID: 30
	public interface IServerFactoryAdapter
	{
		// Token: 0x0600008F RID: 143
		void Initialize(IAppBuilder builder);

		// Token: 0x06000090 RID: 144
		IDisposable Create(IAppBuilder builder);
	}
}
