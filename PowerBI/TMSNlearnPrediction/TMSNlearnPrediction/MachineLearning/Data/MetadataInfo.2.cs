using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000138 RID: 312
	public sealed class MetadataInfo<T> : MetadataInfo
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x00021ADA File Offset: 0x0001FCDA
		public MetadataInfo(ColumnType type, MetadataUtils.MetadataGetter<T> getter)
			: base(type)
		{
			this.Getter = getter;
		}

		// Token: 0x04000333 RID: 819
		public readonly MetadataUtils.MetadataGetter<T> Getter;
	}
}
