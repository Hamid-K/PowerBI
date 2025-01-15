using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200004A RID: 74
	internal sealed class RestartKindDefinition
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00006123 File Offset: 0x00004323
		internal RestartKindDefinition(ExpressionNode restartKind)
		{
			this.RestartKind = restartKind;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006132 File Offset: 0x00004332
		internal ExpressionNode RestartKind { get; }
	}
}
