using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200013A RID: 314
	[Guid("3050F357-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLStyleSheetRule
	{
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060013B6 RID: 5046
		// (set) Token: 0x060013B5 RID: 5045
		[DispId(1001)]
		string selectorText
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

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060013B7 RID: 5047
		[DispId(-2147418038)]
		IHTMLRuleStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060013B8 RID: 5048
		[DispId(1002)]
		bool readOnly
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
