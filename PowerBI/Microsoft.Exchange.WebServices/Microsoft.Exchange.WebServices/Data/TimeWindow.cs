using System;
using System.Globalization;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002AE RID: 686
	public sealed class TimeWindow : ISelfValidate
	{
		// Token: 0x06001876 RID: 6262 RVA: 0x00042FE8 File Offset: 0x00041FE8
		internal TimeWindow()
		{
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00042FF0 File Offset: 0x00041FF0
		public TimeWindow(DateTime startTime, DateTime endTime)
			: this()
		{
			this.startTime = startTime;
			this.endTime = endTime;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x00043006 File Offset: 0x00042006
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x0004300E File Offset: 0x0004200E
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00043017 File Offset: 0x00042017
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x0004301F File Offset: 0x0004201F
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00043028 File Offset: 0x00042028
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, "Duration");
			this.startTime = reader.ReadElementValueAsDateTime(XmlNamespace.Types, "StartTime").Value;
			this.endTime = reader.ReadElementValueAsDateTime(XmlNamespace.Types, "EndTime").Value;
			reader.ReadEndElement(XmlNamespace.Types, "Duration");
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00043084 File Offset: 0x00042084
		internal void LoadFromJson(JsonObject jsonObject, ExchangeService service)
		{
			this.startTime = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonObject.ReadAsString("StartTime")).Value;
			this.endTime = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonObject.ReadAsString("EndTime")).Value;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000430CF File Offset: 0x000420CF
		private static void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName, object startTime, object endTime)
		{
			writer.WriteStartElement(XmlNamespace.Types, xmlElementName);
			writer.WriteElementValue(XmlNamespace.Types, "StartTime", startTime);
			writer.WriteElementValue(XmlNamespace.Types, "EndTime", endTime);
			writer.WriteEndElement();
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000430FC File Offset: 0x000420FC
		internal void WriteToXmlUnscopedDatesOnly(EwsServiceXmlWriter writer, string xmlElementName)
		{
			TimeWindow.WriteToXml(writer, xmlElementName, this.StartTime.ToString("yyyy-MM-ddT00:00:00", CultureInfo.InvariantCulture), this.EndTime.ToString("yyyy-MM-ddT00:00:00", CultureInfo.InvariantCulture));
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00043140 File Offset: 0x00042140
		internal void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			TimeWindow.WriteToXml(writer, xmlElementName, this.StartTime, this.EndTime);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00043160 File Offset: 0x00042160
		internal JsonObject InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("StartTime", EwsUtilities.DateTimeToXSDateTime(this.startTime));
			jsonObject.Add("EndTime", EwsUtilities.DateTimeToXSDateTime(this.endTime));
			return jsonObject;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000431A0 File Offset: 0x000421A0
		internal TimeSpan Duration
		{
			get
			{
				return this.endTime - this.startTime;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000431B3 File Offset: 0x000421B3
		void ISelfValidate.Validate()
		{
			if (this.StartTime >= this.EndTime)
			{
				throw new ArgumentException(Strings.TimeWindowStartTimeMustBeGreaterThanEndTime);
			}
		}

		// Token: 0x040013BE RID: 5054
		private DateTime startTime;

		// Token: 0x040013BF RID: 5055
		private DateTime endTime;
	}
}
