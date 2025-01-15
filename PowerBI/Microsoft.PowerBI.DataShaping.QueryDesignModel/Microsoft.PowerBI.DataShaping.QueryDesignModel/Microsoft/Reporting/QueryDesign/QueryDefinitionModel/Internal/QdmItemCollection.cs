using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x0200010E RID: 270
	internal class QdmItemCollection<T> : Collection<T>
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x0002BD60 File Offset: 0x00029F60
		internal QdmItemCollection(IEnumerable<T> items)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<T>>(items, "items");
			foreach (T t in items)
			{
				base.Add(t);
			}
		}
	}
}
