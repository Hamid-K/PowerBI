using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000A2 RID: 162
	internal class LockEscalationMethodHelper : OptionsHelper<LockEscalationMethod>
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000BA4D File Offset: 0x00009C4D
		private LockEscalationMethodHelper()
		{
			base.AddOptionMapping(LockEscalationMethod.Auto, "AUTO");
			base.AddOptionMapping(LockEscalationMethod.Disable, "DISABLE");
			base.AddOptionMapping(LockEscalationMethod.Table, TSqlTokenType.Table);
		}

		// Token: 0x040003D5 RID: 981
		public static LockEscalationMethodHelper Instance = new LockEscalationMethodHelper();
	}
}
