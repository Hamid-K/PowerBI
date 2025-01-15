using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB3 RID: 3251
	[Guid("3050F690-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHighlightSegment : ISegment
	{
		// Token: 0x0601628B RID: 90763
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPointers([MarshalAs(28)] [In] IMarkupPointer pIStart, [MarshalAs(28)] [In] IMarkupPointer pIEnd);
	}
}
