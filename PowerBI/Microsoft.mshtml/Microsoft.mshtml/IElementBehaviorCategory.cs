using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB3 RID: 3507
	[InterfaceType(1)]
	[Guid("3050F4ED-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IElementBehaviorCategory
	{
		// Token: 0x060174BB RID: 95419
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(21)]
		string GetCategory();
	}
}
