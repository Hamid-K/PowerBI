using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000048 RID: 72
	internal class CreateItemWithContentActionParameters : CreateItemActionParameters
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000FB34 File Offset: 0x0000DD34
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public CatalogItemContent CatalogItemContent { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000FB45 File Offset: 0x0000DD45
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, base.Overwrite);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Name");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.CatalogItemContent == null)
			{
				throw new MissingParameterException("CatalogItemContent");
			}
		}
	}
}
