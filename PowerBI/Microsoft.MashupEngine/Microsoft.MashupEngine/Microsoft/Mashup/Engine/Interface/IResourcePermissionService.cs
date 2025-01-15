using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000AE RID: 174
	public interface IResourcePermissionService
	{
		// Token: 0x060002F1 RID: 753
		bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials);
	}
}
