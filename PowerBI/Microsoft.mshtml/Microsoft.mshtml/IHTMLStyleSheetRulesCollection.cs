using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200013B RID: 315
	[DefaultMember("item")]
	[Guid("3050F2E5-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLStyleSheetRulesCollection
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060013B9 RID: 5049
		[DispId(1001)]
		int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060013BA RID: 5050
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		HTMLStyleSheetRule item([In] int index);
	}
}
