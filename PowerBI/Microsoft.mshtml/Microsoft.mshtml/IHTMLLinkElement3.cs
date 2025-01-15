using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000152 RID: 338
	[TypeLibType(4160)]
	[Guid("3050F81E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLLinkElement3
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060014C9 RID: 5321
		// (set) Token: 0x060014C8 RID: 5320
		[DispId(1018)]
		string charset
		{
			[DispId(1018)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1018)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060014CB RID: 5323
		// (set) Token: 0x060014CA RID: 5322
		[DispId(1019)]
		string hreflang
		{
			[TypeLibFunc(20)]
			[DispId(1019)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1019)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
