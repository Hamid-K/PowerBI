using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000BB RID: 187
	internal sealed class GetUserSettingsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00021A2F File Offset: 0x0001FC2F
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00021A37 File Offset: 0x0001FC37
		public Property[] RequestedProperties
		{
			get
			{
				return this.m_requestedProperties;
			}
			set
			{
				this.m_requestedProperties = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00021A40 File Offset: 0x0001FC40
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00021A48 File Offset: 0x0001FC48
		public Property[] UserProperties
		{
			get
			{
				return this.m_userProperties;
			}
			set
			{
				this.m_userProperties = value;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000420 RID: 1056
		private Property[] m_requestedProperties;

		// Token: 0x04000421 RID: 1057
		private Property[] m_userProperties;
	}
}
