using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200079F RID: 1951
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F3FF-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class CPluginsClass : IHTMLPluginsCollection, CPlugins
	{
		// Token: 0x0600D4BF RID: 54463
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern CPluginsClass();

		// Token: 0x1700464F RID: 17999
		// (get) Token: 0x0600D4C0 RID: 54464
		[DispId(1)]
		public virtual extern int length
		{
			[DispId(1)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D4C1 RID: 54465
		[DispId(2)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void refresh([In] bool reload = false);
	}
}
