using System;
using System.Runtime.InteropServices;
using mshtml;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AB8 RID: 2744
	[Guid("C4D244B0-D43E-11CF-893B-00AA00BDCE1A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDocHostShowUI
	{
		// Token: 0x06004CCF RID: 19663
		[PreserveSig]
		uint ShowMessage(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string lpstrText, [MarshalAs(UnmanagedType.LPWStr)] string lpstrCaption, uint dwType, [MarshalAs(UnmanagedType.LPWStr)] string lpstrHelpFile, uint dwHelpContext, out int lpResult);

		// Token: 0x06004CD0 RID: 19664
		uint ShowHelp(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszHelpFile, uint uCommand, uint dwData, tagPOINT ptMouse, [MarshalAs(UnmanagedType.IDispatch)] object pDispatchObjectHit);
	}
}
