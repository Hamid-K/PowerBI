using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A2 RID: 1186
	public class KnownTypesJsonConverter : JsonConverter
	{
		// Token: 0x06001AA9 RID: 6825 RVA: 0x00050260 File Offset: 0x0004E460
		public KnownTypesJsonConverter(IEnumerable<Type> knownTypes)
		{
			this._nameToType = knownTypes.ToDictionary((Type t) => t.FullName);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00050293 File Offset: 0x0004E493
		public override bool CanConvert(Type objectType)
		{
			return this._nameToType.Values.Contains(objectType);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000502A8 File Offset: 0x0004E4A8
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			JObject jobject = JObject.Load(reader);
			string text = jobject["$$type"].ToString();
			Type type = this._nameToType[text];
			return this.CreateNestedSerializer(serializer, this).Deserialize(jobject.CreateReader(), type);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000502FB File Offset: 0x0004E4FB
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JToken jtoken = JToken.FromObject(value, this.CreateNestedSerializer(serializer, this));
			jtoken["$$type"] = value.GetType().FullName;
			jtoken.WriteTo(writer, Array.Empty<JsonConverter>());
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00050334 File Offset: 0x0004E534
		private JsonSerializer CreateNestedSerializer(JsonSerializer source, JsonConverter converterToExclude)
		{
			JsonSerializer jsonSerializer = new JsonSerializer
			{
				SerializationBinder = source.SerializationBinder,
				TypeNameAssemblyFormatHandling = source.TypeNameAssemblyFormatHandling,
				Context = source.Context,
				Culture = source.Culture,
				ConstructorHandling = source.ConstructorHandling,
				ContractResolver = source.ContractResolver,
				CheckAdditionalContent = source.CheckAdditionalContent,
				DateFormatHandling = source.DateFormatHandling,
				DateFormatString = source.DateFormatString,
				DateParseHandling = source.DateParseHandling,
				DateTimeZoneHandling = source.DateTimeZoneHandling,
				DefaultValueHandling = source.DefaultValueHandling,
				EqualityComparer = source.EqualityComparer,
				FloatFormatHandling = source.FloatFormatHandling,
				Formatting = source.Formatting,
				FloatParseHandling = source.FloatParseHandling,
				MaxDepth = source.MaxDepth,
				MetadataPropertyHandling = source.MetadataPropertyHandling,
				MissingMemberHandling = source.MissingMemberHandling,
				NullValueHandling = source.NullValueHandling,
				ObjectCreationHandling = source.ObjectCreationHandling,
				PreserveReferencesHandling = source.PreserveReferencesHandling,
				ReferenceResolver = source.ReferenceResolver,
				ReferenceLoopHandling = source.ReferenceLoopHandling,
				StringEscapeHandling = source.StringEscapeHandling,
				TraceWriter = source.TraceWriter,
				TypeNameHandling = source.TypeNameHandling
			};
			foreach (JsonConverter jsonConverter in source.Converters)
			{
				if (jsonConverter != converterToExclude)
				{
					jsonSerializer.Converters.Add(jsonConverter);
				}
			}
			return jsonSerializer;
		}

		// Token: 0x04000D1C RID: 3356
		private const string TypePropertyName = "$$type";

		// Token: 0x04000D1D RID: 3357
		private readonly Dictionary<string, Type> _nameToType;
	}
}
