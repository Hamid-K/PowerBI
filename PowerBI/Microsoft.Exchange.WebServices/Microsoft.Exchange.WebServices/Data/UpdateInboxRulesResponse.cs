using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000189 RID: 393
	internal sealed class UpdateInboxRulesResponse : ServiceResponse
	{
		// Token: 0x06001136 RID: 4406 RVA: 0x000324D4 File Offset: 0x000314D4
		internal UpdateInboxRulesResponse()
		{
			this.errors = new RuleOperationErrorCollection();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000324E7 File Offset: 0x000314E7
		internal override bool LoadExtraErrorDetailsFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			if (xmlElementName.Equals("MessageXml"))
			{
				return base.LoadExtraErrorDetailsFromXml(reader, xmlElementName);
			}
			if (xmlElementName.Equals("RuleOperationErrors"))
			{
				this.errors.LoadFromXml(reader, XmlNamespace.Messages, xmlElementName);
				return true;
			}
			return false;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0003251D File Offset: 0x0003151D
		internal RuleOperationErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x040009E0 RID: 2528
		private RuleOperationErrorCollection errors;
	}
}
