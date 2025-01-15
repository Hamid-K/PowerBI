using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002AC RID: 684
	internal sealed class LegacyAvailabilityTimeZoneTime : ComplexProperty
	{
		// Token: 0x06001854 RID: 6228 RVA: 0x000428E1 File Offset: 0x000418E1
		internal LegacyAvailabilityTimeZoneTime()
		{
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000428EC File Offset: 0x000418EC
		internal LegacyAvailabilityTimeZoneTime(TimeZoneInfo.TransitionTime transitionTime, TimeSpan delta)
			: this()
		{
			this.delta = delta;
			if (transitionTime.IsFixedDateRule)
			{
				this.year = DateTime.Today.Year;
				this.month = transitionTime.Month;
				this.dayOrder = transitionTime.Day;
				this.timeOfDay = transitionTime.TimeOfDay.TimeOfDay;
				return;
			}
			this.year = 0;
			this.month = transitionTime.Month;
			this.dayOfTheWeek = EwsUtilities.SystemToEwsDayOfTheWeek(transitionTime.DayOfWeek);
			this.dayOrder = transitionTime.Week;
			this.timeOfDay = transitionTime.TimeOfDay.TimeOfDay;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0004299C File Offset: 0x0004199C
		internal TimeZoneInfo.TransitionTime ToTransitionTime()
		{
			if (this.year == 0)
			{
				DateTime minValue = DateTime.MinValue;
				int num = minValue.Year;
				DateTime minValue2 = DateTime.MinValue;
				int num2 = minValue2.Month;
				DateTime minValue3 = DateTime.MinValue;
				return TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(num, num2, minValue3.Day, this.timeOfDay.Hours, this.timeOfDay.Minutes, this.timeOfDay.Seconds), this.month, this.dayOrder, EwsUtilities.EwsToSystemDayOfWeek(this.dayOfTheWeek));
			}
			return TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(this.timeOfDay.Ticks), this.month, this.dayOrder);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00042A3C File Offset: 0x00041A3C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600173f-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
					dictionary.Add("Bias", 0);
					dictionary.Add("Time", 1);
					dictionary.Add("DayOrder", 2);
					dictionary.Add("DayOfWeek", 3);
					dictionary.Add("Month", 4);
					dictionary.Add("Year", 5);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600173f-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600173f-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.delta = TimeSpan.FromMinutes((double)reader.ReadElementValue<int>());
						return true;
					case 1:
						this.timeOfDay = TimeSpan.Parse(reader.ReadElementValue());
						return true;
					case 2:
						this.dayOrder = reader.ReadElementValue<int>();
						return true;
					case 3:
						this.dayOfTheWeek = reader.ReadElementValue<DayOfTheWeek>();
						return true;
					case 4:
						this.month = reader.ReadElementValue<int>();
						return true;
					case 5:
						this.year = reader.ReadElementValue<int>();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00042B48 File Offset: 0x00041B48
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6001740-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("Bias", 0);
						dictionary.Add("Time", 1);
						dictionary.Add("DayOrder", 2);
						dictionary.Add("DayOfWeek", 3);
						dictionary.Add("Month", 4);
						dictionary.Add("Year", 5);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6001740-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6001740-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.delta = TimeSpan.FromMinutes((double)jsonProperty.ReadAsInt(text));
							break;
						case 1:
							this.timeOfDay = TimeSpan.Parse(jsonProperty.ReadAsString(text));
							break;
						case 2:
							this.dayOrder = jsonProperty.ReadAsInt(text);
							break;
						case 3:
							this.dayOfTheWeek = jsonProperty.ReadEnumValue<DayOfTheWeek>(text);
							break;
						case 4:
							this.month = jsonProperty.ReadAsInt(text);
							break;
						case 5:
							this.year = jsonProperty.ReadAsInt(text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00042CA4 File Offset: 0x00041CA4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Bias", (int)this.delta.TotalMinutes);
			writer.WriteElementValue(XmlNamespace.Types, "Time", EwsUtilities.TimeSpanToXSTime(this.timeOfDay));
			writer.WriteElementValue(XmlNamespace.Types, "DayOrder", this.dayOrder);
			writer.WriteElementValue(XmlNamespace.Types, "Month", this.month);
			if (this.Year == 0)
			{
				writer.WriteElementValue(XmlNamespace.Types, "DayOfWeek", this.dayOfTheWeek);
			}
			if (this.Year != 0)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Year", this.Year);
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00042D54 File Offset: 0x00041D54
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Bias", (int)this.delta.TotalMinutes);
			jsonObject.Add("Time", EwsUtilities.TimeSpanToXSTime(this.timeOfDay));
			jsonObject.Add("DayOrder", this.dayOrder);
			jsonObject.Add("Month", this.month);
			if (this.Year == 0)
			{
				jsonObject.Add("DayOfWeek", this.dayOfTheWeek);
			}
			if (this.Year != 0)
			{
				jsonObject.Add("Year", this.Year);
			}
			return jsonObject;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00042DEE File Offset: 0x00041DEE
		internal bool HasTransitionTime
		{
			get
			{
				return this.month >= 1 && this.month <= 12;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00042E08 File Offset: 0x00041E08
		// (set) Token: 0x0600185D RID: 6237 RVA: 0x00042E10 File Offset: 0x00041E10
		internal TimeSpan Delta
		{
			get
			{
				return this.delta;
			}
			set
			{
				this.delta = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00042E19 File Offset: 0x00041E19
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x00042E21 File Offset: 0x00041E21
		internal TimeSpan TimeOfDay
		{
			get
			{
				return this.timeOfDay;
			}
			set
			{
				this.timeOfDay = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00042E2A File Offset: 0x00041E2A
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x00042E32 File Offset: 0x00041E32
		internal int DayOrder
		{
			get
			{
				return this.dayOrder;
			}
			set
			{
				this.dayOrder = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00042E3B File Offset: 0x00041E3B
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x00042E43 File Offset: 0x00041E43
		internal int Month
		{
			get
			{
				return this.month;
			}
			set
			{
				this.month = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00042E4C File Offset: 0x00041E4C
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x00042E54 File Offset: 0x00041E54
		internal DayOfTheWeek DayOfTheWeek
		{
			get
			{
				return this.dayOfTheWeek;
			}
			set
			{
				this.dayOfTheWeek = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x00042E5D File Offset: 0x00041E5D
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x00042E65 File Offset: 0x00041E65
		internal int Year
		{
			get
			{
				return this.year;
			}
			set
			{
				this.year = value;
			}
		}

		// Token: 0x040013B6 RID: 5046
		private TimeSpan delta;

		// Token: 0x040013B7 RID: 5047
		private int year;

		// Token: 0x040013B8 RID: 5048
		private int month;

		// Token: 0x040013B9 RID: 5049
		private int dayOrder;

		// Token: 0x040013BA RID: 5050
		private DayOfTheWeek dayOfTheWeek;

		// Token: 0x040013BB RID: 5051
		private TimeSpan timeOfDay;
	}
}
