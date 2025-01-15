using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C7E RID: 3198
	[Guid("3050F3D5-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLStyleFontFace
	{
		// Token: 0x17007598 RID: 30104
		// (get) Token: 0x060161E6 RID: 90598
		// (set) Token: 0x060161E5 RID: 90597
		[DispId(-2147413015)]
		string fontsrc
		{
			[DispId(-2147413015)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
