using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001AF RID: 431
	public sealed class PIIPropertyJsonResolver<T> : DefaultContractResolver where T : Attribute
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x00026CBC File Offset: 0x00024EBC
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IEnumerable<PropertyInfo> piiProperties = PIIPropertyJsonResolver<T>.m_piiProperties.GetOrAdd(type.ToString(), from x in type.GetProperties()
				where x.GetCustomAttributes(true).OfType<T>().Any<T>()
				where x.PropertyType == typeof(string)
				select x);
			IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
			list.Where((JsonProperty p) => piiProperties.Any((PropertyInfo info) => info.Name == p.UnderlyingName)).ToList<JsonProperty>().ForEach(delegate(JsonProperty p)
			{
				p.Converter = new PIIAttributeJsonConverter();
			});
			return list;
		}

		// Token: 0x04000462 RID: 1122
		private static readonly ConcurrentDictionary<string, IEnumerable<PropertyInfo>> m_piiProperties = new ConcurrentDictionary<string, IEnumerable<PropertyInfo>>();
	}
}
