using System;
using System.Globalization;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000192 RID: 402
	internal sealed class CreateLinkedReportActionParameters : CreateItemActionParameters
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00035FF3 File Offset: 0x000341F3
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00035FFB File Offset: 0x000341FB
		public string LinkPath
		{
			get
			{
				return this.m_linkPath;
			}
			set
			{
				this.m_linkPath = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00036004 File Offset: 0x00034204
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, this.LinkPath);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00036028 File Offset: 0x00034228
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Report");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.LinkPath == null)
			{
				throw new MissingParameterException("Link");
			}
			if (HttpUtility.UrlDecode(base.ItemName).Length != base.ItemName.Length)
			{
				throw new InvalidItemNameException("Name contains URL-encoded characters", CatalogItemNameUtility.MaxItemNameLength);
			}
		}

		// Token: 0x0400060E RID: 1550
		private string m_linkPath;
	}
}
