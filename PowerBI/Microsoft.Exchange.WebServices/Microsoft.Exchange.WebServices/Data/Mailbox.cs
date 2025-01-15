using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000075 RID: 117
	public class Mailbox : ComplexProperty, ISearchStringProvider
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x000124C2 File Offset: 0x000114C2
		public Mailbox()
		{
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000124CA File Offset: 0x000114CA
		public Mailbox(string smtpAddress)
			: this()
		{
			this.Address = smtpAddress;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000124D9 File Offset: 0x000114D9
		public Mailbox(string address, string routingType)
			: this(address)
		{
			this.RoutingType = routingType;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000124E9 File Offset: 0x000114E9
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.Address);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000124F9 File Offset: 0x000114F9
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00012501 File Offset: 0x00011501
		public string Address { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001250A File Offset: 0x0001150A
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00012512 File Offset: 0x00011512
		public string RoutingType { get; set; }

		// Token: 0x0600053A RID: 1338 RVA: 0x0001251B File Offset: 0x0001151B
		public static implicit operator Mailbox(string smtpAddress)
		{
			return new Mailbox(smtpAddress);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012524 File Offset: 0x00011524
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "EmailAddress")
				{
					this.Address = reader.ReadElementValue();
					return true;
				}
				if (localName == "RoutingType")
				{
					this.RoutingType = reader.ReadElementValue();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00012574 File Offset: 0x00011574
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "EmailAddress", this.Address);
			writer.WriteElementValue(XmlNamespace.Types, "RoutingType", this.RoutingType);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001259A File Offset: 0x0001159A
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			if (jsonProperty.ContainsKey("EmailAddress"))
			{
				this.Address = jsonProperty.ReadAsString("EmailAddress");
			}
			if (jsonProperty.ContainsKey("RoutingType"))
			{
				this.RoutingType = jsonProperty.ReadAsString("RoutingType");
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000125D8 File Offset: 0x000115D8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("EmailAddress", this.Address);
			jsonObject.Add("RoutingType", this.RoutingType);
			return jsonObject;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001260E File Offset: 0x0001160E
		string ISearchStringProvider.GetSearchString()
		{
			return this.Address;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00012616 File Offset: 0x00011616
		internal override void InternalValidate()
		{
			base.InternalValidate();
			EwsUtilities.ValidateNonBlankStringParamAllowNull(this.Address, "address");
			EwsUtilities.ValidateNonBlankStringParamAllowNull(this.RoutingType, "routingType");
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00012640 File Offset: 0x00011640
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			Mailbox mailbox = obj as Mailbox;
			return mailbox != null && ((this.Address == null && mailbox.Address == null) || (this.Address != null && this.Address.Equals(mailbox.Address))) && ((this.RoutingType == null && mailbox.RoutingType == null) || (this.RoutingType != null && this.RoutingType.Equals(mailbox.RoutingType)));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000126C0 File Offset: 0x000116C0
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(this.Address))
			{
				int num = this.Address.GetHashCode();
				if (!string.IsNullOrEmpty(this.RoutingType))
				{
					num ^= this.RoutingType.GetHashCode();
				}
				return num;
			}
			return base.GetHashCode();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00012709 File Offset: 0x00011709
		public override string ToString()
		{
			if (!this.IsValid)
			{
				return string.Empty;
			}
			if (!string.IsNullOrEmpty(this.RoutingType))
			{
				return this.RoutingType + ":" + this.Address;
			}
			return this.Address;
		}
	}
}
