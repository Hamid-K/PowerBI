using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F0C RID: 7948
	[Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISequentialStream
	{
		// Token: 0x0600C2BD RID: 49853
		[PreserveSig]
		unsafe int Read(void* pv, int cb, out uint pcbRead);

		// Token: 0x0600C2BE RID: 49854
		[PreserveSig]
		unsafe int Write(void* pv, int cb, uint* pcbWritten);
	}
}
