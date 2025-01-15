using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C7F RID: 3199
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F3D4-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLStyleFontFaceClass : IHTMLStyleFontFace, HTMLStyleFontFace
	{
		// Token: 0x060161E7 RID: 90599
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleFontFaceClass();

		// Token: 0x17007599 RID: 30105
		// (get) Token: 0x060161E9 RID: 90601
		// (set) Token: 0x060161E8 RID: 90600
		[DispId(-2147413015)]
		public virtual extern string fontsrc
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
