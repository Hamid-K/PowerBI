using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000092 RID: 146
	internal sealed class DataPipelineDbCountLimit : DataPipelineLimit
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		internal DataPipelineDbCountLimit(string id, int capacity, int isExceededDbCount, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
			: base(id, capacity, targetScopes, withinScope, skipInstancesWhenExceeded, warningCount)
		{
			this._isExceededDbCount = isExceededDbCount;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000CABD File Offset: 0x0000ACBD
		internal override bool HasCapacity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		internal override bool IsExceededByAnyInstance
		{
			get
			{
				if (this._isExceededDbCount <= this.InstanceCount)
				{
					int instanceCount = this.InstanceCount;
					int? warningCount = base.WarningCount;
					return (instanceCount > warningCount.GetValueOrDefault()) & (warningCount != null);
				}
				return true;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CAFB File Offset: 0x0000ACFB
		internal override void SetExceeded()
		{
			Contract.RetailFail("Expected SetExceeded to never be called because capacity is infinite.");
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000CB07 File Offset: 0x0000AD07
		internal int IsExceededDbCount
		{
			get
			{
				return this._isExceededDbCount;
			}
		}

		// Token: 0x04000210 RID: 528
		private readonly int _isExceededDbCount;
	}
}
