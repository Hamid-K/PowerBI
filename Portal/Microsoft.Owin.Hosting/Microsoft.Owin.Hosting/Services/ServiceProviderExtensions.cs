using System;

namespace Microsoft.Owin.Hosting.Services
{
	// Token: 0x0200001B RID: 27
	public static class ServiceProviderExtensions
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003B01 File Offset: 0x00001D01
		public static T GetService<T>(this IServiceProvider services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return (T)((object)services.GetService(typeof(T)));
		}
	}
}
