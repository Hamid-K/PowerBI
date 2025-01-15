using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation.ResolvedCommandValidation
{
	// Token: 0x020000F2 RID: 242
	internal sealed class VisualShapeValidator : IVisualShapeValidator
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x000204EF File Offset: 0x0001E6EF
		public VisualShapeValidator(DataShapeGenerationErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000204FE File Offset: 0x0001E6FE
		public void Validate(ResolvedSemanticQueryDataShapeCommand command, SemanticQueryDataShapeAnnotations annotations)
		{
			VisualShapeValidator.ValidateQueryAndSubqueries(this._errorContext, command.QueryDataShape.Query, command.QueryDataShape.Binding, annotations);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00020522 File Offset: 0x0001E722
		public static void ValidateQuery(DataShapeGenerationErrorContext errorContext, ResolvedQueryDefinition topLevelQuery, SemanticQueryDataShapeAnnotations annotations)
		{
			VisualShapeValidator.ValidateQueryAndSubqueries(errorContext, topLevelQuery, null, annotations);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00020530 File Offset: 0x0001E730
		private static void ValidateQueryAndSubqueries(DataShapeGenerationErrorContext errorContext, ResolvedQueryDefinition topLevelQuery, DataShapeBinding binding, SemanticQueryDataShapeAnnotations annotations)
		{
			VisualShapeValidator.ValidateQuery(errorContext, topLevelQuery, binding, annotations);
			IEnumerable<ResolvedQueryDefinition> values = annotations.QueryDefinitionByName.Values;
			Func<ResolvedQueryDefinition, bool> <>9__0;
			Func<ResolvedQueryDefinition, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (ResolvedQueryDefinition q) => q != topLevelQuery);
			}
			foreach (ResolvedQueryDefinition resolvedQueryDefinition in values.Where(func))
			{
				VisualShapeValidator.ValidateQuery(errorContext, resolvedQueryDefinition, null, annotations);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000205C4 File Offset: 0x0001E7C4
		private static void ValidateQuery(DataShapeGenerationErrorContext errorContext, ResolvedQueryDefinition query, DataShapeBinding binding, SemanticQueryDataShapeAnnotations annotations)
		{
			if (query.VisualShape.IsNullOrEmpty<ResolvedQueryAxis>())
			{
				return;
			}
			if (!annotations.QueryHasVisualCalculationsExpressions(query.Name))
			{
				errorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeWithoutVisualCalcs(EngineMessageSeverity.Warning));
				return;
			}
			if (binding == null)
			{
				errorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeWithoutDataShapeBinding(EngineMessageSeverity.Error, query.Name));
				return;
			}
			Dictionary<ResolvedQueryExpression, int> dictionary = (from kvp in query.Select.Select((ResolvedQuerySelect @select, int index) => new global::System.ValueTuple<ResolvedQueryExpression, int>(@select.Expression, index))
				group kvp by kvp.Item1 into item
				select item.First<global::System.ValueTuple<ResolvedQueryExpression, int>>()).ToDictionary(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "index" })] global::System.ValueTuple<ResolvedQueryExpression, int> kvp) => kvp.Item1, ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "index" })] global::System.ValueTuple<ResolvedQueryExpression, int> kvp) => kvp.Item2);
			IReadOnlyList<global::System.ValueTuple<bool, int>?> selectIndexBindingContext = VisualShapeValidator.GetSelectIndexBindingContext(binding, query.Select.Count);
			HashSet<ResolvedQueryExpression> hashSet = new HashSet<ResolvedQueryExpression>();
			foreach (ResolvedQueryAxis resolvedQueryAxis in query.VisualShape)
			{
				VisualShapeValidator.DataShapeBindingAxisMatchState dataShapeBindingAxisMatchState = VisualShapeValidator.DataShapeBindingAxisMatchState.Any;
				int num = -1;
				bool subtotal = resolvedQueryAxis.Groups[0].Subtotal;
				for (int i = 0; i < resolvedQueryAxis.Groups.Count; i++)
				{
					ResolvedQueryAxisGroup resolvedQueryAxisGroup = resolvedQueryAxis.Groups[i];
					VisualShapeValidator.DataShapeBindingAxisMatchState dataShapeBindingAxisMatchState2 = VisualShapeValidator.DataShapeBindingAxisMatchState.Any;
					int num2 = -1;
					foreach (ResolvedQueryExpression resolvedQueryExpression in resolvedQueryAxisGroup.Keys)
					{
						int num3;
						if (!dictionary.TryGetValue(resolvedQueryExpression, out num3))
						{
							errorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeAxisGroupKeyWithoutCorrespondingSelect(EngineMessageSeverity.Error, resolvedQueryAxis.Name, resolvedQueryExpression));
							return;
						}
						if (!hashSet.Add(resolvedQueryExpression))
						{
							errorContext.Register(DataShapeGenerationMessages.UnsupportedDuplicateVisualShapeKey(EngineMessageSeverity.Error, resolvedQueryExpression));
							return;
						}
						global::System.ValueTuple<bool, int>? valueTuple;
						if (!selectIndexBindingContext.TryGetNonNullAtExtendedIndex(num3, out valueTuple))
						{
							if (i == 0)
							{
								errorContext.Register(DataShapeGenerationMessages.UnsupportedUnprojectedFirstVisualShapeAxisGroup(EngineMessageSeverity.Error));
								return;
							}
							if (dataShapeBindingAxisMatchState2 == VisualShapeValidator.DataShapeBindingAxisMatchState.Primary || dataShapeBindingAxisMatchState2 == VisualShapeValidator.DataShapeBindingAxisMatchState.Secondary)
							{
								errorContext.Register(DataShapeGenerationMessages.InvalidVisualShapeAxisGroupProjection(EngineMessageSeverity.Error));
								return;
							}
							dataShapeBindingAxisMatchState2 = VisualShapeValidator.DataShapeBindingAxisMatchState.None;
						}
						else
						{
							VisualShapeValidator.DataShapeBindingAxisMatchState dataShapeBindingAxisMatchState3 = (valueTuple.Value.Item1 ? VisualShapeValidator.DataShapeBindingAxisMatchState.Primary : VisualShapeValidator.DataShapeBindingAxisMatchState.Secondary);
							int item2 = valueTuple.Value.Item2;
							if (dataShapeBindingAxisMatchState2 == VisualShapeValidator.DataShapeBindingAxisMatchState.None || dataShapeBindingAxisMatchState == VisualShapeValidator.DataShapeBindingAxisMatchState.None)
							{
								errorContext.Register(DataShapeGenerationMessages.InvalidVisualShapeAxisGroupProjection(EngineMessageSeverity.Error));
								return;
							}
							if ((num2 >= 0 && item2 != num2) || (dataShapeBindingAxisMatchState2 != VisualShapeValidator.DataShapeBindingAxisMatchState.Any && dataShapeBindingAxisMatchState2 != dataShapeBindingAxisMatchState3))
							{
								errorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeAxisGroupKeysProjectedAcrossDSBGroups(EngineMessageSeverity.Error));
								return;
							}
							if ((num >= 0 && item2 != num && item2 != num + 1) || (dataShapeBindingAxisMatchState != VisualShapeValidator.DataShapeBindingAxisMatchState.Any && dataShapeBindingAxisMatchState != dataShapeBindingAxisMatchState3))
							{
								errorContext.Register(DataShapeGenerationMessages.UnsupportedVisualShapeAxisGroupOutOfOrder(EngineMessageSeverity.Error));
								return;
							}
							dataShapeBindingAxisMatchState2 = dataShapeBindingAxisMatchState3;
							num2 = item2;
						}
					}
					if (subtotal != resolvedQueryAxisGroup.Subtotal)
					{
						errorContext.Register(DataShapeGenerationMessages.UnsupportedInconsistentTotalsInVisualShapeAxis(EngineMessageSeverity.Error, resolvedQueryAxis.Name));
						return;
					}
					dataShapeBindingAxisMatchState = dataShapeBindingAxisMatchState2;
					num = num2;
				}
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00020914 File Offset: 0x0001EB14
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "IsPrimary", "Depth" })]
		private static IReadOnlyList<global::System.ValueTuple<bool, int>?> GetSelectIndexBindingContext(DataShapeBinding binding, int size)
		{
			List<global::System.ValueTuple<bool, int>?> list = new List<global::System.ValueTuple<bool, int>?>(size);
			VisualShapeValidator.SetSelectIndexBindingContext(binding.Primary, true, list);
			VisualShapeValidator.SetSelectIndexBindingContext(binding.Secondary, false, list);
			return list;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00020944 File Offset: 0x0001EB44
		private static void SetSelectIndexBindingContext(DataShapeBindingAxis axis, bool isPrimary, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "IsPrimary", "Depth" })] List<global::System.ValueTuple<bool, int>?> list)
		{
			if (axis == null)
			{
				return;
			}
			for (int i = 0; i < axis.Groupings.Count; i++)
			{
				foreach (int num in axis.Groupings[i].Projections)
				{
					list.SetAtExtendedIndex(num, new global::System.ValueTuple<bool, int>?(new global::System.ValueTuple<bool, int>(isPrimary, i)));
				}
			}
		}

		// Token: 0x04000434 RID: 1076
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x02000160 RID: 352
		private enum DataShapeBindingAxisMatchState
		{
			// Token: 0x04000564 RID: 1380
			Any,
			// Token: 0x04000565 RID: 1381
			Primary,
			// Token: 0x04000566 RID: 1382
			Secondary,
			// Token: 0x04000567 RID: 1383
			None
		}
	}
}
