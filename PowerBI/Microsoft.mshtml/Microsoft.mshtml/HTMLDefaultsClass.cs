using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000099 RID: 153
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F6C8-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLDefaultsClass : DispHTMLDefaults, HTMLDefaults, IHTMLElementDefaults
	{
		// Token: 0x06000D43 RID: 3395
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDefaultsClass();

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06000D44 RID: 3396
		[DispId(1001)]
		public virtual extern IHTMLStyle style
		{
			[DispId(1001)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06000D46 RID: 3398
		// (set) Token: 0x06000D45 RID: 3397
		[DispId(1002)]
		public virtual extern bool tabStop
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06000D48 RID: 3400
		// (set) Token: 0x06000D47 RID: 3399
		[DispId(-2147412913)]
		public virtual extern bool viewInheritStyle
		{
			[DispId(-2147412913)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412913)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06000D4A RID: 3402
		// (set) Token: 0x06000D49 RID: 3401
		[DispId(1006)]
		public virtual extern bool viewMasterTab
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06000D4C RID: 3404
		// (set) Token: 0x06000D4B RID: 3403
		[DispId(1003)]
		public virtual extern int scrollSegmentX
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06000D4E RID: 3406
		// (set) Token: 0x06000D4D RID: 3405
		[DispId(1004)]
		public virtual extern int scrollSegmentY
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06000D50 RID: 3408
		// (set) Token: 0x06000D4F RID: 3407
		[DispId(1008)]
		public virtual extern bool isMultiLine
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06000D52 RID: 3410
		// (set) Token: 0x06000D51 RID: 3409
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06000D54 RID: 3412
		// (set) Token: 0x06000D53 RID: 3411
		[DispId(1009)]
		public virtual extern bool canHaveHTML
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06000D56 RID: 3414
		// (set) Token: 0x06000D55 RID: 3413
		[DispId(1011)]
		public virtual extern IHTMLDocument viewLink
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			set;
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06000D58 RID: 3416
		// (set) Token: 0x06000D57 RID: 3415
		[DispId(-2147412914)]
		public virtual extern bool frozen
		{
			[DispId(-2147412914)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412914)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06000D59 RID: 3417
		public virtual extern IHTMLStyle IHTMLElementDefaults_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06000D5B RID: 3419
		// (set) Token: 0x06000D5A RID: 3418
		public virtual extern bool IHTMLElementDefaults_tabStop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06000D5D RID: 3421
		// (set) Token: 0x06000D5C RID: 3420
		public virtual extern bool IHTMLElementDefaults_viewInheritStyle
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06000D5F RID: 3423
		// (set) Token: 0x06000D5E RID: 3422
		public virtual extern bool IHTMLElementDefaults_viewMasterTab
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06000D61 RID: 3425
		// (set) Token: 0x06000D60 RID: 3424
		public virtual extern int IHTMLElementDefaults_scrollSegmentX
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06000D63 RID: 3427
		// (set) Token: 0x06000D62 RID: 3426
		public virtual extern int IHTMLElementDefaults_scrollSegmentY
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06000D65 RID: 3429
		// (set) Token: 0x06000D64 RID: 3428
		public virtual extern bool IHTMLElementDefaults_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06000D67 RID: 3431
		// (set) Token: 0x06000D66 RID: 3430
		public virtual extern string IHTMLElementDefaults_contentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06000D69 RID: 3433
		// (set) Token: 0x06000D68 RID: 3432
		public virtual extern bool IHTMLElementDefaults_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06000D6B RID: 3435
		// (set) Token: 0x06000D6A RID: 3434
		public virtual extern IHTMLDocument IHTMLElementDefaults_viewLink
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(28)]
			[param: In]
			set;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06000D6D RID: 3437
		// (set) Token: 0x06000D6C RID: 3436
		public virtual extern bool IHTMLElementDefaults_frozen
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
