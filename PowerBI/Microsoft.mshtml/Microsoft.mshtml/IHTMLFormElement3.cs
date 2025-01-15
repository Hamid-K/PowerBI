using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001E4 RID: 484
	[Guid("3050F836-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLFormElement3
	{
		// Token: 0x06001BEF RID: 7151
		[DispId(1506)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object namedItem([MarshalAs(19)] [In] string name);
	}
}
