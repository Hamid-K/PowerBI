using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004A2 RID: 1186
	[TypeLibType(4160)]
	[Guid("3050F822-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLListElement2
	{
		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06004815 RID: 18453
		// (set) Token: 0x06004814 RID: 18452
		[DispId(1001)]
		bool compact
		{
			[TypeLibFunc(4)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
