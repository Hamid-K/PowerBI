using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200007C RID: 124
	[ClassInterface(0)]
	[DefaultMember("item")]
	[Guid("3050F4CC-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLAttributeCollectionClass : DispHTMLAttributeCollection, HTMLAttributeCollection, IHTMLAttributeCollection, IHTMLAttributeCollection2
	{
		// Token: 0x06000BDB RID: 3035
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLAttributeCollectionClass();

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06000BDC RID: 3036
		[DispId(1500)]
		public virtual extern int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BDD RID: 3037
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x06000BDE RID: 3038
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] [Optional] ref object name);

		// Token: 0x06000BDF RID: 3039
		[DispId(1501)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getNamedItem([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06000BE0 RID: 3040
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setNamedItem([MarshalAs(28)] [In] IHTMLDOMAttribute ppNode);

		// Token: 0x06000BE1 RID: 3041
		[DispId(1503)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeNamedItem([MarshalAs(19)] [In] string bstrName);

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06000BE2 RID: 3042
		public virtual extern int IHTMLAttributeCollection_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BE3 RID: 3043
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLAttributeCollection_GetEnumerator();

		// Token: 0x06000BE4 RID: 3044
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLAttributeCollection_item([MarshalAs(27)] [In] [Optional] ref object name);

		// Token: 0x06000BE5 RID: 3045
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLAttributeCollection2_getNamedItem([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06000BE6 RID: 3046
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLAttributeCollection2_setNamedItem([MarshalAs(28)] [In] IHTMLDOMAttribute ppNode);

		// Token: 0x06000BE7 RID: 3047
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLAttributeCollection2_removeNamedItem([MarshalAs(19)] [In] string bstrName);
	}
}
