using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000265 RID: 613
	internal interface IRegionManager
	{
		// Token: 0x0600184C RID: 6220
		Task<string> GetAzureRegionAsync(RequestContext requestContext);
	}
}
