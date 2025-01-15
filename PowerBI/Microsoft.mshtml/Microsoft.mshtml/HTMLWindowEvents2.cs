using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B2 RID: 1970
	[InterfaceType(2)]
	[Guid("3050F625-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLWindowEvents2
	{
		// Token: 0x0600D64A RID: 54858
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onload([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64B RID: 54859
		[DispId(1008)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onunload([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64C RID: 54860
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64D RID: 54861
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64E RID: 54862
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D64F RID: 54863
		[DispId(1002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onerror([MarshalAs(19)] [In] string description, [MarshalAs(19)] [In] string url, [In] int line);

		// Token: 0x0600D650 RID: 54864
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D651 RID: 54865
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D652 RID: 54866
		[DispId(1017)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeunload([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D653 RID: 54867
		[DispId(1024)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeprint([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0600D654 RID: 54868
		[DispId(1025)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterprint([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
