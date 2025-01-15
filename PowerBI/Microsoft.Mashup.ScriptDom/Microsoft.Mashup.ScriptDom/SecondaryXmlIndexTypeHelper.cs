using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	internal class SecondaryXmlIndexTypeHelper : OptionsHelper<SecondaryXmlIndexType>
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x00090B63 File Offset: 0x0008ED63
		private SecondaryXmlIndexTypeHelper()
		{
			base.AddOptionMapping(SecondaryXmlIndexType.Path, "PATH");
			base.AddOptionMapping(SecondaryXmlIndexType.Property, "PROPERTY");
			base.AddOptionMapping(SecondaryXmlIndexType.Value, "VALUE");
		}

		// Token: 0x0400113F RID: 4415
		internal static readonly SecondaryXmlIndexTypeHelper Instance = new SecondaryXmlIndexTypeHelper();
	}
}
