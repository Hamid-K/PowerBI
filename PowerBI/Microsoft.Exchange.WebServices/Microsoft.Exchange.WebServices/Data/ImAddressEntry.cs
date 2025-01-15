using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200006A RID: 106
	[EditorBrowsable(1)]
	public sealed class ImAddressEntry : DictionaryEntryProperty<ImAddressKey>
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x00011D3D File Offset: 0x00010D3D
		internal ImAddressEntry()
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011D45 File Offset: 0x00010D45
		internal ImAddressEntry(ImAddressKey key, string imAddress)
			: base(key)
		{
			this.imAddress = imAddress;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00011D55 File Offset: 0x00010D55
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00011D5D File Offset: 0x00010D5D
		public string ImAddress
		{
			get
			{
				return this.imAddress;
			}
			set
			{
				this.SetFieldValue<string>(ref this.imAddress, value);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011D6C File Offset: 0x00010D6C
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.imAddress = reader.ReadValue();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00011D7A File Offset: 0x00010D7A
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteValue(this.ImAddress, "ImAddress");
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00011D90 File Offset: 0x00010D90
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Key", base.Key);
			jsonObject.Add("ImAddress", this.ImAddress);
			return jsonObject;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011DCB File Offset: 0x00010DCB
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.Key = jsonProperty.ReadEnumValue<ImAddressKey>("Key");
			this.ImAddress = jsonProperty.ReadAsString("ImAddress");
		}

		// Token: 0x040001B2 RID: 434
		private string imAddress;
	}
}
