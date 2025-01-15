using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200009D RID: 157
	public sealed class SetRuleOperation : RuleOperation
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00018D10 File Offset: 0x00017D10
		public SetRuleOperation()
		{
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00018D18 File Offset: 0x00017D18
		public SetRuleOperation(Rule rule)
		{
			this.rule = rule;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00018D27 File Offset: 0x00017D27
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00018D2F File Offset: 0x00017D2F
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

		// Token: 0x06000741 RID: 1857 RVA: 0x00018D40 File Offset: 0x00017D40
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "Rule")
			{
				this.rule = new Rule();
				this.rule.LoadFromXml(reader, reader.LocalName);
				return true;
			}
			return false;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00018D84 File Offset: 0x00017D84
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null && text2 == "Rule")
				{
					this.rule = new Rule();
					this.rule.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
				}
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00018E00 File Offset: 0x00017E00
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.Rule.WriteToXml(writer, "Rule");
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00018E14 File Offset: 0x00017E14
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Rule", this.Rule.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00018E3F File Offset: 0x00017E3F
		internal override void InternalValidate()
		{
			EwsUtilities.ValidateParam(this.rule, "Rule");
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00018E51 File Offset: 0x00017E51
		internal override string XmlElementName
		{
			get
			{
				return "SetRuleOperation";
			}
		}

		// Token: 0x04000261 RID: 609
		private Rule rule;
	}
}
