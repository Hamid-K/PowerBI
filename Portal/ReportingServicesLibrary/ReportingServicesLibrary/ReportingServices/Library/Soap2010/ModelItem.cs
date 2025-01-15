using System;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002ED RID: 749
	public class ModelItem
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x0006C494 File Offset: 0x0006A694
		internal static ModelItem[] Soap2005ModelItemToThisArray(ModelItem[] items)
		{
			if (items == null)
			{
				return null;
			}
			ModelItem[] array = new ModelItem[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = ModelItem.Soap2005ModelItemToThis(items[i]);
			}
			return array;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0006C4CC File Offset: 0x0006A6CC
		internal static ModelItem Soap2005ModelItemToThis(ModelItem item)
		{
			if (item == null)
			{
				return null;
			}
			return new ModelItem
			{
				ID = item.ID,
				Name = item.Name,
				Description = item.Description,
				ModelItemTypeName = item.Type.ToString(),
				ModelItems = ModelItem.Soap2005ModelItemToThisArray(item.ModelItems)
			};
		}

		// Token: 0x040009A7 RID: 2471
		public string ID;

		// Token: 0x040009A8 RID: 2472
		public string Name;

		// Token: 0x040009A9 RID: 2473
		public string ModelItemTypeName;

		// Token: 0x040009AA RID: 2474
		public string Description;

		// Token: 0x040009AB RID: 2475
		public ModelItem[] ModelItems;
	}
}
