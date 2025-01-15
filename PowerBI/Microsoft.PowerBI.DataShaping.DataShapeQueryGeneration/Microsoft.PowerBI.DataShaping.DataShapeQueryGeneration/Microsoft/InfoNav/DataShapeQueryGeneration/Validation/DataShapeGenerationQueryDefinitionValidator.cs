using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation
{
	// Token: 0x020000E7 RID: 231
	internal sealed class DataShapeGenerationQueryDefinitionValidator : QueryDefinitionValidator
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0001FB43 File Offset: 0x0001DD43
		internal DataShapeGenerationQueryDefinitionValidator(DataShapeGenerationQueryExpressionValidator expressionValidator, DataShapeGenerationErrorContext dseErrorContext, IFeatureSwitchProvider featureSwitchProvider)
			: base(expressionValidator)
		{
			this._dseErrorContext = dseErrorContext;
			this._featureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x04000426 RID: 1062
		private readonly DataShapeGenerationErrorContext _dseErrorContext;

		// Token: 0x04000427 RID: 1063
		private readonly IFeatureSwitchProvider _featureSwitchProvider;
	}
}
