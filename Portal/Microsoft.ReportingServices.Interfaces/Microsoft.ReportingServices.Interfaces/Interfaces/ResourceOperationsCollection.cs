using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public sealed class ResourceOperationsCollection : CollectionBase
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000022AB File Offset: 0x000004AB
		public int Add(ResourceOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x1700004E RID: 78
		public ResourceOperation this[int index]
		{
			get
			{
				return (ResourceOperation)base.InnerList[index];
			}
		}
	}
}
