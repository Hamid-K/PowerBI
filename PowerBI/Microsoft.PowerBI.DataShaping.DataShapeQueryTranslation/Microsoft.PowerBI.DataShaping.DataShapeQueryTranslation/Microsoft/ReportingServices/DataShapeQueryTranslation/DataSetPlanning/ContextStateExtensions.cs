using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000104 RID: 260
	internal static class ContextStateExtensions
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x00027800 File Offset: 0x00025A00
		public static bool CanChangeTo(this ContextState startState, ContextState candidateState)
		{
			switch (startState)
			{
			case ContextState.Output:
				return candidateState == ContextState.OutputRollup;
			case ContextState.Context:
				return candidateState == ContextState.Rollup || candidateState == ContextState.OutputRollup || candidateState == ContextState.Output || candidateState == ContextState.ContextOnly;
			case ContextState.Rollup:
				return candidateState == ContextState.OutputRollup || candidateState == ContextState.ContextOnly;
			}
			return false;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002783D File Offset: 0x00025A3D
		public static bool ShouldIncludeInQueryOutput(this ContextState state)
		{
			return state == ContextState.Output || state == ContextState.OutputRollup || state == ContextState.SynchronizationTarget;
		}
	}
}
