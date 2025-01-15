using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002AB RID: 683
	internal sealed class LegacyAvailabilityTimeZone : ComplexProperty
	{
		// Token: 0x0600184D RID: 6221 RVA: 0x000424B0 File Offset: 0x000414B0
		internal LegacyAvailabilityTimeZone()
		{
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000424B8 File Offset: 0x000414B8
		internal LegacyAvailabilityTimeZone(TimeZoneInfo timeZoneInfo)
			: this()
		{
			this.bias = -timeZoneInfo.BaseUtcOffset;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = timeZoneInfo.GetAdjustmentRules();
			if (adjustmentRules.Length == 0)
			{
				this.daylightTime = new LegacyAvailabilityTimeZoneTime();
				this.daylightTime.Delta = TimeSpan.Zero;
				this.daylightTime.DayOrder = 1;
				this.daylightTime.DayOfTheWeek = DayOfTheWeek.Sunday;
				this.daylightTime.Month = 10;
				this.daylightTime.TimeOfDay = TimeSpan.FromHours(2.0);
				this.daylightTime.Year = 0;
				this.standardTime = new LegacyAvailabilityTimeZoneTime();
				this.standardTime.Delta = TimeSpan.Zero;
				this.standardTime.DayOrder = 1;
				this.standardTime.DayOfTheWeek = DayOfTheWeek.Sunday;
				this.standardTime.Month = 3;
				this.standardTime.TimeOfDay = TimeSpan.FromHours(2.0);
				this.daylightTime.Year = 0;
				return;
			}
			TimeZoneInfo.AdjustmentRule adjustmentRule = adjustmentRules[adjustmentRules.Length - 1];
			this.standardTime = new LegacyAvailabilityTimeZoneTime(adjustmentRule.DaylightTransitionEnd, TimeSpan.Zero);
			this.daylightTime = new LegacyAvailabilityTimeZoneTime(adjustmentRule.DaylightTransitionStart, -adjustmentRule.DaylightDelta);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000425F0 File Offset: 0x000415F0
		internal TimeZoneInfo ToTimeZoneInfo()
		{
			if (this.daylightTime.HasTransitionTime && this.standardTime.HasTransitionTime)
			{
				DateTime minValue = DateTime.MinValue;
				DateTime date = minValue.Date;
				DateTime maxValue = DateTime.MaxValue;
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(date, maxValue.Date, -this.daylightTime.Delta, this.daylightTime.ToTransitionTime(), this.standardTime.ToTransitionTime());
				return TimeZoneInfo.CreateCustomTimeZone(Guid.NewGuid().ToString(), -this.bias, "Custom time zone", "Standard time", "Daylight time", new TimeZoneInfo.AdjustmentRule[] { adjustmentRule });
			}
			return TimeZoneInfo.CreateCustomTimeZone(Guid.NewGuid().ToString(), -this.bias, "Custom time zone", "Standard time");
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000426D4 File Offset: 0x000416D4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Bias")
				{
					this.bias = TimeSpan.FromMinutes((double)reader.ReadElementValue<int>());
					return true;
				}
				if (localName == "StandardTime")
				{
					this.standardTime = new LegacyAvailabilityTimeZoneTime();
					this.standardTime.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "DaylightTime")
				{
					this.daylightTime = new LegacyAvailabilityTimeZoneTime();
					this.daylightTime.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00042768 File Offset: 0x00041768
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "Bias"))
					{
						if (!(text2 == "StandardTime"))
						{
							if (text2 == "DaylightTime")
							{
								this.daylightTime = new LegacyAvailabilityTimeZoneTime();
								this.daylightTime.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							}
						}
						else
						{
							this.standardTime = new LegacyAvailabilityTimeZoneTime();
							this.standardTime.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
						}
					}
					else
					{
						this.bias = TimeSpan.FromMinutes((double)jsonProperty.ReadAsInt(text));
					}
				}
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0004283C File Offset: 0x0004183C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Bias", (int)this.bias.TotalMinutes);
			this.standardTime.WriteToXml(writer, "StandardTime");
			this.daylightTime.WriteToXml(writer, "DaylightTime");
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00042888 File Offset: 0x00041888
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Bias", (int)this.bias.TotalMinutes);
			jsonObject.Add("StandardTime", this.standardTime.InternalToJson(service));
			jsonObject.Add("DaylightTime", this.daylightTime.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x040013B3 RID: 5043
		private TimeSpan bias;

		// Token: 0x040013B4 RID: 5044
		private LegacyAvailabilityTimeZoneTime standardTime;

		// Token: 0x040013B5 RID: 5045
		private LegacyAvailabilityTimeZoneTime daylightTime;
	}
}
