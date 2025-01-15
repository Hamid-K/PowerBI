using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200000F RID: 15
	[InterfaceType(1)]
	[Guid("3050F4A7-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorSiteRender
	{
		// Token: 0x06000136 RID: 310
		[MethodImpl(4096, MethodCodeType = 3)]
		void Invalidate([In] ref tagRECT pRect);

		// Token: 0x06000137 RID: 311
		[MethodImpl(4096, MethodCodeType = 3)]
		void InvalidateRenderInfo();

		// Token: 0x06000138 RID: 312
		[MethodImpl(4096, MethodCodeType = 3)]
		void InvalidateStyle();
	}
}
