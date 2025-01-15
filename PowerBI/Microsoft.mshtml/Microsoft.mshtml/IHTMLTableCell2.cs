using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A01 RID: 2561
	[TypeLibType(4160)]
	[Guid("3050F82D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTableCell2
	{
		// Token: 0x17005599 RID: 21913
		// (get) Token: 0x0601040D RID: 66573
		// (set) Token: 0x0601040C RID: 66572
		[DispId(2004)]
		string abbr
		{
			[DispId(2004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2004)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700559A RID: 21914
		// (get) Token: 0x0601040F RID: 66575
		// (set) Token: 0x0601040E RID: 66574
		[DispId(2005)]
		string axis
		{
			[DispId(2005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700559B RID: 21915
		// (get) Token: 0x06010411 RID: 66577
		// (set) Token: 0x06010410 RID: 66576
		[DispId(2006)]
		string ch
		{
			[DispId(2006)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700559C RID: 21916
		// (get) Token: 0x06010413 RID: 66579
		// (set) Token: 0x06010412 RID: 66578
		[DispId(2007)]
		string chOff
		{
			[DispId(2007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700559D RID: 21917
		// (get) Token: 0x06010415 RID: 66581
		// (set) Token: 0x06010414 RID: 66580
		[DispId(2008)]
		string headers
		{
			[TypeLibFunc(20)]
			[DispId(2008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700559E RID: 21918
		// (get) Token: 0x06010417 RID: 66583
		// (set) Token: 0x06010416 RID: 66582
		[DispId(2009)]
		string scope
		{
			[DispId(2009)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
