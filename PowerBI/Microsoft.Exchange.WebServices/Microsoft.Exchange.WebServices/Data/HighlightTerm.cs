using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000068 RID: 104
	public sealed class HighlightTerm : ComplexProperty
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x00011BF8 File Offset: 0x00010BF8
		internal HighlightTerm()
		{
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00011C00 File Offset: 0x00010C00
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00011C08 File Offset: 0x00010C08
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00011C10 File Offset: 0x00010C10
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Scope")
				{
					this.scope = reader.ReadElementValue();
					return true;
				}
				if (localName == "Value")
				{
					this.value = reader.ReadElementValue();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00011C60 File Offset: 0x00010C60
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			if (jsonProperty.ContainsKey("Scope"))
			{
				this.scope = jsonProperty.ReadAsString("Scope");
			}
			if (jsonProperty.ContainsKey("Value"))
			{
				this.value = jsonProperty.ReadAsString("Value");
			}
		}

		// Token: 0x040001B0 RID: 432
		private string scope;

		// Token: 0x040001B1 RID: 433
		private string value;
	}
}
