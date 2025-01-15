using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB9 RID: 3513
	[InterfaceType(1)]
	[Guid("3050F846-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorLayout2
	{
		// Token: 0x060174C8 RID: 95432
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetTextDescent(out int plDescent);
	}
}
