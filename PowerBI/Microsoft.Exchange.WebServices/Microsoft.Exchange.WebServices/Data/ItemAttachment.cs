using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000064 RID: 100
	public class ItemAttachment : Attachment
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00011192 File Offset: 0x00010192
		internal ItemAttachment(Item owner)
			: base(owner)
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001119B File Offset: 0x0001019B
		internal ItemAttachment(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x000111A4 File Offset: 0x000101A4
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x000111AC File Offset: 0x000101AC
		public Item Item
		{
			get
			{
				return this.item;
			}
			internal set
			{
				base.ThrowIfThisIsNotNew();
				if (this.item != null)
				{
					this.item.OnChange -= this.ItemChanged;
				}
				this.item = value;
				if (this.item != null)
				{
					this.item.OnChange += this.ItemChanged;
				}
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00011204 File Offset: 0x00010204
		private void ItemChanged(ServiceObject serviceObject)
		{
			if (base.Owner != null)
			{
				base.Owner.PropertyBag.Changed();
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001121E File Offset: 0x0001021E
		internal override string GetXmlElementName()
		{
			return "ItemAttachment";
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00011228 File Offset: 0x00010228
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			bool flag = base.TryReadElementFromXml(reader);
			if (!flag)
			{
				this.item = EwsUtilities.CreateItemFromXmlElementName(this, reader.LocalName);
				if (this.item != null)
				{
					this.item.LoadFromXml(reader, true);
				}
			}
			return flag;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00011268 File Offset: 0x00010268
		internal override bool TryReadElementFromXmlToPatch(EwsServiceXmlReader reader)
		{
			base.TryReadElementFromXml(reader);
			reader.Read();
			Type itemTypeFromXmlElementName = EwsUtilities.GetItemTypeFromXmlElementName(reader.LocalName);
			if (itemTypeFromXmlElementName == null)
			{
				return false;
			}
			if (this.item == null || this.item.GetType() != itemTypeFromXmlElementName)
			{
				throw new ServiceLocalException(Strings.AttachmentItemTypeMismatch);
			}
			this.item.LoadFromXml(reader, false);
			return true;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000112C8 File Offset: 0x000102C8
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			if (jsonProperty.ContainsKey("Item"))
			{
				JsonObject jsonObject = jsonProperty.ReadAsJsonObject("Item");
				if (jsonObject != null)
				{
					this.item = EwsUtilities.CreateItemFromXmlElementName(this, jsonObject.ReadTypeString());
					if (this.item != null)
					{
						this.item.LoadFromJson(jsonObject, service, true);
					}
				}
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00011321 File Offset: 0x00010321
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			this.Item.WriteToXml(writer);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011338 File Offset: 0x00010338
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = base.InternalToJson(service) as JsonObject;
			jsonObject.Add("Item", this.item.ToJson(service, false) as JsonObject);
			return jsonObject;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011370 File Offset: 0x00010370
		internal override void Validate(int attachmentIndex)
		{
			if (string.IsNullOrEmpty(base.Name))
			{
				throw new ServiceValidationException(string.Format(Strings.ItemAttachmentMustBeNamed, attachmentIndex));
			}
			this.Item.Attachments.Validate();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000113AC File Offset: 0x000103AC
		public void Load(params PropertyDefinitionBase[] additionalProperties)
		{
			base.InternalLoad(default(BodyType?), additionalProperties);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000113CC File Offset: 0x000103CC
		public void Load(IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			base.InternalLoad(default(BodyType?), additionalProperties);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000113E9 File Offset: 0x000103E9
		public void Load(BodyType bodyType, params PropertyDefinitionBase[] additionalProperties)
		{
			base.InternalLoad(new BodyType?(bodyType), additionalProperties);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000113F8 File Offset: 0x000103F8
		public void Load(BodyType bodyType, IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			base.InternalLoad(new BodyType?(bodyType), additionalProperties);
		}

		// Token: 0x040001AB RID: 427
		private Item item;
	}
}
