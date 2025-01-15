using System;
using System.Globalization;
using System.Web;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200006A RID: 106
	internal sealed class CreateKpiActionParameters : CreateItemActionParameters
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001284E File Offset: 0x00010A4E
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00012856 File Offset: 0x00010A56
		public DataSetInfoCollection SharedDataSets
		{
			get
			{
				return this.m_sharedDataSets;
			}
			set
			{
				this.m_sharedDataSets = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000FB45 File Offset: 0x0000DD45
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, base.Overwrite);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00012860 File Offset: 0x00010A60
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("ItemName");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("ParentPath");
			}
			if (HttpUtility.UrlDecode(base.ItemName).Length != base.ItemName.Length)
			{
				throw new InvalidItemNameException("Name contains URL-encoded characters", CatalogItemNameUtility.MaxItemNameLength);
			}
		}

		// Token: 0x0400020F RID: 527
		private DataSetInfoCollection m_sharedDataSets;
	}
}
