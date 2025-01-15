using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000149 RID: 329
	[InterfaceType(2)]
	[Guid("3050F58D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLStyleSheet
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060013E8 RID: 5096
		// (set) Token: 0x060013E7 RID: 5095
		[DispId(1001)]
		string title
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

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060013E9 RID: 5097
		[DispId(1002)]
		IHTMLStyleSheet parentStyleSheet
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060013EA RID: 5098
		[DispId(1003)]
		IHTMLElement owningElement
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060013EC RID: 5100
		// (set) Token: 0x060013EB RID: 5099
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060013ED RID: 5101
		[DispId(1004)]
		bool readOnly
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060013EE RID: 5102
		[DispId(1005)]
		HTMLStyleSheetsCollection imports
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060013F0 RID: 5104
		// (set) Token: 0x060013EF RID: 5103
		[DispId(1006)]
		string href
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

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060013F1 RID: 5105
		[DispId(1007)]
		string type
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060013F2 RID: 5106
		[DispId(1008)]
		string id
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060013F3 RID: 5107
		[DispId(1009)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addImport([MarshalAs(19)] [In] string bstrUrl, [In] int lIndex = -1);

		// Token: 0x060013F4 RID: 5108
		[DispId(1010)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);

		// Token: 0x060013F5 RID: 5109
		[DispId(1011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeImport([In] int lIndex);

		// Token: 0x060013F6 RID: 5110
		[DispId(1012)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeRule([In] int lIndex);

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060013F8 RID: 5112
		// (set) Token: 0x060013F7 RID: 5111
		[DispId(1013)]
		string media
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

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060013FA RID: 5114
		// (set) Token: 0x060013F9 RID: 5113
		[DispId(1014)]
		string cssText
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

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060013FB RID: 5115
		[DispId(1015)]
		HTMLStyleSheetRulesCollection rules
		{
			[DispId(1015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060013FC RID: 5116
		[DispId(1016)]
		HTMLStyleSheetPagesCollection pages
		{
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060013FD RID: 5117
		[DispId(1017)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addPageRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);
	}
}
