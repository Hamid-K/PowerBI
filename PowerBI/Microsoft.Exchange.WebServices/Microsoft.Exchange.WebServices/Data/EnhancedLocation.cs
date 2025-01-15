using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200005A RID: 90
	public sealed class EnhancedLocation : ComplexProperty
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000E434 File Offset: 0x0000D434
		internal EnhancedLocation()
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E43C File Offset: 0x0000D43C
		public EnhancedLocation(string displayName)
			: this(displayName, string.Empty, new PersonaPostalAddress())
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000E44F File Offset: 0x0000D44F
		public EnhancedLocation(string displayName, string annotation)
			: this(displayName, annotation, new PersonaPostalAddress())
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E45E File Offset: 0x0000D45E
		public EnhancedLocation(string displayName, string annotation, PersonaPostalAddress personaPostalAddress)
			: this()
		{
			this.displayName = displayName;
			this.annotation = annotation;
			this.personaPostalAddress = personaPostalAddress;
			this.personaPostalAddress.OnChange += this.PersonaPostalAddress_OnChange;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E494 File Offset: 0x0000D494
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "DisplayName")
				{
					this.displayName = reader.ReadValue<string>();
					return true;
				}
				if (localName == "Annotation")
				{
					this.annotation = reader.ReadValue<string>();
					return true;
				}
				if (localName == "PostalAddress")
				{
					this.personaPostalAddress = new PersonaPostalAddress();
					this.personaPostalAddress.LoadFromXml(reader);
					this.personaPostalAddress.OnChange += this.PersonaPostalAddress_OnChange;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000E524 File Offset: 0x0000D524
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "DisplayName"))
					{
						if (!(text2 == "Annotation"))
						{
							if (text2 == "PostalAddress")
							{
								this.personaPostalAddress = new PersonaPostalAddress();
								this.personaPostalAddress.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
								this.personaPostalAddress.OnChange += this.PersonaPostalAddress_OnChange;
							}
						}
						else
						{
							this.annotation = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.displayName = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000E5F8 File Offset: 0x0000D5F8
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000E600 File Offset: 0x0000D600
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.SetFieldValue<string>(ref this.displayName, value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000E60F File Offset: 0x0000D60F
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000E617 File Offset: 0x0000D617
		public string Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.SetFieldValue<string>(ref this.annotation, value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000E626 File Offset: 0x0000D626
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000E630 File Offset: 0x0000D630
		public PersonaPostalAddress PersonaPostalAddress
		{
			get
			{
				return this.personaPostalAddress;
			}
			set
			{
				if (!this.personaPostalAddress.Equals(value))
				{
					if (this.personaPostalAddress != null)
					{
						this.personaPostalAddress.OnChange -= this.PersonaPostalAddress_OnChange;
					}
					this.SetFieldValue<PersonaPostalAddress>(ref this.personaPostalAddress, value);
					this.personaPostalAddress.OnChange += this.PersonaPostalAddress_OnChange;
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000E68E File Offset: 0x0000D68E
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "DisplayName", this.displayName);
			writer.WriteElementValue(XmlNamespace.Types, "Annotation", this.annotation);
			this.personaPostalAddress.WriteToXml(writer);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000E6C0 File Offset: 0x0000D6C0
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("DisplayName", this.displayName);
			jsonObject.Add("Annotation", this.annotation);
			if (this.personaPostalAddress != null)
			{
				jsonObject.Add("PostalAddress", this.personaPostalAddress.InternalToJson(service));
			}
			return jsonObject;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E715 File Offset: 0x0000D715
		internal override void InternalValidate()
		{
			base.InternalValidate();
			EwsUtilities.ValidateParam(this.displayName, "DisplayName");
			EwsUtilities.ValidateParamAllowNull(this.annotation, "Annotation");
			EwsUtilities.ValidateParamAllowNull(this.personaPostalAddress, "PersonaPostalAddress");
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000E74D File Offset: 0x0000D74D
		private void PersonaPostalAddress_OnChange(ComplexProperty complexProperty)
		{
			this.Changed();
		}

		// Token: 0x04000184 RID: 388
		private string displayName;

		// Token: 0x04000185 RID: 389
		private string annotation;

		// Token: 0x04000186 RID: 390
		private PersonaPostalAddress personaPostalAddress;
	}
}
