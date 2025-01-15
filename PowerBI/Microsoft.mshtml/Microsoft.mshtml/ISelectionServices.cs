using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCA RID: 3274
	[InterfaceType(1)]
	[Guid("3050F684-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ISelectionServices
	{
		// Token: 0x0601630D RID: 90893
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetSelectionType([In] _SELECTION_TYPE eType, [MarshalAs(28)] [In] ISelectionServicesListener pIListener);

		// Token: 0x0601630E RID: 90894
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetMarkupContainer([MarshalAs(28)] out IMarkupContainer ppIContainer);

		// Token: 0x0601630F RID: 90895
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddSegment([MarshalAs(28)] [In] IMarkupPointer pIStart, [MarshalAs(28)] [In] IMarkupPointer pIEnd, [MarshalAs(28)] out ISegment ppISegmentAdded);

		// Token: 0x06016310 RID: 90896
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddElementSegment([MarshalAs(28)] [In] IHTMLElement pIElement, [MarshalAs(28)] out IElementSegment ppISegmentAdded);

		// Token: 0x06016311 RID: 90897
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveSegment([MarshalAs(28)] [In] ISegment pISegment);

		// Token: 0x06016312 RID: 90898
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSelectionServicesListener([MarshalAs(28)] out ISelectionServicesListener ppISelectionServicesListener);
	}
}
