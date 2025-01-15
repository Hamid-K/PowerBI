using System;
using System.Windows.Forms;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001B1 RID: 433
	internal class Win32Window : IWin32Window
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x0004161D File Offset: 0x0003F81D
		public Win32Window(IntPtr handle)
		{
			this.Handle = handle;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0004162C File Offset: 0x0003F82C
		public IntPtr Handle { get; }
	}
}
