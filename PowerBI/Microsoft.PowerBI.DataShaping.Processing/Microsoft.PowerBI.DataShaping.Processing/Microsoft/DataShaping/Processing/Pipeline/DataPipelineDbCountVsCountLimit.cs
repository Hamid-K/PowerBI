using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000093 RID: 147
	internal sealed class DataPipelineDbCountVsCountLimit : DataPipelineLimit
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0000CB0F File Offset: 0x0000AD0F
		internal DataPipelineDbCountVsCountLimit(string id, int capacity, int isExceededDbCount, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
			: base(id, capacity, targetScopes, withinScope, skipInstancesWhenExceeded, warningCount)
		{
			this._isExceededDbCount = isExceededDbCount;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000CB28 File Offset: 0x0000AD28
		internal override bool HasCapacity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		internal override bool IsExceededByAnyInstance
		{
			get
			{
				if (this._isExceededDbCount <= base.Capacity)
				{
					int instanceCount = this.InstanceCount;
					int? warningCount = base.WarningCount;
					return (instanceCount > warningCount.GetValueOrDefault()) & (warningCount != null);
				}
				return true;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000CB67 File Offset: 0x0000AD67
		internal override void ExitInstance(Scope scope)
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000CB69 File Offset: 0x0000AD69
		internal override void SetExceeded()
		{
			Contract.RetailFail("Expected SetExceeded to never be called.");
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000CB75 File Offset: 0x0000AD75
		internal override int InstanceCount
		{
			get
			{
				return Math.Min(this._isExceededDbCount, base.Capacity);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000CB88 File Offset: 0x0000AD88
		internal int IsExceededDbCount
		{
			get
			{
				return this._isExceededDbCount;
			}
		}

		// Token: 0x04000211 RID: 529
		private readonly int _isExceededDbCount;
	}
}
