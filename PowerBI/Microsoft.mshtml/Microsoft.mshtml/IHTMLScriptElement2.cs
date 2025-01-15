using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A93 RID: 2707
	[TypeLibType(4160)]
	[Guid("3050F828-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLScriptElement2
	{
		// Token: 0x17005E1F RID: 24095
		// (get) Token: 0x06011CCD RID: 72909
		// (set) Token: 0x06011CCC RID: 72908
		[DispId(1010)]
		string charset
		{
			[DispId(1010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
