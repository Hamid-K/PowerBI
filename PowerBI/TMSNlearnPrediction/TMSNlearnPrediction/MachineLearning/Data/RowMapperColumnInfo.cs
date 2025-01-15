using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000135 RID: 309
	public sealed class RowMapperColumnInfo
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00021AAE File Offset: 0x0001FCAE
		public RowMapperColumnInfo(string name, ColumnType type, ColumnMetadataInfo metadata)
		{
			this.Name = name;
			this.ColType = type;
			this.Metadata = metadata;
		}

		// Token: 0x0400032F RID: 815
		public readonly string Name;

		// Token: 0x04000330 RID: 816
		public readonly ColumnType ColType;

		// Token: 0x04000331 RID: 817
		public readonly ColumnMetadataInfo Metadata;
	}
}
