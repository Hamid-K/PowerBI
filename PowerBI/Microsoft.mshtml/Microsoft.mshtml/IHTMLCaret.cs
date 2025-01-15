using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CAF RID: 3247
	[InterfaceType(1)]
	[Guid("3050F604-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLCaret
	{
		// Token: 0x06016263 RID: 90723
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveCaretToPointer([MarshalAs(28)] [In] IDisplayPointer pDispPointer, [In] int fScrollIntoView, [In] _CARET_DIRECTION eDir);

		// Token: 0x06016264 RID: 90724
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveCaretToPointerEx([MarshalAs(28)] [In] IDisplayPointer pDispPointer, [In] int fVisible, [In] int fScrollIntoView, [In] _CARET_DIRECTION eDir);

		// Token: 0x06016265 RID: 90725
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveMarkupPointerToCaret([MarshalAs(28)] [In] IMarkupPointer pIMarkupPointer);

		// Token: 0x06016266 RID: 90726
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveDisplayPointerToCaret([MarshalAs(28)] [In] IDisplayPointer pDispPointer);

		// Token: 0x06016267 RID: 90727
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsVisible(out int pIsVisible);

		// Token: 0x06016268 RID: 90728
		[MethodImpl(4096, MethodCodeType = 3)]
		void Show([In] int fScrollIntoView);

		// Token: 0x06016269 RID: 90729
		[MethodImpl(4096, MethodCodeType = 3)]
		void Hide();

		// Token: 0x0601626A RID: 90730
		[MethodImpl(4096, MethodCodeType = 3)]
		void InsertText([In] ref ushort pText, [In] int lLen);

		// Token: 0x0601626B RID: 90731
		[MethodImpl(4096, MethodCodeType = 3)]
		void scrollIntoView();

		// Token: 0x0601626C RID: 90732
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetLocation(out tagPOINT pPoint, [In] int fTranslate);

		// Token: 0x0601626D RID: 90733
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCaretDirection(out _CARET_DIRECTION peDir);

		// Token: 0x0601626E RID: 90734
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCaretDirection([In] _CARET_DIRECTION eDir);
	}
}
