using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCE RID: 3278
	[ClassInterface(0)]
	[Guid("3050F819-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HtmlDlgSafeHelperClass : IHtmlDlgSafeHelper, HtmlDlgSafeHelper
	{
		// Token: 0x06016327 RID: 90919
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HtmlDlgSafeHelperClass();

		// Token: 0x06016328 RID: 90920
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object choosecolordlg([MarshalAs(27)] [In] [Optional] object initColor);

		// Token: 0x06016329 RID: 90921
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getCharset([MarshalAs(19)] [In] string fontName);

		// Token: 0x170075B5 RID: 30133
		// (get) Token: 0x0601632A RID: 90922
		[DispId(3)]
		public virtual extern object fonts
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170075B6 RID: 30134
		// (get) Token: 0x0601632B RID: 90923
		[DispId(4)]
		public virtual extern object BlockFormats
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
