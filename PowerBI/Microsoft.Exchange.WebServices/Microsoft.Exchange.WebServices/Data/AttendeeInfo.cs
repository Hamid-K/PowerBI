using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A8 RID: 680
	public sealed class AttendeeInfo : ISelfValidate
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x00041FE6 File Offset: 0x00040FE6
		public AttendeeInfo()
		{
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00041FF5 File Offset: 0x00040FF5
		public AttendeeInfo(string smtpAddress, MeetingAttendeeType attendeeType, bool excludeConflicts)
			: this()
		{
			this.smtpAddress = smtpAddress;
			this.attendeeType = attendeeType;
			this.excludeConflicts = excludeConflicts;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00042012 File Offset: 0x00041012
		public AttendeeInfo(string smtpAddress)
			: this(smtpAddress, MeetingAttendeeType.Required, false)
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00042024 File Offset: 0x00041024
		public static implicit operator AttendeeInfo(string smtpAddress)
		{
			return new AttendeeInfo(smtpAddress);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0004202C File Offset: 0x0004102C
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "MailboxData");
			writer.WriteStartElement(XmlNamespace.Types, "Email");
			writer.WriteElementValue(XmlNamespace.Types, "Address", this.SmtpAddress);
			writer.WriteEndElement();
			writer.WriteElementValue(XmlNamespace.Types, "AttendeeType", this.attendeeType);
			writer.WriteElementValue(XmlNamespace.Types, "ExcludeConflicts", this.excludeConflicts);
			writer.WriteEndElement();
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0004209D File Offset: 0x0004109D
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x000420A5 File Offset: 0x000410A5
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x000420AE File Offset: 0x000410AE
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x000420B6 File Offset: 0x000410B6
		public MeetingAttendeeType AttendeeType
		{
			get
			{
				return this.attendeeType;
			}
			set
			{
				this.attendeeType = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000420BF File Offset: 0x000410BF
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x000420C7 File Offset: 0x000410C7
		public bool ExcludeConflicts
		{
			get
			{
				return this.excludeConflicts;
			}
			set
			{
				this.excludeConflicts = value;
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000420D0 File Offset: 0x000410D0
		void ISelfValidate.Validate()
		{
			EwsUtilities.ValidateParam(this.smtpAddress, "SmtpAddress");
		}

		// Token: 0x040013A4 RID: 5028
		private string smtpAddress;

		// Token: 0x040013A5 RID: 5029
		private MeetingAttendeeType attendeeType = MeetingAttendeeType.Required;

		// Token: 0x040013A6 RID: 5030
		private bool excludeConflicts;
	}
}
