using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000694 RID: 1684
	[Serializable]
	internal sealed class ExpressionInfoList : ArrayList
	{
		// Token: 0x06005C45 RID: 23621 RVA: 0x00179660 File Offset: 0x00177860
		internal ExpressionInfoList()
		{
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x00179668 File Offset: 0x00177868
		internal ExpressionInfoList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700206E RID: 8302
		internal ExpressionInfo this[int index]
		{
			get
			{
				return (ExpressionInfo)base[index];
			}
		}
	}
}
