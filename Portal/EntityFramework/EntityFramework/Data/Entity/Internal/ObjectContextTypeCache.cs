using System;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000111 RID: 273
	internal static class ObjectContextTypeCache
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x000325C5 File Offset: 0x000307C5
		public static Type GetObjectType(Type type)
		{
			return ObjectContextTypeCache._typeCache.GetOrAdd(type, new Func<Type, Type>(ObjectContext.GetObjectType));
		}

		// Token: 0x0400094C RID: 2380
		private static readonly ConcurrentDictionary<Type, Type> _typeCache = new ConcurrentDictionary<Type, Type>();
	}
}
