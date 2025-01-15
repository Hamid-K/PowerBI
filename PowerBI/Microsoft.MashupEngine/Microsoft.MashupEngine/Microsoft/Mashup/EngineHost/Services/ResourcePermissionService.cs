using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B32 RID: 6962
	internal sealed class ResourcePermissionService : IResourcePermissionService
	{
		// Token: 0x0600AE4E RID: 44622 RVA: 0x0023B09D File Offset: 0x0023929D
		public ResourcePermissionService(IEngineHost engineHost)
		{
			this.engineHost = engineHost;
		}

		// Token: 0x0600AE4F RID: 44623 RVA: 0x0023B0AC File Offset: 0x002392AC
		public bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
		{
			return this.engineHost.QueryService<ICredentialService>().TryGetCredentials(resource, out credentials);
		}

		// Token: 0x040059E1 RID: 23009
		private readonly IEngineHost engineHost;
	}
}
