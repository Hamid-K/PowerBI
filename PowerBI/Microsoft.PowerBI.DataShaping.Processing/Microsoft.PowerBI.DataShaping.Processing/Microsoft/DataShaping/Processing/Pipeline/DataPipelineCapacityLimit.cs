using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000090 RID: 144
	internal sealed class DataPipelineCapacityLimit : DataPipelineLimit
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000C9E5 File Offset: 0x0000ABE5
		internal DataPipelineCapacityLimit(string id, int capacity, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
			: base(id, capacity, targetScopes, withinScope, skipInstancesWhenExceeded, warningCount)
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		internal override bool IsExceededByAnyInstance
		{
			get
			{
				if (!this._limitExceeded)
				{
					int instanceCount = this.InstanceCount;
					int? warningCount = base.WarningCount;
					return (instanceCount > warningCount.GetValueOrDefault()) & (warningCount != null);
				}
				return true;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		internal override bool HasCapacity
		{
			get
			{
				return this.InstanceCount < base.Capacity;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000CA3D File Offset: 0x0000AC3D
		internal override void SetExceeded()
		{
			this._limitExceeded = true;
		}

		// Token: 0x0400020D RID: 525
		private bool _limitExceeded;
	}
}
