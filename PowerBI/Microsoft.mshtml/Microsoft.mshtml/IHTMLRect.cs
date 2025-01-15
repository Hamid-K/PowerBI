using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000066 RID: 102
	[TypeLibType(4160)]
	[Guid("3050F4A3-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLRect
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06000AE3 RID: 2787
		// (set) Token: 0x06000AE2 RID: 2786
		[DispId(1001)]
		int left
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06000AE5 RID: 2789
		// (set) Token: 0x06000AE4 RID: 2788
		[DispId(1002)]
		int top
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06000AE7 RID: 2791
		// (set) Token: 0x06000AE6 RID: 2790
		[DispId(1003)]
		int right
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06000AE9 RID: 2793
		// (set) Token: 0x06000AE8 RID: 2792
		[DispId(1004)]
		int bottom
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
