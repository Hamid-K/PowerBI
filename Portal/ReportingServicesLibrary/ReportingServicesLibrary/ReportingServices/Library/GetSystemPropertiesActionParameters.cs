using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B9 RID: 185
	internal sealed class GetSystemPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00021913 File Offset: 0x0001FB13
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0002191B File Offset: 0x0001FB1B
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

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00021924 File Offset: 0x0001FB24
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0002192C File Offset: 0x0001FB2C
		public Property[] SystemProperties
		{
			get
			{
				return this.m_systemProperties;
			}
			set
			{
				this.m_systemProperties = value;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00021935 File Offset: 0x0001FB35
		internal override void Validate()
		{
			if (this.RequestedProperties != null)
			{
				Microsoft.ReportingServices.Library.SystemProperties.ValidatePropertiesSupportedInMode(this.RequestedProperties, false);
			}
		}

		// Token: 0x0400041E RID: 1054
		private Property[] m_requestedProperties;

		// Token: 0x0400041F RID: 1055
		private Property[] m_systemProperties;
	}
}
