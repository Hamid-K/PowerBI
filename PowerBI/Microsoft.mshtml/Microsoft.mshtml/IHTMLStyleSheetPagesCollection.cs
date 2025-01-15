using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000141 RID: 321
	[TypeLibType(4160)]
	[DefaultMember("item")]
	[Guid("3050F7F0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLStyleSheetPagesCollection
	{
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060013C5 RID: 5061
		[DispId(1001)]
		int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060013C6 RID: 5062
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		HTMLStyleSheetPage item([In] int index);
	}
}
