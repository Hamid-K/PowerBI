using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000A0 RID: 160
	[TypeLibType(4160)]
	[DefaultMember("FireEvent")]
	[Guid("3050F7EB-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTCAttachBehavior2
	{
		// Token: 0x06000D78 RID: 3448
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void FireEvent([MarshalAs(27)] [In] object evt);
	}
}
