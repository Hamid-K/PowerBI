using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000050 RID: 80
	internal sealed class CreateSystemResourceFolderActionParameters : CreateItemActionParameters
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00010225 File Offset: 0x0000E425
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", base.ItemName, base.ParentPath);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00010242 File Offset: 0x0000E442
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
		}
	}
}
