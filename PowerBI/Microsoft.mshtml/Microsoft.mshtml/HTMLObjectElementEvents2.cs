using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B1C RID: 2844
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F620-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLObjectElementEvents2
	{
		// Token: 0x0601271B RID: 75547
		[DispId(-2147418108)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onbeforeupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601271C RID: 75548
		[DispId(-2147418107)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601271D RID: 75549
		[DispId(-2147418099)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerrorupdate([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601271E RID: 75550
		[DispId(-2147418106)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onrowexit([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x0601271F RID: 75551
		[DispId(-2147418105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowenter([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012720 RID: 75552
		[DispId(-2147418098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetchanged([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012721 RID: 75553
		[DispId(-2147418097)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondataavailable([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012722 RID: 75554
		[DispId(-2147418096)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void ondatasetcomplete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012723 RID: 75555
		[DispId(-2147418093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onerror([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012724 RID: 75556
		[DispId(-2147418080)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsdelete([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012725 RID: 75557
		[DispId(-2147418079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onrowsinserted([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012726 RID: 75558
		[DispId(-2147418078)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void oncellchange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);

		// Token: 0x06012727 RID: 75559
		[DispId(-2147418092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onreadystatechange([MarshalAs(28)] [In] IHTMLEventObj pEvtObj);
	}
}
