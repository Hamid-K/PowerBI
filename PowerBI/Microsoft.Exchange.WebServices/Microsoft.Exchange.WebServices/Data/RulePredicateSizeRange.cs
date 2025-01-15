using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000098 RID: 152
	public sealed class RulePredicateSizeRange : ComplexProperty
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x0001893D File Offset: 0x0001793D
		internal RulePredicateSizeRange()
		{
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00018945 File Offset: 0x00017945
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001894D File Offset: 0x0001794D
		public int? MinimumSize
		{
			get
			{
				return this.minimumSize;
			}
			set
			{
				this.SetFieldValue<int?>(ref this.minimumSize, value);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001895C File Offset: 0x0001795C
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00018964 File Offset: 0x00017964
		public int? MaximumSize
		{
			get
			{
				return this.maximumSize;
			}
			set
			{
				this.SetFieldValue<int?>(ref this.maximumSize, value);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00018974 File Offset: 0x00017974
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "MinimumSize")
				{
					this.minimumSize = new int?(reader.ReadElementValue<int>());
					return true;
				}
				if (localName == "MaximumSize")
				{
					this.maximumSize = new int?(reader.ReadElementValue<int>());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000189D0 File Offset: 0x000179D0
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "MinimumSize"))
					{
						if (text2 == "MaximumSize")
						{
							this.maximumSize = new int?(jsonProperty.ReadAsInt(text));
						}
					}
					else
					{
						this.minimumSize = new int?(jsonProperty.ReadAsInt(text));
					}
				}
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00018A64 File Offset: 0x00017A64
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.MinimumSize != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "MinimumSize", this.MinimumSize.Value);
			}
			if (this.MaximumSize != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "MaximumSize", this.MaximumSize.Value);
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00018AD0 File Offset: 0x00017AD0
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.MinimumSize != null)
			{
				jsonObject.Add("MinimumSize", this.MinimumSize.Value);
			}
			if (this.MaximumSize != null)
			{
				jsonObject.Add("MaximumSize", this.MaximumSize.Value);
			}
			return jsonObject;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00018B38 File Offset: 0x00017B38
		internal override void InternalValidate()
		{
			base.InternalValidate();
			if (this.minimumSize != null && this.maximumSize != null && this.minimumSize.Value > this.maximumSize.Value)
			{
				throw new ServiceValidationException("MinimumSize cannot be larger than MaximumSize.");
			}
		}

		// Token: 0x0400025A RID: 602
		private int? minimumSize;

		// Token: 0x0400025B RID: 603
		private int? maximumSize;
	}
}
