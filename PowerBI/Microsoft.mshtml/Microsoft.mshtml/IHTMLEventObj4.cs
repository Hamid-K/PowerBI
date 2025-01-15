using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007AE RID: 1966
	[Guid("3050F814-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLEventObj4
	{
		// Token: 0x17004694 RID: 18068
		// (get) Token: 0x0600D548 RID: 54600
		[DispId(1051)]
		int wheelDelta
		{
			[DispId(1051)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
