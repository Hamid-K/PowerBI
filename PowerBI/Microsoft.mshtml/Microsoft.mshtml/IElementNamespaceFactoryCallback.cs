using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB1 RID: 3505
	[InterfaceType(1)]
	[Guid("3050F7FD-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementNamespaceFactoryCallback
	{
		// Token: 0x060174B3 RID: 95411
		[MethodImpl(4096, MethodCodeType = 3)]
		void Resolve([MarshalAs(19)] [In] string bstrNamespace, [MarshalAs(19)] [In] string bstrTagName, [MarshalAs(19)] [In] string bstrAttrs, [MarshalAs(28)] [In] IElementNamespace pNamespace);
	}
}
