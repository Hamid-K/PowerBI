using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DBB RID: 3515
	[Guid("3050F847-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementBehaviorSiteLayout2
	{
		// Token: 0x060174CC RID: 95436
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFontInfo(out tagLOGFONTW plf);
	}
}
