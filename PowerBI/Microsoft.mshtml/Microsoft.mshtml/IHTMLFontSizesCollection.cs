using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BF3 RID: 3059
	[Guid("3050F377-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[ComImport]
	public interface IHTMLFontSizesCollection : IEnumerable
	{
		// Token: 0x170073ED RID: 29677
		// (get) Token: 0x06015AFD RID: 88829
		[DispId(1502)]
		int length
		{
			[TypeLibFunc(64)]
			[DispId(1502)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015AFE RID: 88830
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x170073EE RID: 29678
		// (get) Token: 0x06015AFF RID: 88831
		[DispId(1503)]
		string forFont
		{
			[DispId(1503)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06015B00 RID: 88832
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int item([In] int index);
	}
}
