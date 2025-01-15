using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CD6 RID: 3286
	[DefaultMember("item")]
	[Guid("3050F6B8-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLNamespaceCollection
	{
		// Token: 0x170075BE RID: 30142
		// (get) Token: 0x0601633E RID: 90942
		[DispId(1000)]
		int length
		{
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601633F RID: 90943
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object item([MarshalAs(27)] [In] object index);

		// Token: 0x06016340 RID: 90944
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object add([MarshalAs(19)] [In] string bstrNamespace, [MarshalAs(19)] [In] string bstrUrn, [MarshalAs(27)] [In] [Optional] object implementationUrl);
	}
}
