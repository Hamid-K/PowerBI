using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF6 RID: 7926
	[Guid("0c733a8b-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBInitialize
	{
		// Token: 0x0600C28B RID: 49803
		void Initialize();

		// Token: 0x0600C28C RID: 49804
		void Uninitialize();
	}
}
