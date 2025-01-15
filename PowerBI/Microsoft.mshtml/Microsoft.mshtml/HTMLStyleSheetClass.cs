using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200014A RID: 330
	[ClassInterface(0)]
	[Guid("3050F2E4-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLStyleSheetClass : DispHTMLStyleSheet, HTMLStyleSheet, IHTMLStyleSheet, IHTMLStyleSheet2
	{
		// Token: 0x060013FE RID: 5118
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleSheetClass();

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001400 RID: 5120
		// (set) Token: 0x060013FF RID: 5119
		[DispId(1001)]
		public virtual extern string title
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001401 RID: 5121
		[DispId(1002)]
		public virtual extern IHTMLStyleSheet parentStyleSheet
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001402 RID: 5122
		[DispId(1003)]
		public virtual extern IHTMLElement owningElement
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001404 RID: 5124
		// (set) Token: 0x06001403 RID: 5123
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001405 RID: 5125
		[DispId(1004)]
		public virtual extern bool readOnly
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001406 RID: 5126
		[DispId(1005)]
		public virtual extern HTMLStyleSheetsCollection imports
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001408 RID: 5128
		// (set) Token: 0x06001407 RID: 5127
		[DispId(1006)]
		public virtual extern string href
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001409 RID: 5129
		[DispId(1007)]
		public virtual extern string type
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600140A RID: 5130
		[DispId(1008)]
		public virtual extern string id
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600140B RID: 5131
		[DispId(1009)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addImport([MarshalAs(19)] [In] string bstrUrl, [In] int lIndex = -1);

		// Token: 0x0600140C RID: 5132
		[DispId(1010)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);

		// Token: 0x0600140D RID: 5133
		[DispId(1011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeImport([In] int lIndex);

		// Token: 0x0600140E RID: 5134
		[DispId(1012)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeRule([In] int lIndex);

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001410 RID: 5136
		// (set) Token: 0x0600140F RID: 5135
		[DispId(1013)]
		public virtual extern string media
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001412 RID: 5138
		// (set) Token: 0x06001411 RID: 5137
		[DispId(1014)]
		public virtual extern string cssText
		{
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001413 RID: 5139
		[DispId(1015)]
		public virtual extern HTMLStyleSheetRulesCollection rules
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001414 RID: 5140
		[DispId(1016)]
		public virtual extern HTMLStyleSheetPagesCollection pages
		{
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06001415 RID: 5141
		[DispId(1017)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addPageRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001417 RID: 5143
		// (set) Token: 0x06001416 RID: 5142
		public virtual extern string IHTMLStyleSheet_title
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001418 RID: 5144
		public virtual extern IHTMLStyleSheet IHTMLStyleSheet_parentStyleSheet
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001419 RID: 5145
		public virtual extern IHTMLElement IHTMLStyleSheet_owningElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600141B RID: 5147
		// (set) Token: 0x0600141A RID: 5146
		public virtual extern bool IHTMLStyleSheet_disabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600141C RID: 5148
		public virtual extern bool IHTMLStyleSheet_readOnly
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600141D RID: 5149
		public virtual extern HTMLStyleSheetsCollection IHTMLStyleSheet_imports
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600141F RID: 5151
		// (set) Token: 0x0600141E RID: 5150
		public virtual extern string IHTMLStyleSheet_href
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001420 RID: 5152
		public virtual extern string IHTMLStyleSheet_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001421 RID: 5153
		public virtual extern string IHTMLStyleSheet_id
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06001422 RID: 5154
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLStyleSheet_addImport([MarshalAs(19)] [In] string bstrUrl, [In] int lIndex = -1);

		// Token: 0x06001423 RID: 5155
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLStyleSheet_addRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);

		// Token: 0x06001424 RID: 5156
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLStyleSheet_removeImport([In] int lIndex);

		// Token: 0x06001425 RID: 5157
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLStyleSheet_removeRule([In] int lIndex);

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001427 RID: 5159
		// (set) Token: 0x06001426 RID: 5158
		public virtual extern string IHTMLStyleSheet_media
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001429 RID: 5161
		// (set) Token: 0x06001428 RID: 5160
		public virtual extern string IHTMLStyleSheet_cssText
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600142A RID: 5162
		public virtual extern HTMLStyleSheetRulesCollection IHTMLStyleSheet_rules
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600142B RID: 5163
		public virtual extern HTMLStyleSheetPagesCollection IHTMLStyleSheet2_pages
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600142C RID: 5164
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLStyleSheet2_addPageRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);
	}
}
