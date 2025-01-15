using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000097 RID: 151
	[Guid("3050F6C9-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLElementDefaults
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06000D19 RID: 3353
		[DispId(1001)]
		IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06000D1B RID: 3355
		// (set) Token: 0x06000D1A RID: 3354
		[DispId(1002)]
		bool tabStop
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06000D1D RID: 3357
		// (set) Token: 0x06000D1C RID: 3356
		[DispId(-2147412913)]
		bool viewInheritStyle
		{
			[DispId(-2147412913)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147412913)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06000D1F RID: 3359
		// (set) Token: 0x06000D1E RID: 3358
		[DispId(1006)]
		bool viewMasterTab
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06000D21 RID: 3361
		// (set) Token: 0x06000D20 RID: 3360
		[DispId(1003)]
		int scrollSegmentX
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06000D23 RID: 3363
		// (set) Token: 0x06000D22 RID: 3362
		[DispId(1004)]
		int scrollSegmentY
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06000D25 RID: 3365
		// (set) Token: 0x06000D24 RID: 3364
		[DispId(1008)]
		bool isMultiLine
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06000D27 RID: 3367
		// (set) Token: 0x06000D26 RID: 3366
		[DispId(-2147412950)]
		string contentEditable
		{
			[DispId(-2147412950)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412950)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06000D29 RID: 3369
		// (set) Token: 0x06000D28 RID: 3368
		[DispId(1009)]
		bool canHaveHTML
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06000D2B RID: 3371
		// (set) Token: 0x06000D2A RID: 3370
		[DispId(1011)]
		IHTMLDocument viewLink
		{
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06000D2D RID: 3373
		// (set) Token: 0x06000D2C RID: 3372
		[DispId(-2147412914)]
		bool frozen
		{
			[DispId(-2147412914)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147412914)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
