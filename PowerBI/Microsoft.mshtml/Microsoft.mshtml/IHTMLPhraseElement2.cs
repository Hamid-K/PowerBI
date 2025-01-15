using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009E6 RID: 2534
	[TypeLibType(4160)]
	[Guid("3050F824-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLPhraseElement2
	{
		// Token: 0x17005238 RID: 21048
		// (get) Token: 0x0600FBB3 RID: 64435
		// (set) Token: 0x0600FBB2 RID: 64434
		[DispId(1001)]
		string cite
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005239 RID: 21049
		// (get) Token: 0x0600FBB5 RID: 64437
		// (set) Token: 0x0600FBB4 RID: 64436
		[DispId(1002)]
		string dateTime
		{
			[DispId(1002)]
			[TypeLibFunc(20)]
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
	}
}
