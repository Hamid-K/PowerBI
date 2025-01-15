using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000DF RID: 223
	internal sealed class DataShapeBuilder : DataShapeBuilder<DataShape>
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x0000D323 File Offset: 0x0000B523
		internal DataShapeBuilder(DataShape parent)
			: base(parent, parent)
		{
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000D32D File Offset: 0x0000B52D
		public static DataShapeBuilder With(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, DataShapeUsage usage = DataShapeUsage.Query)
		{
			return new DataShapeBuilder(BuilderBase<DataShape>.CreateDataShape(identifier, dataSourceId, filterEmptyGroups, contextOnly, false, usage));
		}
	}
}
