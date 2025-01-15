using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009FB RID: 2555
	[Guid("3050F82B-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTableSection3
	{
		// Token: 0x1700557A RID: 21882
		// (get) Token: 0x060103D5 RID: 66517
		// (set) Token: 0x060103D4 RID: 66516
		[DispId(1004)]
		string ch
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700557B RID: 21883
		// (get) Token: 0x060103D7 RID: 66519
		// (set) Token: 0x060103D6 RID: 66518
		[DispId(1005)]
		string chOff
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
