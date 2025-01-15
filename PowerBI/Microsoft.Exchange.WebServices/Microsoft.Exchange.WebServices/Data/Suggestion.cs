using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A9 RID: 169
	public sealed class Suggestion : ComplexProperty
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00019C47 File Offset: 0x00018C47
		internal Suggestion()
		{
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00019C5C File Offset: 0x00018C5C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Date")
				{
					DateTime dateTime = DateTime.Parse(reader.ReadElementValue(), CultureInfo.InvariantCulture);
					if (dateTime.Kind != null)
					{
						this.date = new DateTime(dateTime.Ticks, 0);
					}
					else
					{
						this.date = dateTime;
					}
					return true;
				}
				if (localName == "DayQuality")
				{
					this.quality = reader.ReadElementValue<SuggestionQuality>();
					return true;
				}
				if (localName == "SuggestionArray")
				{
					if (!reader.IsEmptyElement)
					{
						do
						{
							reader.Read();
							if (reader.IsStartElement(XmlNamespace.Types, "Suggestion"))
							{
								TimeSuggestion timeSuggestion = new TimeSuggestion();
								timeSuggestion.LoadFromXml(reader, reader.LocalName);
								this.timeSuggestions.Add(timeSuggestion);
							}
						}
						while (!reader.IsEndElement(XmlNamespace.Types, "SuggestionArray"));
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00019D34 File Offset: 0x00018D34
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			this.date = DateTime.Parse(jsonProperty.ReadAsString("Date"));
			this.quality = jsonProperty.ReadEnumValue<SuggestionQuality>("DayQuality");
			foreach (object obj in jsonProperty.ReadAsArray("SuggestionArray"))
			{
				TimeSuggestion timeSuggestion = new TimeSuggestion();
				timeSuggestion.LoadFromJson(obj as JsonObject, service);
				this.timeSuggestions.Add(timeSuggestion);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00019DA5 File Offset: 0x00018DA5
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00019DAD File Offset: 0x00018DAD
		public SuggestionQuality Quality
		{
			get
			{
				return this.quality;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00019DB5 File Offset: 0x00018DB5
		public Collection<TimeSuggestion> TimeSuggestions
		{
			get
			{
				return this.timeSuggestions;
			}
		}

		// Token: 0x04000282 RID: 642
		private DateTime date;

		// Token: 0x04000283 RID: 643
		private SuggestionQuality quality;

		// Token: 0x04000284 RID: 644
		private Collection<TimeSuggestion> timeSuggestions = new Collection<TimeSuggestion>();
	}
}
