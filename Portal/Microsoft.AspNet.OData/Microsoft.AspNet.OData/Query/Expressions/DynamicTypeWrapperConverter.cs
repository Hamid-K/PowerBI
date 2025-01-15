using System;
using Microsoft.AspNet.OData.Common;
using Newtonsoft.Json;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E0 RID: 224
	internal class DynamicTypeWrapperConverter : JsonConverter
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001ACAF File Offset: 0x00018EAF
		public override bool CanConvert(Type objectType)
		{
			if (objectType == null)
			{
				throw Error.ArgumentNull("objectType");
			}
			return objectType.IsAssignableFrom(typeof(DynamicTypeWrapper));
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001ACDC File Offset: 0x00018EDC
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DynamicTypeWrapper dynamicTypeWrapper = value as DynamicTypeWrapper;
			if (dynamicTypeWrapper != null)
			{
				serializer.Serialize(writer, dynamicTypeWrapper.Values);
			}
		}
	}
}
