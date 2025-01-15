using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000067 RID: 103
	internal sealed class ExpressionValidationResult
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x00012EFB File Offset: 0x000110FB
		internal ExpressionValidationResult(ExpressionNode outputNode, List<Calculation> referencedCalculations)
		{
			this.m_outputNode = outputNode;
			if (referencedCalculations != null)
			{
				this.m_referencedCalculations = referencedCalculations.AsReadOnly();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00012F19 File Offset: 0x00011119
		public ExpressionNode OutputNode
		{
			get
			{
				return this.m_outputNode;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00012F21 File Offset: 0x00011121
		public ReadOnlyCollection<Calculation> ReferencedCalculations
		{
			get
			{
				return this.m_referencedCalculations;
			}
		}

		// Token: 0x04000295 RID: 661
		private readonly ExpressionNode m_outputNode;

		// Token: 0x04000296 RID: 662
		private readonly ReadOnlyCollection<Calculation> m_referencedCalculations;
	}
}
