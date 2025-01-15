using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008CE RID: 2254
	[TypeLibType(4160)]
	[Guid("3050F837-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLAreasCollection3
	{
		// Token: 0x0600E611 RID: 58897
		[DispId(1506)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object namedItem([MarshalAs(19)] [In] string name);
	}
}
