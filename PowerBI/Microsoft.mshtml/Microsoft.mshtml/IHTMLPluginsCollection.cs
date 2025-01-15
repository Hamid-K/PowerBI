using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000797 RID: 1943
	[Guid("3050F3FD-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLPluginsCollection
	{
		// Token: 0x17004633 RID: 17971
		// (get) Token: 0x0600D485 RID: 54405
		[DispId(1)]
		int length
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D486 RID: 54406
		[DispId(2)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void refresh([In] bool reload = false);
	}
}
