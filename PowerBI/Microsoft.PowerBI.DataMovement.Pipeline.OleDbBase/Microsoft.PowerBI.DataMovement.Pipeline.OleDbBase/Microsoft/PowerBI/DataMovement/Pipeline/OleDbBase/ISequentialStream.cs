using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000087 RID: 135
	[Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISequentialStream
	{
		// Token: 0x060002E1 RID: 737
		[PreserveSig]
		unsafe int Read(void* pv, int cb, out uint pcbRead);

		// Token: 0x060002E2 RID: 738
		[PreserveSig]
		unsafe int Write(void* pv, int cb, uint* pcbWritten);
	}
}
