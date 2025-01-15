using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BDB RID: 3035
	[TypeLibType(4160)]
	[Guid("3050F834-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLLegendElement2
	{
		// Token: 0x17006A2A RID: 27178
		// (get) Token: 0x06014131 RID: 82225
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
