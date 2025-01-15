using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000205 RID: 517
	public sealed class PictureType : StructuredType
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0003E97C File Offset: 0x0003CB7C
		public PictureType(int height, int width)
			: base(typeof(Picture))
		{
			Contracts.CheckParam(height > 0, "height");
			Contracts.CheckParam(width > 0, "width");
			Contracts.CheckParam((long)height * (long)width <= 536870911L, "height", "height * width is too large");
			this.Height = height;
			this.Width = width;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0003E9E2 File Offset: 0x0003CBE2
		public PictureType()
			: base(typeof(Picture))
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003E9F4 File Offset: 0x0003CBF4
		public override bool Equals(ColumnType other)
		{
			if (other == this)
			{
				return true;
			}
			PictureType pictureType = other as PictureType;
			return pictureType != null && this.Height == pictureType.Height && this.Width == pictureType.Width;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003EA34 File Offset: 0x0003CC34
		public override string ToString()
		{
			if (this.Height == 0 && this.Width == 0)
			{
				return "Picture";
			}
			return string.Format("Picture<{0}, {1}>", this.Height, this.Width);
		}

		// Token: 0x04000647 RID: 1607
		public readonly int Height;

		// Token: 0x04000648 RID: 1608
		public readonly int Width;
	}
}
