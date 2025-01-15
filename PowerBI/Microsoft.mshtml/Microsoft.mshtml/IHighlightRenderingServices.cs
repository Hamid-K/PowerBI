using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB4 RID: 3252
	[Guid("3050F606-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHighlightRenderingServices
	{
		// Token: 0x0601628C RID: 90764
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddSegment([MarshalAs(28)] [In] IDisplayPointer pDispPointerStart, [MarshalAs(28)] [In] IDisplayPointer pDispPointerEnd, [MarshalAs(28)] [In] IHTMLRenderStyle pIRenderStyle, [MarshalAs(28)] out IHighlightSegment ppISegment);

		// Token: 0x0601628D RID: 90765
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveSegmentToPointers([MarshalAs(28)] [In] IHighlightSegment pISegment, [MarshalAs(28)] [In] IDisplayPointer pDispPointerStart, [MarshalAs(28)] [In] IDisplayPointer pDispPointerEnd);

		// Token: 0x0601628E RID: 90766
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveSegment([MarshalAs(28)] [In] IHighlightSegment pISegment);
	}
}
