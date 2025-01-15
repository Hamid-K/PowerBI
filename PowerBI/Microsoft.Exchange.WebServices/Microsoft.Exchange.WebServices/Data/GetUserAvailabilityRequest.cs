using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000127 RID: 295
	internal sealed class GetUserAvailabilityRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x0002C111 File Offset: 0x0002B111
		internal GetUserAvailabilityRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0002C121 File Offset: 0x0002B121
		internal override string GetXmlElementName()
		{
			return "GetUserAvailabilityRequest";
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0002C128 File Offset: 0x0002B128
		internal override bool EmitTimeZoneHeader
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0002C12B File Offset: 0x0002B12B
		internal bool IsFreeBusyViewRequested
		{
			get
			{
				return this.requestedData == AvailabilityData.FreeBusy || this.requestedData == AvailabilityData.FreeBusyAndSuggestions;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0002C140 File Offset: 0x0002B140
		internal bool IsSuggestionsViewRequested
		{
			get
			{
				return this.requestedData == AvailabilityData.Suggestions || this.requestedData == AvailabilityData.FreeBusyAndSuggestions;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0002C156 File Offset: 0x0002B156
		internal override void Validate()
		{
			base.Validate();
			this.Options.Validate(this.TimeWindow.Duration);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0002C174 File Offset: 0x0002B174
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (writer.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1)
			{
				LegacyAvailabilityTimeZone legacyAvailabilityTimeZone = new LegacyAvailabilityTimeZone(writer.Service.TimeZone);
				legacyAvailabilityTimeZone.WriteToXml(writer, "TimeZone");
			}
			writer.WriteStartElement(XmlNamespace.Messages, "MailboxDataArray");
			foreach (AttendeeInfo attendeeInfo in this.Attendees)
			{
				attendeeInfo.WriteToXml(writer);
			}
			writer.WriteEndElement();
			this.Options.WriteToXml(writer, this);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002C20C File Offset: 0x0002B20C
		internal override string GetResponseXmlElementName()
		{
			return "GetUserAvailabilityResponse";
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0002C214 File Offset: 0x0002B214
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetUserAvailabilityResults getUserAvailabilityResults = new GetUserAvailabilityResults();
			if (this.IsFreeBusyViewRequested)
			{
				getUserAvailabilityResults.AttendeesAvailability = new ServiceResponseCollection<AttendeeAvailability>();
				reader.ReadStartElement(XmlNamespace.Messages, "FreeBusyResponseArray");
				do
				{
					reader.Read();
					if (reader.IsStartElement(XmlNamespace.Messages, "FreeBusyResponse"))
					{
						AttendeeAvailability attendeeAvailability = new AttendeeAvailability();
						attendeeAvailability.LoadFromXml(reader, "ResponseMessage");
						if (attendeeAvailability.ErrorCode == ServiceError.NoError)
						{
							attendeeAvailability.LoadFreeBusyViewFromXml(reader, this.Options.RequestedFreeBusyView);
						}
						getUserAvailabilityResults.AttendeesAvailability.Add(attendeeAvailability);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "FreeBusyResponseArray"));
			}
			if (this.IsSuggestionsViewRequested)
			{
				getUserAvailabilityResults.SuggestionsResponse = new SuggestionsResponse();
				reader.ReadStartElement(XmlNamespace.Messages, "SuggestionsResponse");
				getUserAvailabilityResults.SuggestionsResponse.LoadFromXml(reader, "ResponseMessage");
				if (getUserAvailabilityResults.SuggestionsResponse.ErrorCode == ServiceError.NoError)
				{
					getUserAvailabilityResults.SuggestionsResponse.LoadSuggestedDaysFromXml(reader);
				}
				reader.ReadEndElement(XmlNamespace.Messages, "SuggestionsResponse");
			}
			return getUserAvailabilityResults;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002C2F6 File Offset: 0x0002B2F6
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002C2F9 File Offset: 0x0002B2F9
		internal GetUserAvailabilityResults Execute()
		{
			return (GetUserAvailabilityResults)base.InternalExecute();
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0002C306 File Offset: 0x0002B306
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0002C30E File Offset: 0x0002B30E
		public IEnumerable<AttendeeInfo> Attendees
		{
			get
			{
				return this.attendees;
			}
			set
			{
				this.attendees = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0002C317 File Offset: 0x0002B317
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0002C31F File Offset: 0x0002B31F
		public TimeWindow TimeWindow
		{
			get
			{
				return this.timeWindow;
			}
			set
			{
				this.timeWindow = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002C328 File Offset: 0x0002B328
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0002C330 File Offset: 0x0002B330
		public AvailabilityData RequestedData
		{
			get
			{
				return this.requestedData;
			}
			set
			{
				this.requestedData = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0002C339 File Offset: 0x0002B339
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0002C341 File Offset: 0x0002B341
		public AvailabilityOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x04000928 RID: 2344
		private IEnumerable<AttendeeInfo> attendees;

		// Token: 0x04000929 RID: 2345
		private TimeWindow timeWindow;

		// Token: 0x0400092A RID: 2346
		private AvailabilityData requestedData = AvailabilityData.FreeBusyAndSuggestions;

		// Token: 0x0400092B RID: 2347
		private AvailabilityOptions options;
	}
}
