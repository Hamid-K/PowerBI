using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DBA RID: 3514
	[InterfaceType(1)]
	[Guid("3050F6B7-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorSiteLayout
	{
		// Token: 0x060174C9 RID: 95433
		[MethodImpl(4096, MethodCodeType = 3)]
		void InvalidateLayoutInfo();

		// Token: 0x060174CA RID: 95434
		[MethodImpl(4096, MethodCodeType = 3)]
		void InvalidateSize();

		// Token: 0x060174CB RID: 95435
		[MethodImpl(4096, MethodCodeType = 3)]
		tagSIZE GetMediaResolution();
	}
}
