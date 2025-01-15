using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000A3 RID: 163
	internal class PlanOptimizerHintHelper : OptionsHelper<OptimizerHintKind>
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000BA88 File Offset: 0x00009C88
		private PlanOptimizerHintHelper()
		{
			base.AddOptionMapping(OptimizerHintKind.RobustPlan, "ROBUST", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.ShrinkDBPlan, "SHRINKDB", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.AlterColumnPlan, "ALTERCOLUMN", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.KeepPlan, "KEEP", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.KeepFixedPlan, "KEEPFIXED", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.CheckConstraintsPlan, "CHECKCONSTRAINTS", SqlVersionFlags.TSql90AndAbove);
		}

		// Token: 0x040003D6 RID: 982
		internal static readonly PlanOptimizerHintHelper Instance = new PlanOptimizerHintHelper();
	}
}
