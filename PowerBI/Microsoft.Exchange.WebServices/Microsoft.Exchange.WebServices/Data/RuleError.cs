using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A0 RID: 160
	public sealed class RuleError : ComplexProperty
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00018FD9 File Offset: 0x00017FD9
		internal RuleError()
		{
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00018FE1 File Offset: 0x00017FE1
		public RuleProperty RuleProperty
		{
			get
			{
				return this.ruleProperty;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00018FE9 File Offset: 0x00017FE9
		public RuleErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00018FF1 File Offset: 0x00017FF1
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00018FF9 File Offset: 0x00017FF9
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019004 File Offset: 0x00018004
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "FieldURI")
				{
					this.ruleProperty = reader.ReadElementValue<RuleProperty>();
					return true;
				}
				if (localName == "ErrorCode")
				{
					this.errorCode = reader.ReadElementValue<RuleErrorCode>();
					return true;
				}
				if (localName == "ErrorMessage")
				{
					this.errorMessage = reader.ReadElementValue();
					return true;
				}
				if (localName == "FieldValue")
				{
					this.value = reader.ReadElementValue();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001908C File Offset: 0x0001808C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			if (jsonProperty.ContainsKey("FieldURI"))
			{
				this.ruleProperty = jsonProperty.ReadEnumValue<RuleProperty>("FieldURI");
			}
			if (jsonProperty.ContainsKey("ErrorCode"))
			{
				this.errorCode = jsonProperty.ReadEnumValue<RuleErrorCode>("ErrorCode");
			}
			if (jsonProperty.ContainsKey("ErrorMessage"))
			{
				this.errorMessage = jsonProperty.ReadAsString("ErrorMessage");
			}
			if (jsonProperty.ContainsKey("FieldValue"))
			{
				this.value = jsonProperty.ReadAsString("FieldValue");
			}
		}

		// Token: 0x04000265 RID: 613
		private RuleProperty ruleProperty;

		// Token: 0x04000266 RID: 614
		private RuleErrorCode errorCode;

		// Token: 0x04000267 RID: 615
		private string errorMessage;

		// Token: 0x04000268 RID: 616
		private string value;
	}
}
