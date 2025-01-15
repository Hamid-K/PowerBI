using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000079 RID: 121
	internal sealed class MeetingTimeZone : ComplexProperty
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x00012D16 File Offset: 0x00011D16
		internal MeetingTimeZone(TimeZoneInfo timeZone)
		{
			this.Name = timeZone.Id;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00012D2A File Offset: 0x00011D2A
		public MeetingTimeZone()
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012D32 File Offset: 0x00011D32
		public MeetingTimeZone(string name)
			: this()
		{
			this.name = name;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00012D44 File Offset: 0x00011D44
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "BaseOffset")
				{
					this.baseOffset = new TimeSpan?(EwsUtilities.XSDurationToTimeSpan(reader.ReadElementValue()));
					return true;
				}
				if (localName == "Standard")
				{
					this.standard = new TimeChange();
					this.standard.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "Daylight")
				{
					this.daylight = new TimeChange();
					this.daylight.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012DDB File Offset: 0x00011DDB
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.name = reader.ReadAttributeValue("TimeZoneName");
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012DF0 File Offset: 0x00011DF0
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "BaseOffset"))
					{
						if (!(text2 == "Standard"))
						{
							if (!(text2 == "Daylight"))
							{
								if (text2 == "TimeZoneName")
								{
									this.name = jsonProperty.ReadAsString(text);
								}
							}
							else
							{
								this.daylight = new TimeChange();
								this.daylight.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							}
						}
						else
						{
							this.standard = new TimeChange();
							this.standard.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
						}
					}
					else
					{
						this.baseOffset = new TimeSpan?(EwsUtilities.XSDurationToTimeSpan(jsonProperty.ReadAsString(text)));
					}
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012EE8 File Offset: 0x00011EE8
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("TimeZoneName", this.Name);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012EFC File Offset: 0x00011EFC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.BaseOffset != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "BaseOffset", EwsUtilities.TimeSpanToXSDuration(this.BaseOffset.Value));
			}
			if (this.Standard != null)
			{
				this.Standard.WriteToXml(writer, "Standard");
			}
			if (this.Daylight != null)
			{
				this.Daylight.WriteToXml(writer, "Daylight");
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00012F6C File Offset: 0x00011F6C
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.BaseOffset != null)
			{
				jsonObject.Add("BaseOffset", EwsUtilities.TimeSpanToXSDuration(this.BaseOffset.Value));
			}
			if (this.Standard != null)
			{
				jsonObject.Add("Standard", this.Standard.InternalToJson(service));
			}
			if (this.Daylight != null)
			{
				jsonObject.Add("Daylight", this.Daylight.InternalToJson(service));
			}
			jsonObject.Add("TimeZoneName", this.Name);
			return jsonObject;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013000 File Offset: 0x00012000
		internal TimeZoneInfo ToTimeZoneInfo()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return null;
			}
			TimeZoneInfo timeZoneInfo = null;
			try
			{
				timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(this.Name);
			}
			catch (TimeZoneNotFoundException)
			{
			}
			return timeZoneInfo;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00013040 File Offset: 0x00012040
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00013048 File Offset: 0x00012048
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.SetFieldValue<string>(ref this.name, value);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00013057 File Offset: 0x00012057
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001305F File Offset: 0x0001205F
		public TimeSpan? BaseOffset
		{
			get
			{
				return this.baseOffset;
			}
			set
			{
				this.SetFieldValue<TimeSpan?>(ref this.baseOffset, value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001306E File Offset: 0x0001206E
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00013076 File Offset: 0x00012076
		public TimeChange Standard
		{
			get
			{
				return this.standard;
			}
			set
			{
				this.SetFieldValue<TimeChange>(ref this.standard, value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00013085 File Offset: 0x00012085
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001308D File Offset: 0x0001208D
		public TimeChange Daylight
		{
			get
			{
				return this.daylight;
			}
			set
			{
				this.SetFieldValue<TimeChange>(ref this.daylight, value);
			}
		}

		// Token: 0x040001DA RID: 474
		private string name;

		// Token: 0x040001DB RID: 475
		private TimeSpan? baseOffset;

		// Token: 0x040001DC RID: 476
		private TimeChange standard;

		// Token: 0x040001DD RID: 477
		private TimeChange daylight;
	}
}
