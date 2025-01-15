using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200009E RID: 158
	[TypeLibType(4160)]
	[Guid("3050F4FF-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTCEventBehavior
	{
		// Token: 0x06000D75 RID: 3445
		[DispId(-2147417612)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void fire([MarshalAs(28)] [In] IHTMLEventObj pVar);
	}
}
