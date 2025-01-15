using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B3 RID: 1971
	[InterfaceType(2)]
	[Guid("96A0A4E0-D062-11CF-94B6-00AA0060275C")]
	[TypeLibType(4112)]
	[ComImport]
	public interface HTMLWindowEvents
	{
		// Token: 0x0600D655 RID: 54869
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onload();

		// Token: 0x0600D656 RID: 54870
		[DispId(1008)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onunload();

		// Token: 0x0600D657 RID: 54871
		[DispId(-2147418102)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool onhelp();

		// Token: 0x0600D658 RID: 54872
		[DispId(-2147418111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onfocus();

		// Token: 0x0600D659 RID: 54873
		[DispId(-2147418112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onblur();

		// Token: 0x0600D65A RID: 54874
		[DispId(1002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onerror([MarshalAs(19)] [In] string description, [MarshalAs(19)] [In] string url, [In] int line);

		// Token: 0x0600D65B RID: 54875
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onresize();

		// Token: 0x0600D65C RID: 54876
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onscroll();

		// Token: 0x0600D65D RID: 54877
		[DispId(1017)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeunload();

		// Token: 0x0600D65E RID: 54878
		[DispId(1024)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onbeforeprint();

		// Token: 0x0600D65F RID: 54879
		[DispId(1025)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void onafterprint();
	}
}
