using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200013C RID: 316
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F3CE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLStyleSheetRuleClass : IHTMLStyleSheetRule, HTMLStyleSheetRule
	{
		// Token: 0x060013BB RID: 5051
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleSheetRuleClass();

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060013BD RID: 5053
		// (set) Token: 0x060013BC RID: 5052
		[DispId(1001)]
		public virtual extern string selectorText
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060013BE RID: 5054
		[DispId(-2147418038)]
		public virtual extern IHTMLRuleStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060013BF RID: 5055
		[DispId(1002)]
		public virtual extern bool readOnly
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
