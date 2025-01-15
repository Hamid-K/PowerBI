using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F73 RID: 8051
	[Guid("1DF126B8-0C88-4C22-93DB-5E59E818E058")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IErrorReported
	{
		// Token: 0x0600C4B5 RID: 50357
		[PreserveSig]
		int IsReported();
	}
}
