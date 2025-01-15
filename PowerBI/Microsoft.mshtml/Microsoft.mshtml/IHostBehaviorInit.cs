using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DBD RID: 3517
	[InterfaceType(1)]
	[Guid("3050F842-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHostBehaviorInit
	{
		// Token: 0x060174CD RID: 95437
		[MethodImpl(4096, MethodCodeType = 3)]
		void PopulateNamespaceTable();
	}
}
