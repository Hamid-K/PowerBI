using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200068A RID: 1674
	[Serializable]
	internal sealed class Int64List : ArrayList
	{
		// Token: 0x06005C23 RID: 23587 RVA: 0x00179443 File Offset: 0x00177643
		internal Int64List()
		{
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x0017944B File Offset: 0x0017764B
		internal Int64List(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002064 RID: 8292
		internal long this[int index]
		{
			get
			{
				return (long)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
