using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007BF RID: 1983
	[ClassInterface(0)]
	[Guid("3050F35D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLScreenClass : DispHTMLScreen, HTMLScreen, IHTMLScreen, IHTMLScreen2
	{
		// Token: 0x0600D755 RID: 55125
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLScreenClass();

		// Token: 0x170047B1 RID: 18353
		// (get) Token: 0x0600D756 RID: 55126
		[DispId(1001)]
		public virtual extern int colorDepth
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B2 RID: 18354
		// (get) Token: 0x0600D758 RID: 55128
		// (set) Token: 0x0600D757 RID: 55127
		[DispId(1002)]
		public virtual extern int bufferDepth
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170047B3 RID: 18355
		// (get) Token: 0x0600D759 RID: 55129
		[DispId(1003)]
		public virtual extern int width
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B4 RID: 18356
		// (get) Token: 0x0600D75A RID: 55130
		[DispId(1004)]
		public virtual extern int height
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B5 RID: 18357
		// (get) Token: 0x0600D75C RID: 55132
		// (set) Token: 0x0600D75B RID: 55131
		[DispId(1005)]
		public virtual extern int updateInterval
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170047B6 RID: 18358
		// (get) Token: 0x0600D75D RID: 55133
		[DispId(1006)]
		public virtual extern int availHeight
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B7 RID: 18359
		// (get) Token: 0x0600D75E RID: 55134
		[DispId(1007)]
		public virtual extern int availWidth
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B8 RID: 18360
		// (get) Token: 0x0600D75F RID: 55135
		[DispId(1008)]
		public virtual extern bool fontSmoothingEnabled
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047B9 RID: 18361
		// (get) Token: 0x0600D760 RID: 55136
		[DispId(1009)]
		public virtual extern int logicalXDPI
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047BA RID: 18362
		// (get) Token: 0x0600D761 RID: 55137
		[DispId(1010)]
		public virtual extern int logicalYDPI
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047BB RID: 18363
		// (get) Token: 0x0600D762 RID: 55138
		[DispId(1011)]
		public virtual extern int deviceXDPI
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047BC RID: 18364
		// (get) Token: 0x0600D763 RID: 55139
		[DispId(1012)]
		public virtual extern int deviceYDPI
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047BD RID: 18365
		// (get) Token: 0x0600D764 RID: 55140
		public virtual extern int IHTMLScreen_colorDepth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047BE RID: 18366
		// (get) Token: 0x0600D766 RID: 55142
		// (set) Token: 0x0600D765 RID: 55141
		public virtual extern int IHTMLScreen_bufferDepth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170047BF RID: 18367
		// (get) Token: 0x0600D767 RID: 55143
		public virtual extern int IHTMLScreen_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C0 RID: 18368
		// (get) Token: 0x0600D768 RID: 55144
		public virtual extern int IHTMLScreen_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C1 RID: 18369
		// (get) Token: 0x0600D76A RID: 55146
		// (set) Token: 0x0600D769 RID: 55145
		public virtual extern int IHTMLScreen_updateInterval
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170047C2 RID: 18370
		// (get) Token: 0x0600D76B RID: 55147
		public virtual extern int IHTMLScreen_availHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C3 RID: 18371
		// (get) Token: 0x0600D76C RID: 55148
		public virtual extern int IHTMLScreen_availWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C4 RID: 18372
		// (get) Token: 0x0600D76D RID: 55149
		public virtual extern bool IHTMLScreen_fontSmoothingEnabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C5 RID: 18373
		// (get) Token: 0x0600D76E RID: 55150
		public virtual extern int IHTMLScreen2_logicalXDPI
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C6 RID: 18374
		// (get) Token: 0x0600D76F RID: 55151
		public virtual extern int IHTMLScreen2_logicalYDPI
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C7 RID: 18375
		// (get) Token: 0x0600D770 RID: 55152
		public virtual extern int IHTMLScreen2_deviceXDPI
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047C8 RID: 18376
		// (get) Token: 0x0600D771 RID: 55153
		public virtual extern int IHTMLScreen2_deviceYDPI
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
