using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200003B RID: 59
	internal sealed class DataTransformOutput
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00005B11 File Offset: 0x00003D11
		internal DataTransformOutput(ResultTable table)
		{
			this._table = table;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005B20 File Offset: 0x00003D20
		internal ResultTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x0400010C RID: 268
		private readonly ResultTable _table;
	}
}
