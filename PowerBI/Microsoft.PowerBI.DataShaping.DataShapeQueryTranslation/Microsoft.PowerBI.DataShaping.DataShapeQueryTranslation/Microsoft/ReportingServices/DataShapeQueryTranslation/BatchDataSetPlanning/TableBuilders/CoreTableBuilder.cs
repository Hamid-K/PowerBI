using System;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001BE RID: 446
	internal sealed class CoreTableBuilder
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "CoreTable", "Artifacts" })]
		internal static global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts> CreateCoreTable(BatchDataSetPlannerContext plannerContext, DataShapeContext dsContext, PlanDeclarationCollection declarations, ContextTableManager attributeFilterContextTableManager, InstanceFiltersContext instanceFiltersContext, bool suppressUnconstrainedJoinCheck)
		{
			if (!dsContext.HasAnyPrimaryDynamic && !dsContext.HasAnySecondaryDynamic && !dsContext.HasDataTransforms)
			{
				Contract.RetailFail("CoreTableBuilder should not be called if there are not dynamic members or no transform");
			}
			CoreTableBuilderStrategy coreTableBuilderStrategy;
			if (dsContext.HasComplexSlicer)
			{
				coreTableBuilderStrategy = new CoreTableComplexSlicerStrategy(plannerContext, dsContext, declarations, attributeFilterContextTableManager, instanceFiltersContext, suppressUnconstrainedJoinCheck);
			}
			else
			{
				coreTableBuilderStrategy = new CoreTableSimpleSlicerStrategy(plannerContext, dsContext, declarations, attributeFilterContextTableManager, instanceFiltersContext, suppressUnconstrainedJoinCheck);
			}
			CoreTableArtifacts coreTableArtifacts;
			return new global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts>(coreTableBuilderStrategy.Build(out coreTableArtifacts), coreTableArtifacts);
		}
	}
}
