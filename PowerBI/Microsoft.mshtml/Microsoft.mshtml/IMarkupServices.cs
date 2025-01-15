using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CBC RID: 3260
	[InterfaceType(1)]
	[Guid("3050F4A0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupServices
	{
		// Token: 0x060162B6 RID: 90806
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateMarkupPointer([MarshalAs(28)] out IMarkupPointer ppPointer);

		// Token: 0x060162B7 RID: 90807
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateMarkupContainer([MarshalAs(28)] out IMarkupContainer ppMarkupContainer);

		// Token: 0x060162B8 RID: 90808
		[MethodImpl(4096, MethodCodeType = 3)]
		void createElement([In] _ELEMENT_TAG_ID tagID, [In] ref ushort pchAttributes, [MarshalAs(28)] out IHTMLElement ppElement);

		// Token: 0x060162B9 RID: 90809
		[MethodImpl(4096, MethodCodeType = 3)]
		void CloneElement([MarshalAs(28)] [In] IHTMLElement pElemCloneThis, [MarshalAs(28)] out IHTMLElement ppElementTheClone);

		// Token: 0x060162BA RID: 90810
		[MethodImpl(4096, MethodCodeType = 3)]
		void InsertElement([MarshalAs(28)] [In] IHTMLElement pElementInsert, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162BB RID: 90811
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveElement([MarshalAs(28)] [In] IHTMLElement pElementRemove);

		// Token: 0x060162BC RID: 90812
		[MethodImpl(4096, MethodCodeType = 3)]
		void remove([MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162BD RID: 90813
		[MethodImpl(4096, MethodCodeType = 3)]
		void Copy([MarshalAs(28)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(28)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162BE RID: 90814
		[MethodImpl(4096, MethodCodeType = 3)]
		void move([MarshalAs(28)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(28)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162BF RID: 90815
		[MethodImpl(4096, MethodCodeType = 3)]
		void InsertText([In] ref ushort pchText, [In] int cch, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162C0 RID: 90816
		[MethodImpl(4096, MethodCodeType = 3)]
		void ParseString([In] ref ushort pchHTML, [In] uint dwFlags, [MarshalAs(28)] out IMarkupContainer ppContainerResult, [MarshalAs(28)] [In] IMarkupPointer ppPointerStart, [MarshalAs(28)] [In] IMarkupPointer ppPointerFinish);

		// Token: 0x060162C1 RID: 90817
		[MethodImpl(4096, MethodCodeType = 3)]
		void ParseGlobal([ComAliasName("mshtml.wireHGLOBAL")] [In] ref _userHGLOBAL hglobalHTML, [In] uint dwFlags, [MarshalAs(28)] out IMarkupContainer ppContainerResult, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162C2 RID: 90818
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsScopedElement([MarshalAs(28)] [In] IHTMLElement pElement, out int pfScoped);

		// Token: 0x060162C3 RID: 90819
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetElementTagId([MarshalAs(28)] [In] IHTMLElement pElement, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162C4 RID: 90820
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetTagIDForName([MarshalAs(19)] [In] string bstrName, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162C5 RID: 90821
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetNameForTagID([In] _ELEMENT_TAG_ID tagID, [MarshalAs(19)] out string pbstrName);

		// Token: 0x060162C6 RID: 90822
		[MethodImpl(4096, MethodCodeType = 3)]
		void MovePointersToRange([MarshalAs(28)] [In] IHTMLTxtRange pIRange, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162C7 RID: 90823
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveRangeToPointers([MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish, [MarshalAs(28)] [In] IHTMLTxtRange pIRange);

		// Token: 0x060162C8 RID: 90824
		[MethodImpl(4096, MethodCodeType = 3)]
		void BeginUndoUnit([In] ref ushort pchTitle);

		// Token: 0x060162C9 RID: 90825
		[MethodImpl(4096, MethodCodeType = 3)]
		void EndUndoUnit();
	}
}
