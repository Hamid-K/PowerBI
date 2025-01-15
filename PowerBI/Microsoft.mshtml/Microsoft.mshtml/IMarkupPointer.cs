using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C95 RID: 3221
	[InterfaceType(1)]
	[Guid("3050F49F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupPointer
	{
		// Token: 0x060161EC RID: 90604
		[MethodImpl(4096, MethodCodeType = 3)]
		void OwningDoc([MarshalAs(28)] out IHTMLDocument2 ppDoc);

		// Token: 0x060161ED RID: 90605
		[MethodImpl(4096, MethodCodeType = 3)]
		void Gravity(out _POINTER_GRAVITY pGravity);

		// Token: 0x060161EE RID: 90606
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetGravity([In] _POINTER_GRAVITY Gravity);

		// Token: 0x060161EF RID: 90607
		[MethodImpl(4096, MethodCodeType = 3)]
		void Cling(out int pfCling);

		// Token: 0x060161F0 RID: 90608
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCling([In] int fCLing);

		// Token: 0x060161F1 RID: 90609
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unposition();

		// Token: 0x060161F2 RID: 90610
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsPositioned(out int pfPositioned);

		// Token: 0x060161F3 RID: 90611
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetContainer([MarshalAs(28)] out IMarkupContainer ppContainer);

		// Token: 0x060161F4 RID: 90612
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveAdjacentToElement([MarshalAs(28)] [In] IHTMLElement pElement, [In] _ELEMENT_ADJACENCY eAdj);

		// Token: 0x060161F5 RID: 90613
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToPointer([MarshalAs(28)] [In] IMarkupPointer pPointer);

		// Token: 0x060161F6 RID: 90614
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveToContainer([MarshalAs(28)] [In] IMarkupContainer pContainer, [In] int fAtStart);

		// Token: 0x060161F7 RID: 90615
		[MethodImpl(4096, MethodCodeType = 3)]
		void left([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(28)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060161F8 RID: 90616
		[MethodImpl(4096, MethodCodeType = 3)]
		void right([In] int fMove, out _MARKUP_CONTEXT_TYPE pContext, [MarshalAs(28)] out IHTMLElement ppElement, [In] [Out] ref int pcch, out ushort pchText);

		// Token: 0x060161F9 RID: 90617
		[MethodImpl(4096, MethodCodeType = 3)]
		void CurrentScope([MarshalAs(28)] out IHTMLElement ppElemCurrent);

		// Token: 0x060161FA RID: 90618
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsLeftOf([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FB RID: 90619
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsLeftOfOrEqualTo([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FC RID: 90620
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsRightOf([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FD RID: 90621
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsRightOfOrEqualTo([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfResult);

		// Token: 0x060161FE RID: 90622
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsEqualTo([MarshalAs(28)] [In] IMarkupPointer pPointerThat, out int pfAreEqual);

		// Token: 0x060161FF RID: 90623
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveUnit([In] _MOVEUNIT_ACTION muAction);

		// Token: 0x06016200 RID: 90624
		[MethodImpl(4096, MethodCodeType = 3)]
		void findText([In] ref ushort pchFindText, [In] uint dwFlags, [MarshalAs(28)] [In] IMarkupPointer pIEndMatch, [MarshalAs(28)] [In] IMarkupPointer pIEndSearch);
	}
}
