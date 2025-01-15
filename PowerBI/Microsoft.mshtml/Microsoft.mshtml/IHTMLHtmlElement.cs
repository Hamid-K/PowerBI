using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200076F RID: 1903
	[TypeLibType(4160)]
	[Guid("3050F81C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLHtmlElement
	{
		// Token: 0x1700384E RID: 14414
		// (get) Token: 0x0600AF77 RID: 44919
		// (set) Token: 0x0600AF76 RID: 44918
		[DispId(1001)]
		string version
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
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
	}
}
