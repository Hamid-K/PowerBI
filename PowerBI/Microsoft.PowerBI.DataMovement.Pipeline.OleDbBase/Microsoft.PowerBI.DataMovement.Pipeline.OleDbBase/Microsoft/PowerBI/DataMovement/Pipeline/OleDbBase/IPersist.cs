using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200006B RID: 107
	[Guid("0000010c-0000-0000-C000-000000000046")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersist
	{
		// Token: 0x06000299 RID: 665
		void GetClassID(out Guid clsid);
	}
}
