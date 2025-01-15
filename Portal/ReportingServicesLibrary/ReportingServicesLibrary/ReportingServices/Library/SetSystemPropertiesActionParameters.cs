using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C5 RID: 197
	internal sealed class SetSystemPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000221C4 File Offset: 0x000203C4
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x000221CC File Offset: 0x000203CC
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

		// Token: 0x0600087E RID: 2174 RVA: 0x000221D5 File Offset: 0x000203D5
		internal override void Validate()
		{
			if (this.SystemProperties == null)
			{
				throw new MissingParameterException("Properties");
			}
			Microsoft.ReportingServices.Library.SystemProperties.ValidatePropertiesSupportedInMode(this.SystemProperties, false);
		}

		// Token: 0x0400042E RID: 1070
		private Property[] m_systemProperties;
	}
}
