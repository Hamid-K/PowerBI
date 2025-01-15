using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000007 RID: 7
	[InterfaceType(1)]
	[Guid("3050F425-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehavior
	{
		// Token: 0x06000129 RID: 297
		[MethodImpl(4096, MethodCodeType = 3)]
		void Init([MarshalAs(28)] [In] IElementBehaviorSite pBehaviorSite);

		// Token: 0x0600012A RID: 298
		[MethodImpl(4096, MethodCodeType = 3)]
		void Notify([In] int lEvent, [MarshalAs(27)] [In] [Out] ref object pVar);

		// Token: 0x0600012B RID: 299
		[MethodImpl(4096, MethodCodeType = 3)]
		void Detach();
	}
}
