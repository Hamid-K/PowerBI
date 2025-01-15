using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F9 RID: 2553
	[Guid("3050F82A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTableCol2
	{
		// Token: 0x17005578 RID: 21880
		// (get) Token: 0x060103D0 RID: 66512
		// (set) Token: 0x060103CF RID: 66511
		[DispId(1002)]
		string ch
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

		// Token: 0x17005579 RID: 21881
		// (get) Token: 0x060103D2 RID: 66514
		// (set) Token: 0x060103D1 RID: 66513
		[DispId(1003)]
		string chOff
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
	}
}
