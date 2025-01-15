using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000E2 RID: 226
	internal sealed class DataReductionWindowExpansionStateValidator
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x0001D318 File Offset: 0x0001B518
		internal static void Validate(DataShapeGenerationErrorContext errorContext, DataReductionWindowExpansionState windowExpansionState, DataShapeBindingAxis bindingAxis)
		{
			if (windowExpansionState == null)
			{
				return;
			}
			if (bindingAxis.Groupings.Any(delegate(DataShapeBindingAxisGrouping group)
			{
				SubtotalType? subtotal = group.Subtotal;
				SubtotalType subtotalType = SubtotalType.None;
				return (subtotal.GetValueOrDefault() == subtotalType) & (subtotal != null);
			}))
			{
				errorContext.Register(DataShapeGenerationMessages.UnsupportedExpansionWithoutTotalsInSemanticQueryDataShapeCommand(EngineMessageSeverity.Error));
			}
			if (windowExpansionState.From.IsNullOrEmptyCollection<EntitySource>() && !windowExpansionState.Levels.IsNullOrEmptyCollection<DataShapeBindingAxisExpansionLevel>())
			{
				errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionWithNoProperty("windowExpansionState", "From")));
			}
			List<int> list = null;
			if (!windowExpansionState.Levels.IsNullOrEmptyCollection<DataShapeBindingAxisExpansionLevel>())
			{
				if (windowExpansionState.Levels.Count > bindingAxis.Groupings.Count)
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.MismatchedAxisGroupingWithLevelsCount(windowExpansionState.Levels.Count, bindingAxis.Groupings.Count)));
				}
				if (windowExpansionState.Levels.Any((DataShapeBindingAxisExpansionLevel level) => level.Default > ExpansionDefaultState.Collapsed))
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionWindowExpansionState("Expanded")));
				}
				list = windowExpansionState.Levels.Select((DataShapeBindingAxisExpansionLevel level) => level.Expressions.Count).ToList<int>();
			}
			DataReductionWindowExpansionStateValidator.ValidateWindowExpansionInstance(errorContext, windowExpansionState.WindowInstances, list, 0);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001D470 File Offset: 0x0001B670
		private static void ValidateWindowExpansionInstance(DataShapeGenerationErrorContext errorContext, DataReductionWindowExpansionInstance windowInstance, List<int> levelCounts, int levelIndex)
		{
			if (windowInstance == null)
			{
				return;
			}
			if (levelCounts == null && levelIndex > 0)
			{
				errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.NoLevelInfoWithMoreThanOneLevelOfExpansion()));
			}
			if (levelCounts != null && levelIndex > levelCounts.Count)
			{
				errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.TooManyWindowExpansionInstanceWithLevel()));
				return;
			}
			if (!windowInstance.Values.IsNullOrEmptyCollection<QueryExpressionContainer>())
			{
				if (levelIndex == 0)
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionWithUnexpectedValues("Values")));
				}
				else if (levelCounts != null && windowInstance.Values.Count != levelCounts[levelIndex - 1])
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidTopNPerLevelDataReduction(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.MismatchedValuesCountWithLevelExpressionsCount("Values", windowInstance.Values.Count, levelCounts[levelIndex - 1])));
				}
			}
			if (!windowInstance.Children.IsNullOrEmptyCollection<DataReductionWindowExpansionInstance>())
			{
				foreach (DataReductionWindowExpansionInstance dataReductionWindowExpansionInstance in windowInstance.Children)
				{
					DataReductionWindowExpansionStateValidator.ValidateWindowExpansionInstance(errorContext, dataReductionWindowExpansionInstance, levelCounts, levelIndex + 1);
				}
			}
		}
	}
}
