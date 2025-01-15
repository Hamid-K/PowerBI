using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200070E RID: 1806
	[Serializable]
	internal sealed class RecordSetPropertyNamesList : ArrayList
	{
		// Token: 0x060064ED RID: 25837 RVA: 0x0018EC01 File Offset: 0x0018CE01
		internal RecordSetPropertyNamesList()
		{
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x0018EC09 File Offset: 0x0018CE09
		internal RecordSetPropertyNamesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170023BD RID: 9149
		internal RecordSetPropertyNames this[int index]
		{
			get
			{
				return (RecordSetPropertyNames)base[index];
			}
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x0018EC20 File Offset: 0x0018CE20
		internal StringList GetPropertyNames(int aliasIndex)
		{
			if (aliasIndex >= 0 && aliasIndex < this.Count)
			{
				return this[aliasIndex].PropertyNames;
			}
			return null;
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x0018EC40 File Offset: 0x0018CE40
		internal string GetPropertyName(int aliasIndex, int propertyIndex)
		{
			StringList propertyNames = this.GetPropertyNames(aliasIndex);
			if (propertyNames != null && propertyIndex >= 0 && propertyIndex < propertyNames.Count)
			{
				return propertyNames[propertyIndex];
			}
			return null;
		}
	}
}
