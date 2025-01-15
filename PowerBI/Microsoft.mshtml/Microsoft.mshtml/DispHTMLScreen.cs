using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007BE RID: 1982
	[InterfaceType(2)]
	[Guid("3050F591-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLScreen
	{
		// Token: 0x170047A5 RID: 18341
		// (get) Token: 0x0600D747 RID: 55111
		[DispId(1001)]
		int colorDepth
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047A6 RID: 18342
		// (get) Token: 0x0600D749 RID: 55113
		// (set) Token: 0x0600D748 RID: 55112
		[DispId(1002)]
		int bufferDepth
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170047A7 RID: 18343
		// (get) Token: 0x0600D74A RID: 55114
		[DispId(1003)]
		int width
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047A8 RID: 18344
		// (get) Token: 0x0600D74B RID: 55115
		[DispId(1004)]
		int height
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047A9 RID: 18345
		// (get) Token: 0x0600D74D RID: 55117
		// (set) Token: 0x0600D74C RID: 55116
		[DispId(1005)]
		int updateInterval
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170047AA RID: 18346
		// (get) Token: 0x0600D74E RID: 55118
		[DispId(1006)]
		int availHeight
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047AB RID: 18347
		// (get) Token: 0x0600D74F RID: 55119
		[DispId(1007)]
		int availWidth
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047AC RID: 18348
		// (get) Token: 0x0600D750 RID: 55120
		[DispId(1008)]
		bool fontSmoothingEnabled
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047AD RID: 18349
		// (get) Token: 0x0600D751 RID: 55121
		[DispId(1009)]
		int logicalXDPI
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047AE RID: 18350
		// (get) Token: 0x0600D752 RID: 55122
		[DispId(1010)]
		int logicalYDPI
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047AF RID: 18351
		// (get) Token: 0x0600D753 RID: 55123
		[DispId(1011)]
		int deviceXDPI
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B0 RID: 18352
		// (get) Token: 0x0600D754 RID: 55124
		[DispId(1012)]
		int deviceYDPI
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}
	}
}
