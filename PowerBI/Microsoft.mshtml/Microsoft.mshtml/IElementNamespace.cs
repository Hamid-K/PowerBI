using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DAD RID: 3501
	[Guid("3050F671-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IElementNamespace
	{
		// Token: 0x060174AE RID: 95406
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddTag([MarshalAs(19)] [In] string bstrTagName, [In] int lFlags);
	}
}
