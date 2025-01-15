using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation
{
	// Token: 0x020000E8 RID: 232
	internal sealed class DataShapeGenerationQueryExpressionValidator : QueryExpressionValidator
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0001FB5A File Offset: 0x0001DD5A
		internal DataShapeGenerationQueryExpressionValidator(IErrorContext errorContext, DataShapeGenerationErrorContext dseErrorContext, IFeatureSwitchProvider featureSwitchProvider, ConceptualCapabilities capabilities)
			: base(errorContext)
		{
			this._dseErrorContext = dseErrorContext;
			this._featureSwitchProvider = featureSwitchProvider;
			this._capabilities = capabilities;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		protected internal override void Visit(QueryNativeVisualCalculationExpression expression)
		{
			if (!this._featureSwitchProvider.IsEnabled(FeatureSwitchKind.VisualCalculations) || !this._capabilities.SupportsVisualCalculations)
			{
				this._dseErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualCalculation(EngineMessageSeverity.Error));
				return;
			}
			if (!expression.Language.Equals("Dax", StringComparison.OrdinalIgnoreCase))
			{
				this._dseErrorContext.Register(DataShapeGenerationMessages.UnsupportedVisualCalculationLanguage(EngineMessageSeverity.Error, expression.Language, "Dax"));
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x04000428 RID: 1064
		private readonly DataShapeGenerationErrorContext _dseErrorContext;

		// Token: 0x04000429 RID: 1065
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x0400042A RID: 1066
		private readonly ConceptualCapabilities _capabilities;
	}
}
