using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000F8 RID: 248
	internal class XmlForClauseOptionsHelper : OptionsHelper<XmlForClauseOptions>
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x000902B8 File Offset: 0x0008E4B8
		private XmlForClauseOptionsHelper()
		{
			base.AddOptionMapping(XmlForClauseOptions.Elements, "ELEMENTS");
			base.AddOptionMapping(XmlForClauseOptions.Root, "ROOT");
			base.AddOptionMapping(XmlForClauseOptions.XmlSchema, "XMLSCHEMA");
			base.AddOptionMapping(XmlForClauseOptions.XmlData, "XMLDATA");
			base.AddOptionMapping(XmlForClauseOptions.Type, "TYPE");
		}

		// Token: 0x04000B03 RID: 2819
		internal static readonly XmlForClauseOptionsHelper Instance = new XmlForClauseOptionsHelper();
	}
}
