using System;
using System.Collections.ObjectModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000401 RID: 1025
	public class CustomPropertyCollection : Collection<CustomProperty>
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x0007FA8F File Offset: 0x0007DC8F
		public void Add(string name, string value)
		{
			base.Add(new CustomProperty(name, value));
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0007FAA0 File Offset: 0x0007DCA0
		public static string Find(CustomPropertyCollection collection, string name)
		{
			if (collection == null)
			{
				return null;
			}
			CustomProperty customProperty = collection.Find(name);
			if (customProperty == null)
			{
				return null;
			}
			return customProperty.Value;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0007FAC8 File Offset: 0x0007DCC8
		public CustomProperty Find(string name)
		{
			foreach (CustomProperty customProperty in this)
			{
				if (customProperty.Name == name)
				{
					return customProperty;
				}
			}
			return null;
		}
	}
}
