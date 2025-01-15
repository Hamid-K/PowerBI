using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F6 RID: 1270
	[Serializable]
	internal sealed class RecordSetPropertyNamesList : ArrayList
	{
		// Token: 0x0600409D RID: 16541 RVA: 0x00110875 File Offset: 0x0010EA75
		public RecordSetPropertyNamesList()
		{
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0011087D File Offset: 0x0010EA7D
		internal RecordSetPropertyNamesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001B38 RID: 6968
		internal RecordSetPropertyNames this[int index]
		{
			get
			{
				return (RecordSetPropertyNames)base[index];
			}
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x00110894 File Offset: 0x0010EA94
		internal List<string> GetPropertyNames(int aliasIndex)
		{
			if (aliasIndex >= 0 && aliasIndex < this.Count)
			{
				return this[aliasIndex].PropertyNames;
			}
			return null;
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x001108B4 File Offset: 0x0010EAB4
		internal string GetPropertyName(int aliasIndex, int propertyIndex)
		{
			List<string> propertyNames = this.GetPropertyNames(aliasIndex);
			if (propertyNames != null && propertyIndex >= 0 && propertyIndex < propertyNames.Count)
			{
				return propertyNames[propertyIndex];
			}
			return null;
		}
	}
}
