using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200009F RID: 159
	[DefaultMember("FireEvent")]
	[Guid("3050F5F4-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTCAttachBehavior
	{
		// Token: 0x06000D76 RID: 3446
		[DispId(0)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void FireEvent([MarshalAs(26)] [In] object evt);

		// Token: 0x06000D77 RID: 3447
		[DispId(-2147417612)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void detachEvent();
	}
}
