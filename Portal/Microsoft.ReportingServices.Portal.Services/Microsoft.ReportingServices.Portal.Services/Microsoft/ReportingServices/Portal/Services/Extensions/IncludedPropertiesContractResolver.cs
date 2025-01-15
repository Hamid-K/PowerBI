using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x0200005E RID: 94
	internal class IncludedPropertiesContractResolver : DefaultContractResolver
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00012BC4 File Offset: 0x00010DC4
		internal IncludedPropertiesContractResolver(IDictionary<Type, IEnumerable<string>> includedPropertiesMap)
		{
			this.includedPropertiesMap = includedPropertiesMap;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00012BD4 File Offset: 0x00010DD4
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IQueryable<JsonProperty> queryable = base.CreateProperties(type, memberSerialization).AsQueryable<JsonProperty>();
			if (this.includedPropertiesMap != null)
			{
				IEnumerable<string> includedProperties = null;
				Type type2 = this.includedPropertiesMap.Keys.LastOrDefault((Type key) => key.IsAssignableFrom(type));
				if (type2 != null)
				{
					this.includedPropertiesMap.TryGetValue(type2, out includedProperties);
				}
				if (includedProperties == null)
				{
					this.includedPropertiesMap.TryGetValue(typeof(object), out includedProperties);
				}
				if (includedProperties != null)
				{
					queryable = queryable.Where((JsonProperty x) => includedProperties.Contains(x.PropertyName) || includedProperties.Contains(string.Format("{0}.{1}", x.DeclaringType.Name, x.PropertyName)));
				}
			}
			return queryable.ToList<JsonProperty>();
		}

		// Token: 0x040000DA RID: 218
		private IDictionary<Type, IEnumerable<string>> includedPropertiesMap;
	}
}
