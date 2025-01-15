using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB8 RID: 3512
	[InterfaceType(1)]
	[Guid("3050F6BA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorLayout
	{
		// Token: 0x060174C4 RID: 95428
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSize([In] int dwFlags, [In] tagSIZE sizeContent, [In] [Out] ref tagPOINT pptTranslateBy, [In] [Out] ref tagPOINT pptTopLeft, [In] [Out] ref tagSIZE psizeProposed);

		// Token: 0x060174C5 RID: 95429
		[MethodImpl(4096, MethodCodeType = 3)]
		int GetLayoutInfo();

		// Token: 0x060174C6 RID: 95430
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPosition([In] int lFlags, [In] [Out] ref tagPOINT pptTopLeft);

		// Token: 0x060174C7 RID: 95431
		[MethodImpl(4096, MethodCodeType = 3)]
		void MapSize([In] ref tagSIZE psizeIn, out tagRECT prcOut);
	}
}
