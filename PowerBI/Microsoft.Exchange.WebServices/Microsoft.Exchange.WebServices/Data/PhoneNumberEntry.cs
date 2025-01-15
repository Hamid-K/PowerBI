using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000083 RID: 131
	[EditorBrowsable(1)]
	public sealed class PhoneNumberEntry : DictionaryEntryProperty<PhoneNumberKey>
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x00014319 File Offset: 0x00013319
		internal PhoneNumberEntry()
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014321 File Offset: 0x00013321
		internal PhoneNumberEntry(PhoneNumberKey key, string phoneNumber)
			: base(key)
		{
			this.phoneNumber = phoneNumber;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00014331 File Offset: 0x00013331
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.phoneNumber = reader.ReadValue();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001433F File Offset: 0x0001333F
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteValue(this.PhoneNumber, "PhoneNumber");
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00014354 File Offset: 0x00013354
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Key", base.Key);
			jsonObject.Add("PhoneNumber", this.PhoneNumber);
			return jsonObject;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001438F File Offset: 0x0001338F
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.Key = jsonProperty.ReadEnumValue<PhoneNumberKey>("Key");
			this.PhoneNumber = jsonProperty.ReadAsString("PhoneNumber");
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000143B3 File Offset: 0x000133B3
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x000143BB File Offset: 0x000133BB
		public string PhoneNumber
		{
			get
			{
				return this.phoneNumber;
			}
			set
			{
				this.SetFieldValue<string>(ref this.phoneNumber, value);
			}
		}

		// Token: 0x040001FB RID: 507
		private string phoneNumber;
	}
}
