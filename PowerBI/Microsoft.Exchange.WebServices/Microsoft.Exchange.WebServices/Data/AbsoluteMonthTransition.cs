using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C0 RID: 192
	internal abstract class AbsoluteMonthTransition : TimeZoneTransition
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x0001C260 File Offset: 0x0001B260
		internal override void InitializeFromTransitionTime(TimeZoneInfo.TransitionTime transitionTime)
		{
			base.InitializeFromTransitionTime(transitionTime);
			this.timeOffset = transitionTime.TimeOfDay.TimeOfDay;
			this.month = transitionTime.Month;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001C298 File Offset: 0x0001B298
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (base.TryReadElementFromXml(reader))
			{
				return true;
			}
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "TimeOffset")
				{
					this.timeOffset = EwsUtilities.XSDurationToTimeSpan(reader.ReadElementValue());
					return true;
				}
				if (localName == "Month")
				{
					this.month = reader.ReadElementValue<int>();
					EwsUtilities.Assert(this.month > 0 && this.month <= 12, "AbsoluteMonthTransition.TryReadElementFromXml", "month is not in the valid 1 - 12 range.");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001C320 File Offset: 0x0001B320
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteElementValue(XmlNamespace.Types, "TimeOffset", EwsUtilities.TimeSpanToXSDuration(this.timeOffset));
			writer.WriteElementValue(XmlNamespace.Types, "Month", this.month);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001C357 File Offset: 0x0001B357
		internal AbsoluteMonthTransition(TimeZoneDefinition timeZoneDefinition)
			: base(timeZoneDefinition)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001C360 File Offset: 0x0001B360
		internal AbsoluteMonthTransition(TimeZoneDefinition timeZoneDefinition, TimeZonePeriod targetPeriod)
			: base(timeZoneDefinition, targetPeriod)
		{
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001C36A File Offset: 0x0001B36A
		internal TimeSpan TimeOffset
		{
			get
			{
				return this.timeOffset;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001C372 File Offset: 0x0001B372
		internal int Month
		{
			get
			{
				return this.month;
			}
		}

		// Token: 0x040002A9 RID: 681
		private TimeSpan timeOffset;

		// Token: 0x040002AA RID: 682
		private int month;
	}
}
