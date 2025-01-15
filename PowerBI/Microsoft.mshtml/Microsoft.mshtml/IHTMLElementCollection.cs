using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200008F RID: 143
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLElementCollection : IEnumerable
	{
		// Token: 0x06000C6F RID: 3183
		[DispId(1501)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06000C71 RID: 3185
		// (set) Token: 0x06000C70 RID: 3184
		[DispId(1500)]
		int length
		{
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1500)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06000C72 RID: 3186
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		IEnumerator GetEnumerator();

		// Token: 0x06000C73 RID: 3187
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06000C74 RID: 3188
		[DispId(1502)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object tags([MarshalAs(27)] [In] object tagName);
	}
}
