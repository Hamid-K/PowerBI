using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB0 RID: 3504
	[Guid("3050F805-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementNamespaceFactory2 : IElementNamespaceFactory
	{
		// Token: 0x060174B1 RID: 95409
		[MethodImpl(4096, MethodCodeType = 3)]
		void create([MarshalAs(28)] [In] IElementNamespace pNamespace);

		// Token: 0x060174B2 RID: 95410
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateWithImplementation([MarshalAs(28)] [In] IElementNamespace pNamespace, [MarshalAs(19)] [In] string bstrImplementation);
	}
}
