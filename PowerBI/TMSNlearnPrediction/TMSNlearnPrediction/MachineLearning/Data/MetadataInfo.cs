using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000137 RID: 311
	public abstract class MetadataInfo
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00021ACB File Offset: 0x0001FCCB
		protected MetadataInfo(ColumnType type)
		{
			this.Type = type;
		}

		// Token: 0x04000332 RID: 818
		public readonly ColumnType Type;
	}
}
