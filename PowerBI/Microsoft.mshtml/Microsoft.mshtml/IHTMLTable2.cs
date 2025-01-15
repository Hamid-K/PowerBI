using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009F6 RID: 2550
	[Guid("3050F4AD-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLTable2
	{
		// Token: 0x060103C1 RID: 66497
		[DispId(1035)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void firstPage();

		// Token: 0x060103C2 RID: 66498
		[DispId(1036)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void lastPage();

		// Token: 0x17005572 RID: 21874
		// (get) Token: 0x060103C3 RID: 66499
		[DispId(1037)]
		IHTMLElementCollection cells
		{
			[DispId(1037)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060103C4 RID: 66500
		[DispId(1038)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object moveRow([In] int indexFrom = -1, [In] int indexTo = -1);
	}
}
