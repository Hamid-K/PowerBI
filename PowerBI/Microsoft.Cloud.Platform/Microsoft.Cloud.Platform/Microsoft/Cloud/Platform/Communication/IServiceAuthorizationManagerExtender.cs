using System;
using System.ServiceModel.Web;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004AB RID: 1195
	public interface IServiceAuthorizationManagerExtender
	{
		// Token: 0x060024AB RID: 9387
		bool CheckAccessCore(WebOperationContext webOperationContext);
	}
}
