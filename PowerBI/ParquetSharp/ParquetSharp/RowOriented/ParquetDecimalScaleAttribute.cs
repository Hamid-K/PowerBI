using System;

namespace ParquetSharp.RowOriented
{
	// Token: 0x0200009D RID: 157
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class ParquetDecimalScaleAttribute : Attribute
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000100EC File Offset: 0x0000E2EC
		public ParquetDecimalScaleAttribute(int scale)
		{
			this.Scale = scale;
		}

		// Token: 0x04000160 RID: 352
		public readonly int Scale;
	}
}
