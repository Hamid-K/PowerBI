using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004E0 RID: 1248
	[TypeLibType(4160)]
	[Guid("3050F5ED-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLSelectElement2
	{
		// Token: 0x06007F7D RID: 32637
		[DispId(1505)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object urns([MarshalAs(27)] [In] object urn);
	}
}
