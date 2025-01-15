using System;
using System.Collections;
using System.Configuration;

namespace Microsoft.Data
{
	// Token: 0x02000012 RID: 18
	internal sealed class LocalDBInstancesCollection : ConfigurationElementCollection
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x0000B187 File Offset: 0x00009387
		internal LocalDBInstancesCollection()
			: base(LocalDBInstancesCollection.s_comparer)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000B194 File Offset: 0x00009394
		protected override ConfigurationElement CreateNewElement()
		{
			return new LocalDBInstanceElement();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000B19B File Offset: 0x0000939B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LocalDBInstanceElement)element).Name;
		}

		// Token: 0x0400001C RID: 28
		private static readonly LocalDBInstancesCollection.TrimOrdinalIgnoreCaseStringComparer s_comparer = new LocalDBInstancesCollection.TrimOrdinalIgnoreCaseStringComparer();

		// Token: 0x0200018E RID: 398
		private class TrimOrdinalIgnoreCaseStringComparer : IComparer
		{
			// Token: 0x06001D40 RID: 7488 RVA: 0x00077FCC File Offset: 0x000761CC
			public int Compare(object x, object y)
			{
				string text = x as string;
				if (text != null)
				{
					x = text.Trim();
				}
				string text2 = y as string;
				if (text2 != null)
				{
					y = text2.Trim();
				}
				return StringComparer.OrdinalIgnoreCase.Compare(x, y);
			}
		}
	}
}
