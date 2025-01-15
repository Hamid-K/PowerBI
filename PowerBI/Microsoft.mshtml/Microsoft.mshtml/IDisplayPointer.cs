using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB0 RID: 3248
	[Guid("3050F69E-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IDisplayPointer
	{
		// Token: 0x0601626F RID: 90735
		[MethodImpl(4096, MethodCodeType = 3)]
		void moveToPoint([In] tagPOINT ptPoint, [In] _COORD_SYSTEM eCoordSystem, [MarshalAs(28)] [In] IHTMLElement pElementContext, [In] uint dwHitTestOptions, out uint pdwHitTestResults);

		// Token: 0x06016270 RID: 90736
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveUnit([In] _DISPLAY_MOVEUNIT eMoveUnit, [In] int lXPos);

		// Token: 0x06016271 RID: 90737
		[MethodImpl(4096, MethodCodeType = 3)]
		void PositionMarkupPointer([MarshalAs(28)] [In] IMarkupPointer pMarkupPointer);

		// Token: 0x06016272 RID: 90738
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToPointer([MarshalAs(28)] [In] IDisplayPointer pDispPointer);

		// Token: 0x06016273 RID: 90739
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetPointerGravity([In] _POINTER_GRAVITY eGravity);

		// Token: 0x06016274 RID: 90740
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPointerGravity(out _POINTER_GRAVITY peGravity);

		// Token: 0x06016275 RID: 90741
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDisplayGravity([In] _DISPLAY_GRAVITY eGravity);

		// Token: 0x06016276 RID: 90742
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetDisplayGravity(out _DISPLAY_GRAVITY peGravity);

		// Token: 0x06016277 RID: 90743
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsPositioned(out int pfPositioned);

		// Token: 0x06016278 RID: 90744
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unposition();

		// Token: 0x06016279 RID: 90745
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsEqualTo([MarshalAs(28)] [In] IDisplayPointer pDispPointer, out int pfIsEqual);

		// Token: 0x0601627A RID: 90746
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsLeftOf([MarshalAs(28)] [In] IDisplayPointer pDispPointer, out int pfIsLeftOf);

		// Token: 0x0601627B RID: 90747
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsRightOf([MarshalAs(28)] [In] IDisplayPointer pDispPointer, out int pfIsRightOf);

		// Token: 0x0601627C RID: 90748
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsAtBOL(out int pfBOL);

		// Token: 0x0601627D RID: 90749
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToMarkupPointer([MarshalAs(28)] [In] IMarkupPointer pPointer, [MarshalAs(28)] [In] IDisplayPointer pDispLineContext);

		// Token: 0x0601627E RID: 90750
		[MethodImpl(4096, MethodCodeType = 3)]
		void scrollIntoView();

		// Token: 0x0601627F RID: 90751
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetLineInfo([MarshalAs(28)] out ILineInfo ppLineInfo);

		// Token: 0x06016280 RID: 90752
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFlowElement([MarshalAs(28)] out IHTMLElement ppLayoutElement);

		// Token: 0x06016281 RID: 90753
		[MethodImpl(4096, MethodCodeType = 3)]
		void QueryBreaks(out uint pdwBreaks);
	}
}
