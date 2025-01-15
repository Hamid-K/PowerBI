using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B7 RID: 1975
	[TypeLibType(4160)]
	[Guid("3050F35C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLScreen
	{
		// Token: 0x17004789 RID: 18313
		// (get) Token: 0x0600D715 RID: 55061
		[DispId(1001)]
		int colorDepth
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700478A RID: 18314
		// (get) Token: 0x0600D717 RID: 55063
		// (set) Token: 0x0600D716 RID: 55062
		[DispId(1002)]
		int bufferDepth
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700478B RID: 18315
		// (get) Token: 0x0600D718 RID: 55064
		[DispId(1003)]
		int width
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700478C RID: 18316
		// (get) Token: 0x0600D719 RID: 55065
		[DispId(1004)]
		int height
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700478D RID: 18317
		// (get) Token: 0x0600D71B RID: 55067
		// (set) Token: 0x0600D71A RID: 55066
		[DispId(1005)]
		int updateInterval
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700478E RID: 18318
		// (get) Token: 0x0600D71C RID: 55068
		[DispId(1006)]
		int availHeight
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700478F RID: 18319
		// (get) Token: 0x0600D71D RID: 55069
		[DispId(1007)]
		int availWidth
		{
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004790 RID: 18320
		// (get) Token: 0x0600D71E RID: 55070
		[DispId(1008)]
		bool fontSmoothingEnabled
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
