using System;
using System.ComponentModel;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000AE RID: 174
	public abstract class Recurrence : ComplexProperty
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x0001A809 File Offset: 0x00019809
		internal Recurrence()
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001A811 File Offset: 0x00019811
		internal Recurrence(DateTime startDate)
			: this()
		{
			this.startDate = new DateTime?(startDate);
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007C6 RID: 1990
		internal abstract string XmlElementName { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001A825 File Offset: 0x00019825
		internal virtual bool IsRegenerationPattern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001A828 File Offset: 0x00019828
		internal virtual void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001A82C File Offset: 0x0001982C
		internal sealed override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, this.XmlElementName);
			this.InternalWritePropertiesToXml(writer);
			writer.WriteEndElement();
			RecurrenceRange recurrenceRange;
			if (!this.HasEnd)
			{
				recurrenceRange = new NoEndRecurrenceRange(this.StartDate);
			}
			else if (this.NumberOfOccurrences != null)
			{
				recurrenceRange = new NumberedRecurrenceRange(this.StartDate, this.NumberOfOccurrences);
			}
			else
			{
				recurrenceRange = new EndDateRecurrenceRange(this.StartDate, this.EndDate.Value);
			}
			recurrenceRange.WriteToXml(writer, recurrenceRange.XmlElementName);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001A8B4 File Offset: 0x000198B4
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("RecurrencePattern", this.PatternToJson(service));
			jsonObject.Add("RecurrenceRange", this.RangeToJson(service));
			return jsonObject;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001A8EC File Offset: 0x000198EC
		private object RangeToJson(ExchangeService service)
		{
			RecurrenceRange recurrenceRange;
			if (!this.HasEnd)
			{
				recurrenceRange = new NoEndRecurrenceRange(this.StartDate);
			}
			else if (this.NumberOfOccurrences != null)
			{
				recurrenceRange = new NumberedRecurrenceRange(this.StartDate, this.NumberOfOccurrences);
			}
			else
			{
				recurrenceRange = new EndDateRecurrenceRange(this.StartDate, this.EndDate.Value);
			}
			return recurrenceRange.InternalToJson(service);
		}

		// Token: 0x060007CC RID: 1996
		internal abstract JsonObject PatternToJson(ExchangeService service);

		// Token: 0x060007CD RID: 1997 RVA: 0x0001A954 File Offset: 0x00019954
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001A95E File Offset: 0x0001995E
		internal T GetFieldValueOrThrowIfNull<T>(T? value, string name) where T : struct
		{
			if (value != null)
			{
				return value.Value;
			}
			throw new ServiceValidationException(string.Format(Strings.PropertyValueMustBeSpecifiedForRecurrencePattern, name));
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0001A986 File Offset: 0x00019986
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0001A999 File Offset: 0x00019999
		public DateTime StartDate
		{
			get
			{
				return this.GetFieldValueOrThrowIfNull<DateTime>(this.startDate, "StartDate");
			}
			set
			{
				this.startDate = new DateTime?(value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001A9A7 File Offset: 0x000199A7
		public bool HasEnd
		{
			get
			{
				return this.numberOfOccurrences != null || this.endDate != null;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001A9C3 File Offset: 0x000199C3
		public void NeverEnds()
		{
			this.numberOfOccurrences = default(int?);
			this.endDate = default(DateTime?);
			this.Changed();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001A9E3 File Offset: 0x000199E3
		internal override void InternalValidate()
		{
			base.InternalValidate();
			if (this.startDate == null)
			{
				throw new ServiceValidationException(Strings.RecurrencePatternMustHaveStartDate);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001AA08 File Offset: 0x00019A08
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001AA10 File Offset: 0x00019A10
		public int? NumberOfOccurrences
		{
			get
			{
				return this.numberOfOccurrences;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Strings.NumberOfOccurrencesMustBeGreaterThanZero);
				}
				this.SetFieldValue<int?>(ref this.numberOfOccurrences, value);
				this.endDate = default(DateTime?);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001AA5E File Offset: 0x00019A5E
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001AA66 File Offset: 0x00019A66
		public DateTime? EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.SetFieldValue<DateTime?>(ref this.endDate, value);
				this.numberOfOccurrences = default(int?);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001AA84 File Offset: 0x00019A84
		public bool IsSame(Recurrence otherRecurrence)
		{
			if (otherRecurrence == null)
			{
				return false;
			}
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				((JsonObject)this.InternalToJson(null)).SerializeToJson(memoryStream);
				memoryStream.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			string text2;
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				((JsonObject)otherRecurrence.InternalToJson(null)).SerializeToJson(memoryStream2);
				memoryStream2.Position = 0L;
				using (StreamReader streamReader2 = new StreamReader(memoryStream2))
				{
					text2 = streamReader2.ReadToEnd();
				}
			}
			return string.Equals(text, text2, 4);
		}

		// Token: 0x04000291 RID: 657
		private DateTime? startDate;

		// Token: 0x04000292 RID: 658
		private int? numberOfOccurrences;

		// Token: 0x04000293 RID: 659
		private DateTime? endDate;

		// Token: 0x020000AF RID: 175
		[EditorBrowsable(1)]
		public abstract class IntervalPattern : Recurrence
		{
			// Token: 0x060007D9 RID: 2009 RVA: 0x0001AB68 File Offset: 0x00019B68
			internal IntervalPattern()
			{
			}

			// Token: 0x060007DA RID: 2010 RVA: 0x0001AB77 File Offset: 0x00019B77
			internal IntervalPattern(DateTime startDate, int interval)
				: base(startDate)
			{
				if (interval < 1)
				{
					throw new ArgumentOutOfRangeException("interval", Strings.IntervalMustBeGreaterOrEqualToOne);
				}
				this.Interval = interval;
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x0001ABA7 File Offset: 0x00019BA7
			internal override void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
			{
				base.InternalWritePropertiesToXml(writer);
				writer.WriteElementValue(XmlNamespace.Types, "Interval", this.Interval);
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x0001ABC8 File Offset: 0x00019BC8
			internal override JsonObject PatternToJson(ExchangeService service)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter(this.XmlElementName);
				jsonObject.Add("Interval", this.Interval);
				return jsonObject;
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x0001ABFC File Offset: 0x00019BFC
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				if (base.TryReadElementFromXml(reader))
				{
					return true;
				}
				string localName;
				if ((localName = reader.LocalName) != null && localName == "Interval")
				{
					this.interval = reader.ReadElementValue<int>();
					return true;
				}
				return false;
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x0001AC3C File Offset: 0x00019C3C
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				foreach (string text in jsonProperty.Keys)
				{
					string text2;
					if ((text2 = text) != null && text2 == "Interval")
					{
						this.interval = jsonProperty.ReadAsInt(text);
					}
				}
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x060007DF RID: 2015 RVA: 0x0001ACB0 File Offset: 0x00019CB0
			// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0001ACB8 File Offset: 0x00019CB8
			public int Interval
			{
				get
				{
					return this.interval;
				}
				set
				{
					if (value < 1)
					{
						throw new ArgumentOutOfRangeException("value", Strings.IntervalMustBeGreaterOrEqualToOne);
					}
					this.SetFieldValue<int>(ref this.interval, value);
				}
			}

			// Token: 0x04000294 RID: 660
			private int interval = 1;
		}

		// Token: 0x020000B0 RID: 176
		public sealed class DailyPattern : Recurrence.IntervalPattern
		{
			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001ACE0 File Offset: 0x00019CE0
			internal override string XmlElementName
			{
				get
				{
					return "DailyRecurrence";
				}
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x0001ACE7 File Offset: 0x00019CE7
			public DailyPattern()
			{
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x0001ACEF File Offset: 0x00019CEF
			public DailyPattern(DateTime startDate, int interval)
				: base(startDate, interval)
			{
			}
		}

		// Token: 0x020000B1 RID: 177
		public sealed class DailyRegenerationPattern : Recurrence.IntervalPattern
		{
			// Token: 0x060007E4 RID: 2020 RVA: 0x0001ACF9 File Offset: 0x00019CF9
			public DailyRegenerationPattern()
			{
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x0001AD01 File Offset: 0x00019D01
			public DailyRegenerationPattern(DateTime startDate, int interval)
				: base(startDate, interval)
			{
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001AD0B File Offset: 0x00019D0B
			internal override string XmlElementName
			{
				get
				{
					return "DailyRegeneration";
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001AD12 File Offset: 0x00019D12
			internal override bool IsRegenerationPattern
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020000B2 RID: 178
		public sealed class MonthlyPattern : Recurrence.IntervalPattern
		{
			// Token: 0x060007E8 RID: 2024 RVA: 0x0001AD15 File Offset: 0x00019D15
			public MonthlyPattern()
			{
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x0001AD1D File Offset: 0x00019D1D
			public MonthlyPattern(DateTime startDate, int interval, int dayOfMonth)
				: base(startDate, interval)
			{
				this.DayOfMonth = dayOfMonth;
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001AD2E File Offset: 0x00019D2E
			internal override string XmlElementName
			{
				get
				{
					return "AbsoluteMonthlyRecurrence";
				}
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x0001AD35 File Offset: 0x00019D35
			internal override void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
			{
				base.InternalWritePropertiesToXml(writer);
				writer.WriteElementValue(XmlNamespace.Types, "DayOfMonth", this.DayOfMonth);
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x0001AD58 File Offset: 0x00019D58
			internal override JsonObject PatternToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.PatternToJson(service);
				jsonObject.Add("DayOfMonth", this.DayOfMonth);
				return jsonObject;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x0001AD80 File Offset: 0x00019D80
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				if (base.TryReadElementFromXml(reader))
				{
					return true;
				}
				string localName;
				if ((localName = reader.LocalName) != null && localName == "DayOfMonth")
				{
					this.dayOfMonth = new int?(reader.ReadElementValue<int>());
					return true;
				}
				return false;
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x0001ADC4 File Offset: 0x00019DC4
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				foreach (string text in jsonProperty.Keys)
				{
					string text2;
					if ((text2 = text) != null && text2 == "DayOfMonth")
					{
						this.dayOfMonth = new int?(jsonProperty.ReadAsInt(text));
					}
				}
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0001AE3C File Offset: 0x00019E3C
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (this.dayOfMonth == null)
				{
					throw new ServiceValidationException(Strings.DayOfMonthMustBeBetween1And31);
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001AE61 File Offset: 0x00019E61
			// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001AE74 File Offset: 0x00019E74
			public int DayOfMonth
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<int>(this.dayOfMonth, "DayOfMonth");
				}
				set
				{
					if (value < 1 || value > 31)
					{
						throw new ArgumentOutOfRangeException("DayOfMonth", Strings.DayOfMonthMustBeBetween1And31);
					}
					this.SetFieldValue<int?>(ref this.dayOfMonth, new int?(value));
				}
			}

			// Token: 0x04000295 RID: 661
			private int? dayOfMonth;
		}

		// Token: 0x020000B3 RID: 179
		public sealed class MonthlyRegenerationPattern : Recurrence.IntervalPattern
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x0001AEA6 File Offset: 0x00019EA6
			public MonthlyRegenerationPattern()
			{
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x0001AEAE File Offset: 0x00019EAE
			public MonthlyRegenerationPattern(DateTime startDate, int interval)
				: base(startDate, interval)
			{
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001AEB8 File Offset: 0x00019EB8
			internal override string XmlElementName
			{
				get
				{
					return "MonthlyRegeneration";
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001AEBF File Offset: 0x00019EBF
			internal override bool IsRegenerationPattern
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020000B4 RID: 180
		public sealed class RelativeMonthlyPattern : Recurrence.IntervalPattern
		{
			// Token: 0x060007F6 RID: 2038 RVA: 0x0001AEC2 File Offset: 0x00019EC2
			public RelativeMonthlyPattern()
			{
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x0001AECA File Offset: 0x00019ECA
			public RelativeMonthlyPattern(DateTime startDate, int interval, DayOfTheWeek dayOfTheWeek, DayOfTheWeekIndex dayOfTheWeekIndex)
				: base(startDate, interval)
			{
				this.DayOfTheWeek = dayOfTheWeek;
				this.DayOfTheWeekIndex = dayOfTheWeekIndex;
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001AEE3 File Offset: 0x00019EE3
			internal override string XmlElementName
			{
				get
				{
					return "RelativeMonthlyRecurrence";
				}
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x0001AEEA File Offset: 0x00019EEA
			internal override void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
			{
				base.InternalWritePropertiesToXml(writer);
				writer.WriteElementValue(XmlNamespace.Types, "DaysOfWeek", this.DayOfTheWeek);
				writer.WriteElementValue(XmlNamespace.Types, "DayOfWeekIndex", this.DayOfTheWeekIndex);
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x0001AF24 File Offset: 0x00019F24
			internal override JsonObject PatternToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.PatternToJson(service);
				jsonObject.Add("DaysOfWeek", this.DayOfTheWeek);
				jsonObject.Add("DayOfWeekIndex", this.DayOfTheWeekIndex);
				return jsonObject;
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x0001AF68 File Offset: 0x00019F68
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				if (base.TryReadElementFromXml(reader))
				{
					return true;
				}
				string localName;
				if ((localName = reader.LocalName) != null)
				{
					if (localName == "DaysOfWeek")
					{
						this.dayOfTheWeek = new DayOfTheWeek?(reader.ReadElementValue<DayOfTheWeek>());
						return true;
					}
					if (localName == "DayOfWeekIndex")
					{
						this.dayOfTheWeekIndex = new DayOfTheWeekIndex?(reader.ReadElementValue<DayOfTheWeekIndex>());
						return true;
					}
				}
				return false;
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x0001AFD0 File Offset: 0x00019FD0
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				foreach (string text in jsonProperty.Keys)
				{
					string text2;
					if ((text2 = text) != null)
					{
						if (!(text2 == "DaysOfWeek"))
						{
							if (text2 == "DayOfWeekIndex")
							{
								this.dayOfTheWeekIndex = new DayOfTheWeekIndex?(jsonProperty.ReadEnumValue<DayOfTheWeekIndex>(text));
							}
						}
						else
						{
							this.dayOfTheWeek = new DayOfTheWeek?(jsonProperty.ReadEnumValue<DayOfTheWeek>(text));
						}
					}
				}
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x0001B06C File Offset: 0x0001A06C
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (this.dayOfTheWeek == null)
				{
					throw new ServiceValidationException(Strings.DayOfTheWeekMustBeSpecifiedForRecurrencePattern);
				}
				if (this.dayOfTheWeekIndex == null)
				{
					throw new ServiceValidationException(Strings.DayOfWeekIndexMustBeSpecifiedForRecurrencePattern);
				}
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001B0B9 File Offset: 0x0001A0B9
			// (set) Token: 0x060007FF RID: 2047 RVA: 0x0001B0CC File Offset: 0x0001A0CC
			public DayOfTheWeekIndex DayOfTheWeekIndex
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<DayOfTheWeekIndex>(this.dayOfTheWeekIndex, "DayOfTheWeekIndex");
				}
				set
				{
					this.SetFieldValue<DayOfTheWeekIndex?>(ref this.dayOfTheWeekIndex, new DayOfTheWeekIndex?(value));
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001B0E0 File Offset: 0x0001A0E0
			// (set) Token: 0x06000801 RID: 2049 RVA: 0x0001B0F3 File Offset: 0x0001A0F3
			public DayOfTheWeek DayOfTheWeek
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<DayOfTheWeek>(this.dayOfTheWeek, "DayOfTheWeek");
				}
				set
				{
					this.SetFieldValue<DayOfTheWeek?>(ref this.dayOfTheWeek, new DayOfTheWeek?(value));
				}
			}

			// Token: 0x04000296 RID: 662
			private DayOfTheWeek? dayOfTheWeek;

			// Token: 0x04000297 RID: 663
			private DayOfTheWeekIndex? dayOfTheWeekIndex;
		}

		// Token: 0x020000B5 RID: 181
		public sealed class RelativeYearlyPattern : Recurrence
		{
			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001B107 File Offset: 0x0001A107
			internal override string XmlElementName
			{
				get
				{
					return "RelativeYearlyRecurrence";
				}
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x0001B110 File Offset: 0x0001A110
			internal override void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
			{
				base.InternalWritePropertiesToXml(writer);
				writer.WriteElementValue(XmlNamespace.Types, "DaysOfWeek", this.DayOfTheWeek);
				writer.WriteElementValue(XmlNamespace.Types, "DayOfWeekIndex", this.DayOfTheWeekIndex);
				writer.WriteElementValue(XmlNamespace.Types, "Month", this.Month);
			}

			// Token: 0x06000804 RID: 2052 RVA: 0x0001B16C File Offset: 0x0001A16C
			internal override JsonObject PatternToJson(ExchangeService service)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter(this.XmlElementName);
				jsonObject.Add("DaysOfWeek", this.DayOfTheWeek);
				jsonObject.Add("DayOfWeekIndex", this.DayOfTheWeekIndex);
				jsonObject.Add("Month", this.Month);
				return jsonObject;
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x0001B1D0 File Offset: 0x0001A1D0
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				if (base.TryReadElementFromXml(reader))
				{
					return true;
				}
				string localName;
				if ((localName = reader.LocalName) != null)
				{
					if (localName == "DaysOfWeek")
					{
						this.dayOfTheWeek = new DayOfTheWeek?(reader.ReadElementValue<DayOfTheWeek>());
						return true;
					}
					if (localName == "DayOfWeekIndex")
					{
						this.dayOfTheWeekIndex = new DayOfTheWeekIndex?(reader.ReadElementValue<DayOfTheWeekIndex>());
						return true;
					}
					if (localName == "Month")
					{
						this.month = new Month?(reader.ReadElementValue<Month>());
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x0001B258 File Offset: 0x0001A258
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				foreach (string text in jsonProperty.Keys)
				{
					string text2;
					if ((text2 = text) != null)
					{
						if (!(text2 == "DaysOfWeek"))
						{
							if (!(text2 == "DayOfWeekIndex"))
							{
								if (text2 == "Month")
								{
									this.month = new Month?(jsonProperty.ReadEnumValue<Month>(text));
								}
							}
							else
							{
								this.dayOfTheWeekIndex = new DayOfTheWeekIndex?(jsonProperty.ReadEnumValue<DayOfTheWeekIndex>(text));
							}
						}
						else
						{
							this.dayOfTheWeek = new DayOfTheWeek?(jsonProperty.ReadEnumValue<DayOfTheWeek>(text));
						}
					}
				}
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x0001B314 File Offset: 0x0001A314
			public RelativeYearlyPattern()
			{
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x0001B31C File Offset: 0x0001A31C
			public RelativeYearlyPattern(DateTime startDate, Month month, DayOfTheWeek dayOfTheWeek, DayOfTheWeekIndex dayOfTheWeekIndex)
				: base(startDate)
			{
				this.Month = month;
				this.DayOfTheWeek = dayOfTheWeek;
				this.DayOfTheWeekIndex = dayOfTheWeekIndex;
			}

			// Token: 0x06000809 RID: 2057 RVA: 0x0001B33C File Offset: 0x0001A33C
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (this.dayOfTheWeekIndex == null)
				{
					throw new ServiceValidationException(Strings.DayOfWeekIndexMustBeSpecifiedForRecurrencePattern);
				}
				if (this.dayOfTheWeek == null)
				{
					throw new ServiceValidationException(Strings.DayOfTheWeekMustBeSpecifiedForRecurrencePattern);
				}
				if (this.month == null)
				{
					throw new ServiceValidationException(Strings.MonthMustBeSpecifiedForRecurrencePattern);
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001B3A6 File Offset: 0x0001A3A6
			// (set) Token: 0x0600080B RID: 2059 RVA: 0x0001B3B9 File Offset: 0x0001A3B9
			public DayOfTheWeekIndex DayOfTheWeekIndex
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<DayOfTheWeekIndex>(this.dayOfTheWeekIndex, "DayOfTheWeekIndex");
				}
				set
				{
					this.SetFieldValue<DayOfTheWeekIndex?>(ref this.dayOfTheWeekIndex, new DayOfTheWeekIndex?(value));
				}
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001B3CD File Offset: 0x0001A3CD
			// (set) Token: 0x0600080D RID: 2061 RVA: 0x0001B3E0 File Offset: 0x0001A3E0
			public DayOfTheWeek DayOfTheWeek
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<DayOfTheWeek>(this.dayOfTheWeek, "DayOfTheWeek");
				}
				set
				{
					this.SetFieldValue<DayOfTheWeek?>(ref this.dayOfTheWeek, new DayOfTheWeek?(value));
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001B3F4 File Offset: 0x0001A3F4
			// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001B407 File Offset: 0x0001A407
			public Month Month
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<Month>(this.month, "Month");
				}
				set
				{
					this.SetFieldValue<Month?>(ref this.month, new Month?(value));
				}
			}

			// Token: 0x04000298 RID: 664
			private DayOfTheWeek? dayOfTheWeek;

			// Token: 0x04000299 RID: 665
			private DayOfTheWeekIndex? dayOfTheWeekIndex;

			// Token: 0x0400029A RID: 666
			private Month? month;
		}

		// Token: 0x020000B6 RID: 182
		public sealed class WeeklyPattern : Recurrence.IntervalPattern
		{
			// Token: 0x06000810 RID: 2064 RVA: 0x0001B41B File Offset: 0x0001A41B
			public WeeklyPattern()
			{
				this.daysOfTheWeek.OnChange += this.DaysOfTheWeekChanged;
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x0001B445 File Offset: 0x0001A445
			public WeeklyPattern(DateTime startDate, int interval, params DayOfTheWeek[] daysOfTheWeek)
				: base(startDate, interval)
			{
				this.daysOfTheWeek.AddRange(daysOfTheWeek);
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x0001B466 File Offset: 0x0001A466
			private void DaysOfTheWeekChanged(ComplexProperty complexProperty)
			{
				this.Changed();
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001B46E File Offset: 0x0001A46E
			internal override string XmlElementName
			{
				get
				{
					return "WeeklyRecurrence";
				}
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x0001B478 File Offset: 0x0001A478
			internal override void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
			{
				base.InternalWritePropertiesToXml(writer);
				this.DaysOfTheWeek.WriteToXml(writer, "DaysOfWeek");
				if (this.firstDayOfWeek != null)
				{
					EwsUtilities.ValidatePropertyVersion((ExchangeService)writer.Service, ExchangeVersion.Exchange2010_SP1, "FirstDayOfWeek");
					writer.WriteElementValue(XmlNamespace.Types, "FirstDayOfWeek", this.firstDayOfWeek.Value);
				}
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x0001B4DC File Offset: 0x0001A4DC
			internal override JsonObject PatternToJson(ExchangeService service)
			{
				JsonObject jsonObject = base.PatternToJson(service);
				jsonObject.Add("DayOfWeek", this.DaysOfTheWeek.InternalToJson(service));
				if (this.firstDayOfWeek != null)
				{
					EwsUtilities.ValidatePropertyVersion(service, ExchangeVersion.Exchange2010_SP1, "FirstDayOfWeek");
					jsonObject.Add("FirstDayOfWeek", this.firstDayOfWeek.Value);
				}
				return jsonObject;
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x0001B540 File Offset: 0x0001A540
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				if (base.TryReadElementFromXml(reader))
				{
					return true;
				}
				string localName;
				if ((localName = reader.LocalName) != null)
				{
					if (localName == "DaysOfWeek")
					{
						this.DaysOfTheWeek.LoadFromXml(reader, reader.LocalName);
						return true;
					}
					if (localName == "FirstDayOfWeek")
					{
						this.FirstDayOfWeek = reader.ReadElementValue<DayOfWeek>(XmlNamespace.Types, "FirstDayOfWeek");
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x0001B5A8 File Offset: 0x0001A5A8
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				foreach (string text in jsonProperty.Keys)
				{
					string text2;
					if ((text2 = text) != null)
					{
						if (!(text2 == "DaysOfWeek"))
						{
							if (text2 == "FirstDayOfWeek")
							{
								this.FirstDayOfWeek = jsonProperty.ReadEnumValue<DayOfWeek>(text);
							}
						}
						else
						{
							this.DaysOfTheWeek.LoadFromJsonValue(jsonProperty.ReadAsString(text));
						}
					}
				}
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x0001B640 File Offset: 0x0001A640
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (this.DaysOfTheWeek.Count == 0)
				{
					throw new ServiceValidationException(Strings.DaysOfTheWeekNotSpecified);
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001B665 File Offset: 0x0001A665
			public DayOfTheWeekCollection DaysOfTheWeek
			{
				get
				{
					return this.daysOfTheWeek;
				}
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001B66D File Offset: 0x0001A66D
			// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001B680 File Offset: 0x0001A680
			public DayOfWeek FirstDayOfWeek
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<DayOfWeek>(this.firstDayOfWeek, "FirstDayOfWeek");
				}
				set
				{
					this.SetFieldValue<DayOfWeek?>(ref this.firstDayOfWeek, new DayOfWeek?(value));
				}
			}

			// Token: 0x0400029B RID: 667
			private DayOfTheWeekCollection daysOfTheWeek = new DayOfTheWeekCollection();

			// Token: 0x0400029C RID: 668
			private DayOfWeek? firstDayOfWeek;
		}

		// Token: 0x020000B7 RID: 183
		public sealed class WeeklyRegenerationPattern : Recurrence.IntervalPattern
		{
			// Token: 0x0600081C RID: 2076 RVA: 0x0001B694 File Offset: 0x0001A694
			public WeeklyRegenerationPattern()
			{
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x0001B69C File Offset: 0x0001A69C
			public WeeklyRegenerationPattern(DateTime startDate, int interval)
				: base(startDate, interval)
			{
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001B6A6 File Offset: 0x0001A6A6
			internal override string XmlElementName
			{
				get
				{
					return "WeeklyRegeneration";
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001B6AD File Offset: 0x0001A6AD
			internal override bool IsRegenerationPattern
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020000B8 RID: 184
		public sealed class YearlyPattern : Recurrence
		{
			// Token: 0x06000820 RID: 2080 RVA: 0x0001B6B0 File Offset: 0x0001A6B0
			public YearlyPattern()
			{
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x0001B6B8 File Offset: 0x0001A6B8
			public YearlyPattern(DateTime startDate, Month month, int dayOfMonth)
				: base(startDate)
			{
				this.Month = month;
				this.DayOfMonth = dayOfMonth;
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001B6CF File Offset: 0x0001A6CF
			internal override string XmlElementName
			{
				get
				{
					return "AbsoluteYearlyRecurrence";
				}
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x0001B6D6 File Offset: 0x0001A6D6
			internal override void InternalWritePropertiesToXml(EwsServiceXmlWriter writer)
			{
				base.InternalWritePropertiesToXml(writer);
				writer.WriteElementValue(XmlNamespace.Types, "DayOfMonth", this.DayOfMonth);
				writer.WriteElementValue(XmlNamespace.Types, "Month", this.Month);
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x0001B710 File Offset: 0x0001A710
			internal override JsonObject PatternToJson(ExchangeService service)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter(this.XmlElementName);
				jsonObject.Add("DayOfMonth", this.DayOfMonth);
				jsonObject.Add("Month", this.Month);
				return jsonObject;
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x0001B758 File Offset: 0x0001A758
			internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
			{
				if (base.TryReadElementFromXml(reader))
				{
					return true;
				}
				string localName;
				if ((localName = reader.LocalName) != null)
				{
					if (localName == "DayOfMonth")
					{
						this.dayOfMonth = new int?(reader.ReadElementValue<int>());
						return true;
					}
					if (localName == "Month")
					{
						this.month = new Month?(reader.ReadElementValue<Month>());
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x0001B7C0 File Offset: 0x0001A7C0
			internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
			{
				base.LoadFromJson(jsonProperty, service);
				foreach (string text in jsonProperty.Keys)
				{
					string text2;
					if ((text2 = text) != null)
					{
						if (!(text2 == "DayOfMonth"))
						{
							if (text2 == "Month")
							{
								this.month = new Month?(jsonProperty.ReadEnumValue<Month>(text));
							}
						}
						else
						{
							this.dayOfMonth = new int?(jsonProperty.ReadAsInt(text));
						}
					}
				}
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x0001B85C File Offset: 0x0001A85C
			internal override void InternalValidate()
			{
				base.InternalValidate();
				if (this.month == null)
				{
					throw new ServiceValidationException(Strings.MonthMustBeSpecifiedForRecurrencePattern);
				}
				if (this.dayOfMonth == null)
				{
					throw new ServiceValidationException(Strings.DayOfMonthMustBeSpecifiedForRecurrencePattern);
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001B8A9 File Offset: 0x0001A8A9
			// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001B8BC File Offset: 0x0001A8BC
			public Month Month
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<Month>(this.month, "Month");
				}
				set
				{
					this.SetFieldValue<Month?>(ref this.month, new Month?(value));
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001B8D0 File Offset: 0x0001A8D0
			// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001B8E3 File Offset: 0x0001A8E3
			public int DayOfMonth
			{
				get
				{
					return base.GetFieldValueOrThrowIfNull<int>(this.dayOfMonth, "DayOfMonth");
				}
				set
				{
					if (value < 1 || value > 31)
					{
						throw new ArgumentOutOfRangeException("DayOfMonth", Strings.DayOfMonthMustBeBetween1And31);
					}
					this.SetFieldValue<int?>(ref this.dayOfMonth, new int?(value));
				}
			}

			// Token: 0x0400029D RID: 669
			private Month? month;

			// Token: 0x0400029E RID: 670
			private int? dayOfMonth;
		}

		// Token: 0x020000B9 RID: 185
		public sealed class YearlyRegenerationPattern : Recurrence.IntervalPattern
		{
			// Token: 0x170001FF RID: 511
			// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001B915 File Offset: 0x0001A915
			internal override string XmlElementName
			{
				get
				{
					return "YearlyRegeneration";
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001B91C File Offset: 0x0001A91C
			internal override bool IsRegenerationPattern
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x0001B91F File Offset: 0x0001A91F
			public YearlyRegenerationPattern()
			{
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x0001B927 File Offset: 0x0001A927
			public YearlyRegenerationPattern(DateTime startDate, int interval)
				: base(startDate, interval)
			{
			}
		}
	}
}
