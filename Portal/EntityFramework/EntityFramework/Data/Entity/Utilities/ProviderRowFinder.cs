using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000080 RID: 128
	internal class ProviderRowFinder
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public virtual DataRow FindRow(Type hintType, Func<DataRow, bool> selector, IEnumerable<DataRow> dataRows)
		{
			AssemblyName assemblyName = ((hintType == null) ? null : new AssemblyName(hintType.Assembly().FullName));
			foreach (DataRow dataRow in dataRows)
			{
				string text = (string)dataRow[3];
				AssemblyName rowProviderFactoryAssemblyName = null;
				Type.GetType(text, delegate(AssemblyName a)
				{
					rowProviderFactoryAssemblyName = a;
					return null;
				}, (Assembly _, string __, bool ___) => null);
				if (rowProviderFactoryAssemblyName != null && (hintType == null || string.Equals(assemblyName.Name, rowProviderFactoryAssemblyName.Name, StringComparison.OrdinalIgnoreCase)))
				{
					try
					{
						if (selector(dataRow))
						{
							return dataRow;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return null;
		}
	}
}
