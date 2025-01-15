using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000776 RID: 1910
	[TypeLibType(4160)]
	[Guid("3050F82F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLIsIndexElement2
	{
		// Token: 0x1700385B RID: 14427
		// (get) Token: 0x0600AF90 RID: 44944
		[DispId(1012)]
		IHTMLFormElement form
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
