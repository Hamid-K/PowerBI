using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200014C RID: 332
	[Guid("3050F37F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[DefaultMember("item")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLStyleSheetsCollectionClass : IHTMLStyleSheetsCollection, HTMLStyleSheetsCollection, IEnumerable
	{
		// Token: 0x0600142D RID: 5165
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleSheetsCollectionClass();

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600142E RID: 5166
		[DispId(1001)]
		public virtual extern int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600142F RID: 5167
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x06001430 RID: 5168
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object item([MarshalAs(27)] [In] ref object pvarIndex);
	}
}
