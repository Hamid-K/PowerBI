using System;
using System.Globalization;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000BF RID: 191
	internal class AbsoluteDateTransition : TimeZoneTransition
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0001C1BE File Offset: 0x0001B1BE
		internal override void InitializeFromTransitionTime(TimeZoneInfo.TransitionTime transitionTime)
		{
			throw new ServiceLocalException(Strings.UnsupportedTimeZonePeriodTransitionTarget);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001C1CF File Offset: 0x0001B1CF
		internal override string GetXmlElementName()
		{
			return "AbsoluteDateTransition";
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001C1D8 File Offset: 0x0001B1D8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			bool flag = base.TryReadElementFromXml(reader);
			if (!flag && reader.LocalName == "DateTime")
			{
				this.dateTime = DateTime.Parse(reader.ReadElementValue(), CultureInfo.InvariantCulture);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001C21B File Offset: 0x0001B21B
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteElementValue(XmlNamespace.Types, "DateTime", this.dateTime);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001C23B File Offset: 0x0001B23B
		internal AbsoluteDateTransition(TimeZoneDefinition timeZoneDefinition)
			: base(timeZoneDefinition)
		{
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001C244 File Offset: 0x0001B244
		internal AbsoluteDateTransition(TimeZoneDefinition timeZoneDefinition, TimeZoneTransitionGroup targetGroup)
			: base(timeZoneDefinition, targetGroup)
		{
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001C24E File Offset: 0x0001B24E
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0001C256 File Offset: 0x0001B256
		internal DateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
			set
			{
				this.dateTime = value;
			}
		}

		// Token: 0x040002A8 RID: 680
		private DateTime dateTime;
	}
}
