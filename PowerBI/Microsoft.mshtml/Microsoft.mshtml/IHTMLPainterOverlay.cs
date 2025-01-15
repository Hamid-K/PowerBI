using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE9 RID: 3305
	[Guid("3050F7E3-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLPainterOverlay
	{
		// Token: 0x06016364 RID: 90980
		[MethodImpl(4096, MethodCodeType = 3)]
		void onmove([In] tagRECT rcDevice);
	}
}
