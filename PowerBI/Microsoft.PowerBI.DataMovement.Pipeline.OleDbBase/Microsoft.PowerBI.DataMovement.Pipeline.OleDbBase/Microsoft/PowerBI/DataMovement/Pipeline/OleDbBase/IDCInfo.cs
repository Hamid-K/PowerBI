using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200007E RID: 126
	[Guid("0c733a9c-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDCInfo
	{
		// Token: 0x060002CC RID: 716
		unsafe void GetInfo(uint info, DCINFOTYPE* infoType, out DCINFO* nativeInfo);

		// Token: 0x060002CD RID: 717
		unsafe void SetInfo(uint info, DCINFO* nativeInfo);
	}
}
