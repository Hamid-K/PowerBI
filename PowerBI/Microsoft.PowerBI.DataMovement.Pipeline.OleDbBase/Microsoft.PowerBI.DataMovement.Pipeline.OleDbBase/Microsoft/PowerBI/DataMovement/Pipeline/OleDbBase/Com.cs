using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000B9 RID: 185
	public static class Com
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00008FA4 File Offset: 0x000071A4
		public static void RegisterClassObject(Guid clsid, IntPtr unk, CLSCTX clsContext, REGCLS flags, out uint register)
		{
			Com.CoRegisterClassObject(ref clsid, unk, clsContext, flags, out register);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008FB2 File Offset: 0x000071B2
		public static void RevokeClassObject(uint register)
		{
			Com.CoRevokeClassObject(register);
		}

		// Token: 0x0600030B RID: 779
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern void CoRegisterClassObject(ref Guid clsid, IntPtr unk, CLSCTX clsContext, REGCLS flags, out uint register);

		// Token: 0x0600030C RID: 780
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern void CoRevokeClassObject(uint register);
	}
}
