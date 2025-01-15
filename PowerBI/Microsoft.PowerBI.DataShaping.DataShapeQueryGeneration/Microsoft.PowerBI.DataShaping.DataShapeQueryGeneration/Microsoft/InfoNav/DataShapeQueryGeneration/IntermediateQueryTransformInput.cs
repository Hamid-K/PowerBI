using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D9 RID: 217
	internal sealed class IntermediateQueryTransformInput
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x0001CB5D File Offset: 0x0001AD5D
		internal IntermediateQueryTransformInput(IReadOnlyList<IntermediateQueryTransformParameter> parameters, IntermediateQueryTransformTable table)
		{
			this._parameters = parameters;
			this._table = table;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001CB73 File Offset: 0x0001AD73
		internal IReadOnlyList<IntermediateQueryTransformParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001CB7B File Offset: 0x0001AD7B
		internal IntermediateQueryTransformTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x040003F6 RID: 1014
		private readonly IReadOnlyList<IntermediateQueryTransformParameter> _parameters;

		// Token: 0x040003F7 RID: 1015
		private readonly IntermediateQueryTransformTable _table;
	}
}
