using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000103 RID: 259
	internal sealed class FireEventActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00027A44 File Offset: 0x00025C44
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00027A4C File Offset: 0x00025C4C
		public string EventType
		{
			get
			{
				return this.m_eventType;
			}
			set
			{
				this.m_eventType = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00027A55 File Offset: 0x00025C55
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00027A5D File Offset: 0x00025C5D
		public string EventData
		{
			get
			{
				return this.m_eventData;
			}
			set
			{
				this.m_eventData = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00027A66 File Offset: 0x00025C66
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00027A6E File Offset: 0x00025C6E
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				this.m_site = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00027A77 File Offset: 0x00025C77
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", this.EventType, this.EventData, this.Site);
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00027A9A File Offset: 0x00025C9A
		internal override void Validate()
		{
			if (this.EventType == null)
			{
				throw new MissingParameterException("EventType");
			}
		}

		// Token: 0x0400048E RID: 1166
		private string m_eventType;

		// Token: 0x0400048F RID: 1167
		private string m_eventData;

		// Token: 0x04000490 RID: 1168
		private string m_site;
	}
}
