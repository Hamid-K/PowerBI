using System;

namespace Microsoft.Internal
{
	// Token: 0x020001CC RID: 460
	internal static class X64Helper
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0001191C File Offset: 0x0000FB1C
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}
	}
}
