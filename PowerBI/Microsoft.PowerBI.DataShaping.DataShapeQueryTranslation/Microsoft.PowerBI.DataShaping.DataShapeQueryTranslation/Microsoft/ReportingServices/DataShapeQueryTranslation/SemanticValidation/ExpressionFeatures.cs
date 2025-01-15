using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000063 RID: 99
	internal static class ExpressionFeatures
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00011CD0 File Offset: 0x0000FED0
		internal static ExpressionFeatureFlags CombineFlags(params ExpressionFeatureFlags[] flags)
		{
			ExpressionFeatureFlags combinedFlags = ExpressionFeatureFlags.None;
			Array.ForEach<ExpressionFeatureFlags>(flags, delegate(ExpressionFeatureFlags flag)
			{
				combinedFlags |= flag;
			});
			return combinedFlags;
		}

		// Token: 0x04000270 RID: 624
		internal static readonly ExpressionFeatureFlags CalculationValue = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.CalculationReferences,
			ExpressionFeatureFlags.ModelReferences,
			ExpressionFeatureFlags.ProcessingFunctions,
			ExpressionFeatureFlags.AggregateFunctions,
			ExpressionFeatureFlags.Evaluate,
			ExpressionFeatureFlags.EvaluateWithRollup,
			ExpressionFeatureFlags.FilterConditionReferences,
			ExpressionFeatureFlags.BinaryOrImageFieldReference,
			ExpressionFeatureFlags.Subtotal,
			ExpressionFeatureFlags.ScopeReference,
			ExpressionFeatureFlags.EvaluateWithScope
		});

		// Token: 0x04000271 RID: 625
		internal static readonly ExpressionFeatureFlags DataShapeCalculationValue = ExpressionFeatures.CalculationValue;

		// Token: 0x04000272 RID: 626
		internal static readonly ExpressionFeatureFlags DataTableValue = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.DataTableFunctions,
			ExpressionFeatureFlags.CalculationReferences,
			ExpressionFeatureFlags.ScopeReference
		});

		// Token: 0x04000273 RID: 627
		internal static readonly ExpressionFeatureFlags FilterExpression = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.CalculationReferences,
			ExpressionFeatureFlags.ModelReferences,
			ExpressionFeatureFlags.AggregateFunctions,
			ExpressionFeatureFlags.EvaluateWithRollup,
			ExpressionFeatureFlags.ScopeReference,
			ExpressionFeatureFlags.EvaluateWithScope
		});

		// Token: 0x04000274 RID: 628
		internal static readonly ExpressionFeatureFlags GroupExpression = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.ModelReferences,
			ExpressionFeatureFlags.CalculationReferences
		});

		// Token: 0x04000275 RID: 629
		internal static readonly ExpressionFeatureFlags SortExpression = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.ModelReferences,
			ExpressionFeatureFlags.EvaluateWithRollup,
			ExpressionFeatureFlags.ScopeReference,
			ExpressionFeatureFlags.EvaluateWithScope,
			ExpressionFeatureFlags.CalculationReferences
		});

		// Token: 0x04000276 RID: 630
		internal static readonly ExpressionFeatureFlags SyncTargetExpression = ExpressionFeatureFlags.ScopeReference;

		// Token: 0x04000277 RID: 631
		internal static readonly ExpressionFeatureFlags DataTransformTableColumnValue = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.ModelReferences,
			ExpressionFeatureFlags.ProcessingFunctions,
			ExpressionFeatureFlags.AggregateFunctions,
			ExpressionFeatureFlags.Evaluate,
			ExpressionFeatureFlags.ScopeReference,
			ExpressionFeatureFlags.EvaluateWithScope,
			ExpressionFeatureFlags.CalculationReferences
		});

		// Token: 0x04000278 RID: 632
		internal static readonly ExpressionFeatureFlags ExtensionPropertyExpression = ExpressionFeatures.CombineFlags(new ExpressionFeatureFlags[]
		{
			ExpressionFeatureFlags.ModelReferences,
			ExpressionFeatureFlags.AggregateFunctions
		});
	}
}
