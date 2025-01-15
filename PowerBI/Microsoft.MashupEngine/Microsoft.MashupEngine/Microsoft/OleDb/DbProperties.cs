using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E93 RID: 7827
	public static class DbProperties
	{
		// Token: 0x0600C189 RID: 49545 RVA: 0x0026E613 File Offset: 0x0026C813
		public static IDBProperties Create()
		{
			return new DictionaryDBProperties(Array.Empty<PropertyInfo>());
		}
	}
}
