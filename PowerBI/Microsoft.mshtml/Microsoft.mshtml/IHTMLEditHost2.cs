using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC6 RID: 3270
	[Guid("3050F848-98B5-11CF-BB82-00AA00BDCE0D")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLEditHost2 : IHTMLEditHost
	{
		// Token: 0x06016304 RID: 90884
		[MethodImpl(4096, MethodCodeType = 3)]
		void SnapRect([MarshalAs(28)] [In] IHTMLElement pIElement, [In] [Out] ref tagRECT prcNew, [In] _ELEMENT_CORNER eHandle);

		// Token: 0x06016305 RID: 90885
		[MethodImpl(4096, MethodCodeType = 3)]
		void PreDrag();
	}
}
