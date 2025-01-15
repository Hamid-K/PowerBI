using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F7 RID: 2551
	[Guid("3050F829-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTable3
	{
		// Token: 0x17005573 RID: 21875
		// (get) Token: 0x060103C6 RID: 66502
		// (set) Token: 0x060103C5 RID: 66501
		[DispId(1039)]
		string summary
		{
			[DispId(1039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
