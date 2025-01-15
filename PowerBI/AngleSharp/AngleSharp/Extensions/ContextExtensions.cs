using System;
using AngleSharp.Services;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E7 RID: 231
	internal static class ContextExtensions
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x00033578 File Offset: 0x00031778
		public static TService CreateService<TService>(this IBrowsingContext context)
		{
			return context.Configuration.GetFactory<IServiceFactory>().Create<TService>(context);
		}
	}
}
