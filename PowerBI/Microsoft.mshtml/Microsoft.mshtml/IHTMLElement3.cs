using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000091 RID: 145
	[TypeLibType(4160)]
	[Guid("3050F673-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLElement3
	{
		// Token: 0x06000CD7 RID: 3287
		[DispId(-2147417016)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06000CD8 RID: 3288
		[DispId(-2147417015)]
		bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06000CD9 RID: 3289
		[DispId(-2147417014)]
		bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06000CDB RID: 3291
		// (set) Token: 0x06000CDA RID: 3290
		[DispId(-2147412039)]
		object onlayoutcomplete
		{
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06000CDD RID: 3293
		// (set) Token: 0x06000CDC RID: 3292
		[DispId(-2147412038)]
		object onpage
		{
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06000CDF RID: 3295
		// (set) Token: 0x06000CDE RID: 3294
		[DispId(-2147417012)]
		bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06000CE1 RID: 3297
		// (set) Token: 0x06000CE0 RID: 3296
		[DispId(-2147412035)]
		object onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06000CE2 RID: 3298
		[DispId(-2147417011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void setActive();

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06000CE4 RID: 3300
		// (set) Token: 0x06000CE3 RID: 3299
		[DispId(-2147412950)]
		string contentEditable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06000CE5 RID: 3301
		[DispId(-2147417010)]
		bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06000CE7 RID: 3303
		// (set) Token: 0x06000CE6 RID: 3302
		[DispId(-2147412949)]
		bool hideFocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06000CE9 RID: 3305
		// (set) Token: 0x06000CE8 RID: 3304
		[DispId(-2147418036)]
		bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06000CEA RID: 3306
		[DispId(-2147417007)]
		bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06000CEC RID: 3308
		// (set) Token: 0x06000CEB RID: 3307
		[DispId(-2147412034)]
		object onmove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06000CEE RID: 3310
		// (set) Token: 0x06000CED RID: 3309
		[DispId(-2147412033)]
		object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06000CEF RID: 3311
		[DispId(-2147417006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06000CF1 RID: 3313
		// (set) Token: 0x06000CF0 RID: 3312
		[DispId(-2147412029)]
		object onresizestart
		{
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06000CF3 RID: 3315
		// (set) Token: 0x06000CF2 RID: 3314
		[DispId(-2147412028)]
		object onresizeend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06000CF5 RID: 3317
		// (set) Token: 0x06000CF4 RID: 3316
		[DispId(-2147412031)]
		object onmovestart
		{
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06000CF7 RID: 3319
		// (set) Token: 0x06000CF6 RID: 3318
		[DispId(-2147412030)]
		object onmoveend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06000CF9 RID: 3321
		// (set) Token: 0x06000CF8 RID: 3320
		[DispId(-2147412027)]
		object onmouseenter
		{
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06000CFB RID: 3323
		// (set) Token: 0x06000CFA RID: 3322
		[DispId(-2147412026)]
		object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06000CFD RID: 3325
		// (set) Token: 0x06000CFC RID: 3324
		[DispId(-2147412025)]
		object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06000CFF RID: 3327
		// (set) Token: 0x06000CFE RID: 3326
		[DispId(-2147412024)]
		object ondeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06000D00 RID: 3328
		[DispId(-2147417005)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool dragDrop();

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06000D01 RID: 3329
		[DispId(-2147417004)]
		int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
