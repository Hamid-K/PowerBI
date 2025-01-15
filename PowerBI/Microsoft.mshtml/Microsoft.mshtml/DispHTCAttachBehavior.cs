using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000AE RID: 174
	[DefaultMember("FireEvent")]
	[Guid("3050F583-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[ComImport]
	public interface DispHTCAttachBehavior
	{
		// Token: 0x06000D97 RID: 3479
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void FireEvent([MarshalAs(27)] [In] object evt);

		// Token: 0x06000D98 RID: 3480
		[DispId(-2147417612)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent();
	}
}
