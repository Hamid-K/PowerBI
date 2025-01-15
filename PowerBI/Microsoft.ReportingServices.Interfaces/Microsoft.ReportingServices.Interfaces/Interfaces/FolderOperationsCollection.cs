using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public sealed class FolderOperationsCollection : CollectionBase
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000224F File Offset: 0x0000044F
		public int Add(FolderOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x1700004C RID: 76
		public FolderOperation this[int index]
		{
			get
			{
				return (FolderOperation)base.InnerList[index];
			}
		}
	}
}
