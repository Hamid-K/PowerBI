using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BD9 RID: 3033
	[TypeLibType(4160)]
	[Guid("3050F833-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFieldSetElement2
	{
		// Token: 0x17006A28 RID: 27176
		// (get) Token: 0x0601412E RID: 82222
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
