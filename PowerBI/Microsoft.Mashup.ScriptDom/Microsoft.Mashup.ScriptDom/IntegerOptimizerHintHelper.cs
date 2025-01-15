using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000A0 RID: 160
	internal class IntegerOptimizerHintHelper : OptionsHelper<OptimizerHintKind>
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		private IntegerOptimizerHintHelper()
		{
			base.AddOptionMapping(OptimizerHintKind.Fast, "FAST", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.MaxDop, "MAXDOP", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.UsePlan, "USEPLAN", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(OptimizerHintKind.MaxRecursion, "MAXRECURSION", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(OptimizerHintKind.QueryTraceOn, "QUERYTRACEON", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(OptimizerHintKind.CardinalityTunerLimit, "CARDINALITY_TUNER_LIMIT", SqlVersionFlags.TSql100AndAbove);
		}

		// Token: 0x040003D0 RID: 976
		internal static readonly IntegerOptimizerHintHelper Instance = new IntegerOptimizerHintHelper();
	}
}
