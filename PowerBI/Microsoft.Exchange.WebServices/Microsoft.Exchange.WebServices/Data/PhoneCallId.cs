using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200030A RID: 778
	internal sealed class PhoneCallId : ComplexProperty
	{
		// Token: 0x06001BA5 RID: 7077 RVA: 0x00049C30 File Offset: 0x00048C30
		internal PhoneCallId()
		{
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00049C38 File Offset: 0x00048C38
		internal PhoneCallId(string id)
		{
			this.id = id;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00049C47 File Offset: 0x00048C47
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.id = reader.ReadAttributeValue("Id");
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00049C5A File Offset: 0x00048C5A
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			this.id = jsonProperty.ReadAsString("Id");
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00049C6D File Offset: 0x00048C6D
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Id", this.id);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00049C80 File Offset: 0x00048C80
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.WriteToXml(writer, "PhoneCallId");
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00049C90 File Offset: 0x00048C90
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Id", this.id);
			return jsonObject;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00049CB5 File Offset: 0x00048CB5
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x00049CBD File Offset: 0x00048CBD
		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x04001466 RID: 5222
		private string id;
	}
}
