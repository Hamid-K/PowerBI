using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000B5 RID: 181
	[TypeLibType(2)]
	[DefaultMember("item")]
	[ClassInterface(0)]
	[Guid("3050F580-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLUrnCollectionClass : IHTMLUrnCollection, HTMLUrnCollection
	{
		// Token: 0x06000DA8 RID: 3496
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLUrnCollectionClass();

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06000DA9 RID: 3497
		[DispId(1001)]
		public virtual extern int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000DAA RID: 3498
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string item([In] int index);
	}
}
