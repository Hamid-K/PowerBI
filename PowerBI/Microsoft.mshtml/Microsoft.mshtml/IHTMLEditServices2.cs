using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCD RID: 3277
	[Guid("3050F812-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IHTMLEditServices2 : IHTMLEditServices
	{
		// Token: 0x0601631D RID: 90909
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddDesigner([MarshalAs(28)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x0601631E RID: 90910
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveDesigner([MarshalAs(28)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x0601631F RID: 90911
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSelectionServices([MarshalAs(28)] [In] IMarkupContainer pIContainer, [MarshalAs(28)] out ISelectionServices ppSelSvc);

		// Token: 0x06016320 RID: 90912
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToSelectionAnchor([MarshalAs(28)] [In] IMarkupPointer pIStartAnchor);

		// Token: 0x06016321 RID: 90913
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToSelectionEnd([MarshalAs(28)] [In] IMarkupPointer pIEndAnchor);

		// Token: 0x06016322 RID: 90914
		[MethodImpl(4096, MethodCodeType = 3)]
		void SelectRange([MarshalAs(28)] [In] IMarkupPointer pStart, [MarshalAs(28)] [In] IMarkupPointer pEnd, [In] _SELECTION_TYPE eType);

		// Token: 0x06016323 RID: 90915
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToSelectionAnchorEx([MarshalAs(28)] [In] IDisplayPointer pIStartAnchor);

		// Token: 0x06016324 RID: 90916
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToSelectionEndEx([MarshalAs(28)] [In] IDisplayPointer pIEndAnchor);

		// Token: 0x06016325 RID: 90917
		[MethodImpl(4096, MethodCodeType = 3)]
		void FreezeVirtualCaretPos([In] int fReCompute);

		// Token: 0x06016326 RID: 90918
		[MethodImpl(4096, MethodCodeType = 3)]
		void UnFreezeVirtualCaretPos([In] int fReset);
	}
}
