using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009FD RID: 2557
	[TypeLibType(4160)]
	[Guid("3050F4A1-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTableRow2
	{
		// Token: 0x17005585 RID: 21893
		// (get) Token: 0x060103EA RID: 66538
		// (set) Token: 0x060103E9 RID: 66537
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
