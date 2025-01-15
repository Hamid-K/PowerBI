using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x02000519 RID: 1305
	internal class EdmProviderManifest : DbProviderManifest
	{
		// Token: 0x06004043 RID: 16451 RVA: 0x000D724E File Offset: 0x000D544E
		private EdmProviderManifest()
		{
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x000D7256 File Offset: 0x000D5456
		internal static EdmProviderManifest Instance
		{
			get
			{
				return EdmProviderManifest._instance;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000D725D File Offset: 0x000D545D
		public override string NamespaceName
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x000D7264 File Offset: 0x000D5464
		internal virtual string Token
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000D726B File Offset: 0x000D546B
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			this.InitializeCanonicalFunctions();
			return this._functions;
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x000D727C File Offset: 0x000D547C
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			this.InitializeFacetDescriptions();
			ReadOnlyCollection<FacetDescription> readOnlyCollection = null;
			if (this._facetDescriptions.TryGetValue(type as PrimitiveType, out readOnlyCollection))
			{
				return readOnlyCollection;
			}
			return Helper.EmptyFacetDescriptionEnumerable;
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x000D72AD File Offset: 0x000D54AD
		public PrimitiveType GetPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes[(int)primitiveTypeKind];
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x000D72C4 File Offset: 0x000D54C4
		private void InitializePrimitiveTypes()
		{
			if (this._primitiveTypes != null)
			{
				return;
			}
			PrimitiveType[] array = new PrimitiveType[32];
			array[0] = new PrimitiveType();
			array[1] = new PrimitiveType();
			array[2] = new PrimitiveType();
			array[3] = new PrimitiveType();
			array[4] = new PrimitiveType();
			array[5] = new PrimitiveType();
			array[7] = new PrimitiveType();
			array[6] = new PrimitiveType();
			array[31] = new PrimitiveType();
			array[9] = new PrimitiveType();
			array[10] = new PrimitiveType();
			array[11] = new PrimitiveType();
			array[8] = new PrimitiveType();
			array[12] = new PrimitiveType();
			array[13] = new PrimitiveType();
			array[14] = new PrimitiveType();
			array[15] = new PrimitiveType();
			array[17] = new PrimitiveType();
			array[18] = new PrimitiveType();
			array[19] = new PrimitiveType();
			array[20] = new PrimitiveType();
			array[21] = new PrimitiveType();
			array[22] = new PrimitiveType();
			array[23] = new PrimitiveType();
			array[16] = new PrimitiveType();
			array[24] = new PrimitiveType();
			array[25] = new PrimitiveType();
			array[26] = new PrimitiveType();
			array[27] = new PrimitiveType();
			array[28] = new PrimitiveType();
			array[29] = new PrimitiveType();
			array[30] = new PrimitiveType();
			this.InitializePrimitiveType(array[0], PrimitiveTypeKind.Binary, "Binary", typeof(byte[]));
			this.InitializePrimitiveType(array[1], PrimitiveTypeKind.Boolean, "Boolean", typeof(bool));
			this.InitializePrimitiveType(array[2], PrimitiveTypeKind.Byte, "Byte", typeof(byte));
			this.InitializePrimitiveType(array[3], PrimitiveTypeKind.DateTime, "DateTime", typeof(DateTime));
			this.InitializePrimitiveType(array[4], PrimitiveTypeKind.Decimal, "Decimal", typeof(decimal));
			this.InitializePrimitiveType(array[5], PrimitiveTypeKind.Double, "Double", typeof(double));
			this.InitializePrimitiveType(array[7], PrimitiveTypeKind.Single, "Single", typeof(float));
			this.InitializePrimitiveType(array[6], PrimitiveTypeKind.Guid, "Guid", typeof(Guid));
			this.InitializePrimitiveType(array[31], PrimitiveTypeKind.HierarchyId, "HierarchyId", typeof(HierarchyId));
			this.InitializePrimitiveType(array[9], PrimitiveTypeKind.Int16, "Int16", typeof(short));
			this.InitializePrimitiveType(array[10], PrimitiveTypeKind.Int32, "Int32", typeof(int));
			this.InitializePrimitiveType(array[11], PrimitiveTypeKind.Int64, "Int64", typeof(long));
			this.InitializePrimitiveType(array[8], PrimitiveTypeKind.SByte, "SByte", typeof(sbyte));
			this.InitializePrimitiveType(array[12], PrimitiveTypeKind.String, "String", typeof(string));
			this.InitializePrimitiveType(array[13], PrimitiveTypeKind.Time, "Time", typeof(TimeSpan));
			this.InitializePrimitiveType(array[14], PrimitiveTypeKind.DateTimeOffset, "DateTimeOffset", typeof(DateTimeOffset));
			this.InitializePrimitiveType(array[16], PrimitiveTypeKind.Geography, "Geography", typeof(DbGeography));
			this.InitializePrimitiveType(array[24], PrimitiveTypeKind.GeographyPoint, "GeographyPoint", typeof(DbGeography));
			this.InitializePrimitiveType(array[25], PrimitiveTypeKind.GeographyLineString, "GeographyLineString", typeof(DbGeography));
			this.InitializePrimitiveType(array[26], PrimitiveTypeKind.GeographyPolygon, "GeographyPolygon", typeof(DbGeography));
			this.InitializePrimitiveType(array[27], PrimitiveTypeKind.GeographyMultiPoint, "GeographyMultiPoint", typeof(DbGeography));
			this.InitializePrimitiveType(array[28], PrimitiveTypeKind.GeographyMultiLineString, "GeographyMultiLineString", typeof(DbGeography));
			this.InitializePrimitiveType(array[29], PrimitiveTypeKind.GeographyMultiPolygon, "GeographyMultiPolygon", typeof(DbGeography));
			this.InitializePrimitiveType(array[30], PrimitiveTypeKind.GeographyCollection, "GeographyCollection", typeof(DbGeography));
			this.InitializePrimitiveType(array[15], PrimitiveTypeKind.Geometry, "Geometry", typeof(DbGeometry));
			this.InitializePrimitiveType(array[17], PrimitiveTypeKind.GeometryPoint, "GeometryPoint", typeof(DbGeometry));
			this.InitializePrimitiveType(array[18], PrimitiveTypeKind.GeometryLineString, "GeometryLineString", typeof(DbGeometry));
			this.InitializePrimitiveType(array[19], PrimitiveTypeKind.GeometryPolygon, "GeometryPolygon", typeof(DbGeometry));
			this.InitializePrimitiveType(array[20], PrimitiveTypeKind.GeometryMultiPoint, "GeometryMultiPoint", typeof(DbGeometry));
			this.InitializePrimitiveType(array[21], PrimitiveTypeKind.GeometryMultiLineString, "GeometryMultiLineString", typeof(DbGeometry));
			this.InitializePrimitiveType(array[22], PrimitiveTypeKind.GeometryMultiPolygon, "GeometryMultiPolygon", typeof(DbGeometry));
			this.InitializePrimitiveType(array[23], PrimitiveTypeKind.GeometryCollection, "GeometryCollection", typeof(DbGeometry));
			foreach (PrimitiveType primitiveType in array)
			{
				primitiveType.ProviderManifest = this;
				primitiveType.SetReadOnly();
			}
			ReadOnlyCollection<PrimitiveType> readOnlyCollection = new ReadOnlyCollection<PrimitiveType>(array);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>>(ref this._primitiveTypes, readOnlyCollection, null);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000D777B File Offset: 0x000D597B
		private void InitializePrimitiveType(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, string name, Type clrType)
		{
			EdmType.Initialize(primitiveType, name, "Edm", DataSpace.CSpace, true, null);
			PrimitiveType.Initialize(primitiveType, primitiveTypeKind, this);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x000D7794 File Offset: 0x000D5994
		private void InitializeFacetDescriptions()
		{
			if (this._facetDescriptions != null)
			{
				return;
			}
			this.InitializePrimitiveTypes();
			Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> dictionary = new Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>();
			FacetDescription[] array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.String);
			PrimitiveType primitiveType = this._primitiveTypes[12];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Binary);
			primitiveType = this._primitiveTypes[0];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTime);
			primitiveType = this._primitiveTypes[3];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Time);
			primitiveType = this._primitiveTypes[13];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTimeOffset);
			primitiveType = this._primitiveTypes[14];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Decimal);
			primitiveType = this._primitiveTypes[4];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Geography);
			primitiveType = this._primitiveTypes[16];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyPoint);
			primitiveType = this._primitiveTypes[24];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyLineString);
			primitiveType = this._primitiveTypes[25];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyPolygon);
			primitiveType = this._primitiveTypes[26];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiPoint);
			primitiveType = this._primitiveTypes[27];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiLineString);
			primitiveType = this._primitiveTypes[28];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiPolygon);
			primitiveType = this._primitiveTypes[29];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyCollection);
			primitiveType = this._primitiveTypes[30];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Geometry);
			primitiveType = this._primitiveTypes[15];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryPoint);
			primitiveType = this._primitiveTypes[17];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryLineString);
			primitiveType = this._primitiveTypes[18];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryPolygon);
			primitiveType = this._primitiveTypes[19];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiPoint);
			primitiveType = this._primitiveTypes[20];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiLineString);
			primitiveType = this._primitiveTypes[21];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiPolygon);
			primitiveType = this._primitiveTypes[22];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			array = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryCollection);
			primitiveType = this._primitiveTypes[23];
			dictionary.Add(primitiveType, new ReadOnlyCollection<FacetDescription>(array));
			Interlocked.CompareExchange<Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>>(ref this._facetDescriptions, dictionary, null);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000D7AC0 File Offset: 0x000D5CC0
		internal static FacetDescription[] GetInitialFacetDescriptions(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return new FacetDescription[]
				{
					new FacetDescription("MaxLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), null),
					new FacetDescription("FixedLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null)
				};
			case PrimitiveTypeKind.DateTime:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), null)
				};
			case PrimitiveTypeKind.Decimal:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(1), new int?(255), null),
					new FacetDescription("Scale", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), null)
				};
			case PrimitiveTypeKind.String:
				return new FacetDescription[]
				{
					new FacetDescription("MaxLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), null),
					new FacetDescription("Unicode", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null),
					new FacetDescription("FixedLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null)
				};
			case PrimitiveTypeKind.Time:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), TypeUsage.DefaultDateTimePrecisionFacetValue)
				};
			case PrimitiveTypeKind.DateTimeOffset:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), TypeUsage.DefaultDateTimePrecisionFacetValue)
				};
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
				return new FacetDescription[]
				{
					new FacetDescription("SRID", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), DbGeometry.DefaultCoordinateSystemId),
					new FacetDescription("IsStrict", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true)
				};
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
				return new FacetDescription[]
				{
					new FacetDescription("SRID", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), DbGeography.DefaultCoordinateSystemId),
					new FacetDescription("IsStrict", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true)
				};
			}
			return null;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000D7E08 File Offset: 0x000D6008
		private void InitializeCanonicalFunctions()
		{
			if (this._functions != null)
			{
				return;
			}
			this.InitializePrimitiveTypes();
			EdmProviderManifestFunctionBuilder functions = new EdmProviderManifestFunctionBuilder(this._primitiveTypes);
			PrimitiveTypeKind[] array = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.SByte,
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.String,
				PrimitiveTypeKind.Binary,
				PrimitiveTypeKind.Time,
				PrimitiveTypeKind.DateTimeOffset
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Max", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Min", type);
			});
			PrimitiveTypeKind[] array2 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array2, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Avg", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array2, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Sum", type);
			});
			PrimitiveTypeKind[] array3 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array3, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "StDev", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array3, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "StDevP", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array3, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "Var", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array3, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "VarP", type);
			});
			EdmProviderManifestFunctionBuilder.ForAllBasePrimitiveTypes(delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Int32, "Count", type);
			});
			EdmProviderManifestFunctionBuilder.ForAllBasePrimitiveTypes(delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Int64, "BigCount", type);
			});
			functions.AddFunction(PrimitiveTypeKind.String, "Trim", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "RTrim", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "LTrim", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "Concat", PrimitiveTypeKind.String, "string1", PrimitiveTypeKind.String, "string2");
			functions.AddFunction(PrimitiveTypeKind.Int32, "Length", PrimitiveTypeKind.String, "stringArgument");
			PrimitiveTypeKind[] array4 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.SByte
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array4, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Substring", PrimitiveTypeKind.String, "stringArgument", type, "start", type, "length");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array4, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Left", PrimitiveTypeKind.String, "stringArgument", type, "length");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array4, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Right", PrimitiveTypeKind.String, "stringArgument", type, "length");
			});
			functions.AddFunction(PrimitiveTypeKind.String, "Replace", PrimitiveTypeKind.String, "stringArgument", PrimitiveTypeKind.String, "toReplace", PrimitiveTypeKind.String, "replacement");
			functions.AddFunction(PrimitiveTypeKind.Int32, "IndexOf", PrimitiveTypeKind.String, "searchString", PrimitiveTypeKind.String, "stringToFind");
			functions.AddFunction(PrimitiveTypeKind.String, "ToUpper", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "ToLower", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "Reverse", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "Contains", PrimitiveTypeKind.String, "searchedString", PrimitiveTypeKind.String, "searchedForString");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "StartsWith", PrimitiveTypeKind.String, "stringArgument", PrimitiveTypeKind.String, "prefix");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "EndsWith", PrimitiveTypeKind.String, "stringArgument", PrimitiveTypeKind.String, "suffix");
			PrimitiveTypeKind[] array5 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Year", type, "dateValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Month", type, "dateValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Day", type, "dateValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DayOfYear", type, "dateValue");
			});
			PrimitiveTypeKind[] array6 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Time
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Hour", type, "timeValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Minute", type, "timeValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Second", type, "timeValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Millisecond", type, "timeValue");
			});
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CurrentDateTime");
			functions.AddFunction(PrimitiveTypeKind.DateTimeOffset, "CurrentDateTimeOffset");
			functions.AddFunction(PrimitiveTypeKind.Int32, "GetTotalOffsetMinutes", PrimitiveTypeKind.DateTimeOffset, "dateTimeOffsetArgument");
			functions.AddFunction(PrimitiveTypeKind.DateTime, "LocalDateTime", PrimitiveTypeKind.DateTimeOffset, "dateTimeOffsetArgument");
			functions.AddFunction(PrimitiveTypeKind.DateTime, "UtcDateTime", PrimitiveTypeKind.DateTimeOffset, "dateTimeOffsetArgument");
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CurrentUtcDateTime");
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "TruncateTime", type, "dateValue");
			});
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CreateDateTime", PrimitiveTypeKind.Int32, "year", PrimitiveTypeKind.Int32, "month", PrimitiveTypeKind.Int32, "day", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second");
			functions.AddFunction(PrimitiveTypeKind.DateTimeOffset, "CreateDateTimeOffset", PrimitiveTypeKind.Int32, "year", PrimitiveTypeKind.Int32, "month", PrimitiveTypeKind.Int32, "day", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second", PrimitiveTypeKind.Int32, "timeZoneOffset");
			functions.AddFunction(PrimitiveTypeKind.Time, "CreateTime", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second");
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddYears", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMonths", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddDays", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddHours", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMinutes", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddSeconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMilliseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMicroseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddNanoseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffYears", type, "dateValue1", type, "dateValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMonths", type, "dateValue1", type, "dateValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array5, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffDays", type, "dateValue1", type, "dateValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffHours", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMinutes", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffSeconds", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMilliseconds", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMicroseconds", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array6, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffNanoseconds", type, "timeValue1", type, "timeValue2");
			});
			PrimitiveTypeKind[] array7 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array7, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Round", type, "value");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array7, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Floor", type, "value");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array7, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Ceiling", type, "value");
			});
			PrimitiveTypeKind[] array8 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array8, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Round", type, "value", PrimitiveTypeKind.Int32, "digits");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array8, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Truncate", type, "value", PrimitiveTypeKind.Int32, "digits");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.Single
			}, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Abs", type, "value");
			});
			PrimitiveTypeKind[] array9 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			PrimitiveTypeKind[] array10 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int64
			};
			foreach (PrimitiveTypeKind primitiveTypeKind in array9)
			{
				foreach (PrimitiveTypeKind primitiveTypeKind2 in array10)
				{
					functions.AddFunction(primitiveTypeKind, "Power", primitiveTypeKind, "baseArgument", primitiveTypeKind2, "exponent");
				}
			}
			PrimitiveTypeKind[] array13 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.Byte
			};
			EdmProviderManifestFunctionBuilder.ForTypes(array13, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseAnd", type, "value1", type, "value2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array13, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseOr", type, "value1", type, "value2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array13, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseXor", type, "value1", type, "value2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(array13, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseNot", type, "value");
			});
			functions.AddFunction(PrimitiveTypeKind.Guid, "NewGuid");
			EdmProviderManifestSpatialFunctions.AddFunctions(functions);
			EdmProviderManifestHierarchyIdFunctions.AddFunctions(functions);
			ReadOnlyCollection<EdmFunction> readOnlyCollection = functions.ToFunctionCollection();
			Interlocked.CompareExchange<ReadOnlyCollection<EdmFunction>>(ref this._functions, readOnlyCollection, null);
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000D85AE File Offset: 0x000D67AE
		internal ReadOnlyCollection<PrimitiveType> GetPromotionTypes(PrimitiveType primitiveType)
		{
			this.InitializePromotableTypes();
			return this._promotionTypes[(int)primitiveType.PrimitiveTypeKind];
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000D85C4 File Offset: 0x000D67C4
		private void InitializePromotableTypes()
		{
			if (this._promotionTypes != null)
			{
				return;
			}
			ReadOnlyCollection<PrimitiveType>[] array = new ReadOnlyCollection<PrimitiveType>[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[] { this._primitiveTypes[i] });
			}
			array[2] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[2],
				this._primitiveTypes[9],
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[9] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[9],
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[10] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[11] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[7] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			this.InitializeSpatialPromotionGroup(array, new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.GeographyPoint,
				PrimitiveTypeKind.GeographyLineString,
				PrimitiveTypeKind.GeographyPolygon,
				PrimitiveTypeKind.GeographyMultiPoint,
				PrimitiveTypeKind.GeographyMultiLineString,
				PrimitiveTypeKind.GeographyMultiPolygon,
				PrimitiveTypeKind.GeographyCollection
			}, PrimitiveTypeKind.Geography);
			this.InitializeSpatialPromotionGroup(array, new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.GeometryPoint,
				PrimitiveTypeKind.GeometryLineString,
				PrimitiveTypeKind.GeometryPolygon,
				PrimitiveTypeKind.GeometryMultiPoint,
				PrimitiveTypeKind.GeometryMultiLineString,
				PrimitiveTypeKind.GeometryMultiPolygon,
				PrimitiveTypeKind.GeometryCollection
			}, PrimitiveTypeKind.Geometry);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>[]>(ref this._promotionTypes, array, null);
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x000D8808 File Offset: 0x000D6A08
		private void InitializeSpatialPromotionGroup(ReadOnlyCollection<PrimitiveType>[] promotionTypes, PrimitiveTypeKind[] promotableKinds, PrimitiveTypeKind baseKind)
		{
			foreach (PrimitiveTypeKind primitiveTypeKind in promotableKinds)
			{
				promotionTypes[(int)primitiveTypeKind] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
				{
					this._primitiveTypes[(int)primitiveTypeKind],
					this._primitiveTypes[(int)baseKind]
				});
			}
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000D8855 File Offset: 0x000D6A55
		internal TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			if (EdmProviderManifest._canonicalModelTypes == null)
			{
				this.InitializeCanonicalModelTypes();
			}
			return EdmProviderManifest._canonicalModelTypes[(int)primitiveTypeKind];
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000D886C File Offset: 0x000D6A6C
		private void InitializeCanonicalModelTypes()
		{
			this.InitializePrimitiveTypes();
			TypeUsage[] array = new TypeUsage[32];
			for (int i = 0; i < 32; i++)
			{
				TypeUsage typeUsage = TypeUsage.CreateDefaultTypeUsage(this._primitiveTypes[i]);
				array[i] = typeUsage;
			}
			Interlocked.CompareExchange<TypeUsage[]>(ref EdmProviderManifest._canonicalModelTypes, array, null);
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x000D88B7 File Offset: 0x000D6AB7
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x000D88C5 File Offset: 0x000D6AC5
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			Check.NotNull<TypeUsage>(storeType, "storeType");
			throw new NotImplementedException();
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000D88D8 File Offset: 0x000D6AD8
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			Check.NotNull<TypeUsage>(edmType, "edmType");
			throw new NotImplementedException();
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x000D88EC File Offset: 0x000D6AEC
		internal TypeUsage ForgetScalarConstraints(TypeUsage type)
		{
			PrimitiveType primitiveType = type.EdmType as PrimitiveType;
			if (primitiveType != null)
			{
				return this.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return type;
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000D8916 File Offset: 0x000D6B16
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001662 RID: 5730
		internal const string ConcurrencyModeFacetName = "ConcurrencyMode";

		// Token: 0x04001663 RID: 5731
		internal const string StoreGeneratedPatternFacetName = "StoreGeneratedPattern";

		// Token: 0x04001664 RID: 5732
		private Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions;

		// Token: 0x04001665 RID: 5733
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04001666 RID: 5734
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x04001667 RID: 5735
		private static readonly EdmProviderManifest _instance = new EdmProviderManifest();

		// Token: 0x04001668 RID: 5736
		private ReadOnlyCollection<PrimitiveType>[] _promotionTypes;

		// Token: 0x04001669 RID: 5737
		private static TypeUsage[] _canonicalModelTypes;

		// Token: 0x0400166A RID: 5738
		internal const byte MaximumDecimalPrecision = 255;

		// Token: 0x0400166B RID: 5739
		internal const byte MaximumDateTimePrecision = 255;
	}
}
