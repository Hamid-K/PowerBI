using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public sealed class ModelItemOperationsCollection : CollectionBase
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00002335 File Offset: 0x00000535
		public int Add(ModelItemOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x17000051 RID: 81
		public ModelItemOperation this[int index]
		{
			get
			{
				return (ModelItemOperation)base.InnerList[index];
			}
		}
	}
}
