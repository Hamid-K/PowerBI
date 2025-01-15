using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200005C RID: 92
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F58B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLRenderStyle
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060008E7 RID: 2279
		// (set) Token: 0x060008E6 RID: 2278
		[DispId(-2147412946)]
		string textLineThroughStyle
		{
			[DispId(-2147412946)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412946)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060008E9 RID: 2281
		// (set) Token: 0x060008E8 RID: 2280
		[DispId(-2147412945)]
		string textUnderlineStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147412945)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412945)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060008EB RID: 2283
		// (set) Token: 0x060008EA RID: 2282
		[DispId(-2147412944)]
		string textEffect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412944)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412944)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060008ED RID: 2285
		// (set) Token: 0x060008EC RID: 2284
		[DispId(-2147412922)]
		object textColor
		{
			[DispId(-2147412922)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412922)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060008EF RID: 2287
		// (set) Token: 0x060008EE RID: 2286
		[DispId(-2147412943)]
		object textBackgroundColor
		{
			[DispId(-2147412943)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412943)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060008F1 RID: 2289
		// (set) Token: 0x060008F0 RID: 2288
		[DispId(-2147412923)]
		object textDecorationColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412923)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412923)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060008F3 RID: 2291
		// (set) Token: 0x060008F2 RID: 2290
		[DispId(-2147412942)]
		int renderingPriority
		{
			[DispId(-2147412942)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412942)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060008F5 RID: 2293
		// (set) Token: 0x060008F4 RID: 2292
		[DispId(-2147412924)]
		string defaultTextSelection
		{
			[TypeLibFunc(20)]
			[DispId(-2147412924)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412924)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060008F7 RID: 2295
		// (set) Token: 0x060008F6 RID: 2294
		[DispId(-2147412921)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147412921)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412921)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
