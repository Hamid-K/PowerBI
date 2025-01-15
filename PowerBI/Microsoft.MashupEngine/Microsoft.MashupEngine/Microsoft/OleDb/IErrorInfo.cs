using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F0E RID: 7950
	[Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IErrorInfo
	{
		// Token: 0x0600C2C0 RID: 49856
		int GetGUID(out Guid pGUID);

		// Token: 0x0600C2C1 RID: 49857
		[PreserveSig]
		int GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

		// Token: 0x0600C2C2 RID: 49858
		[PreserveSig]
		int GetDescription([MarshalAs(UnmanagedType.BStr)] out string pBstrDescription);

		// Token: 0x0600C2C3 RID: 49859
		[PreserveSig]
		int GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		// Token: 0x0600C2C4 RID: 49860
		[PreserveSig]
		int GetHelpContext(out uint pdwHelpContext);
	}
}
