using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Linq;
using Microsoft.Identity.Json.Serialization;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000E5 RID: 229
	internal class DiscriminatedUnionConverter : JsonConverter
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00031264 File Offset: 0x0002F464
		private static Type CreateUnionTypeLookup(Type t)
		{
			MethodCall<object, object> getUnionCases = FSharpUtils.Instance.GetUnionCases;
			object obj = null;
			object[] array = new object[2];
			array[0] = t;
			object obj2 = ((object[])getUnionCases(obj, array)).First<object>();
			return (Type)FSharpUtils.Instance.GetUnionCaseInfoDeclaringType(obj2);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000312AC File Offset: 0x0002F4AC
		private static DiscriminatedUnionConverter.Union CreateUnion(Type t)
		{
			MethodCall<object, object> preComputeUnionTagReader = FSharpUtils.Instance.PreComputeUnionTagReader;
			object obj = null;
			object[] array = new object[2];
			array[0] = t;
			DiscriminatedUnionConverter.Union union = new DiscriminatedUnionConverter.Union((FSharpFunction)preComputeUnionTagReader(obj, array), new List<DiscriminatedUnionConverter.UnionCase>());
			MethodCall<object, object> getUnionCases = FSharpUtils.Instance.GetUnionCases;
			object obj2 = null;
			object[] array2 = new object[2];
			array2[0] = t;
			foreach (object obj3 in (object[])getUnionCases(obj2, array2))
			{
				int num = (int)FSharpUtils.Instance.GetUnionCaseInfoTag(obj3);
				string text = (string)FSharpUtils.Instance.GetUnionCaseInfoName(obj3);
				PropertyInfo[] array4 = (PropertyInfo[])FSharpUtils.Instance.GetUnionCaseInfoFields(obj3, Array.Empty<object>());
				MethodCall<object, object> preComputeUnionReader = FSharpUtils.Instance.PreComputeUnionReader;
				object obj4 = null;
				object[] array5 = new object[2];
				array5[0] = obj3;
				FSharpFunction fsharpFunction = (FSharpFunction)preComputeUnionReader(obj4, array5);
				MethodCall<object, object> preComputeUnionConstructor = FSharpUtils.Instance.PreComputeUnionConstructor;
				object obj5 = null;
				object[] array6 = new object[2];
				array6[0] = obj3;
				DiscriminatedUnionConverter.UnionCase unionCase = new DiscriminatedUnionConverter.UnionCase(num, text, array4, fsharpFunction, (FSharpFunction)preComputeUnionConstructor(obj5, array6));
				union.Cases.Add(unionCase);
			}
			return union;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000313B4 File Offset: 0x0002F5B4
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			Type type = DiscriminatedUnionConverter.UnionTypeLookupCache.Get(value.GetType());
			DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(type);
			int tag = (int)union.TagReader.Invoke(new object[] { value });
			DiscriminatedUnionConverter.UnionCase unionCase = union.Cases.Single((DiscriminatedUnionConverter.UnionCase c) => c.Tag == tag);
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Case") : "Case");
			writer.WriteValue(unionCase.Name);
			if (unionCase.Fields != null && unionCase.Fields.Length != 0)
			{
				object[] array = (object[])unionCase.FieldReader.Invoke(new object[] { value });
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Fields") : "Fields");
				writer.WriteStartArray();
				foreach (object obj in array)
				{
					serializer.Serialize(writer, obj);
				}
				writer.WriteEndArray();
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000314E4 File Offset: 0x0002F6E4
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DiscriminatedUnionConverter.UnionCase unionCase = null;
			string caseName = null;
			JArray jarray = null;
			reader.ReadAndAssert();
			Func<DiscriminatedUnionConverter.UnionCase, bool> <>9__0;
			while (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (string.Equals(text, "Case", StringComparison.OrdinalIgnoreCase))
				{
					reader.ReadAndAssert();
					DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(objectType);
					caseName = reader.Value.ToString();
					IEnumerable<DiscriminatedUnionConverter.UnionCase> cases = union.Cases;
					Func<DiscriminatedUnionConverter.UnionCase, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (DiscriminatedUnionConverter.UnionCase c) => c.Name == caseName);
					}
					unionCase = cases.SingleOrDefault(func);
					if (unionCase == null)
					{
						throw JsonSerializationException.Create(reader, "No union type found with the name '{0}'.".FormatWith(CultureInfo.InvariantCulture, caseName));
					}
				}
				else
				{
					if (!string.Equals(text, "Fields", StringComparison.OrdinalIgnoreCase))
					{
						throw JsonSerializationException.Create(reader, "Unexpected property '{0}' found when reading union.".FormatWith(CultureInfo.InvariantCulture, text));
					}
					reader.ReadAndAssert();
					if (reader.TokenType != JsonToken.StartArray)
					{
						throw JsonSerializationException.Create(reader, "Union fields must been an array.");
					}
					jarray = (JArray)JToken.ReadFrom(reader);
				}
				reader.ReadAndAssert();
			}
			if (unionCase == null)
			{
				throw JsonSerializationException.Create(reader, "No '{0}' property with union name found.".FormatWith(CultureInfo.InvariantCulture, "Case"));
			}
			object[] array = new object[unionCase.Fields.Length];
			if (unionCase.Fields.Length != 0 && jarray == null)
			{
				throw JsonSerializationException.Create(reader, "No '{0}' property with union fields found.".FormatWith(CultureInfo.InvariantCulture, "Fields"));
			}
			if (jarray != null)
			{
				if (unionCase.Fields.Length != jarray.Count)
				{
					throw JsonSerializationException.Create(reader, "The number of field values does not match the number of properties defined by union '{0}'.".FormatWith(CultureInfo.InvariantCulture, caseName));
				}
				for (int i = 0; i < jarray.Count; i++)
				{
					JToken jtoken = jarray[i];
					PropertyInfo propertyInfo = unionCase.Fields[i];
					array[i] = jtoken.ToObject(propertyInfo.PropertyType, serializer);
				}
			}
			object[] array2 = new object[] { array };
			return unionCase.Constructor.Invoke(array2);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000316E0 File Offset: 0x0002F8E0
		public override bool CanConvert(Type objectType)
		{
			if (typeof(IEnumerable).IsAssignableFrom(objectType))
			{
				return false;
			}
			object[] customAttributes = objectType.GetCustomAttributes(true);
			bool flag = false;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				Type type = array[i].GetType();
				if (type.FullName == "Microsoft.FSharp.Core.CompilationMappingAttribute")
				{
					FSharpUtils.EnsureInitialized(type.Assembly());
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			MethodCall<object, object> isUnion = FSharpUtils.Instance.IsUnion;
			object obj = null;
			object[] array2 = new object[2];
			array2[0] = objectType;
			return (bool)isUnion(obj, array2);
		}

		// Token: 0x040003D3 RID: 979
		private const string CasePropertyName = "Case";

		// Token: 0x040003D4 RID: 980
		private const string FieldsPropertyName = "Fields";

		// Token: 0x040003D5 RID: 981
		private static readonly ThreadSafeStore<Type, DiscriminatedUnionConverter.Union> UnionCache = new ThreadSafeStore<Type, DiscriminatedUnionConverter.Union>(new Func<Type, DiscriminatedUnionConverter.Union>(DiscriminatedUnionConverter.CreateUnion));

		// Token: 0x040003D6 RID: 982
		private static readonly ThreadSafeStore<Type, Type> UnionTypeLookupCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(DiscriminatedUnionConverter.CreateUnionTypeLookup));

		// Token: 0x020003B6 RID: 950
		internal class Union
		{
			// Token: 0x06001DCF RID: 7631 RVA: 0x00068487 File Offset: 0x00066687
			public Union(FSharpFunction tagReader, List<DiscriminatedUnionConverter.UnionCase> cases)
			{
				this.TagReader = tagReader;
				this.Cases = cases;
			}

			// Token: 0x04001086 RID: 4230
			public readonly FSharpFunction TagReader;

			// Token: 0x04001087 RID: 4231
			public readonly List<DiscriminatedUnionConverter.UnionCase> Cases;
		}

		// Token: 0x020003B7 RID: 951
		internal class UnionCase
		{
			// Token: 0x06001DD0 RID: 7632 RVA: 0x0006849D File Offset: 0x0006669D
			public UnionCase(int tag, string name, PropertyInfo[] fields, FSharpFunction fieldReader, FSharpFunction constructor)
			{
				this.Tag = tag;
				this.Name = name;
				this.Fields = fields;
				this.FieldReader = fieldReader;
				this.Constructor = constructor;
			}

			// Token: 0x04001088 RID: 4232
			public readonly int Tag;

			// Token: 0x04001089 RID: 4233
			public readonly string Name;

			// Token: 0x0400108A RID: 4234
			public readonly PropertyInfo[] Fields;

			// Token: 0x0400108B RID: 4235
			public readonly FSharpFunction FieldReader;

			// Token: 0x0400108C RID: 4236
			public readonly FSharpFunction Constructor;
		}
	}
}
