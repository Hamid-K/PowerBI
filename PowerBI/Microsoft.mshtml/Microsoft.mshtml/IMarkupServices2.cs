using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC0 RID: 3264
	[InterfaceType(1)]
	[Guid("3050F682-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupServices2 : IMarkupServices
	{
		// Token: 0x060162CA RID: 90826
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateMarkupPointer([MarshalAs(28)] out IMarkupPointer ppPointer);

		// Token: 0x060162CB RID: 90827
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateMarkupContainer([MarshalAs(28)] out IMarkupContainer ppMarkupContainer);

		// Token: 0x060162CC RID: 90828
		[MethodImpl(4096, MethodCodeType = 3)]
		void createElement([In] _ELEMENT_TAG_ID tagID, [In] ref ushort pchAttributes, [MarshalAs(28)] out IHTMLElement ppElement);

		// Token: 0x060162CD RID: 90829
		[MethodImpl(4096, MethodCodeType = 3)]
		void CloneElement([MarshalAs(28)] [In] IHTMLElement pElemCloneThis, [MarshalAs(28)] out IHTMLElement ppElementTheClone);

		// Token: 0x060162CE RID: 90830
		[MethodImpl(4096, MethodCodeType = 3)]
		void InsertElement([MarshalAs(28)] [In] IHTMLElement pElementInsert, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162CF RID: 90831
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveElement([MarshalAs(28)] [In] IHTMLElement pElementRemove);

		// Token: 0x060162D0 RID: 90832
		[MethodImpl(4096, MethodCodeType = 3)]
		void remove([MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162D1 RID: 90833
		[MethodImpl(4096, MethodCodeType = 3)]
		void Copy([MarshalAs(28)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(28)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162D2 RID: 90834
		[MethodImpl(4096, MethodCodeType = 3)]
		void move([MarshalAs(28)] [In] IMarkupPointer pPointerSourceStart, [MarshalAs(28)] [In] IMarkupPointer pPointerSourceFinish, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162D3 RID: 90835
		[MethodImpl(4096, MethodCodeType = 3)]
		void InsertText([In] ref ushort pchText, [In] int cch, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget);

		// Token: 0x060162D4 RID: 90836
		[MethodImpl(4096, MethodCodeType = 3)]
		void ParseString([In] ref ushort pchHTML, [In] uint dwFlags, [MarshalAs(28)] out IMarkupContainer ppContainerResult, [MarshalAs(28)] [In] IMarkupPointer ppPointerStart, [MarshalAs(28)] [In] IMarkupPointer ppPointerFinish);

		// Token: 0x060162D5 RID: 90837
		[MethodImpl(4096, MethodCodeType = 3)]
		void ParseGlobal([ComAliasName("mshtml.wireHGLOBAL")] [In] ref _userHGLOBAL hglobalHTML, [In] uint dwFlags, [MarshalAs(28)] out IMarkupContainer ppContainerResult, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162D6 RID: 90838
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsScopedElement([MarshalAs(28)] [In] IHTMLElement pElement, out int pfScoped);

		// Token: 0x060162D7 RID: 90839
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetElementTagId([MarshalAs(28)] [In] IHTMLElement pElement, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162D8 RID: 90840
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetTagIDForName([MarshalAs(19)] [In] string bstrName, out _ELEMENT_TAG_ID ptagId);

		// Token: 0x060162D9 RID: 90841
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetNameForTagID([In] _ELEMENT_TAG_ID tagID, [MarshalAs(19)] out string pbstrName);

		// Token: 0x060162DA RID: 90842
		[MethodImpl(4096, MethodCodeType = 3)]
		void MovePointersToRange([MarshalAs(28)] [In] IHTMLTxtRange pIRange, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162DB RID: 90843
		[MethodImpl(4096, MethodCodeType = 3)]
		void MoveRangeToPointers([MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish, [MarshalAs(28)] [In] IHTMLTxtRange pIRange);

		// Token: 0x060162DC RID: 90844
		[MethodImpl(4096, MethodCodeType = 3)]
		void BeginUndoUnit([In] ref ushort pchTitle);

		// Token: 0x060162DD RID: 90845
		[MethodImpl(4096, MethodCodeType = 3)]
		void EndUndoUnit();

		// Token: 0x060162DE RID: 90846
		[MethodImpl(4096, MethodCodeType = 3)]
		void ParseGlobalEx([ComAliasName("mshtml.wireHGLOBAL")] [In] ref _userHGLOBAL hglobalHTML, [In] uint dwFlags, [MarshalAs(28)] [In] IMarkupContainer pContext, [MarshalAs(28)] out IMarkupContainer ppContainerResult, [MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish);

		// Token: 0x060162DF RID: 90847
		[MethodImpl(4096, MethodCodeType = 3)]
		void ValidateElements([MarshalAs(28)] [In] IMarkupPointer pPointerStart, [MarshalAs(28)] [In] IMarkupPointer pPointerFinish, [MarshalAs(28)] [In] IMarkupPointer pPointerTarget, [MarshalAs(28)] [In] [Out] IMarkupPointer pPointerStatus, [MarshalAs(28)] out IHTMLElement ppElemFailBottom, [MarshalAs(28)] out IHTMLElement ppElemFailTop);

		// Token: 0x060162E0 RID: 90848
		[MethodImpl(4096, MethodCodeType = 3)]
		void SaveSegmentsToClipboard([MarshalAs(28)] [In] ISegmentList pSegmentList, [In] uint dwFlags);
	}
}
