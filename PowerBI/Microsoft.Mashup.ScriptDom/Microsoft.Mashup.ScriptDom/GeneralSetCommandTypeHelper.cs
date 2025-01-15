using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200014D RID: 333
	internal class GeneralSetCommandTypeHelper : OptionsHelper<GeneralSetCommandType>
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x000913C8 File Offset: 0x0008F5C8
		private GeneralSetCommandTypeHelper()
		{
			base.AddOptionMapping(GeneralSetCommandType.Language, "LANGUAGE");
			base.AddOptionMapping(GeneralSetCommandType.DateFormat, "DATEFORMAT");
			base.AddOptionMapping(GeneralSetCommandType.DateFirst, "DATEFIRST");
			base.AddOptionMapping(GeneralSetCommandType.DeadlockPriority, "DEADLOCK_PRIORITY");
			base.AddOptionMapping(GeneralSetCommandType.LockTimeout, "LOCK_TIMEOUT");
			base.AddOptionMapping(GeneralSetCommandType.QueryGovernorCostLimit, "QUERY_GOVERNOR_COST_LIMIT");
			base.AddOptionMapping(GeneralSetCommandType.ContextInfo, "CONTEXT_INFO");
		}

		// Token: 0x040011FF RID: 4607
		internal static readonly GeneralSetCommandTypeHelper Instance = new GeneralSetCommandTypeHelper();
	}
}
