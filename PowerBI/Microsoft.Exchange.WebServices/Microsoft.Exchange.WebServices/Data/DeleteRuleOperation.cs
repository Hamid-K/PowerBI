using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200009B RID: 155
	public sealed class DeleteRuleOperation : RuleOperation
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00018C18 File Offset: 0x00017C18
		public DeleteRuleOperation()
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00018C20 File Offset: 0x00017C20
		public DeleteRuleOperation(string ruleId)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00018C2F File Offset: 0x00017C2F
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00018C37 File Offset: 0x00017C37
		public string RuleId
		{
			get
			{
				return this.ruleId;
			}
			set
			{
				this.SetFieldValue<string>(ref this.ruleId, value);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00018C46 File Offset: 0x00017C46
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "RuleId", this.RuleId);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00018C5C File Offset: 0x00017C5C
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("RuleId", this.RuleId);
			return jsonObject;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00018C81 File Offset: 0x00017C81
		internal override void InternalValidate()
		{
			EwsUtilities.ValidateParam(this.ruleId, "RuleId");
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00018C93 File Offset: 0x00017C93
		internal override string XmlElementName
		{
			get
			{
				return "DeleteRuleOperation";
			}
		}

		// Token: 0x0400025D RID: 605
		private string ruleId;
	}
}
