using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007BA RID: 1978
	[Guid("3050F84A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLScreen2
	{
		// Token: 0x1700479F RID: 18335
		// (get) Token: 0x0600D73E RID: 55102
		[DispId(1009)]
		int logicalXDPI
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047A0 RID: 18336
		// (get) Token: 0x0600D73F RID: 55103
		[DispId(1010)]
		int logicalYDPI
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047A1 RID: 18337
		// (get) Token: 0x0600D740 RID: 55104
		[DispId(1011)]
		int deviceXDPI
		{
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047A2 RID: 18338
		// (get) Token: 0x0600D741 RID: 55105
		[DispId(1012)]
		int deviceYDPI
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
