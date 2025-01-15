using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000091 RID: 145
	internal sealed class DataPipelineCapacityWindow : DataPipelineWindow
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000CA46 File Offset: 0x0000AC46
		internal DataPipelineCapacityWindow(string id, int capacity, IList<Scope> targetScopes)
			: base(id, capacity)
		{
			this._targetLeafScopes = new HashSet<Scope>(targetScopes);
			this._instanceCount = 0;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000CA63 File Offset: 0x0000AC63
		internal override int DiagnosticInstanceCount
		{
			get
			{
				return this._instanceCount;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		internal override bool HasCapacity
		{
			get
			{
				return this._instanceCount < base.Capacity;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000CA7B File Offset: 0x0000AC7B
		internal override bool IsComplete
		{
			get
			{
				return !base.HasExplicitlyExceededCapacity;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000CA86 File Offset: 0x0000AC86
		internal override void ExitInstance(Scope scope)
		{
			if (this._targetLeafScopes.Contains(scope))
			{
				this._instanceCount++;
			}
		}

		// Token: 0x0400020E RID: 526
		private readonly HashSet<Scope> _targetLeafScopes;

		// Token: 0x0400020F RID: 527
		private int _instanceCount;
	}
}
