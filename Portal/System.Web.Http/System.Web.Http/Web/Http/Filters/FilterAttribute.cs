using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Http.Internal;

namespace System.Web.Http.Filters
{
	// Token: 0x020000D2 RID: 210
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class FilterAttribute : Attribute, IFilter
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		public virtual bool AllowMultiple
		{
			get
			{
				return FilterAttribute.AllowsMultiple(base.GetType());
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000E0CE File Offset: 0x0000C2CE
		private static bool AllowsMultiple(Type attributeType)
		{
			return FilterAttribute._attributeUsageCache.GetOrAdd(attributeType, (Type type) => type.GetCustomAttributes(true).First<AttributeUsageAttribute>().AllowMultiple);
		}

		// Token: 0x0400013C RID: 316
		private static readonly ConcurrentDictionary<Type, bool> _attributeUsageCache = new ConcurrentDictionary<Type, bool>();
	}
}
