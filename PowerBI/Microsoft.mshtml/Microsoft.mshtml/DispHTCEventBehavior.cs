using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000AB RID: 171
	[InterfaceType(2)]
	[Guid("3050F574-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTCEventBehavior
	{
		// Token: 0x06000D93 RID: 3475
		[DispId(-2147417612)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void fire([MarshalAs(28)] [In] IHTMLEventObj pVar);
	}
}
