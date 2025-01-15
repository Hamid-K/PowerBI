using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200005D RID: 93
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F6AA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLRenderStyleClass : DispHTMLRenderStyle, HTMLRenderStyle, IHTMLRenderStyle
	{
		// Token: 0x060008F8 RID: 2296
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLRenderStyleClass();

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060008FA RID: 2298
		// (set) Token: 0x060008F9 RID: 2297
		[DispId(-2147412946)]
		public virtual extern string textLineThroughStyle
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060008FC RID: 2300
		// (set) Token: 0x060008FB RID: 2299
		[DispId(-2147412945)]
		public virtual extern string textUnderlineStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147412945)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412945)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060008FE RID: 2302
		// (set) Token: 0x060008FD RID: 2301
		[DispId(-2147412944)]
		public virtual extern string textEffect
		{
			[DispId(-2147412944)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412944)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000900 RID: 2304
		// (set) Token: 0x060008FF RID: 2303
		[DispId(-2147412922)]
		public virtual extern object textColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412922)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412922)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000902 RID: 2306
		// (set) Token: 0x06000901 RID: 2305
		[DispId(-2147412943)]
		public virtual extern object textBackgroundColor
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

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000904 RID: 2308
		// (set) Token: 0x06000903 RID: 2307
		[DispId(-2147412923)]
		public virtual extern object textDecorationColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412923)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412923)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000906 RID: 2310
		// (set) Token: 0x06000905 RID: 2309
		[DispId(-2147412942)]
		public virtual extern int renderingPriority
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

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000908 RID: 2312
		// (set) Token: 0x06000907 RID: 2311
		[DispId(-2147412924)]
		public virtual extern string defaultTextSelection
		{
			[DispId(-2147412924)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412924)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600090A RID: 2314
		// (set) Token: 0x06000909 RID: 2313
		[DispId(-2147412921)]
		public virtual extern string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147412921)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412921)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600090C RID: 2316
		// (set) Token: 0x0600090B RID: 2315
		public virtual extern string IHTMLRenderStyle_textLineThroughStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600090E RID: 2318
		// (set) Token: 0x0600090D RID: 2317
		public virtual extern string IHTMLRenderStyle_textUnderlineStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000910 RID: 2320
		// (set) Token: 0x0600090F RID: 2319
		public virtual extern string IHTMLRenderStyle_textEffect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000912 RID: 2322
		// (set) Token: 0x06000911 RID: 2321
		public virtual extern object IHTMLRenderStyle_textColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000914 RID: 2324
		// (set) Token: 0x06000913 RID: 2323
		public virtual extern object IHTMLRenderStyle_textBackgroundColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000916 RID: 2326
		// (set) Token: 0x06000915 RID: 2325
		public virtual extern object IHTMLRenderStyle_textDecorationColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000918 RID: 2328
		// (set) Token: 0x06000917 RID: 2327
		public virtual extern int IHTMLRenderStyle_renderingPriority
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600091A RID: 2330
		// (set) Token: 0x06000919 RID: 2329
		public virtual extern string IHTMLRenderStyle_defaultTextSelection
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600091C RID: 2332
		// (set) Token: 0x0600091B RID: 2331
		public virtual extern string IHTMLRenderStyle_textDecoration
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
