using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BD8 RID: 3032
	[TypeLibType(4160)]
	[Guid("3050F3E7-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFieldSetElement
	{
		// Token: 0x17006A27 RID: 27175
		// (get) Token: 0x0601412D RID: 82221
		// (set) Token: 0x0601412C RID: 82220
		[DispId(-2147418039)]
		string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
