using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008CD RID: 2253
	[Guid("3050F5EC-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLAreasCollection2
	{
		// Token: 0x0600E610 RID: 58896
		[DispId(1505)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object urns([MarshalAs(27)] [In] object urn);
	}
}
