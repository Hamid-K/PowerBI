using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004C3 RID: 1219
	[Guid("3050F1F0-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLBRElement
	{
		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x06006A30 RID: 27184
		// (set) Token: 0x06006A2F RID: 27183
		[DispId(-2147413096)]
		string clear
		{
			[TypeLibFunc(20)]
			[DispId(-2147413096)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413096)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
