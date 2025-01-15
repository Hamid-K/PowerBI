using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200005B RID: 91
	[Guid("3050F6AE-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLRenderStyle
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060008D5 RID: 2261
		// (set) Token: 0x060008D4 RID: 2260
		[DispId(-2147412946)]
		string textLineThroughStyle
		{
			[DispId(-2147412946)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412946)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060008D7 RID: 2263
		// (set) Token: 0x060008D6 RID: 2262
		[DispId(-2147412945)]
		string textUnderlineStyle
		{
			[DispId(-2147412945)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412945)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060008D9 RID: 2265
		// (set) Token: 0x060008D8 RID: 2264
		[DispId(-2147412944)]
		string textEffect
		{
			[DispId(-2147412944)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412944)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060008DB RID: 2267
		// (set) Token: 0x060008DA RID: 2266
		[DispId(-2147412922)]
		object textColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412922)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412922)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060008DD RID: 2269
		// (set) Token: 0x060008DC RID: 2268
		[DispId(-2147412943)]
		object textBackgroundColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412943)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412943)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060008DF RID: 2271
		// (set) Token: 0x060008DE RID: 2270
		[DispId(-2147412923)]
		object textDecorationColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412923)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412923)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060008E1 RID: 2273
		// (set) Token: 0x060008E0 RID: 2272
		[DispId(-2147412942)]
		int renderingPriority
		{
			[DispId(-2147412942)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412942)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060008E3 RID: 2275
		// (set) Token: 0x060008E2 RID: 2274
		[DispId(-2147412924)]
		string defaultTextSelection
		{
			[DispId(-2147412924)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412924)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060008E5 RID: 2277
		// (set) Token: 0x060008E4 RID: 2276
		[DispId(-2147412921)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147412921)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412921)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
