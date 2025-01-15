using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000EF RID: 239
	internal class ResolvedSemanticQueryDataShapeCommandValidator : IResolvedSemanticQueryDataShapeCommandValidator
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		public ResolvedSemanticQueryDataShapeCommandValidator(ITracer tracer, IFeatureSwitchProvider featureSwitchProvider, DataShapeGenerationErrorContext dataShapeGenerationErrorContext)
			: this(new HiddenProjectionsValidator(dataShapeGenerationErrorContext), new SuppressedJoinPredicatesByNameValidator(tracer, dataShapeGenerationErrorContext), new TransformQueryValidator(dataShapeGenerationErrorContext, featureSwitchProvider), new VisualShapeValidator(dataShapeGenerationErrorContext))
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001FD4E File Offset: 0x0001DF4E
		internal ResolvedSemanticQueryDataShapeCommandValidator(IHiddenProjectionsValidator hiddenProjectionsValidator, ISuppressedJoinPredicatesByNameValidator suppressedJoinPredicatesByNameValidator, ITransformQueryValidator transformQueryValidator, IVisualShapeValidator visualShapeValidator)
		{
			this._hiddenProjectionsValidator = hiddenProjectionsValidator;
			this._suppressedJoinPredicatesByNameValidator = suppressedJoinPredicatesByNameValidator;
			this._transformQueryValidator = transformQueryValidator;
			this._visualShapeValidator = visualShapeValidator;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001FD73 File Offset: 0x0001DF73
		public void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations)
		{
			this._hiddenProjectionsValidator.Validate(command, annotations);
			this._suppressedJoinPredicatesByNameValidator.Validate(command, annotations);
			this._transformQueryValidator.Validate(command, annotations);
			this._visualShapeValidator.Validate(command, annotations);
		}

		// Token: 0x0400042C RID: 1068
		private readonly IHiddenProjectionsValidator _hiddenProjectionsValidator;

		// Token: 0x0400042D RID: 1069
		private readonly ISuppressedJoinPredicatesByNameValidator _suppressedJoinPredicatesByNameValidator;

		// Token: 0x0400042E RID: 1070
		private readonly ITransformQueryValidator _transformQueryValidator;

		// Token: 0x0400042F RID: 1071
		private readonly IVisualShapeValidator _visualShapeValidator;
	}
}
