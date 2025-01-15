using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000E9 RID: 233
	internal class HiddenProjectionsValidator : IHiddenProjectionsValidator
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x0001FBEE File Offset: 0x0001DDEE
		public HiddenProjectionsValidator(DataShapeGenerationErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations)
		{
			DataShapeBinding binding = command.QueryDataShape.Binding;
			if (((binding != null) ? binding.HiddenProjections : null) == null)
			{
				return;
			}
			using (IEnumerator<DataShapeBindingHiddenProjections> enumerator = command.QueryDataShape.Binding.HiddenProjections.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataShapeBindingHiddenProjections hiddenProjection = enumerator.Current;
					if (!hiddenProjection.QueryReference.SourceName.Equals(command.QueryDataShape.Query.Name))
					{
						this._errorContext.Register(DataShapeGenerationMessages.HiddenProjectionSourceNameMustBeTheRootQuery(EngineMessageSeverity.Error, hiddenProjection.QueryReference.SourceName, hiddenProjection.QueryReference.ExpressionName));
					}
					else if (!command.QueryDataShape.Query.Select.Any((ResolvedQuerySelect select) => QueryNameComparer.Instance.Equals(select.Name, hiddenProjection.QueryReference.ExpressionName)))
					{
						this._errorContext.Register(DataShapeGenerationMessages.CouldNotResolveExpressionReferenceOnHiddenProjection(EngineMessageSeverity.Error, hiddenProjection.QueryReference.SourceName, hiddenProjection.QueryReference.ExpressionName));
					}
				}
			}
		}

		// Token: 0x0400042B RID: 1067
		private readonly DataShapeGenerationErrorContext _errorContext;
	}
}
