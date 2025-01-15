using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000048 RID: 72
	public interface IActionPermissionService
	{
		// Token: 0x06000142 RID: 322
		bool IsActionPermitted(IResource resource);

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000143 RID: 323
		bool AreActionsAvailable { get; }
	}
}
