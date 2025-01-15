using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C7 RID: 199
	internal sealed class SetUserSettingsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00022774 File Offset: 0x00020974
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0002277C File Offset: 0x0002097C
		public Property[] Properties
		{
			get
			{
				return this.m_properties;
			}
			set
			{
				this.m_properties = value;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00022785 File Offset: 0x00020985
		internal override void Validate()
		{
			if (this.Properties == null)
			{
				throw new MissingParameterException("Properties");
			}
		}

		// Token: 0x0400042F RID: 1071
		private Property[] m_properties;
	}
}
