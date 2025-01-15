using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200006D RID: 109
	[Guid("0c733a8b-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBInitialize
	{
		// Token: 0x060002A0 RID: 672
		void Initialize();

		// Token: 0x060002A1 RID: 673
		void Uninitialize();
	}
}
