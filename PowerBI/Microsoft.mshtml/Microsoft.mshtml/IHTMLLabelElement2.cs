using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200041E RID: 1054
	[Guid("3050F832-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLLabelElement2
	{
		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060041F4 RID: 16884
		[DispId(1002)]
		IHTMLFormElement form
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
