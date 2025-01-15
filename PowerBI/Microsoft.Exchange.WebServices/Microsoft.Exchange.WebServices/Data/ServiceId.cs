using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000030 RID: 48
	public abstract class ServiceId : ComplexProperty
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00009B25 File Offset: 0x00008B25
		internal ServiceId()
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009B2D File Offset: 0x00008B2D
		internal ServiceId(string uniqueId)
			: this()
		{
			EwsUtilities.ValidateParam(uniqueId, "uniqueId");
			this.uniqueId = uniqueId;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009B47 File Offset: 0x00008B47
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.uniqueId = reader.ReadAttributeValue("Id");
			this.changeKey = reader.ReadAttributeValue("ChangeKey");
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009B6C File Offset: 0x00008B6C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Id"))
					{
						if (text2 == "ChangeKey")
						{
							this.changeKey = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.uniqueId = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009BF8 File Offset: 0x00008BF8
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Id", this.UniqueId);
			writer.WriteAttributeValue("ChangeKey", this.ChangeKey);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009C1C File Offset: 0x00008C1C
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetJsonTypeName());
			jsonObject.Add("Id", this.UniqueId);
			jsonObject.Add("ChangeKey", this.ChangeKey);
			return jsonObject;
		}

		// Token: 0x06000239 RID: 569
		internal abstract string GetXmlElementName();

		// Token: 0x0600023A RID: 570 RVA: 0x00009C5E File Offset: 0x00008C5E
		internal virtual string GetJsonTypeName()
		{
			return this.GetXmlElementName();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009C66 File Offset: 0x00008C66
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.WriteToXml(writer, this.GetXmlElementName());
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009C75 File Offset: 0x00008C75
		internal void Assign(ServiceId source)
		{
			this.uniqueId = source.UniqueId;
			this.changeKey = source.ChangeKey;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00009C8F File Offset: 0x00008C8F
		internal virtual bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.uniqueId);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009C9F File Offset: 0x00008C9F
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009CA7 File Offset: 0x00008CA7
		public string UniqueId
		{
			get
			{
				return this.uniqueId;
			}
			internal set
			{
				this.uniqueId = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009CB0 File Offset: 0x00008CB0
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00009CB8 File Offset: 0x00008CB8
		public string ChangeKey
		{
			get
			{
				return this.changeKey;
			}
			internal set
			{
				this.changeKey = value;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009CC1 File Offset: 0x00008CC1
		public bool SameIdAndChangeKey(ServiceId other)
		{
			return this.Equals(other) && ((this.ChangeKey == null && other.ChangeKey == null) || this.ChangeKey.Equals(other.ChangeKey));
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009CF4 File Offset: 0x00008CF4
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			ServiceId serviceId = obj as ServiceId;
			return serviceId != null && this.IsValid && serviceId.IsValid && this.UniqueId.Equals(serviceId.UniqueId);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009D3B File Offset: 0x00008D3B
		public override int GetHashCode()
		{
			if (!this.IsValid)
			{
				return base.GetHashCode();
			}
			return this.UniqueId.GetHashCode();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009D57 File Offset: 0x00008D57
		public override string ToString()
		{
			if (this.uniqueId != null)
			{
				return this.uniqueId;
			}
			return string.Empty;
		}

		// Token: 0x04000118 RID: 280
		private string changeKey;

		// Token: 0x04000119 RID: 281
		private string uniqueId;
	}
}
