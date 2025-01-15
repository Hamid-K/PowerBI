using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CCC RID: 3276
	[InterfaceType(1)]
	[Guid("3050F663-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLEditServices
	{
		// Token: 0x06016317 RID: 90903
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddDesigner([MarshalAs(28)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x06016318 RID: 90904
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveDesigner([MarshalAs(28)] [In] IHTMLEditDesigner pIDesigner);

		// Token: 0x06016319 RID: 90905
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSelectionServices([MarshalAs(28)] [In] IMarkupContainer pIContainer, [MarshalAs(28)] out ISelectionServices ppSelSvc);

		// Token: 0x0601631A RID: 90906
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToSelectionAnchor([MarshalAs(28)] [In] IMarkupPointer pIStartAnchor);

		// Token: 0x0601631B RID: 90907
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToSelectionEnd([MarshalAs(28)] [In] IMarkupPointer pIEndAnchor);

		// Token: 0x0601631C RID: 90908
		[MethodImpl(4096, MethodCodeType = 3)]
		void SelectRange([MarshalAs(28)] [In] IMarkupPointer pStart, [MarshalAs(28)] [In] IMarkupPointer pEnd, [In] _SELECTION_TYPE eType);
	}
}
