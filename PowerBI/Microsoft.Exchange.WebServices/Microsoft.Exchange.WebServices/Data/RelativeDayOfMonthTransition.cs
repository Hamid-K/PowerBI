using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C2 RID: 194
	internal class RelativeDayOfMonthTransition : AbsoluteMonthTransition
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0001C468 File Offset: 0x0001B468
		internal override string GetXmlElementName()
		{
			return "RecurringDayTransition";
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001C470 File Offset: 0x0001B470
		internal override TimeZoneInfo.TransitionTime CreateTransitionTime()
		{
			return TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(base.TimeOffset.Ticks), base.Month, (this.WeekIndex == -1) ? 5 : this.WeekIndex, EwsUtilities.EwsToSystemDayOfWeek(this.DayOfTheWeek));
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001C4B8 File Offset: 0x0001B4B8
		internal override void InitializeFromTransitionTime(TimeZoneInfo.TransitionTime transitionTime)
		{
			base.InitializeFromTransitionTime(transitionTime);
			this.dayOfTheWeek = EwsUtilities.SystemToEwsDayOfTheWeek(transitionTime.DayOfWeek);
			this.weekIndex = ((transitionTime.Week == 5) ? (-1) : transitionTime.Week);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001C4F0 File Offset: 0x0001B4F0
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (base.TryReadElementFromXml(reader))
			{
				return true;
			}
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "DayOfWeek")
				{
					this.dayOfTheWeek = reader.ReadElementValue<DayOfTheWeek>();
					return true;
				}
				if (localName == "Occurrence")
				{
					this.weekIndex = reader.ReadElementValue<int>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C54B File Offset: 0x0001B54B
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteElementValue(XmlNamespace.Types, "DayOfWeek", this.dayOfTheWeek);
			writer.WriteElementValue(XmlNamespace.Types, "Occurrence", this.weekIndex);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001C582 File Offset: 0x0001B582
		internal RelativeDayOfMonthTransition(TimeZoneDefinition timeZoneDefinition)
			: base(timeZoneDefinition)
		{
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001C58B File Offset: 0x0001B58B
		internal RelativeDayOfMonthTransition(TimeZoneDefinition timeZoneDefinition, TimeZonePeriod targetPeriod)
			: base(timeZoneDefinition, targetPeriod)
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001C595 File Offset: 0x0001B595
		internal DayOfTheWeek DayOfTheWeek
		{
			get
			{
				return this.dayOfTheWeek;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001C59D File Offset: 0x0001B59D
		internal int WeekIndex
		{
			get
			{
				return this.weekIndex;
			}
		}

		// Token: 0x040002AC RID: 684
		private DayOfTheWeek dayOfTheWeek;

		// Token: 0x040002AD RID: 685
		private int weekIndex;
	}
}
