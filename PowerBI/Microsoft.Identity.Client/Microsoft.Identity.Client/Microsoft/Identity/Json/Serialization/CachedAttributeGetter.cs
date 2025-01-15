using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000072 RID: 114
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00019574 File Offset: 0x00017774
		[return: Nullable(2)]
		public static T GetAttribute(object type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		// Token: 0x04000210 RID: 528
		[Nullable(new byte[] { 0, 0, 2 })]
		private static readonly ThreadSafeStore<object, T> TypeAttributeCache = new ThreadSafeStore<object, T>(new Func<object, T>(JsonTypeReflector.GetAttribute<T>));
	}
}
