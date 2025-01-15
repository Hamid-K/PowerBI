using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200004E RID: 78
	internal class IncludedPropertiesContractResolver : DefaultContractResolver
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00011D48 File Offset: 0x0000FF48
		internal IncludedPropertiesContractResolver(IDictionary<Type, IEnumerable<string>> includedPropertiesMap)
		{
			this.includedPropertiesMap = includedPropertiesMap;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00011D58 File Offset: 0x0000FF58
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

		// Token: 0x040000BF RID: 191
		private IDictionary<Type, IEnumerable<string>> includedPropertiesMap;
	}
}
