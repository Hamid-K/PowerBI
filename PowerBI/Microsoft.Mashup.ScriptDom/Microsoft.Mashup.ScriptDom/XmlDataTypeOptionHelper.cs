using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	internal class XmlDataTypeOptionHelper : OptionsHelper<XmlDataTypeOption>
	{
		// Token: 0x0600214E RID: 8526 RVA: 0x0015D0B6 File Offset: 0x0015B2B6
		private XmlDataTypeOptionHelper()
		{
			base.AddOptionMapping(XmlDataTypeOption.Content, "CONTENT");
			base.AddOptionMapping(XmlDataTypeOption.Document, "DOCUMENT");
		}

		// Token: 0x0400198E RID: 6542
		internal static readonly XmlDataTypeOptionHelper Instance = new XmlDataTypeOptionHelper();
	}
}
