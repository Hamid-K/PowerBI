using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200009A RID: 154
	internal readonly struct DataWindowToken
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000D25A File Offset: 0x0000B45A
		internal DataWindowToken(WindowConstraintMode? constraintMode, DataMember activeMember)
		{
			this.ConstraintMode = constraintMode;
			this.ActiveMember = activeMember;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000D26A File Offset: 0x0000B46A
		internal WindowConstraintMode? ConstraintMode { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000D272 File Offset: 0x0000B472
		internal DataMember ActiveMember { get; }
	}
}
