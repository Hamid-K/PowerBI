using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000010 RID: 16
	internal sealed class DataShapeExpressionsAxisSynchronizationBuilder
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004B1C File Offset: 0x00002D1C
		internal DataShapeExpressionsAxisSynchronizationBuilder()
		{
			this.Result = new SynchronizedGroupingBlock();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004B2F File Offset: 0x00002D2F
		internal DataShapeExpressionsAxisSynchronizationBuilder WithDataShape(string dataShapeId)
		{
			this.Result.DataShape = dataShapeId;
			return this;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B3E File Offset: 0x00002D3E
		internal DataShapeExpressionsAxisSynchronizationBuilder WithGroupings(IList<int> groupingsIndices)
		{
			this.Result.Groupings = groupingsIndices;
			return this;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004B4D File Offset: 0x00002D4D
		internal SynchronizedGroupingBlock Result { get; }
	}
}
