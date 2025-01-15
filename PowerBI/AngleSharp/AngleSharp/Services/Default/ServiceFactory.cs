using System;
using AngleSharp.Extensions;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000053 RID: 83
	public class ServiceFactory : IServiceFactory
	{
		// Token: 0x0600019D RID: 413 RVA: 0x0000CB90 File Offset: 0x0000AD90
		public TService Create<TService>(IBrowsingContext context)
		{
			Func<IBrowsingContext, TService> service = context.Configuration.GetService<Func<IBrowsingContext, TService>>();
			if (service == null)
			{
				return default(TService);
			}
			return service(context);
		}
	}
}
