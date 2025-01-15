using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000FD RID: 253
	internal class SetDataSetItemReferencesActionParameters : SetReportItemReferencesActionParameters
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x0002772C File Offset: 0x0002592C
		internal override void Validate()
		{
			base.Validate();
			if (base.ItemReferences.Length != 1)
			{
				throw new InvalidParameterException("ItemReferences");
			}
		}
	}
}
