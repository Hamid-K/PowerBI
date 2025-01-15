using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A6 RID: 1190
	public class DefaultEndpointProvider : IEndpointProvider
	{
		// Token: 0x06002493 RID: 9363 RVA: 0x0008335C File Offset: 0x0008155C
		public Uri GetPublishedEndpoint(EndpointInfo endpointInfo, string serviceName)
		{
			string text = (endpointInfo.UsePrefix ? "{0}/".FormatWithInvariantCulture(new object[] { serviceName }) : string.Empty);
			return CommunicationUtilities.ConstructUri(endpointInfo.BindingType, endpointInfo.SecurityMode.ToString(), Environment.MachineName, endpointInfo.Port, text);
		}
	}
}
