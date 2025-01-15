using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000F0 RID: 240
	internal class SuppressedJoinPredicatesByNameValidator : ISuppressedJoinPredicatesByNameValidator
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0001FDA9 File Offset: 0x0001DFA9
		public SuppressedJoinPredicatesByNameValidator(ITracer tracer, DataShapeGenerationErrorContext dataShapeGenerationErrorContext)
		{
			this._tracer = tracer;
			this._errorContext = dataShapeGenerationErrorContext;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		public void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations)
		{
			DataShapeBinding binding = command.QueryDataShape.Binding;
			if (((binding != null) ? binding.SuppressedJoinPredicatesByName : null) == null)
			{
				return;
			}
			using (IEnumerator<DataShapeBindingSuppressedJoinPredicate> enumerator = command.QueryDataShape.Binding.SuppressedJoinPredicatesByName.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataShapeBindingSuppressedJoinPredicate suppressedJoinPredicate = enumerator.Current;
					ResolvedQueryDefinition resolvedQueryDefinition;
					if (!annotations.QueryDefinitionByName.TryGetValue(suppressedJoinPredicate.QueryReference.SourceName, out resolvedQueryDefinition))
					{
						this._tracer.SanitizedTrace(TraceLevel.Warning, "Query contains a SuppressedJoinPredicateByName whose SourceName cannot be resolved. The SuppressedJoinPredicateByName will be ignored.");
						this._errorContext.Register(DataShapeGenerationMessages.CouldNotResolveSourceNameOnSuppressedJoinPredicateByName(EngineMessageSeverity.Warning, suppressedJoinPredicate.QueryReference.SourceName, suppressedJoinPredicate.QueryReference.ExpressionName));
					}
					else if (resolvedQueryDefinition.Select.FirstOrDefault((ResolvedQuerySelect select) => QueryNameComparer.Instance.Equals(select.Name, suppressedJoinPredicate.QueryReference.ExpressionName)) == null)
					{
						this._tracer.SanitizedTrace(TraceLevel.Warning, "Query contains a SuppressedJoinPredicateByName whose ExpressionName cannot be resolved. The SuppressedJoinPredicateByName will be ignored.");
						this._errorContext.Register(DataShapeGenerationMessages.CouldNotResolveExpressionReferenceOnSuppressedJoinPredicateByName(EngineMessageSeverity.Warning, suppressedJoinPredicate.QueryReference.SourceName, suppressedJoinPredicate.QueryReference.ExpressionName));
					}
				}
			}
		}

		// Token: 0x04000430 RID: 1072
		private readonly ITracer _tracer;

		// Token: 0x04000431 RID: 1073
		private readonly DataShapeGenerationErrorContext _errorContext;
	}
}
