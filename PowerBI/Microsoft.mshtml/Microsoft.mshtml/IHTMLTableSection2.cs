using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009FA RID: 2554
	[TypeLibType(4160)]
	[Guid("3050F5C7-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTableSection2
	{
		// Token: 0x060103D3 RID: 66515
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object moveRow([In] int indexFrom = -1, [In] int indexTo = -1);
	}
}
