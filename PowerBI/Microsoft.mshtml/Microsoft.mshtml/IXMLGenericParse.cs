using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC4 RID: 3268
	[Guid("E4E23071-4D07-11D2-AE76-0080C73BC199")]
	[InterfaceType(1)]
	[ComImport]
	public interface IXMLGenericParse
	{
		// Token: 0x06016302 RID: 90882
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetGenericParse([In] bool fDoGeneric);
	}
}
