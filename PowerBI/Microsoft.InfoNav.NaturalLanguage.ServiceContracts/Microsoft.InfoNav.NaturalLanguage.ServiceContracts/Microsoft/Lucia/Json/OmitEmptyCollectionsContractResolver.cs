using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Lucia.Json
{
	// Token: 0x0200002D RID: 45
	public sealed class OmitEmptyCollectionsContractResolver : DefaultContractResolver
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000035D4 File Offset: 0x000017D4
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty prop = base.CreateProperty(member, memberSerialization);
			if (prop.ShouldSerialize == null && prop.PropertyType != typeof(string))
			{
				if (typeof(ICollection).IsAssignableFrom(prop.PropertyType))
				{
					prop.ShouldSerialize = delegate(object obj)
					{
						ICollection collection = prop.ValueProvider.GetValue(obj) as ICollection;
						return collection != null && collection.Count > 0;
					};
				}
				else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
				{
					prop.ShouldSerialize = delegate(object obj)
					{
						IEnumerable enumerable = prop.ValueProvider.GetValue(obj) as IEnumerable;
						return enumerable != null && enumerable.GetEnumerator().MoveNext();
					};
				}
			}
			return prop;
		}
	}
}
