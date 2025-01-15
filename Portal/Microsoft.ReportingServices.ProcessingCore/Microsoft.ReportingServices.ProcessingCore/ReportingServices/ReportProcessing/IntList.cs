using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000689 RID: 1673
	[Serializable]
	public sealed class IntList : ArrayList
	{
		// Token: 0x06005C1E RID: 23582 RVA: 0x001793D8 File Offset: 0x001775D8
		internal IntList()
		{
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x001793E0 File Offset: 0x001775E0
		internal IntList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002063 RID: 8291
		internal int this[int index]
		{
			get
			{
				return (int)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x00179408 File Offset: 0x00177608
		internal void CopyTo(IntList target)
		{
			if (target == null)
			{
				return;
			}
			target.Clear();
			for (int i = 0; i < this.Count; i++)
			{
				target.Add(this[i]);
			}
		}
	}
}
