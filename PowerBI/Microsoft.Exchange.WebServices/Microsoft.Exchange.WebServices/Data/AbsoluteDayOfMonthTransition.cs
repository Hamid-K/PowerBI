using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C1 RID: 193
	internal class AbsoluteDayOfMonthTransition : AbsoluteMonthTransition
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x0001C37A File Offset: 0x0001B37A
		internal override string GetXmlElementName()
		{
			return "RecurringDateTransition";
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001C384 File Offset: 0x0001B384
		internal override TimeZoneInfo.TransitionTime CreateTransitionTime()
		{
			return TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(base.TimeOffset.Ticks), base.Month, this.DayOfMonth);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001C3B5 File Offset: 0x0001B3B5
		internal override void InitializeFromTransitionTime(TimeZoneInfo.TransitionTime transitionTime)
		{
			base.InitializeFromTransitionTime(transitionTime);
			this.dayOfMonth = transitionTime.Day;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001C3CC File Offset: 0x0001B3CC
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (base.TryReadElementFromXml(reader))
			{
				return true;
			}
			if (reader.LocalName == "Day")
			{
				this.dayOfMonth = reader.ReadElementValue<int>();
				EwsUtilities.Assert(this.dayOfMonth > 0 && this.dayOfMonth <= 31, "AbsoluteDayOfMonthTransition.TryReadElementFromXml", "dayOfMonth is not in the valid 1 - 31 range.");
				return true;
			}
			return false;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001C42D File Offset: 0x0001B42D
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteElementValue(XmlNamespace.Types, "Day", this.dayOfMonth);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001C44D File Offset: 0x0001B44D
		internal AbsoluteDayOfMonthTransition(TimeZoneDefinition timeZoneDefinition)
			: base(timeZoneDefinition)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001C456 File Offset: 0x0001B456
		internal AbsoluteDayOfMonthTransition(TimeZoneDefinition timeZoneDefinition, TimeZonePeriod targetPeriod)
			: base(timeZoneDefinition, targetPeriod)
		{
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001C460 File Offset: 0x0001B460
		internal int DayOfMonth
		{
			get
			{
				return this.dayOfMonth;
			}
		}

		// Token: 0x040002AB RID: 683
		private int dayOfMonth;
	}
}
