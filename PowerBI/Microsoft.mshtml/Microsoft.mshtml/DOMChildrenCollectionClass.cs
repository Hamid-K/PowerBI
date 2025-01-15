using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200007F RID: 127
	[ClassInterface(0)]
	[DefaultMember("item")]
	[Guid("3050F5AA-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class DOMChildrenCollectionClass : DispDOMChildrenCollection, DOMChildrenCollection, IHTMLDOMChildrenCollection
	{
		// Token: 0x06000BEB RID: 3051
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern DOMChildrenCollectionClass();

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06000BEC RID: 3052
		[DispId(1500)]
		public virtual extern int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BED RID: 3053
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x06000BEE RID: 3054
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([In] int index);

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06000BEF RID: 3055
		public virtual extern int IHTMLDOMChildrenCollection_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BF0 RID: 3056
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLDOMChildrenCollection_GetEnumerator();

		// Token: 0x06000BF1 RID: 3057
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLDOMChildrenCollection_item([In] int index);
	}
}
