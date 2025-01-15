using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF3 RID: 7923
	[Guid("0000010c-0000-0000-C000-000000000046")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersist
	{
		// Token: 0x0600C27F RID: 49791
		void GetClassID(out Guid clsid);
	}
}
