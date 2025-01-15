using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001DD RID: 477
	[TypeLibType(4160)]
	[Guid("3050F40B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTextRangeMetrics
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001B48 RID: 6984
		[DispId(1035)]
		int offsetTop
		{
			[DispId(1035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001B49 RID: 6985
		[DispId(1036)]
		int offsetLeft
		{
			[DispId(1036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001B4A RID: 6986
		[DispId(1037)]
		int boundingTop
		{
			[DispId(1037)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001B4B RID: 6987
		[DispId(1038)]
		int boundingLeft
		{
			[DispId(1038)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001B4C RID: 6988
		[DispId(1039)]
		int boundingWidth
		{
			[DispId(1039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001B4D RID: 6989
		[DispId(1040)]
		int boundingHeight
		{
			[DispId(1040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
