using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000772 RID: 1906
	[TypeLibType(4160)]
	[Guid("3050F203-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLMetaElement
	{
		// Token: 0x17003851 RID: 14417
		// (get) Token: 0x0600AF7D RID: 44925
		// (set) Token: 0x0600AF7C RID: 44924
		[DispId(1001)]
		string httpEquiv
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003852 RID: 14418
		// (get) Token: 0x0600AF7F RID: 44927
		// (set) Token: 0x0600AF7E RID: 44926
		[DispId(1002)]
		string content
		{
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1002)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003853 RID: 14419
		// (get) Token: 0x0600AF81 RID: 44929
		// (set) Token: 0x0600AF80 RID: 44928
		[DispId(-2147418112)]
		string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003854 RID: 14420
		// (get) Token: 0x0600AF83 RID: 44931
		// (set) Token: 0x0600AF82 RID: 44930
		[DispId(1003)]
		string url
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17003855 RID: 14421
		// (get) Token: 0x0600AF85 RID: 44933
		// (set) Token: 0x0600AF84 RID: 44932
		[DispId(1013)]
		string charset
		{
			[TypeLibFunc(20)]
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
