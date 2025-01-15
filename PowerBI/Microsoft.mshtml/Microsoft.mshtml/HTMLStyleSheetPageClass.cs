using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000142 RID: 322
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F7EF-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLStyleSheetPageClass : IHTMLStyleSheetPage, HTMLStyleSheetPage
	{
		// Token: 0x060013C7 RID: 5063
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleSheetPageClass();

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060013C8 RID: 5064
		[DispId(1001)]
		public virtual extern string selector
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060013C9 RID: 5065
		[DispId(1002)]
		public virtual extern string pseudoClass
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
