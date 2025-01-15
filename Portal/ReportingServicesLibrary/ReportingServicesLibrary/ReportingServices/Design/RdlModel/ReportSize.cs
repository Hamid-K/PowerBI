using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000409 RID: 1033
	[TypeConverter(typeof(ReportSizeConverter))]
	public struct ReportSize
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x0007FC28 File Offset: 0x0007DE28
		public ReportSize(Unit width, Unit height)
		{
			this.m_width = width;
			this.m_height = height;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0007FC38 File Offset: 0x0007DE38
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x0007FC40 File Offset: 0x0007DE40
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0007FC49 File Offset: 0x0007DE49
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x0007FC51 File Offset: 0x0007DE51
		public Unit Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0007FC5A File Offset: 0x0007DE5A
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0007FC67 File Offset: 0x0007DE67
		internal double WidthF
		{
			get
			{
				return this.m_width.FPixels;
			}
			set
			{
				this.m_width.FPixels = value;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0007FC75 File Offset: 0x0007DE75
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x0007FC82 File Offset: 0x0007DE82
		internal double HeightF
		{
			get
			{
				return this.m_height.FPixels;
			}
			set
			{
				this.m_height.FPixels = value;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0007FC90 File Offset: 0x0007DE90
		public override bool Equals(object obj)
		{
			return obj is ReportSize && ((ReportSize)obj).Height == this.Height && ((ReportSize)obj).Width == this.Width;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0007FCDD File Offset: 0x0007DEDD
		public static bool operator ==(ReportSize size1, ReportSize size2)
		{
			return size1.Equals(size2);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0007FCF2 File Offset: 0x0007DEF2
		public static bool operator !=(ReportSize size1, ReportSize size2)
		{
			return !size1.Equals(size2);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0007FD0C File Offset: 0x0007DF0C
		public override int GetHashCode()
		{
			return this.Height.GetHashCode() ^ this.Width.GetHashCode();
		}

		// Token: 0x04000E5A RID: 3674
		private Unit m_width;

		// Token: 0x04000E5B RID: 3675
		private Unit m_height;

		// Token: 0x04000E5C RID: 3676
		public static readonly ReportSize Empty;
	}
}
