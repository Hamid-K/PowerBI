using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200007F RID: 127
	public sealed class PersonaPostalAddress : ComplexProperty
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x00013840 File Offset: 0x00012840
		internal PersonaPostalAddress()
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00013848 File Offset: 0x00012848
		public PersonaPostalAddress(string street, string city, string state, string country, string postalCode, string postOfficeBox, LocationSource locationSource, string locationUri, string formattedAddress, double latitude, double longitude, double accuracy, double altitude, double altitudeAccuracy)
			: this()
		{
			this.street = street;
			this.city = city;
			this.state = state;
			this.country = country;
			this.postalCode = postalCode;
			this.postOfficeBox = postOfficeBox;
			this.latitude = new double?(latitude);
			this.longitude = new double?(longitude);
			this.source = locationSource;
			this.uri = locationUri;
			this.formattedAddress = formattedAddress;
			this.accuracy = new double?(accuracy);
			this.altitude = new double?(altitude);
			this.altitudeAccuracy = new double?(altitudeAccuracy);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000138E4 File Offset: 0x000128E4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000552-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(15);
					dictionary.Add("Street", 0);
					dictionary.Add("City", 1);
					dictionary.Add("State", 2);
					dictionary.Add("Country", 3);
					dictionary.Add("PostalCode", 4);
					dictionary.Add("PostOfficeBox", 5);
					dictionary.Add("Type", 6);
					dictionary.Add("Latitude", 7);
					dictionary.Add("Longitude", 8);
					dictionary.Add("Accuracy", 9);
					dictionary.Add("Altitude", 10);
					dictionary.Add("AltitudeAccuracy", 11);
					dictionary.Add("FormattedAddress", 12);
					dictionary.Add("LocationUri", 13);
					dictionary.Add("LocationSource", 14);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000552-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000552-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.street = reader.ReadValue<string>();
						return true;
					case 1:
						this.city = reader.ReadValue<string>();
						return true;
					case 2:
						this.state = reader.ReadValue<string>();
						return true;
					case 3:
						this.country = reader.ReadValue<string>();
						return true;
					case 4:
						this.postalCode = reader.ReadValue<string>();
						return true;
					case 5:
						this.postOfficeBox = reader.ReadValue<string>();
						return true;
					case 6:
						this.type = reader.ReadValue<string>();
						return true;
					case 7:
						this.latitude = new double?(reader.ReadValue<double>());
						return true;
					case 8:
						this.longitude = new double?(reader.ReadValue<double>());
						return true;
					case 9:
						this.accuracy = new double?(reader.ReadValue<double>());
						return true;
					case 10:
						this.altitude = new double?(reader.ReadValue<double>());
						return true;
					case 11:
						this.altitudeAccuracy = new double?(reader.ReadValue<double>());
						return true;
					case 12:
						this.formattedAddress = reader.ReadValue<string>();
						return true;
					case 13:
						this.uri = reader.ReadValue<string>();
						return true;
					case 14:
						this.source = reader.ReadValue<LocationSource>();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00013B19 File Offset: 0x00012B19
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					this.TryReadElementFromXml(reader);
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Types, "PostalAddress"));
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00013B40 File Offset: 0x00012B40
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000554-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(14);
						dictionary.Add("Street", 0);
						dictionary.Add("City", 1);
						dictionary.Add("Country", 2);
						dictionary.Add("PostalCode", 3);
						dictionary.Add("PostOfficeBox", 4);
						dictionary.Add("Type", 5);
						dictionary.Add("Latitude", 6);
						dictionary.Add("Longitude", 7);
						dictionary.Add("Accuracy", 8);
						dictionary.Add("Altitude", 9);
						dictionary.Add("AltitudeAccuracy", 10);
						dictionary.Add("FormattedAddress", 11);
						dictionary.Add("LocationUri", 12);
						dictionary.Add("LocationSource", 13);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000554-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000554-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.street = jsonProperty.ReadAsString(text);
							break;
						case 1:
							this.city = jsonProperty.ReadAsString(text);
							break;
						case 2:
							this.country = jsonProperty.ReadAsString(text);
							break;
						case 3:
							this.postalCode = jsonProperty.ReadAsString(text);
							break;
						case 4:
							this.postOfficeBox = jsonProperty.ReadAsString(text);
							break;
						case 5:
							this.type = jsonProperty.ReadAsString(text);
							break;
						case 6:
							this.latitude = new double?(jsonProperty.ReadAsDouble(text));
							break;
						case 7:
							this.longitude = new double?(jsonProperty.ReadAsDouble(text));
							break;
						case 8:
							this.accuracy = new double?(jsonProperty.ReadAsDouble(text));
							break;
						case 9:
							this.altitude = new double?(jsonProperty.ReadAsDouble(text));
							break;
						case 10:
							this.altitudeAccuracy = new double?(jsonProperty.ReadAsDouble(text));
							break;
						case 11:
							this.formattedAddress = jsonProperty.ReadAsString(text);
							break;
						case 12:
							this.uri = jsonProperty.ReadAsString(text);
							break;
						case 13:
							this.source = jsonProperty.ReadEnumValue<LocationSource>(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00013DC0 File Offset: 0x00012DC0
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00013DC8 File Offset: 0x00012DC8
		public string Street
		{
			get
			{
				return this.street;
			}
			set
			{
				this.SetFieldValue<string>(ref this.street, value);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00013DD7 File Offset: 0x00012DD7
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00013DDF File Offset: 0x00012DDF
		public string City
		{
			get
			{
				return this.city;
			}
			set
			{
				this.SetFieldValue<string>(ref this.city, value);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00013DEE File Offset: 0x00012DEE
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00013DF6 File Offset: 0x00012DF6
		public string State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.SetFieldValue<string>(ref this.state, value);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00013E05 File Offset: 0x00012E05
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00013E0D File Offset: 0x00012E0D
		public string Country
		{
			get
			{
				return this.country;
			}
			set
			{
				this.SetFieldValue<string>(ref this.country, value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00013E1C File Offset: 0x00012E1C
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00013E24 File Offset: 0x00012E24
		public string PostalCode
		{
			get
			{
				return this.postalCode;
			}
			set
			{
				this.SetFieldValue<string>(ref this.postalCode, value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00013E33 File Offset: 0x00012E33
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00013E3B File Offset: 0x00012E3B
		public string PostOfficeBox
		{
			get
			{
				return this.postOfficeBox;
			}
			set
			{
				this.SetFieldValue<string>(ref this.postOfficeBox, value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00013E4A File Offset: 0x00012E4A
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00013E52 File Offset: 0x00012E52
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.SetFieldValue<string>(ref this.type, value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00013E61 File Offset: 0x00012E61
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00013E69 File Offset: 0x00012E69
		public LocationSource Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.SetFieldValue<LocationSource>(ref this.source, value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00013E78 File Offset: 0x00012E78
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00013E80 File Offset: 0x00012E80
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.SetFieldValue<string>(ref this.uri, value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00013E8F File Offset: 0x00012E8F
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00013E97 File Offset: 0x00012E97
		public double? Latitude
		{
			get
			{
				return this.latitude;
			}
			set
			{
				this.SetFieldValue<double?>(ref this.latitude, value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00013EA6 File Offset: 0x00012EA6
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00013EAE File Offset: 0x00012EAE
		public double? Longitude
		{
			get
			{
				return this.longitude;
			}
			set
			{
				this.SetFieldValue<double?>(ref this.longitude, value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00013EBD File Offset: 0x00012EBD
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00013EC5 File Offset: 0x00012EC5
		public double? Accuracy
		{
			get
			{
				return this.accuracy;
			}
			set
			{
				this.SetFieldValue<double?>(ref this.accuracy, value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00013ED4 File Offset: 0x00012ED4
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x00013EDC File Offset: 0x00012EDC
		public double? Altitude
		{
			get
			{
				return this.altitude;
			}
			set
			{
				this.SetFieldValue<double?>(ref this.altitude, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00013EEB File Offset: 0x00012EEB
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x00013EF3 File Offset: 0x00012EF3
		public double? AltitudeAccuracy
		{
			get
			{
				return this.altitudeAccuracy;
			}
			set
			{
				this.SetFieldValue<double?>(ref this.altitudeAccuracy, value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00013F02 File Offset: 0x00012F02
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00013F0A File Offset: 0x00012F0A
		public string FormattedAddress
		{
			get
			{
				return this.formattedAddress;
			}
			set
			{
				this.SetFieldValue<string>(ref this.formattedAddress, value);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00013F1C File Offset: 0x00012F1C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Street", this.street);
			writer.WriteElementValue(XmlNamespace.Types, "City", this.city);
			writer.WriteElementValue(XmlNamespace.Types, "State", this.state);
			writer.WriteElementValue(XmlNamespace.Types, "Country", this.country);
			writer.WriteElementValue(XmlNamespace.Types, "PostalCode", this.postalCode);
			writer.WriteElementValue(XmlNamespace.Types, "PostOfficeBox", this.postOfficeBox);
			writer.WriteElementValue(XmlNamespace.Types, "Type", this.type);
			writer.WriteElementValue(XmlNamespace.Types, "Latitude", this.latitude);
			writer.WriteElementValue(XmlNamespace.Types, "Longitude", this.longitude);
			writer.WriteElementValue(XmlNamespace.Types, "Accuracy", this.accuracy);
			writer.WriteElementValue(XmlNamespace.Types, "Altitude", this.altitude);
			writer.WriteElementValue(XmlNamespace.Types, "AltitudeAccuracy", this.altitudeAccuracy);
			writer.WriteElementValue(XmlNamespace.Types, "FormattedAddress", this.formattedAddress);
			writer.WriteElementValue(XmlNamespace.Types, "LocationUri", this.uri);
			writer.WriteElementValue(XmlNamespace.Types, "LocationSource", this.source);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00014058 File Offset: 0x00013058
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Street", this.street);
			jsonObject.Add("City", this.city);
			jsonObject.Add("Country", this.country);
			jsonObject.Add("PostalCode", this.postalCode);
			jsonObject.Add("PostOfficeBox", this.postOfficeBox);
			jsonObject.Add("Type", this.type);
			jsonObject.Add("Latitude", this.latitude);
			jsonObject.Add("Longitude", this.longitude);
			jsonObject.Add("Accuracy", this.accuracy);
			jsonObject.Add("Altitude", this.altitude);
			jsonObject.Add("AltitudeAccuracy", this.altitudeAccuracy);
			jsonObject.Add("FormattedAddress", this.formattedAddress);
			jsonObject.Add("LocationUri", this.uri);
			jsonObject.Add("LocationSource", this.source);
			return jsonObject;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00014178 File Offset: 0x00013178
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "PostalAddress");
			this.WriteElementsToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x040001E9 RID: 489
		private string street;

		// Token: 0x040001EA RID: 490
		private string city;

		// Token: 0x040001EB RID: 491
		private string state;

		// Token: 0x040001EC RID: 492
		private string country;

		// Token: 0x040001ED RID: 493
		private string postalCode;

		// Token: 0x040001EE RID: 494
		private string postOfficeBox;

		// Token: 0x040001EF RID: 495
		private string type;

		// Token: 0x040001F0 RID: 496
		private double? latitude;

		// Token: 0x040001F1 RID: 497
		private double? longitude;

		// Token: 0x040001F2 RID: 498
		private double? accuracy;

		// Token: 0x040001F3 RID: 499
		private double? altitude;

		// Token: 0x040001F4 RID: 500
		private double? altitudeAccuracy;

		// Token: 0x040001F5 RID: 501
		private string formattedAddress;

		// Token: 0x040001F6 RID: 502
		private string uri;

		// Token: 0x040001F7 RID: 503
		private LocationSource source;
	}
}
