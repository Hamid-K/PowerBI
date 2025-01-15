using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200004C RID: 76
	public interface IConnectionStringServiceHandler
	{
		// Token: 0x06000149 RID: 329
		bool TryGetConnectionStringService(string providerName, bool validateProvider, out IConnectionStringService service);
	}
}
