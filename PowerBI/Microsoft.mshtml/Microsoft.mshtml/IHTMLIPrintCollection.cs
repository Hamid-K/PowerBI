using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CEA RID: 3306
	[DefaultMember("item")]
	[Guid("3050F6B5-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLIPrintCollection : IEnumerable
	{
		// Token: 0x170075C5 RID: 30149
		// (get) Token: 0x06016365 RID: 90981
		[DispId(1501)]
		int length
		{
			[TypeLibFunc(64)]
			[DispId(1501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016366 RID: 90982
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06016367 RID: 90983
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(25)]
		object item([In] int index);
	}
}
