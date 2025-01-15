using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.Advanced
{
	// Token: 0x02000290 RID: 656
	public static class AcquireTokenParameterBuilderExtensions
	{
		// Token: 0x0600191D RID: 6429 RVA: 0x00052BB9 File Offset: 0x00050DB9
		public static T WithExtraHttpHeaders<T>(this AbstractAcquireTokenParameterBuilder<T> builder, IDictionary<string, string> extraHttpHeaders) where T : AbstractAcquireTokenParameterBuilder<T>
		{
			builder.CommonParameters.ExtraHttpHeaders = extraHttpHeaders;
			return (T)((object)builder);
		}
	}
}
