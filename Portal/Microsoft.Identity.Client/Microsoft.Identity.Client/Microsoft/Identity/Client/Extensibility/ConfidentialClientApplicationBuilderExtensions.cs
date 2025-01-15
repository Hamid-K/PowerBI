using System;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000297 RID: 663
	public static class ConfidentialClientApplicationBuilderExtensions
	{
		// Token: 0x06001935 RID: 6453 RVA: 0x00052CF3 File Offset: 0x00050EF3
		public static ConfidentialClientApplicationBuilder WithAppTokenProvider(this ConfidentialClientApplicationBuilder builder, Func<AppTokenProviderParameters, Task<AppTokenProviderResult>> appTokenProvider)
		{
			ApplicationConfiguration config = builder.Config;
			if (appTokenProvider == null)
			{
				throw new ArgumentNullException("appTokenProvider");
			}
			config.AppTokenProvider = appTokenProvider;
			return builder;
		}
	}
}
