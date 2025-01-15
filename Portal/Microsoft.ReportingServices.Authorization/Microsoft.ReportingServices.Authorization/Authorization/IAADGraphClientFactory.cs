using System;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000013 RID: 19
	internal interface IAADGraphClientFactory
	{
		// Token: 0x06000041 RID: 65
		IGraphServiceClient CreateAADGraphClient(Uri oAuthTokenUri, string oAuthClientId, string oAuthClientSecret, string oAuthTenant, Uri oAuthGraphUrl);

		// Token: 0x06000042 RID: 66
		IGraphServiceClient CreateAADGraphClient(Uri oAuthTokenUri, string oAuthClientId, string oAuthClientSecret, string oAuthTenant, Uri oAuthGraphUrl, Func<Task<string>> accessTokenGetter);
	}
}
