using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000189 RID: 393
	internal class XmlForClauseModeHelper : OptionsHelper<XmlForClauseOptions>
	{
		// Token: 0x06002150 RID: 8528 RVA: 0x0015D0E2 File Offset: 0x0015B2E2
		private XmlForClauseModeHelper()
		{
			base.AddOptionMapping(XmlForClauseOptions.Auto, "AUTO");
			base.AddOptionMapping(XmlForClauseOptions.Raw, "RAW");
			base.AddOptionMapping(XmlForClauseOptions.Explicit, "EXPLICIT");
			base.AddOptionMapping(XmlForClauseOptions.Path, "PATH");
		}

		// Token: 0x0400198F RID: 6543
		internal static readonly XmlForClauseModeHelper Instance = new XmlForClauseModeHelper();
	}
}
