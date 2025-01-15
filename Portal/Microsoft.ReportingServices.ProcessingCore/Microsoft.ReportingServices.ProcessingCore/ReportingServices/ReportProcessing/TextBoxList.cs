using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006AB RID: 1707
	internal sealed class TextBoxList : ArrayList
	{
		// Token: 0x06005C99 RID: 23705 RVA: 0x00179B42 File Offset: 0x00177D42
		internal TextBoxList()
		{
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x00179B4A File Offset: 0x00177D4A
		internal TextBoxList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002086 RID: 8326
		internal TextBox this[int index]
		{
			get
			{
				return (TextBox)base[index];
			}
		}
	}
}
