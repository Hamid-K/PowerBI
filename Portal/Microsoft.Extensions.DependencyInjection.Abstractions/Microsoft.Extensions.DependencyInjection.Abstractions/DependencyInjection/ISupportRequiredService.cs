using System;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000007 RID: 7
	public interface ISupportRequiredService
	{
		// Token: 0x0600000A RID: 10
		object GetRequiredService(Type serviceType);
	}
}
