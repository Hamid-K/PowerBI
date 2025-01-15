using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200007A RID: 122
	[DefaultMember("item")]
	[Guid("3050F5AB-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDOMChildrenCollection : IEnumerable
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06000BD2 RID: 3026
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BD3 RID: 3027
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06000BD4 RID: 3028
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([In] int index);
	}
}
