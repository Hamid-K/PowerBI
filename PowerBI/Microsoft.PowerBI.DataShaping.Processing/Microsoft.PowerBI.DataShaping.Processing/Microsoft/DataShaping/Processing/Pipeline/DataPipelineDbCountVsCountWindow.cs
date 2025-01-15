using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000094 RID: 148
	internal sealed class DataPipelineDbCountVsCountWindow : DataPipelineWindow
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000CB90 File Offset: 0x0000AD90
		internal DataPipelineDbCountVsCountWindow(string id, int capacity, int isExceededDbCount, IList<Scope> targetScopes)
			: base(id, capacity)
		{
			this._isExceededDbCount = isExceededDbCount;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000CBA1 File Offset: 0x0000ADA1
		internal override int DiagnosticInstanceCount
		{
			get
			{
				return Math.Min(this._isExceededDbCount, base.Capacity);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		internal override bool HasCapacity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000CBB7 File Offset: 0x0000ADB7
		internal override void SetHasExceededCapacity()
		{
			Contract.RetailFail("Expected SetExceeded to never be called.");
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000CBC3 File Offset: 0x0000ADC3
		internal override void ExitInstance(Scope scope)
		{
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000CBC5 File Offset: 0x0000ADC5
		internal override bool IsComplete
		{
			get
			{
				return this._isExceededDbCount <= base.Capacity;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		internal int DbCount
		{
			get
			{
				return this._isExceededDbCount;
			}
		}

		// Token: 0x04000212 RID: 530
		private readonly int _isExceededDbCount;
	}
}
