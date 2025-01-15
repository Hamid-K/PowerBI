using System;

namespace AngleSharp.Css
{
	// Token: 0x0200010E RID: 270
	public class RenderDevice
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		public RenderDevice(int width, int height)
		{
			this.DeviceWidth = width;
			this.DeviceHeight = height;
			this.ViewPortWidth = width;
			this.ViewPortHeight = height;
			this.ColorBits = 32;
			this.MonochromeBits = 0;
			this.Resolution = 96;
			this.DeviceType = RenderDevice.Kind.Screen;
			this.IsInterlaced = false;
			this.IsGrid = false;
			this.Frequency = 60;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0003CADF File Offset: 0x0003ACDF
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0003CAE7 File Offset: 0x0003ACE7
		public IConfiguration Options { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
		public int ViewPortWidth { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0003CB01 File Offset: 0x0003AD01
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0003CB09 File Offset: 0x0003AD09
		public int ViewPortHeight { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0003CB12 File Offset: 0x0003AD12
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0003CB1A File Offset: 0x0003AD1A
		public bool IsInterlaced { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0003CB23 File Offset: 0x0003AD23
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0003CB2B File Offset: 0x0003AD2B
		public bool IsGrid { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0003CB34 File Offset: 0x0003AD34
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0003CB3C File Offset: 0x0003AD3C
		public int DeviceWidth { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0003CB45 File Offset: 0x0003AD45
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0003CB4D File Offset: 0x0003AD4D
		public int DeviceHeight { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0003CB56 File Offset: 0x0003AD56
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0003CB5E File Offset: 0x0003AD5E
		public int Resolution { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0003CB67 File Offset: 0x0003AD67
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0003CB6F File Offset: 0x0003AD6F
		public int Frequency { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0003CB78 File Offset: 0x0003AD78
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0003CB80 File Offset: 0x0003AD80
		public int ColorBits { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0003CB89 File Offset: 0x0003AD89
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0003CB91 File Offset: 0x0003AD91
		public int MonochromeBits { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0003CB9A File Offset: 0x0003AD9A
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0003CBA2 File Offset: 0x0003ADA2
		public RenderDevice.Kind DeviceType { get; set; }

		// Token: 0x020004AA RID: 1194
		public enum Kind : byte
		{
			// Token: 0x040010F9 RID: 4345
			Screen,
			// Token: 0x040010FA RID: 4346
			Printer,
			// Token: 0x040010FB RID: 4347
			Speech,
			// Token: 0x040010FC RID: 4348
			Other
		}
	}
}
