using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000034 RID: 52
	public class RetentionTagBase : ComplexProperty
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00009FC8 File Offset: 0x00008FC8
		public RetentionTagBase(string xmlElementName)
		{
			this.xmlElementName = xmlElementName;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009FD7 File Offset: 0x00008FD7
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00009FDF File Offset: 0x00008FDF
		public bool IsExplicit
		{
			get
			{
				return this.isExplicit;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isExplicit, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009FEE File Offset: 0x00008FEE
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00009FF6 File Offset: 0x00008FF6
		public Guid RetentionId
		{
			get
			{
				return this.retentionId;
			}
			set
			{
				this.SetFieldValue<Guid>(ref this.retentionId, value);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A005 File Offset: 0x00009005
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.isExplicit = reader.ReadAttributeValue<bool>("IsExplicit");
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A018 File Offset: 0x00009018
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.retentionId = new Guid(reader.ReadValue());
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A02C File Offset: 0x0000902C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "IsExplicit"))
					{
						if (text2 == "Value")
						{
							this.retentionId = new Guid(jsonProperty.ReadAsString(text));
						}
					}
					else
					{
						this.isExplicit = jsonProperty.ReadAsBool(text);
					}
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A0BC File Offset: 0x000090BC
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("IsExplicit", this.isExplicit);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A0D4 File Offset: 0x000090D4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.retentionId != Guid.Empty)
			{
				writer.WriteValue(this.retentionId.ToString(), this.xmlElementName);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A108 File Offset: 0x00009108
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("IsExplicit", this.isExplicit);
			if (this.retentionId != Guid.Empty)
			{
				jsonObject.Add("Value", this.retentionId);
			}
			return jsonObject;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A155 File Offset: 0x00009155
		public override string ToString()
		{
			if (this.retentionId == Guid.Empty)
			{
				return string.Empty;
			}
			return this.retentionId.ToString();
		}

		// Token: 0x0400011F RID: 287
		private readonly string xmlElementName;

		// Token: 0x04000120 RID: 288
		private bool isExplicit;

		// Token: 0x04000121 RID: 289
		private Guid retentionId;
	}
}
