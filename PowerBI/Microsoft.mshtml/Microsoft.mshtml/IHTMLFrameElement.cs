using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BCD RID: 3021
	[TypeLibType(4160)]
	[Guid("3050F313-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFrameElement
	{
		// Token: 0x1700668A RID: 26250
		// (get) Token: 0x060137EB RID: 79851
		// (set) Token: 0x060137EA RID: 79850
		[DispId(-2147414111)]
		object borderColor
		{
			[DispId(-2147414111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147414111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
