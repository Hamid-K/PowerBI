using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000098 RID: 152
	[InterfaceType(2)]
	[Guid("3050F58C-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLDefaults
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06000D2E RID: 3374
		[DispId(1001)]
		IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06000D30 RID: 3376
		// (set) Token: 0x06000D2F RID: 3375
		[DispId(1002)]
		bool tabStop
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06000D32 RID: 3378
		// (set) Token: 0x06000D31 RID: 3377
		[DispId(-2147412913)]
		bool viewInheritStyle
		{
			[DispId(-2147412913)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412913)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06000D34 RID: 3380
		// (set) Token: 0x06000D33 RID: 3379
		[DispId(1006)]
		bool viewMasterTab
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06000D36 RID: 3382
		// (set) Token: 0x06000D35 RID: 3381
		[DispId(1003)]
		int scrollSegmentX
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06000D38 RID: 3384
		// (set) Token: 0x06000D37 RID: 3383
		[DispId(1004)]
		int scrollSegmentY
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

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06000D3A RID: 3386
		// (set) Token: 0x06000D39 RID: 3385
		[DispId(1008)]
		bool isMultiLine
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06000D3C RID: 3388
		// (set) Token: 0x06000D3B RID: 3387
		[DispId(-2147412950)]
		string contentEditable
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

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06000D3E RID: 3390
		// (set) Token: 0x06000D3D RID: 3389
		[DispId(1009)]
		bool canHaveHTML
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06000D40 RID: 3392
		// (set) Token: 0x06000D3F RID: 3391
		[DispId(1011)]
		IHTMLDocument viewLink
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

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06000D42 RID: 3394
		// (set) Token: 0x06000D41 RID: 3393
		[DispId(-2147412914)]
		bool frozen
		{
			[DispId(-2147412914)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412914)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}
	}
}
