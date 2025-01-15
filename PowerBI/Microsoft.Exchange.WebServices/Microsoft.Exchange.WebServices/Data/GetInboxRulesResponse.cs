using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200016B RID: 363
	internal sealed class GetInboxRulesResponse : ServiceResponse
	{
		// Token: 0x060010B8 RID: 4280 RVA: 0x0003132A File Offset: 0x0003032A
		internal GetInboxRulesResponse()
		{
			this.ruleCollection = new RuleCollection();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00031340 File Offset: 0x00030340
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			reader.Read();
			this.ruleCollection.OutlookRuleBlobExists = reader.ReadElementValue<bool>(XmlNamespace.Messages, "OutlookRuleBlobExists");
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.NotSpecified, "InboxRules"))
			{
				this.ruleCollection.LoadFromXml(reader, XmlNamespace.NotSpecified, "InboxRules");
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00031390 File Offset: 0x00030390
		internal RuleCollection Rules
		{
			get
			{
				return this.ruleCollection;
			}
		}

		// Token: 0x040009C3 RID: 2499
		private RuleCollection ruleCollection;
	}
}
