using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x020007A4 RID: 1956
	internal sealed class MemberInfoList : ArrayList
	{
		// Token: 0x06006C72 RID: 27762 RVA: 0x001B79C0 File Offset: 0x001B5BC0
		internal MemberInfoList()
		{
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x001B79C8 File Offset: 0x001B5BC8
		internal MemberInfoList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170025BF RID: 9663
		internal MemberInfo this[int index]
		{
			get
			{
				return (MemberInfo)base[index];
			}
		}
	}
}
