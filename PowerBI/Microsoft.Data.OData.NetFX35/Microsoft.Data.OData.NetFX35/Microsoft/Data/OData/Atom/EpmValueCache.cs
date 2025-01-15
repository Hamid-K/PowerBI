using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000269 RID: 617
	internal class EpmValueCache
	{
		// Token: 0x06001342 RID: 4930 RVA: 0x000483C4 File Offset: 0x000465C4
		internal EpmValueCache()
		{
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000483CC File Offset: 0x000465CC
		internal static IEnumerable<ODataProperty> GetComplexValueProperties(EpmValueCache epmValueCache, ODataComplexValue complexValue, bool writingContent)
		{
			if (epmValueCache == null)
			{
				return complexValue.Properties;
			}
			return epmValueCache.GetComplexValueProperties(complexValue, writingContent);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000483E0 File Offset: 0x000465E0
		internal IEnumerable<ODataProperty> CacheComplexValueProperties(ODataComplexValue complexValue)
		{
			if (complexValue == null)
			{
				return null;
			}
			IEnumerable<ODataProperty> properties = complexValue.Properties;
			List<ODataProperty> list = null;
			if (properties != null)
			{
				list = new List<ODataProperty>(properties);
			}
			if (this.epmValuesCache == null)
			{
				this.epmValuesCache = new Dictionary<object, object>(ReferenceEqualityComparer<object>.Instance);
			}
			this.epmValuesCache.Add(complexValue, list);
			return list;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004842C File Offset: 0x0004662C
		private IEnumerable<ODataProperty> GetComplexValueProperties(ODataComplexValue complexValue, bool writingContent)
		{
			object obj;
			if (this.epmValuesCache != null && this.epmValuesCache.TryGetValue(complexValue, ref obj))
			{
				return (IEnumerable<ODataProperty>)obj;
			}
			return complexValue.Properties;
		}

		// Token: 0x0400072C RID: 1836
		private Dictionary<object, object> epmValuesCache;
	}
}
