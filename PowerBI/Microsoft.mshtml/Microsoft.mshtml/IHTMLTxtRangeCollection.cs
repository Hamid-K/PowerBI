using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001DF RID: 479
	[TypeLibType(4160)]
	[Guid("3050F7ED-98B5-11CF-BB82-00AA00BDCE0B")]
	[DefaultMember("item")]
	[ComImport]
	public interface IHTMLTxtRangeCollection : IEnumerable
	{
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001B50 RID: 6992
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001B51 RID: 6993
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06001B52 RID: 6994
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object item([MarshalAs(27)] [In] ref object pvarIndex);
	}
}
