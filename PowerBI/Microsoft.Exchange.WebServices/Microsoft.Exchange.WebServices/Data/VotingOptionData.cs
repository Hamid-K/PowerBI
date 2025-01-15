using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000092 RID: 146
	public sealed class VotingOptionData : ComplexProperty
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x00016748 File Offset: 0x00015748
		internal VotingOptionData()
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00016750 File Offset: 0x00015750
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "DisplayName")
				{
					this.displayName = reader.ReadElementValue<string>();
					return true;
				}
				if (localName == "SendPrompt")
				{
					this.sendPrompt = reader.ReadElementValue<SendPrompt>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000167A0 File Offset: 0x000157A0
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "DisplayName"))
					{
						if (text2 == "SendPrompt")
						{
							this.sendPrompt = jsonProperty.ReadEnumValue<SendPrompt>(text);
						}
					}
					else
					{
						this.displayName = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001682C File Offset: 0x0001582C
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00016834 File Offset: 0x00015834
		public SendPrompt SendPrompt
		{
			get
			{
				return this.sendPrompt;
			}
		}

		// Token: 0x0400021B RID: 539
		private string displayName;

		// Token: 0x0400021C RID: 540
		private SendPrompt sendPrompt;
	}
}
