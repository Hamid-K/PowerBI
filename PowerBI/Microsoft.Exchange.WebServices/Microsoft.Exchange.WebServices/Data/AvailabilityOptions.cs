using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A9 RID: 681
	public sealed class AvailabilityOptions
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x000420E2 File Offset: 0x000410E2
		internal void Validate(TimeSpan timeWindow)
		{
			if (TimeSpan.FromMinutes((double)this.MergedFreeBusyInterval) > timeWindow)
			{
				throw new ArgumentException(Strings.MergedFreeBusyIntervalMustBeSmallerThanTimeWindow, "MergedFreeBusyInterval");
			}
			EwsUtilities.ValidateParamAllowNull(this.DetailedSuggestionsWindow, "DetailedSuggestionsWindow");
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00042120 File Offset: 0x00041120
		internal void WriteToXml(EwsServiceXmlWriter writer, GetUserAvailabilityRequest request)
		{
			if (request.IsFreeBusyViewRequested)
			{
				writer.WriteStartElement(XmlNamespace.Types, "FreeBusyViewOptions");
				request.TimeWindow.WriteToXmlUnscopedDatesOnly(writer, "TimeWindow");
				writer.WriteElementValue(XmlNamespace.Types, "MergedFreeBusyIntervalInMinutes", this.MergedFreeBusyInterval);
				writer.WriteElementValue(XmlNamespace.Types, "RequestedView", this.RequestedFreeBusyView);
				writer.WriteEndElement();
			}
			if (request.IsSuggestionsViewRequested)
			{
				writer.WriteStartElement(XmlNamespace.Types, "SuggestionsViewOptions");
				writer.WriteElementValue(XmlNamespace.Types, "GoodThreshold", this.GoodSuggestionThreshold);
				writer.WriteElementValue(XmlNamespace.Types, "MaximumResultsByDay", this.MaximumSuggestionsPerDay);
				writer.WriteElementValue(XmlNamespace.Types, "MaximumNonWorkHourResultsByDay", this.MaximumNonWorkHoursSuggestionsPerDay);
				writer.WriteElementValue(XmlNamespace.Types, "MeetingDurationInMinutes", this.MeetingDuration);
				writer.WriteElementValue(XmlNamespace.Types, "MinimumSuggestionQuality", this.MinimumSuggestionQuality);
				TimeWindow timeWindow = ((this.DetailedSuggestionsWindow == null) ? request.TimeWindow : this.DetailedSuggestionsWindow);
				timeWindow.WriteToXmlUnscopedDatesOnly(writer, "DetailedSuggestionsWindow");
				if (this.CurrentMeetingTime != null)
				{
					writer.WriteElementValue(XmlNamespace.Types, "CurrentMeetingTime", this.CurrentMeetingTime.Value);
				}
				writer.WriteElementValue(XmlNamespace.Types, "GlobalObjectId", this.GlobalObjectId);
				writer.WriteEndElement();
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x000422B0 File Offset: 0x000412B0
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x000422B8 File Offset: 0x000412B8
		public int MergedFreeBusyInterval
		{
			get
			{
				return this.mergedFreeBusyInterval;
			}
			set
			{
				if (value < 5 || value > 1440)
				{
					throw new ArgumentException(string.Format(Strings.InvalidPropertyValueNotInRange, "MergedFreeBusyInterval", 5, 1440));
				}
				this.mergedFreeBusyInterval = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000422F7 File Offset: 0x000412F7
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x000422FF File Offset: 0x000412FF
		public FreeBusyViewType RequestedFreeBusyView
		{
			get
			{
				return this.requestedFreeBusyView;
			}
			set
			{
				this.requestedFreeBusyView = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00042308 File Offset: 0x00041308
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x00042310 File Offset: 0x00041310
		public int GoodSuggestionThreshold
		{
			get
			{
				return this.goodSuggestionThreshold;
			}
			set
			{
				if (value < 1 || value > 49)
				{
					throw new ArgumentException(string.Format(Strings.InvalidPropertyValueNotInRange, "GoodSuggestionThreshold", 1, 49));
				}
				this.goodSuggestionThreshold = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00042349 File Offset: 0x00041349
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00042351 File Offset: 0x00041351
		public int MaximumSuggestionsPerDay
		{
			get
			{
				return this.maximumSuggestionsPerDay;
			}
			set
			{
				if (value < 0 || value > 48)
				{
					throw new ArgumentException(string.Format(Strings.InvalidPropertyValueNotInRange, "MaximumSuggestionsPerDay", 0, 48));
				}
				this.maximumSuggestionsPerDay = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0004238A File Offset: 0x0004138A
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x00042392 File Offset: 0x00041392
		public int MaximumNonWorkHoursSuggestionsPerDay
		{
			get
			{
				return this.maximumNonWorkHoursSuggestionsPerDay;
			}
			set
			{
				if (value < 0 || value > 48)
				{
					throw new ArgumentException(string.Format(Strings.InvalidPropertyValueNotInRange, "MaximumNonWorkHoursSuggestionsPerDay", 0, 48));
				}
				this.maximumNonWorkHoursSuggestionsPerDay = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000423CB File Offset: 0x000413CB
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x000423D4 File Offset: 0x000413D4
		public int MeetingDuration
		{
			get
			{
				return this.meetingDuration;
			}
			set
			{
				if (value < 30 || value > 1440)
				{
					throw new ArgumentException(string.Format(Strings.InvalidPropertyValueNotInRange, "MeetingDuration", 30, 1440));
				}
				this.meetingDuration = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00042420 File Offset: 0x00041420
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x00042428 File Offset: 0x00041428
		public SuggestionQuality MinimumSuggestionQuality
		{
			get
			{
				return this.minimumSuggestionQuality;
			}
			set
			{
				this.minimumSuggestionQuality = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00042431 File Offset: 0x00041431
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x00042439 File Offset: 0x00041439
		public TimeWindow DetailedSuggestionsWindow
		{
			get
			{
				return this.detailedSuggestionsWindow;
			}
			set
			{
				this.detailedSuggestionsWindow = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00042442 File Offset: 0x00041442
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x0004244A File Offset: 0x0004144A
		public DateTime? CurrentMeetingTime
		{
			get
			{
				return this.currentMeetingTime;
			}
			set
			{
				this.currentMeetingTime = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00042453 File Offset: 0x00041453
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x0004245B File Offset: 0x0004145B
		public string GlobalObjectId
		{
			get
			{
				return this.globalObjectId;
			}
			set
			{
				this.globalObjectId = value;
			}
		}

		// Token: 0x040013A7 RID: 5031
		private int mergedFreeBusyInterval = 30;

		// Token: 0x040013A8 RID: 5032
		private FreeBusyViewType requestedFreeBusyView = FreeBusyViewType.Detailed;

		// Token: 0x040013A9 RID: 5033
		private int goodSuggestionThreshold = 25;

		// Token: 0x040013AA RID: 5034
		private int maximumSuggestionsPerDay = 10;

		// Token: 0x040013AB RID: 5035
		private int maximumNonWorkHoursSuggestionsPerDay;

		// Token: 0x040013AC RID: 5036
		private int meetingDuration = 60;

		// Token: 0x040013AD RID: 5037
		private SuggestionQuality minimumSuggestionQuality = SuggestionQuality.Fair;

		// Token: 0x040013AE RID: 5038
		private TimeWindow detailedSuggestionsWindow;

		// Token: 0x040013AF RID: 5039
		private DateTime? currentMeetingTime;

		// Token: 0x040013B0 RID: 5040
		private string globalObjectId;
	}
}
