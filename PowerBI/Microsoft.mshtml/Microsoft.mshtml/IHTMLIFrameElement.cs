using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BD2 RID: 3026
	[Guid("3050F315-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLIFrameElement
	{
		// Token: 0x17006854 RID: 26708
		// (get) Token: 0x06013C83 RID: 81027
		// (set) Token: 0x06013C82 RID: 81026
		[DispId(-2147414111)]
		int vspace
		{
			[DispId(-2147414111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17006855 RID: 26709
		// (get) Token: 0x06013C85 RID: 81029
		// (set) Token: 0x06013C84 RID: 81028
		[DispId(-2147414110)]
		int hspace
		{
			[DispId(-2147414110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147414110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17006856 RID: 26710
		// (get) Token: 0x06013C87 RID: 81031
		// (set) Token: 0x06013C86 RID: 81030
		[DispId(-2147418039)]
		string align
		{
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
