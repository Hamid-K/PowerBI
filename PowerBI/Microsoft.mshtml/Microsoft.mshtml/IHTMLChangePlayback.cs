using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC1 RID: 3265
	[InterfaceType(1)]
	[Guid("3050F6E0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLChangePlayback
	{
		// Token: 0x060162E1 RID: 90849
		[MethodImpl(4096, MethodCodeType = 3)]
		void ExecChange([In] ref byte pbRecord, [In] int fForward);
	}
}
