using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE5 RID: 3301
	[ComConversionLoss]
	[Guid("3050F6A6-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLPainter
	{
		// Token: 0x06016355 RID: 90965
		[MethodImpl(4096, MethodCodeType = 3)]
		void Draw([In] tagRECT rcBounds, [In] tagRECT rcUpdate, [In] int lDrawFlags, [ComAliasName("mshtml.wireHDC")] [In] ref _RemotableHandle hdc, [In] IntPtr pvDrawObject);

		// Token: 0x06016356 RID: 90966
		[MethodImpl(4096, MethodCodeType = 3)]
		void onresize([In] tagSIZE size);

		// Token: 0x06016357 RID: 90967
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPainterInfo(out _HTML_PAINTER_INFO pInfo);

		// Token: 0x06016358 RID: 90968
		[MethodImpl(4096, MethodCodeType = 3)]
		void HitTestPoint([In] tagPOINT pt, out int pbHit, out int plPartID);
	}
}
