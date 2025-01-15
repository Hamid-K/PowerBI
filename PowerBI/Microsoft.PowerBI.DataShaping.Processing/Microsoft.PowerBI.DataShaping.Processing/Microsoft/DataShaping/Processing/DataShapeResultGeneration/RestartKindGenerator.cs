using System;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x0200007C RID: 124
	public sealed class RestartKindGenerator
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A517 File Offset: 0x00008717
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000A51F File Offset: 0x0000871F
		public bool HasRestartKind { get; private set; }

		// Token: 0x0600032F RID: 815 RVA: 0x0000A528 File Offset: 0x00008728
		internal RestartKindGenerator(ExpressionEvaluator expressionEvaluator)
		{
			this._expressionEvaluator = expressionEvaluator;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000A538 File Offset: 0x00008738
		internal RestartKind GetRestartKind(DataMember dataMember)
		{
			if (dataMember.RestartKindDefinition == null)
			{
				return RestartKind.None;
			}
			ExpressionNode restartKind = dataMember.RestartKindDefinition.RestartKind;
			int num = (int)(this._expressionEvaluator.Evaluate(restartKind) as long?).GetValueOrDefault();
			if (num != 0)
			{
				this.HasRestartKind = true;
			}
			return (RestartKind)num;
		}

		// Token: 0x040001D2 RID: 466
		private readonly ExpressionEvaluator _expressionEvaluator;
	}
}
