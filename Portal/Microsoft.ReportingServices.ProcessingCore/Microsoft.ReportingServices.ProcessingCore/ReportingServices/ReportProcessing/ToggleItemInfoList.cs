using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006CA RID: 1738
	internal sealed class ToggleItemInfoList : ArrayList
	{
		// Token: 0x170020A4 RID: 8356
		internal ToggleItemInfo this[int index]
		{
			get
			{
				return (ToggleItemInfo)base[index];
			}
		}
	}
}
