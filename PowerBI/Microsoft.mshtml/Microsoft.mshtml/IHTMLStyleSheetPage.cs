using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000140 RID: 320
	[TypeLibType(4160)]
	[Guid("3050F7EE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLStyleSheetPage
	{
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060013C3 RID: 5059
		[DispId(1001)]
		string selector
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060013C4 RID: 5060
		[DispId(1002)]
		string pseudoClass
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
