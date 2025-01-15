using System;
using System.Collections;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public sealed class ModelOperationsCollection : CollectionBase
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00002307 File Offset: 0x00000507
		public int Add(ModelOperation operation)
		{
			return base.InnerList.Add(operation);
		}

		// Token: 0x17000050 RID: 80
		public ModelOperation this[int index]
		{
			get
			{
				return (ModelOperation)base.InnerList[index];
			}
		}
	}
}
