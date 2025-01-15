using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000092 RID: 146
	internal class JoinHintHelper : OptionsHelper<JoinHint>
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000B82B File Offset: 0x00009A2B
		private JoinHintHelper()
		{
			base.AddOptionMapping(JoinHint.Hash, "HASH", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(JoinHint.Loop, "LOOP", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(JoinHint.Merge, "MERGE", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(JoinHint.Remote, "REMOTE", SqlVersionFlags.TSqlAll);
		}

		// Token: 0x0400039B RID: 923
		internal static readonly JoinHintHelper Instance = new JoinHintHelper();
	}
}
