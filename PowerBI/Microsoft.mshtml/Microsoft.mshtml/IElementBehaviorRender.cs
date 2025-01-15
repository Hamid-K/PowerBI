using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200000A RID: 10
	[InterfaceType(1)]
	[Guid("3050F4AA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorRender
	{
		// Token: 0x06000133 RID: 307
		[MethodImpl(4096, MethodCodeType = 3)]
		void Draw([ComAliasName("mshtml.wireHDC")] [In] ref _RemotableHandle hdc, [In] int lLayer, [In] ref tagRECT pRect, [MarshalAs(25)] [In] object pReserved);

		// Token: 0x06000134 RID: 308
		[MethodImpl(4096, MethodCodeType = 3)]
		int GetRenderInfo();

		// Token: 0x06000135 RID: 309
		[MethodImpl(4096, MethodCodeType = 3)]
		int HitTestPoint([In] ref tagPOINT pPoint, [MarshalAs(25)] [In] object pReserved);
	}
}
