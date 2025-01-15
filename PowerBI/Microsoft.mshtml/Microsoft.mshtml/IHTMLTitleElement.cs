using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000771 RID: 1905
	[TypeLibType(4160)]
	[Guid("3050F322-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTitleElement
	{
		// Token: 0x17003850 RID: 14416
		// (get) Token: 0x0600AF7B RID: 44923
		// (set) Token: 0x0600AF7A RID: 44922
		[DispId(-2147413011)]
		string text
		{
			[TypeLibFunc(4)]
			[DispId(-2147413011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
