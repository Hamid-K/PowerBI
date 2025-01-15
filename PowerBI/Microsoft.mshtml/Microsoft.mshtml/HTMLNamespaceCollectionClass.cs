using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CDB RID: 3291
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F6B9-98B5-11CF-BB82-00AA00BDCE0B")]
	[DefaultMember("item")]
	[ComImport]
	public class HTMLNamespaceCollectionClass : IHTMLNamespaceCollection, HTMLNamespaceCollection
	{
		// Token: 0x06016351 RID: 90961
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLNamespaceCollectionClass();

		// Token: 0x170075C4 RID: 30148
		// (get) Token: 0x06016352 RID: 90962
		[DispId(1000)]
		public virtual extern int length
		{
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016353 RID: 90963
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] object index);

		// Token: 0x06016354 RID: 90964
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object add([MarshalAs(19)] [In] string bstrNamespace, [MarshalAs(19)] [In] string bstrUrn, [MarshalAs(27)] [In] [Optional] object implementationUrl);
	}
}
