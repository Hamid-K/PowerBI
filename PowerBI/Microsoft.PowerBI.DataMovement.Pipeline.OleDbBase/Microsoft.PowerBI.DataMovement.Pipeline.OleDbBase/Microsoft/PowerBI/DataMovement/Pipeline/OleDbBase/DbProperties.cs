using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000040 RID: 64
	public static class DbProperties
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00006E27 File Offset: 0x00005027
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static IDBProperties Create()
		{
			return new DictionaryDBProperties(Array.Empty<PropertyInfo>());
		}
	}
}
