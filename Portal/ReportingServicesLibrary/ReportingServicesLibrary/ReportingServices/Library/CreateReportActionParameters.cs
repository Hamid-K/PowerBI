using System;
using System.Globalization;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000194 RID: 404
	internal class CreateReportActionParameters : CreateItemActionParameters
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000362F0 File Offset: 0x000344F0
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000362F8 File Offset: 0x000344F8
		public byte[] ReportDefinition
		{
			get
			{
				return this.m_reportDefinition;
			}
			set
			{
				this.m_reportDefinition = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00036301 File Offset: 0x00034501
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00036309 File Offset: 0x00034509
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
			set
			{
				this.m_warnings = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0000FB45 File Offset: 0x0000DD45
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, base.Overwrite);
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00036314 File Offset: 0x00034514
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "Name" : "Report");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.ReportDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
			if (HttpUtility.UrlDecode(base.ItemName).Length != base.ItemName.Length)
			{
				throw new InvalidItemNameException("Name contains URL-encoded characters", CatalogItemNameUtility.MaxItemNameLength);
			}
		}

		// Token: 0x0400060F RID: 1551
		private byte[] m_reportDefinition;

		// Token: 0x04000610 RID: 1552
		private Warning[] m_warnings;
	}
}
