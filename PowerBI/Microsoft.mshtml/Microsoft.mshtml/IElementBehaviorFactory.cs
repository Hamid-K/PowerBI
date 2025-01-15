using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000008 RID: 8
	[InterfaceType(1)]
	[Guid("3050F429-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorFactory
	{
		// Token: 0x0600012C RID: 300
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IElementBehavior FindBehavior([MarshalAs(19)] [In] string bstrBehavior, [MarshalAs(19)] [In] string bstrBehaviorUrl, [MarshalAs(28)] [In] IElementBehaviorSite pSite);
	}
}
