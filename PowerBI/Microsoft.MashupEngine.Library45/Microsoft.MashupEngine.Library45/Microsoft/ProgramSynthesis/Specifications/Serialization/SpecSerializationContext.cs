using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Specifications.Serialization
{
	// Token: 0x02000377 RID: 887
	public abstract class SpecSerializationContext
	{
		// Token: 0x060013C3 RID: 5059 RVA: 0x00039AD4 File Offset: 0x00037CD4
		private static XElement BuiltInSerializer<T>(T value)
		{
			if (typeof(T) == typeof(string) && value.Equals(null))
			{
				return new XElement(SpecSerializationContext.GetCanonicalTypeName(typeof(string))).WithAttribute("IsNull", true);
			}
			if (typeof(T) == typeof(DateTime) && value is DateTime)
			{
				DateTime dateTime = value as DateTime;
				return new XElement(SpecSerializationContext.GetCanonicalTypeName(typeof(DateTime)), dateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			return new XElement(SpecSerializationContext.GetCanonicalTypeName(typeof(T)), value);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00039BBC File Offset: 0x00037DBC
		private static object BuiltInDeserializer(XElement value)
		{
			XAttribute xattribute = value.Attribute("ObjectTypeName");
			string text = ((xattribute != null) ? xattribute.Value : null);
			string value2 = value.Value;
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException();
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(byte)))
			{
				return byte.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(int)))
			{
				return int.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(short)))
			{
				return short.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(long)))
			{
				return long.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(uint)))
			{
				return uint.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(ushort)))
			{
				return ushort.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(ulong)))
			{
				return ulong.Parse(value2);
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(byte?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new byte?(byte.Parse(value2));
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(int?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new int?(int.Parse(value2));
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(short?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new short?(short.Parse(value2));
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(long?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new long?(long.Parse(value2));
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(uint?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new uint?(uint.Parse(value2));
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(ushort?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new ushort?(ushort.Parse(value2));
			}
			if (text == SpecSerializationContext.GetCanonicalTypeName(typeof(ulong?)))
			{
				return string.IsNullOrEmpty(value2) ? null : new ulong?(ulong.Parse(value2));
			}
			if (!(text == SpecSerializationContext.GetCanonicalTypeName(typeof(string))))
			{
				foreach (Type type in StdLiteralParsing.KnownTypes)
				{
					if (text == SpecSerializationContext.GetCanonicalTypeName(type))
					{
						return StdLiteralParsing.TryParse(value2, type, default(DeserializationContext)).Value;
					}
				}
				throw new NotImplementedException();
			}
			XAttribute xattribute2 = value.Attribute("IsNull");
			if (((xattribute2 != null) ? xattribute2.Value : null) == "true")
			{
				return null;
			}
			return value2;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00039F4C File Offset: 0x0003814C
		private static List<Type> BuiltInSerializationTypes
		{
			get
			{
				return SpecSerializationContext.BuiltInSerializationTypesLazy.Value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00039F58 File Offset: 0x00038158
		private static IReadOnlyDictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> BuiltInSerializers
		{
			get
			{
				return SpecSerializationContext.BuiltInSerializersLazy.Value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00039F64 File Offset: 0x00038164
		private static IReadOnlyDictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> BuiltInDeserializers
		{
			get
			{
				return SpecSerializationContext.BuiltInDeserializersLazy.Value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00039F70 File Offset: 0x00038170
		public IReadOnlyDictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> Deserializers { get; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00039F78 File Offset: 0x00038178
		public IReadOnlyDictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> Serializers { get; }

		// Token: 0x060013CA RID: 5066 RVA: 0x00039F80 File Offset: 0x00038180
		protected SpecSerializationContext(Grammar grammar, IReadOnlyDictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> deserializers, IReadOnlyDictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> serializers = null)
		{
			Dictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> dictionary = SpecSerializationContext.BuiltInDeserializers.ToDictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>>();
			foreach (KeyValuePair<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> keyValuePair in deserializers)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			this.Deserializers = dictionary;
			Dictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> dictionary2 = SpecSerializationContext.BuiltInSerializers.ToDictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>>();
			foreach (KeyValuePair<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> keyValuePair2 in (serializers ?? Enumerable.Empty<KeyValuePair<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>>>()))
			{
				dictionary2[keyValuePair2.Key] = keyValuePair2.Value;
			}
			this.Serializers = dictionary2;
			this.Grammar = grammar;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0003A060 File Offset: 0x00038260
		public Grammar Grammar { get; }

		// Token: 0x060013CC RID: 5068 RVA: 0x0003A068 File Offset: 0x00038268
		public object DeserializeObject(XElement valueNode, Dictionary<int, object> identityCache)
		{
			if (valueNode.Name == "Null")
			{
				return null;
			}
			if (valueNode.Name == "Reference")
			{
				int num = int.Parse(valueNode.Value);
				return identityCache[num];
			}
			object obj = this.DeserializeObjectImpl(valueNode, identityCache);
			XAttribute xattribute = valueNode.Attribute("ObjectID");
			string text = ((xattribute != null) ? xattribute.Value : null);
			int num2;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num2))
			{
				throw new InvalidOperationException();
			}
			identityCache[num2] = obj;
			return obj;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003A100 File Offset: 0x00038300
		private object DeserializeObjectImpl(XElement valueNode, Dictionary<int, object> identityCache)
		{
			XAttribute xattribute = valueNode.Attribute("ObjectTypeName");
			string text = ((xattribute != null) ? xattribute.Value : null);
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> deserializer;
			if (!string.IsNullOrEmpty(text) && this.Deserializers.TryGetValue(text, out deserializer))
			{
				return deserializer(valueNode, identityCache, this);
			}
			deserializer = this.GetDeserializer(valueNode);
			if (deserializer != null)
			{
				return deserializer(valueNode, identityCache, this);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00002188 File Offset: 0x00000388
		protected virtual Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> GetDeserializer(XElement valueNode)
		{
			return null;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00002188 File Offset: 0x00000388
		protected virtual Func<object, Dictionary<object, int>, SpecSerializationContext, XElement> GetSerializer(object value)
		{
			return null;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003A168 File Offset: 0x00038368
		public XElement SerializeObject(object value, Dictionary<object, int> identityCache)
		{
			if (value == null)
			{
				return new XElement("Null");
			}
			int num;
			if (identityCache.TryGetValue(value, out num))
			{
				return new XElement("Reference", num);
			}
			XElement xelement = this.SerializeObjectImpl(value, identityCache).WithAttribute("ObjectID", identityCache.Count).WithAttribute("ObjectTypeName", SpecSerializationContext.GetCanonicalTypeName(value.GetType()));
			identityCache[value] = identityCache.Count;
			return xelement;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0003A1E8 File Offset: 0x000383E8
		private XElement SerializeObjectImpl(object value, Dictionary<object, int> identityCache)
		{
			string typeNameAttribute = this.GetTypeNameAttribute(value);
			Func<object, Dictionary<object, int>, SpecSerializationContext, XElement> serializer;
			if (this.Serializers.TryGetValue(typeNameAttribute, out serializer))
			{
				return serializer(value, identityCache, this);
			}
			serializer = this.GetSerializer(value);
			if (serializer != null)
			{
				return serializer(value, identityCache, this);
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Serialization of type {0} is not supported.", new object[] { SpecSerializationContext.GetCanonicalTypeName(value.GetType()) })));
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0003A254 File Offset: 0x00038454
		public TSpec DeserializeSpec<TSpec>(XElement specNode, Dictionary<int, object> identityCache) where TSpec : Spec
		{
			return (TSpec)((object)this.DeserializeSpec(specNode, identityCache));
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0003A263 File Offset: 0x00038463
		public Spec DeserializeSpec(XElement specNode, Dictionary<int, object> identityCache)
		{
			return (Spec)this.DeserializeObject(specNode, identityCache);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0003A272 File Offset: 0x00038472
		public Symbol Symbol(string symbolName)
		{
			return this.Grammar.Symbol(symbolName);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0003A280 File Offset: 0x00038480
		protected virtual string GetTypeNameAttribute(object value)
		{
			return SpecSerializationContext.GetCanonicalTypeName(value.GetType());
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003A290 File Offset: 0x00038490
		protected static string GetCanonicalTypeName(Type type)
		{
			if (type.GenericTypeArguments.Any<Type>())
			{
				string text = "{0}<{1}>";
				object[] array = new object[2];
				array[0] = type.Name;
				int num = 1;
				string text2 = ", ";
				IEnumerable<Type> genericTypeArguments = type.GenericTypeArguments;
				Func<Type, string> func;
				if ((func = SpecSerializationContext.<>O.<0>__GetCanonicalTypeName) == null)
				{
					func = (SpecSerializationContext.<>O.<0>__GetCanonicalTypeName = new Func<Type, string>(SpecSerializationContext.GetCanonicalTypeName));
				}
				array[num] = string.Join(text2, genericTypeArguments.Select(func));
				return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
			}
			return type.Name;
		}

		// Token: 0x040009DA RID: 2522
		private static readonly Lazy<List<Type>> BuiltInSerializationTypesLazy = new Lazy<List<Type>>(() => new List<Type>
		{
			typeof(byte),
			typeof(int),
			typeof(short),
			typeof(long),
			typeof(uint),
			typeof(ushort),
			typeof(ulong),
			typeof(decimal),
			typeof(double),
			typeof(byte?),
			typeof(int?),
			typeof(short?),
			typeof(long?),
			typeof(uint?),
			typeof(ushort?),
			typeof(ulong?),
			typeof(decimal?),
			typeof(double?),
			typeof(DateTime),
			typeof(string)
		});

		// Token: 0x040009DB RID: 2523
		private static readonly Lazy<IReadOnlyDictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>>> BuiltInSerializersLazy = new Lazy<IReadOnlyDictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>>>(() => SpecSerializationContext.BuiltInSerializationTypes.ToDictionary(new Func<Type, string>(SpecSerializationContext.GetCanonicalTypeName), (Type type) => (object node, Dictionary<object, int> idCache, SpecSerializationContext ctx) => SpecSerializationContext.BuiltInSerializer<object>(node)));

		// Token: 0x040009DC RID: 2524
		private static readonly Lazy<IReadOnlyDictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>>> BuiltInDeserializersLazy = new Lazy<IReadOnlyDictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>>>(() => SpecSerializationContext.BuiltInSerializationTypes.ToDictionary(new Func<Type, string>(SpecSerializationContext.GetCanonicalTypeName), (Type type) => (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => SpecSerializationContext.BuiltInDeserializer(node)));

		// Token: 0x02000378 RID: 888
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040009E0 RID: 2528
			public static Func<Type, string> <0>__GetCanonicalTypeName;
		}
	}
}
