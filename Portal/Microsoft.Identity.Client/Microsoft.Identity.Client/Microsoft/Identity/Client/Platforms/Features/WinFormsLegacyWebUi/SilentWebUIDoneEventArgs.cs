using System;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AB RID: 427
	internal class SilentWebUIDoneEventArgs : EventArgs
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x000411D8 File Offset: 0x0003F3D8
		public SilentWebUIDoneEventArgs(Exception e)
		{
			this.TransferredException = e;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x000411E7 File Offset: 0x0003F3E7
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x000411EF File Offset: 0x0003F3EF
		public Exception TransferredException { get; private set; }
	}
}
