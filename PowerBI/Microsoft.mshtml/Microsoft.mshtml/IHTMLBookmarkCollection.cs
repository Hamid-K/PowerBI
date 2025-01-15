using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007A7 RID: 1959
	[DefaultMember("item")]
	[Guid("3050F4CE-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLBookmarkCollection : IEnumerable
	{
		// Token: 0x17004669 RID: 18025
		// (get) Token: 0x0600D4F6 RID: 54518
		[DispId(1501)]
		int length
		{
			[TypeLibFunc(64)]
			[DispId(1501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D4F7 RID: 54519
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x0600D4F8 RID: 54520
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object item([In] int index);
	}
}
