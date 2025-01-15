using System;
using System.Globalization;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D6 RID: 470
	internal sealed class ListScheduleActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x00039B29 File Offset: 0x00037D29
		// (set) Token: 0x0600105A RID: 4186 RVA: 0x00039B31 File Offset: 0x00037D31
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_site = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00039B4A File Offset: 0x00037D4A
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.Site);
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00039B61 File Offset: 0x00037D61
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x00039B69 File Offset: 0x00037D69
		public Schedule[] Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		// Token: 0x04000659 RID: 1625
		private string m_site;

		// Token: 0x0400065A RID: 1626
		private Schedule[] m_children = new Schedule[0];
	}
}
