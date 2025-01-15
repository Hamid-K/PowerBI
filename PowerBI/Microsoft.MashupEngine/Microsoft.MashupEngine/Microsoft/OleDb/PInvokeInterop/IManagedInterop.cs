using System;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F75 RID: 8053
	internal interface IManagedInterop : IManagedDisposable
	{
		// Token: 0x0600C4B7 RID: 50359
		void AttachPUnknown(IntPtr punk);

		// Token: 0x17002FD2 RID: 12242
		// (get) Token: 0x0600C4B8 RID: 50360
		IntPtr PUnknown { get; }
	}
}
