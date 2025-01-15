using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008D1 RID: 2257
	[Guid("3050F4CA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[DefaultMember("item")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLAreasCollectionClass : DispHTMLAreasCollection, HTMLAreasCollection, IHTMLAreasCollection, IHTMLAreasCollection2, IHTMLAreasCollection3
	{
		// Token: 0x0600E61E RID: 58910
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLAreasCollectionClass();

		// Token: 0x17004BC4 RID: 19396
		// (get) Token: 0x0600E620 RID: 58912
		// (set) Token: 0x0600E61F RID: 58911
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

		// Token: 0x0600E621 RID: 58913
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x0600E622 RID: 58914
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x0600E623 RID: 58915
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x0600E624 RID: 58916
		[DispId(1503)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void add([MarshalAs(28)] [In] IHTMLElement element, [MarshalAs(27)] [In] [Optional] object before);

		// Token: 0x0600E625 RID: 58917
		[DispId(1504)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void remove([In] int index = -1);

		// Token: 0x0600E626 RID: 58918
		[DispId(1505)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object urns([MarshalAs(27)] [In] object urn);

		// Token: 0x0600E627 RID: 58919
		[DispId(1506)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x17004BC5 RID: 19397
		// (get) Token: 0x0600E629 RID: 58921
		// (set) Token: 0x0600E628 RID: 58920
		public virtual extern int IHTMLAreasCollection_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600E62A RID: 58922
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLAreasCollection_GetEnumerator();

		// Token: 0x0600E62B RID: 58923
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLAreasCollection_item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x0600E62C RID: 58924
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLAreasCollection_tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x0600E62D RID: 58925
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLAreasCollection_add([MarshalAs(28)] [In] IHTMLElement element, [MarshalAs(27)] [In] [Optional] object before);

		// Token: 0x0600E62E RID: 58926
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLAreasCollection_remove([In] int index = -1);

		// Token: 0x0600E62F RID: 58927
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLAreasCollection2_urns([MarshalAs(27)] [In] object urn);

		// Token: 0x0600E630 RID: 58928
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLAreasCollection3_namedItem([MarshalAs(19)] [In] string name);
	}
}
