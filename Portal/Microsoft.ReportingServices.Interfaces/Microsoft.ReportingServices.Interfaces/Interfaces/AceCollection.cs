using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public sealed class AceCollection : CollectionBase
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000243F File Offset: 0x0000063F
		public int Add(AceStruct ace)
		{
			return base.InnerList.Add(ace);
		}

		// Token: 0x17000052 RID: 82
		public AceStruct this[int index]
		{
			get
			{
				return (AceStruct)base.InnerList[index];
			}
		}
	}
}
