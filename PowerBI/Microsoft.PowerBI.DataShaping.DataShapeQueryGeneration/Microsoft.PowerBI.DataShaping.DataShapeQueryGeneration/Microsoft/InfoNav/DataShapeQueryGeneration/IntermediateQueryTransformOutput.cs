using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000DA RID: 218
	internal sealed class IntermediateQueryTransformOutput
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0001CB83 File Offset: 0x0001AD83
		internal IntermediateQueryTransformOutput(IntermediateQueryTransformTable table)
		{
			this._table = table;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001CB92 File Offset: 0x0001AD92
		internal IntermediateQueryTransformTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x040003F8 RID: 1016
		private readonly IntermediateQueryTransformTable _table;
	}
}
