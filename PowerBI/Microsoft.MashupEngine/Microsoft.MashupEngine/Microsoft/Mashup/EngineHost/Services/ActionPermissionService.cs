using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200198A RID: 6538
	internal sealed class ActionPermissionService : IActionPermissionService
	{
		// Token: 0x17002A5F RID: 10847
		// (get) Token: 0x0600A5E0 RID: 42464 RVA: 0x00224DDE File Offset: 0x00222FDE
		public bool AreActionsAvailable
		{
			get
			{
				return this.allowActions;
			}
		}

		// Token: 0x0600A5E1 RID: 42465 RVA: 0x00224DE6 File Offset: 0x00222FE6
		public ActionPermissionService(bool allowActions)
		{
			this.allowActions = allowActions;
		}

		// Token: 0x0600A5E2 RID: 42466 RVA: 0x00224DDE File Offset: 0x00222FDE
		public bool IsActionPermitted(IResource resource)
		{
			return this.allowActions;
		}

		// Token: 0x04005651 RID: 22097
		private readonly bool allowActions;
	}
}
