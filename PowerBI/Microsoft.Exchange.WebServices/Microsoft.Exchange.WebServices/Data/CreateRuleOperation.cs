using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200009A RID: 154
	public sealed class CreateRuleOperation : RuleOperation
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00018B90 File Offset: 0x00017B90
		public CreateRuleOperation()
		{
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00018B98 File Offset: 0x00017B98
		public CreateRuleOperation(Rule rule)
		{
			this.rule = rule;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00018BA7 File Offset: 0x00017BA7
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x00018BAF File Offset: 0x00017BAF
		public Rule Rule
		{
			get
			{
				return this.rule;
			}
			set
			{
				this.SetFieldValue<Rule>(ref this.rule, value);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00018BBE File Offset: 0x00017BBE
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.Rule.WriteToXml(writer, "Rule");
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00018BD4 File Offset: 0x00017BD4
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Rule", this.Rule.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00018BFF File Offset: 0x00017BFF
		internal override void InternalValidate()
		{
			EwsUtilities.ValidateParam(this.rule, "Rule");
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00018C11 File Offset: 0x00017C11
		internal override string XmlElementName
		{
			get
			{
				return "CreateRuleOperation";
			}
		}

		// Token: 0x0400025C RID: 604
		private Rule rule;
	}
}
