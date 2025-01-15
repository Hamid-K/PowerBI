using System;

namespace AngleSharp.Services
{
	// Token: 0x02000037 RID: 55
	public interface IServiceFactory
	{
		// Token: 0x0600013C RID: 316
		TService Create<TService>(IBrowsingContext context);
	}
}
