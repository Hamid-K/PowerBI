using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC2 RID: 3266
	[Guid("3050F675-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMarkupPointer2 : IMarkupPointer
	{
		// Token: 0x060162E2 RID: 90850
		[MethodImpl(4096, MethodCodeType = 3)]
		void OwningDoc([MarshalAs(28)] out IHTMLDocument2 ppDoc);

		// Token: 0x060162E3 RID: 90851
		[MethodImpl(4096, MethodCodeType = 3)]
		void Gravity(out _POINTER_GRAVITY pGravity);

		// Token: 0x060162E4 RID: 90852
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetGravity([In] _POINTER_GRAVITY Gravity);

		// Token: 0x060162E5 RID: 90853
		[MethodImpl(4096, MethodCodeType = 3)]
		void Cling(out int pfCling);

		// Token: 0x060162E6 RID: 90854
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCling([In] int fCLing);

		// Token: 0x060162E7 RID: 90855
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unposition();

		// Token: 0x060162E8 RID: 90856
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsPositioned(out int pfPositioned);

		// Token: 0x060162E9 RID: 90857
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetContainer([MarshalAs(28)] out IMarkupContainer ppContainer);

		// Token: 0x060162EA RID: 90858
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveAdjacentToElement([MarshalAs(28)] [In] IHTMLElement pElement, [In] _ELEMENT_ADJACENCY eAdj);

		// Token: 0x060162EB RID: 90859
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToPointer([MarshalAs(28)] [In] IMarkupPointer pPointer);

		// Token: 0x060162EC RID: 90860
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToContainer([MarshalAs(28)] [In] IMarkupContainer pContainer, [In] int fAtStart);

		// Token: 0x060162ED RID: 90861
		[MethodImpl(4096, MethodCodeType = 3)]
		void left([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(28)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060162EE RID: 90862
		[MethodImpl(4096, MethodCodeType = 3)]
		void right([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(28)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060162EF RID: 90863
		[MethodImpl(4096, MethodCodeType = 3)]
		void CurrentScope([MarshalAs(28)] out IHTMLElement ppElemCurrent);

		// Token: 0x060162F0 RID: 90864
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsLeftOf([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F1 RID: 90865
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsLeftOfOrEqualTo([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F2 RID: 90866
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsRightOf([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F3 RID: 90867
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsRightOfOrEqualTo([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060162F4 RID: 90868
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsEqualTo([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfAreEqual);

		// Token: 0x060162F5 RID: 90869
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveUnit([In] _MOVEUNIT_ACTION muAction);

		// Token: 0x060162F6 RID: 90870
		[MethodImpl(4096, MethodCodeType = 3)]
		void findText([In] ref ushort pchFindText, [In] uint dwFlags, [MarshalAs(28)] [In] IMarkupPointer pIEndMatch, [MarshalAs(28)] [In] IMarkupPointer pIEndSearch);

		// Token: 0x060162F7 RID: 90871
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsAtWordBreak(out int pfAtBreak);

		// Token: 0x060162F8 RID: 90872
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetMarkupPosition(out int plMP);

		// Token: 0x060162F9 RID: 90873
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToMarkupPosition([MarshalAs(28)] [In] IMarkupContainer pContainer, [In] int lMP);

		// Token: 0x060162FA RID: 90874
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveUnitBounded([In] _MOVEUNIT_ACTION muAction, [MarshalAs(28)] [In] IMarkupPointer pIBoundary);

		// Token: 0x060162FB RID: 90875
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsInsideURL([MarshalAs(28)] [In] IMarkupPointer pRight, out int pfResult);

		// Token: 0x060162FC RID: 90876
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToContent([MarshalAs(28)] [In] IHTMLElement pIElement, [In] int fAtStart);
	}
}
