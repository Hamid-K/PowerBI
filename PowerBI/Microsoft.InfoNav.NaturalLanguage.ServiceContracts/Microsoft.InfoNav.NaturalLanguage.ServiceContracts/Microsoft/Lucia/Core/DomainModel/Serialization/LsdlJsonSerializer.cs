using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200018F RID: 399
	public static class LsdlJsonSerializer
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0000FE94 File Offset: 0x0000E094
		public static LsdlDocument ReadJson(TextReader reader)
		{
			LsdlDocument lsdlDocument;
			using (JsonReader jsonReader = JsonReaderFactory.Create(reader, true))
			{
				lsdlDocument = LsdlJsonSerializer.ReadJson(jsonReader);
			}
			return lsdlDocument;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		public static LsdlDocument ReadJson(JsonReader reader)
		{
			return LsdlJsonSerializer.ReadJson<LsdlDocument>(reader, false);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0000FED9 File Offset: 0x0000E0D9
		public static LsdlDocument ReadInternalJson(JsonReader reader)
		{
			return LsdlJsonSerializer.ReadJson<LsdlDocument>(reader, true);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0000FEE2 File Offset: 0x0000E0E2
		internal static T ReadJson<T>(JsonReader reader, bool canonical)
		{
			return (canonical ? LsdlJsonSerializer._canonicalSerializer : LsdlJsonSerializer._defaultSerializer).Deserialize<T>(reader);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0000FEF9 File Offset: 0x0000E0F9
		public static void WriteJson(this LsdlDocument lsdlDoc, TextWriter writer, Formatting formatting = Formatting.None)
		{
			lsdlDoc.WriteJson(JsonWriterFactory.Create(writer, Formatting.None), formatting);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0000FF09 File Offset: 0x0000E109
		public static void WriteJson(this LsdlDocument lsdlDoc, JsonWriter writer, Formatting formatting = Formatting.None)
		{
			((formatting == Formatting.None) ? LsdlJsonSerializer._canonicalSerializer : LsdlJsonSerializer.CreateJsonSerializer(formatting, true)).Serialize(writer, lsdlDoc);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0000FF23 File Offset: 0x0000E123
		internal static bool SupportsNonCanonicalForm(this JsonSerializer serializer)
		{
			return serializer.Converters.Count == LsdlJsonSerializer._defaultConverters.Count && serializer.Converters[0] == LsdlJsonSerializer._defaultConverters[0];
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0000FF58 File Offset: 0x0000E158
		private static JsonSerializer CreateJsonSerializer(Formatting formatting, bool canonical)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create();
			jsonSerializer.Formatting = formatting;
			jsonSerializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
			if (!canonical)
			{
				foreach (JsonConverter jsonConverter in LsdlJsonSerializer._defaultConverters)
				{
					jsonSerializer.Converters.Add(jsonConverter);
				}
			}
			return jsonSerializer;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		internal static void MapBindingPropertiesToCanonicalForm(JObject binding, JsonSerializer serializer)
		{
			if (serializer.SupportsNonCanonicalForm())
			{
				LsdlJsonSerializer.MapBindingPropertiesToCanonicalFormCore(binding);
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		private static void MapBindingPropertiesToCanonicalFormCore(JObject binding)
		{
			foreach (JProperty jproperty in binding.Properties().ToList<JProperty>())
			{
				string canonicalPropertyName = LsdlJsonSerializer.GetCanonicalPropertyName(jproperty.Name);
				if (canonicalPropertyName != jproperty.Name)
				{
					jproperty.Replace(new JProperty(canonicalPropertyName, jproperty.Value));
				}
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00010054 File Offset: 0x0000E254
		private static string GetCanonicalPropertyName(string name)
		{
			if (name == "Table")
			{
				return "ConceptualEntity";
			}
			if (!(name == "Column") && !(name == "Measure"))
			{
				return name;
			}
			return "ConceptualProperty";
		}

		// Token: 0x040006FC RID: 1788
		private const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x040006FD RID: 1789
		private static readonly IReadOnlyList<JsonConverter> _defaultConverters = new JsonConverter[]
		{
			new LsdlJsonSerializer.StringScalarFormConverter(),
			new LsdlJsonSerializer.LongDoubleUnionScalarFormConverter(),
			new LsdlJsonSerializer.BooleanScalarFormConverter(),
			new LsdlJsonSerializer.ValueListScalarFormConverter(),
			new LsdlJsonSerializer.BindingCommonPropertyNamesConverter<ConceptualEntityBinding>(),
			new LsdlJsonSerializer.BindingCommonPropertyNamesConverter<ConceptualPropertyBinding>(),
			new LsdlJsonSerializer.ImpliedRelationshipRoleConverter()
		};

		// Token: 0x040006FE RID: 1790
		private static readonly JsonSerializer _defaultSerializer = LsdlJsonSerializer.CreateJsonSerializer(Formatting.None, false);

		// Token: 0x040006FF RID: 1791
		private static readonly JsonSerializer _canonicalSerializer = LsdlJsonSerializer.CreateJsonSerializer(Formatting.None, true);

		// Token: 0x0200022B RID: 555
		private abstract class ScalarFormConverter<T> : JsonConverter
		{
			// Token: 0x06000BE4 RID: 3044
			protected abstract bool IsScalarTokenType(JsonToken tokenType);

			// Token: 0x06000BE5 RID: 3045 RVA: 0x00017C4E File Offset: 0x00015E4E
			protected virtual T ReadScalarValue(JsonReader reader, JsonSerializer serializer)
			{
				return (T)((object)reader.Value);
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x00017C5B File Offset: 0x00015E5B
			public override bool CanConvert(Type objectType)
			{
				return typeof(IScalarForm<T>).IsAssignableFrom(objectType);
			}

			// Token: 0x06000BE7 RID: 3047 RVA: 0x00017C70 File Offset: 0x00015E70
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.Null)
				{
					return null;
				}
				IScalarForm<T> scalarForm = (IScalarForm<T>)Activator.CreateInstance(objectType);
				if (this.IsScalarTokenType(reader.TokenType))
				{
					T t = this.ReadScalarValue(reader, serializer);
					if (t == null)
					{
						return null;
					}
					scalarForm.SetFromScalarForm(t);
				}
				else
				{
					serializer.Populate(reader, scalarForm);
				}
				return scalarForm;
			}

			// Token: 0x1700034A RID: 842
			// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00017CCA File Offset: 0x00015ECA
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000BE9 RID: 3049 RVA: 0x00017CCD File Offset: 0x00015ECD
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0200022C RID: 556
		private sealed class StringScalarFormConverter : LsdlJsonSerializer.ScalarFormConverter<string>
		{
			// Token: 0x06000BEB RID: 3051 RVA: 0x00017CDC File Offset: 0x00015EDC
			protected override bool IsScalarTokenType(JsonToken tokenType)
			{
				return tokenType == JsonToken.String;
			}
		}

		// Token: 0x0200022D RID: 557
		private sealed class LongDoubleUnionScalarFormConverter : LsdlJsonSerializer.ScalarFormConverter<Union<long, double>>
		{
			// Token: 0x06000BED RID: 3053 RVA: 0x00017CEB File Offset: 0x00015EEB
			protected override bool IsScalarTokenType(JsonToken tokenType)
			{
				return tokenType == JsonToken.Integer || tokenType == JsonToken.Float;
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x00017CF8 File Offset: 0x00015EF8
			protected override Union<long, double> ReadScalarValue(JsonReader reader, JsonSerializer serializer)
			{
				object value = reader.Value;
				if (value is long)
				{
					long num = (long)value;
					return num;
				}
				return (double)reader.Value;
			}
		}

		// Token: 0x0200022E RID: 558
		private sealed class BooleanScalarFormConverter : LsdlJsonSerializer.ScalarFormConverter<bool?>
		{
			// Token: 0x06000BF0 RID: 3056 RVA: 0x00017D3A File Offset: 0x00015F3A
			protected override bool IsScalarTokenType(JsonToken tokenType)
			{
				return tokenType == JsonToken.Boolean;
			}
		}

		// Token: 0x0200022F RID: 559
		private sealed class ValueListScalarFormConverter : LsdlJsonSerializer.ScalarFormConverter<IValueList>
		{
			// Token: 0x06000BF2 RID: 3058 RVA: 0x00017D49 File Offset: 0x00015F49
			protected override bool IsScalarTokenType(JsonToken tokenType)
			{
				return tokenType == JsonToken.StartArray || tokenType - JsonToken.Integer <= 3;
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x00017D58 File Offset: 0x00015F58
			protected override IValueList ReadScalarValue(JsonReader reader, JsonSerializer serializer)
			{
				object obj = serializer.Deserialize(reader);
				JArray jarray = obj as JArray;
				IValueList valueList2;
				if (jarray != null)
				{
					if (jarray.Count > 0)
					{
						List<object> list = (from JValue v in jarray
							select v.Value).ToList<object>();
						IValueList valueList = null;
						foreach (object obj2 in list)
						{
							if (this.TryCreateValueListForType(obj2, out valueList))
							{
								break;
							}
							if (obj2 != null)
							{
								throw new NotSupportedException("Unexpected element in Value array: " + ((obj2 != null) ? obj2.ToString() : null));
							}
						}
						if (valueList == null)
						{
							return null;
						}
						foreach (object obj3 in list)
						{
							this.AddToValueList(valueList, obj3);
						}
						return valueList;
					}
				}
				else if (this.TryCreateValueListForType(obj, out valueList2))
				{
					this.AddToValueList(valueList2, obj);
					return valueList2;
				}
				string text = "Unexpected Value '";
				string text2 = ((obj != null) ? obj.ToString() : null);
				string text3 = "' of type ";
				Type type = obj.GetType();
				throw new NotSupportedException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x00017EB8 File Offset: 0x000160B8
			private bool TryCreateValueListForType(object value, out IValueList list)
			{
				if (value is string)
				{
					list = new ValueList<string>();
					return true;
				}
				if (value is long)
				{
					long num = (long)value;
					list = new ValueList<Union<long, double>>();
					return true;
				}
				if (value is double)
				{
					double num2 = (double)value;
					list = new ValueList<Union<long, double>>();
					return true;
				}
				if (value is bool)
				{
					bool flag = (bool)value;
					list = new ValueList<bool?>();
					return true;
				}
				list = null;
				return false;
			}

			// Token: 0x06000BF5 RID: 3061 RVA: 0x00017F28 File Offset: 0x00016128
			private void AddToValueList(IValueList list, object value)
			{
				if (value is long)
				{
					long num = (long)value;
					value = num;
				}
				else if (value is double)
				{
					double num2 = (double)value;
					value = num2;
				}
				list.Add(value);
			}
		}

		// Token: 0x02000230 RID: 560
		private sealed class BindingCommonPropertyNamesConverter<T> : JTokenConverterBase<T, JObject> where T : Binding, new()
		{
			// Token: 0x06000BF7 RID: 3063 RVA: 0x00017F7C File Offset: 0x0001617C
			protected override T Create(Type objectType, JObject obj, JsonSerializer serializer)
			{
				LsdlJsonSerializer.MapBindingPropertiesToCanonicalFormCore(obj);
				T t = new T();
				serializer.Populate(obj.CreateReader(), t);
				return t;
			}
		}

		// Token: 0x02000231 RID: 561
		private sealed class ImpliedRelationshipRoleConverter : JsonConverter
		{
			// Token: 0x06000BF9 RID: 3065 RVA: 0x00017FB0 File Offset: 0x000161B0
			public override bool CanConvert(Type objectType)
			{
				return typeof(Relationship).IsAssignableFrom(objectType);
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x00017FC4 File Offset: 0x000161C4
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				Relationship relationship = new Relationship();
				serializer.Populate(reader, relationship);
				this.PopulateImpliedRoles(relationship);
				return relationship;
			}

			// Token: 0x1700034B RID: 843
			// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00017FE8 File Offset: 0x000161E8
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x00017FEB File Offset: 0x000161EB
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x00017FF4 File Offset: 0x000161F4
			private void PopulateImpliedRoles(Relationship relationship)
			{
				foreach (string text in relationship.GetAllImplicitRoles())
				{
					if (!relationship.Roles.ContainsKey(text))
					{
						relationship.Roles.Add(text, Role.FromScalarForm(text));
					}
				}
			}

			// Token: 0x0200025C RID: 604
			private sealed class RoleReferenceCollector : DefaultLsdlDocumentVisitor
			{
				// Token: 0x1700036F RID: 879
				// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0001ACEC File Offset: 0x00018EEC
				internal HashSet<string> Roles { get; } = new HashSet<string>();

				// Token: 0x06000CED RID: 3309 RVA: 0x0001ACF4 File Offset: 0x00018EF4
				public override void Visit(RoleReference roleReference)
				{
					this.Roles.Add(roleReference.Role);
				}
			}
		}
	}
}
