using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F04 RID: 7940
	[Guid("0c733a9c-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDCInfo
	{
		// Token: 0x0600C2AB RID: 49835
		unsafe void GetInfo(uint cInfo, DCINFOTYPE* rgeInfoType, out DCINFO* rgInfo);

		// Token: 0x0600C2AC RID: 49836
		unsafe void SetInfo(uint cInfo, DCINFO* nativeInfo);
	}
}
