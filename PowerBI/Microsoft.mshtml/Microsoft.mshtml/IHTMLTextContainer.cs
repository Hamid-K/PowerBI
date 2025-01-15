using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000276 RID: 630
	[Guid("3050F230-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTextContainer
	{
		// Token: 0x06002784 RID: 10116
		[DispId(1001)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createControlRange();

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06002785 RID: 10117
		[DispId(1002)]
		int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002786 RID: 10118
		[DispId(1003)]
		int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002788 RID: 10120
		// (set) Token: 0x06002787 RID: 10119
		[DispId(1004)]
		int scrollTop
		{
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x0600278A RID: 10122
		// (set) Token: 0x06002789 RID: 10121
		[DispId(1005)]
		int scrollLeft
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600278C RID: 10124
		// (set) Token: 0x0600278B RID: 10123
		[DispId(-2147412081)]
		object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
