using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B5 RID: 1973
	[DefaultMember("item")]
	[Guid("332C4426-26CB-11D0-B483-00C04FD90119")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLFramesCollection2
	{
		// Token: 0x0600D6CD RID: 54989
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x17004768 RID: 18280
		// (get) Token: 0x0600D6CE RID: 54990
		[DispId(1001)]
		int length
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
