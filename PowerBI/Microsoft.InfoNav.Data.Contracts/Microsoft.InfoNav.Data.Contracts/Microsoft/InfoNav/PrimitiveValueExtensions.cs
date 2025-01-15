using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200005C RID: 92
	public static class PrimitiveValueExtensions
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000031A0 File Offset: 0x000013A0
		public static bool TryGetValue<T>(this PrimitiveValue primitiveValue, out T value)
		{
			object obj = ((primitiveValue != null) ? primitiveValue.GetValueAsObject() : null);
			if (obj is T)
			{
				T t = (T)((object)obj);
				value = t;
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
