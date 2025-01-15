using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000035 RID: 53
	public static class Utility
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000818E File Offset: 0x0000638E
		public static IEnumerable<object> GetCustomAttributes(Type type, Type attributeType, bool inherit)
		{
			return type.GetCustomAttributes(attributeType, inherit);
		}
	}
}
