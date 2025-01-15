using System;

namespace Microsoft.Owin.Hosting.ServerFactory
{
	// Token: 0x0200001F RID: 31
	public interface IServerFactoryLoader
	{
		// Token: 0x06000091 RID: 145
		IServerFactoryAdapter Load(string serverName);
	}
}
