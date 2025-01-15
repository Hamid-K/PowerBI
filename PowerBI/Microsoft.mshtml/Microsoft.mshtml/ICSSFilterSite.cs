using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C94 RID: 3220
	[InterfaceType(1)]
	[Guid("3050F3ED-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ICSSFilterSite
	{
		// Token: 0x060161EA RID: 90602
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement GetElement();

		// Token: 0x060161EB RID: 90603
		[MethodImpl(4096, MethodCodeType = 3)]
		void FireOnFilterChangeEvent();
	}
}
