using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000147 RID: 327
	[Guid("3050F2E3-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLStyleSheet
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060013D1 RID: 5073
		// (set) Token: 0x060013D0 RID: 5072
		[DispId(1001)]
		string title
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

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060013D2 RID: 5074
		[DispId(1002)]
		IHTMLStyleSheet parentStyleSheet
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060013D3 RID: 5075
		[DispId(1003)]
		IHTMLElement owningElement
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060013D5 RID: 5077
		// (set) Token: 0x060013D4 RID: 5076
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060013D6 RID: 5078
		[DispId(1004)]
		bool readOnly
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060013D7 RID: 5079
		[DispId(1005)]
		HTMLStyleSheetsCollection imports
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060013D9 RID: 5081
		// (set) Token: 0x060013D8 RID: 5080
		[DispId(1006)]
		string href
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060013DA RID: 5082
		[DispId(1007)]
		string type
		{
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060013DB RID: 5083
		[DispId(1008)]
		string id
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060013DC RID: 5084
		[DispId(1009)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int addImport([MarshalAs(19)] [In] string bstrUrl, [In] int lIndex = -1);

		// Token: 0x060013DD RID: 5085
		[DispId(1010)]
		[MethodImpl(4096, MethodCodeType = 3)]
		int addRule([MarshalAs(19)] [In] string bstrSelector, [MarshalAs(19)] [In] string bstrStyle, [In] int lIndex = -1);

		// Token: 0x060013DE RID: 5086
		[DispId(1011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void removeImport([In] int lIndex);

		// Token: 0x060013DF RID: 5087
		[DispId(1012)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void removeRule([In] int lIndex);

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060013E1 RID: 5089
		// (set) Token: 0x060013E0 RID: 5088
		[DispId(1013)]
		string media
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060013E3 RID: 5091
		// (set) Token: 0x060013E2 RID: 5090
		[DispId(1014)]
		string cssText
		{
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060013E4 RID: 5092
		[DispId(1015)]
		HTMLStyleSheetRulesCollection rules
		{
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
