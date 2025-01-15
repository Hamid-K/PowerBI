using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200013E RID: 318
	[DefaultMember("item")]
	[ClassInterface(0)]
	[Guid("3050F3CD-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLStyleSheetRulesCollectionClass : IHTMLStyleSheetRulesCollection, HTMLStyleSheetRulesCollection
	{
		// Token: 0x060013C0 RID: 5056
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleSheetRulesCollectionClass();

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060013C1 RID: 5057
		[DispId(1001)]
		public virtual extern int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060013C2 RID: 5058
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern HTMLStyleSheetRule item([In] int index);
	}
}
