using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D6 RID: 1238
	[DefaultMember("item")]
	[Guid("3050F4CB-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLElementCollectionClass : DispHTMLElementCollection, HTMLElementCollection, IHTMLElementCollection, IHTMLElementCollection2, IHTMLElementCollection3
	{
		// Token: 0x06007AB9 RID: 31417
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLElementCollectionClass();

		// Token: 0x06007ABA RID: 31418
		[DispId(1501)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170029AD RID: 10669
		// (get) Token: 0x06007ABC RID: 31420
		// (set) Token: 0x06007ABB RID: 31419
		[DispId(1500)]
		public virtual extern int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06007ABD RID: 31421
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x06007ABE RID: 31422
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06007ABF RID: 31423
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x06007AC0 RID: 31424
		[DispId(1505)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06007AC1 RID: 31425
		[DispId(1506)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x06007AC2 RID: 31426
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElementCollection_toString();

		// Token: 0x170029AE RID: 10670
		// (get) Token: 0x06007AC4 RID: 31428
		// (set) Token: 0x06007AC3 RID: 31427
		public virtual extern int IHTMLElementCollection_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06007AC5 RID: 31429
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLElementCollection_GetEnumerator();

		// Token: 0x06007AC6 RID: 31430
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElementCollection_item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06007AC7 RID: 31431
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElementCollection_tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x06007AC8 RID: 31432
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElementCollection2_urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06007AC9 RID: 31433
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElementCollection3_namedItem([MarshalAs(19)] [In] string name);
	}
}
