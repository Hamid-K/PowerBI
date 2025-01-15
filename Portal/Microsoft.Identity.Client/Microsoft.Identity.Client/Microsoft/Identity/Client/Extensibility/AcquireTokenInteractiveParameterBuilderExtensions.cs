using System;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000293 RID: 659
	public static class AcquireTokenInteractiveParameterBuilderExtensions
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x00052C42 File Offset: 0x00050E42
		public static AcquireTokenInteractiveParameterBuilder WithCustomWebUi(this AcquireTokenInteractiveParameterBuilder builder, ICustomWebUi customWebUi)
		{
			builder.SetCustomWebUi(customWebUi);
			return builder;
		}
	}
}
