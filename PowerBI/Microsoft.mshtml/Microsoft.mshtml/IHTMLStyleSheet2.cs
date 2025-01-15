using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000148 RID: 328
	[TypeLibType(4160)]
	[Guid("3050F3D1-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLStyleSheet2
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060013E5 RID: 5093
		[DispId(1016)]
		HTMLStyleSheetPagesCollection pages
		{
			[DispId(1016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060013E6 RID: 5094
		[DispId(1017)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int addPageRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);
	}
}
