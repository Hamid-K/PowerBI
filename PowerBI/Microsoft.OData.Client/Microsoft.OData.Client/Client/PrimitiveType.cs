using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.Spatial;

namespace Microsoft.OData.Client
{
	// Token: 0x0200008B RID: 139
	internal sealed class PrimitiveType
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0000EB10 File Offset: 0x0000CD10
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Required")]
		static PrimitiveType()
		{
			PrimitiveType.InitializeTypes();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000EB5E File Offset: 0x0000CD5E
		private PrimitiveType(Type clrType, string edmTypeName, EdmPrimitiveTypeKind primitiveKind, PrimitiveTypeConverter typeConverter, bool hasReverseMapping)
		{
			this.ClrType = clrType;
			this.EdmTypeName = edmTypeName;
			this.PrimitiveKind = primitiveKind;
			this.TypeConverter = typeConverter;
			this.HasReverseMapping = hasReverseMapping;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000EB8B File Offset: 0x0000CD8B
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000EB93 File Offset: 0x0000CD93
		internal Type ClrType { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		internal string EdmTypeName { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000EBB5 File Offset: 0x0000CDB5
		internal PrimitiveTypeConverter TypeConverter { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000EBBE File Offset: 0x0000CDBE
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000EBC6 File Offset: 0x0000CDC6
		internal bool HasReverseMapping { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000EBCF File Offset: 0x0000CDCF
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000EBD7 File Offset: 0x0000CDD7
		internal EdmPrimitiveTypeKind PrimitiveKind { get; private set; }

		// Token: 0x06000440 RID: 1088 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		internal static bool TryGetPrimitiveType(Type clrType, out PrimitiveType ptype)
		{
			Type type = Nullable.GetUnderlyingType(clrType) ?? clrType;
			if (!PrimitiveType.TryGetWellKnownPrimitiveType(type, out ptype))
			{
				HashSet<Type> hashSet = PrimitiveType.knownNonPrimitiveTypes;
				lock (hashSet)
				{
					if (PrimitiveType.knownNonPrimitiveTypes.Contains(clrType))
					{
						ptype = null;
						return false;
					}
				}
				Dictionary<Type, PrimitiveType> dictionary = PrimitiveType.derivedPrimitiveTypeMapping;
				KeyValuePair<Type, PrimitiveType>[] array;
				lock (dictionary)
				{
					if (PrimitiveType.derivedPrimitiveTypeMapping.TryGetValue(clrType, out ptype))
					{
						return true;
					}
					array = PrimitiveType.clrMapping.Where((KeyValuePair<Type, PrimitiveType> m) => !m.Key.IsPrimitive() && !m.Key.IsSealed()).Concat(PrimitiveType.derivedPrimitiveTypeMapping).ToArray<KeyValuePair<Type, PrimitiveType>>();
				}
				KeyValuePair<Type, PrimitiveType> keyValuePair = new KeyValuePair<Type, PrimitiveType>(typeof(object), null);
				foreach (KeyValuePair<Type, PrimitiveType> keyValuePair2 in array)
				{
					if (type.IsSubclassOf(keyValuePair2.Key) && keyValuePair2.Key.IsSubclassOf(keyValuePair.Key))
					{
						keyValuePair = keyValuePair2;
					}
				}
				if (keyValuePair.Value == null)
				{
					HashSet<Type> hashSet2 = PrimitiveType.knownNonPrimitiveTypes;
					lock (hashSet2)
					{
						PrimitiveType.knownNonPrimitiveTypes.Add(clrType);
					}
					return false;
				}
				ptype = keyValuePair.Value;
				Dictionary<Type, PrimitiveType> dictionary2 = PrimitiveType.derivedPrimitiveTypeMapping;
				lock (dictionary2)
				{
					PrimitiveType.derivedPrimitiveTypeMapping[type] = ptype;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		internal static bool TryGetPrimitiveType(string edmTypeName, out PrimitiveType ptype)
		{
			return PrimitiveType.edmMapping.TryGetValue(edmTypeName, out ptype);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		internal static bool IsKnownType(Type type)
		{
			PrimitiveType primitiveType;
			return PrimitiveType.TryGetPrimitiveType(type, out primitiveType);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000EDD1 File Offset: 0x0000CFD1
		internal static bool IsKnownNullableType(Type type)
		{
			return PrimitiveType.IsKnownType(Nullable.GetUnderlyingType(type) ?? type);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000EDE3 File Offset: 0x0000CFE3
		internal static void DeleteKnownType(Type clrType, string edmTypeName)
		{
			PrimitiveType.clrMapping.Remove(clrType);
			if (edmTypeName != null)
			{
				PrimitiveType.edmMapping.Remove(edmTypeName);
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000EE00 File Offset: 0x0000D000
		internal static void RegisterKnownType(Type clrType, string edmTypeName, EdmPrimitiveTypeKind primitiveKind, PrimitiveTypeConverter converter, bool twoWay)
		{
			PrimitiveType primitiveType = new PrimitiveType(clrType, edmTypeName, primitiveKind, converter, twoWay);
			PrimitiveType.clrMapping.Add(clrType, primitiveType);
			if (twoWay)
			{
				PrimitiveType.edmMapping.Add(edmTypeName, primitiveType);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000EE35 File Offset: 0x0000D035
		internal IEdmPrimitiveType CreateEdmPrimitiveType()
		{
			return PrimitiveType.ClientEdmPrimitiveType.CreateType(this.PrimitiveKind);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000EE44 File Offset: 0x0000D044
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Coupling necessary for type table")]
		private static void InitializeTypes()
		{
			PrimitiveType.RegisterKnownType(typeof(bool), "Edm.Boolean", EdmPrimitiveTypeKind.Boolean, new BooleanTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(byte), "Edm.Byte", EdmPrimitiveTypeKind.Byte, new ByteTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(byte[]), "Edm.Binary", EdmPrimitiveTypeKind.Binary, new ByteArrayTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(DateTimeOffset), "Edm.DateTimeOffset", EdmPrimitiveTypeKind.DateTimeOffset, new DateTimeOffsetTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(decimal), "Edm.Decimal", EdmPrimitiveTypeKind.Decimal, new DecimalTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(double), "Edm.Double", EdmPrimitiveTypeKind.Double, new DoubleTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(Guid), "Edm.Guid", EdmPrimitiveTypeKind.Guid, new GuidTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(short), "Edm.Int16", EdmPrimitiveTypeKind.Int16, new Int16TypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(int), "Edm.Int32", EdmPrimitiveTypeKind.Int32, new Int32TypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(long), "Edm.Int64", EdmPrimitiveTypeKind.Int64, new Int64TypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(float), "Edm.Single", EdmPrimitiveTypeKind.Single, new SingleTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(string), "Edm.String", EdmPrimitiveTypeKind.String, new StringTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(sbyte), "Edm.SByte", EdmPrimitiveTypeKind.SByte, new SByteTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(TimeSpan), "Edm.Duration", EdmPrimitiveTypeKind.Duration, new TimeSpanTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(Geography), "Edm.Geography", EdmPrimitiveTypeKind.Geography, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyPoint), "Edm.GeographyPoint", EdmPrimitiveTypeKind.GeographyPoint, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyLineString), "Edm.GeographyLineString", EdmPrimitiveTypeKind.GeographyLineString, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyPolygon), "Edm.GeographyPolygon", EdmPrimitiveTypeKind.GeographyPolygon, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyCollection), "Edm.GeographyCollection", EdmPrimitiveTypeKind.GeographyCollection, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyMultiPoint), "Edm.GeographyMultiPoint", EdmPrimitiveTypeKind.GeographyMultiPoint, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyMultiLineString), "Edm.GeographyMultiLineString", EdmPrimitiveTypeKind.GeographyMultiLineString, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyMultiPolygon), "Edm.GeographyMultiPolygon", EdmPrimitiveTypeKind.GeographyMultiPolygon, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(Geometry), "Edm.Geometry", EdmPrimitiveTypeKind.Geometry, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryPoint), "Edm.GeometryPoint", EdmPrimitiveTypeKind.GeometryPoint, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryLineString), "Edm.GeometryLineString", EdmPrimitiveTypeKind.GeometryLineString, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryPolygon), "Edm.GeometryPolygon", EdmPrimitiveTypeKind.GeometryPolygon, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryCollection), "Edm.GeometryCollection", EdmPrimitiveTypeKind.GeometryCollection, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryMultiPoint), "Edm.GeometryMultiPoint", EdmPrimitiveTypeKind.GeometryMultiPoint, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryMultiLineString), "Edm.GeometryMultiLineString", EdmPrimitiveTypeKind.GeometryMultiLineString, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryMultiPolygon), "Edm.GeometryMultiPolygon", EdmPrimitiveTypeKind.GeometryMultiPolygon, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(DataServiceStreamLink), "Edm.Stream", EdmPrimitiveTypeKind.Stream, new NamedStreamTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(Date), "Edm.Date", EdmPrimitiveTypeKind.Date, new DateTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(TimeOfDay), "Edm.TimeOfDay", EdmPrimitiveTypeKind.TimeOfDay, new TimeOfDayConvert(), true);
			PrimitiveType.RegisterKnownType(typeof(char), "Edm.String", EdmPrimitiveTypeKind.String, new CharTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(char[]), "Edm.String", EdmPrimitiveTypeKind.String, new CharArrayTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(Type), "Edm.String", EdmPrimitiveTypeKind.String, new ClrTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(Uri), "Edm.String", EdmPrimitiveTypeKind.String, new UriTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(XDocument), "Edm.String", EdmPrimitiveTypeKind.String, new XDocumentTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(XElement), "Edm.String", EdmPrimitiveTypeKind.String, new XElementTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(ushort), null, EdmPrimitiveTypeKind.String, new UInt16TypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(uint), null, EdmPrimitiveTypeKind.String, new UInt32TypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(ulong), null, EdmPrimitiveTypeKind.String, new UInt64TypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(DateTime), null, EdmPrimitiveTypeKind.DateTimeOffset, new DateTimeTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(PrimitiveType.BinaryTypeSub), "Edm.Binary", EdmPrimitiveTypeKind.Binary, new BinaryTypeConverter(), false);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000F307 File Offset: 0x0000D507
		private static bool TryGetWellKnownPrimitiveType(Type clrType, out PrimitiveType ptype)
		{
			ptype = null;
			if (!PrimitiveType.clrMapping.TryGetValue(clrType, out ptype) && PrimitiveType.IsBinaryType(clrType))
			{
				ptype = PrimitiveType.clrMapping[typeof(PrimitiveType.BinaryTypeSub)];
			}
			return ptype != null;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000F340 File Offset: 0x0000D540
		private static bool IsBinaryType(Type type)
		{
			if (BinaryTypeConverter.BinaryType == null && type.Name == "Binary" && type.Namespace == "System.Data.Linq" && AssemblyName.ReferenceMatchesDefinition(type.Assembly.GetName(), new AssemblyName("System.Data.Linq")))
			{
				BinaryTypeConverter.BinaryType = type;
			}
			return type == BinaryTypeConverter.BinaryType;
		}

		// Token: 0x0400012F RID: 303
		private static readonly Dictionary<Type, PrimitiveType> clrMapping = new Dictionary<Type, PrimitiveType>(EqualityComparer<Type>.Default);

		// Token: 0x04000130 RID: 304
		private static readonly Dictionary<Type, PrimitiveType> derivedPrimitiveTypeMapping = new Dictionary<Type, PrimitiveType>(EqualityComparer<Type>.Default);

		// Token: 0x04000131 RID: 305
		private static readonly Dictionary<string, PrimitiveType> edmMapping = new Dictionary<string, PrimitiveType>(StringComparer.Ordinal);

		// Token: 0x04000132 RID: 306
		private static readonly HashSet<Type> knownNonPrimitiveTypes = new HashSet<Type>(EqualityComparer<Type>.Default);

		// Token: 0x02000171 RID: 369
		private sealed class BinaryTypeSub
		{
		}

		// Token: 0x02000172 RID: 370
		private class ClientEdmPrimitiveType : EdmType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
		{
			// Token: 0x06000D8C RID: 3468 RVA: 0x0002EC34 File Offset: 0x0002CE34
			private ClientEdmPrimitiveType(string namespaceName, string name, EdmPrimitiveTypeKind primitiveKind)
			{
				this.namespaceName = namespaceName;
				this.name = name;
				this.primitiveKind = primitiveKind;
				this.fullName = this.namespaceName + "." + this.name;
			}

			// Token: 0x17000356 RID: 854
			// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0002EC6D File Offset: 0x0002CE6D
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000357 RID: 855
			// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0002EC75 File Offset: 0x0002CE75
			public string Namespace
			{
				get
				{
					return this.namespaceName;
				}
			}

			// Token: 0x17000358 RID: 856
			// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0002EC7D File Offset: 0x0002CE7D
			public string FullName
			{
				get
				{
					return this.fullName;
				}
			}

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0002EC85 File Offset: 0x0002CE85
			public EdmPrimitiveTypeKind PrimitiveKind
			{
				get
				{
					return this.primitiveKind;
				}
			}

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00004A70 File Offset: 0x00002C70
			public EdmSchemaElementKind SchemaElementKind
			{
				get
				{
					return EdmSchemaElementKind.TypeDefinition;
				}
			}

			// Token: 0x1700035B RID: 859
			// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00004A70 File Offset: 0x00002C70
			public override EdmTypeKind TypeKind
			{
				get
				{
					return EdmTypeKind.Primitive;
				}
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x0002EC8D File Offset: 0x0002CE8D
			public static IEdmPrimitiveType CreateType(EdmPrimitiveTypeKind primitiveKind)
			{
				return new PrimitiveType.ClientEdmPrimitiveType("Edm", primitiveKind.ToString(), primitiveKind);
			}

			// Token: 0x04000727 RID: 1831
			private readonly string namespaceName;

			// Token: 0x04000728 RID: 1832
			private readonly string name;

			// Token: 0x04000729 RID: 1833
			private readonly string fullName;

			// Token: 0x0400072A RID: 1834
			private readonly EdmPrimitiveTypeKind primitiveKind;
		}
	}
}
