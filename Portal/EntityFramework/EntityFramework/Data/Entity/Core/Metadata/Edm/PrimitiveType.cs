using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EB RID: 1259
	public class PrimitiveType : SimpleType
	{
		// Token: 0x06003EA0 RID: 16032 RVA: 0x000D08DF File Offset: 0x000CEADF
		internal PrimitiveType()
		{
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000D08E7 File Offset: 0x000CEAE7
		internal PrimitiveType(string name, string namespaceName, DataSpace dataSpace, PrimitiveType baseType, DbProviderManifest providerManifest)
			: base(name, namespaceName, dataSpace)
		{
			Check.NotNull<PrimitiveType>(baseType, "baseType");
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			this.BaseType = baseType;
			PrimitiveType.Initialize(this, baseType.PrimitiveTypeKind, providerManifest);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000D0923 File Offset: 0x000CEB23
		internal PrimitiveType(Type clrType, PrimitiveType baseType, DbProviderManifest providerManifest)
			: this(Check.NotNull<Type>(clrType, "clrType").Name, clrType.NestingNamespace(), DataSpace.OSpace, baseType, providerManifest)
		{
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000D0944 File Offset: 0x000CEB44
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.PrimitiveType;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000D0948 File Offset: 0x000CEB48
		internal override Type ClrType
		{
			get
			{
				return this.ClrEquivalentType;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000D0950 File Offset: 0x000CEB50
		// (set) Token: 0x06003EA6 RID: 16038 RVA: 0x000D0958 File Offset: 0x000CEB58
		[MetadataProperty(BuiltInTypeKind.PrimitiveTypeKind, false)]
		public virtual PrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return this._primitiveTypeKind;
			}
			internal set
			{
				this._primitiveTypeKind = value;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000D0961 File Offset: 0x000CEB61
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x000D0969 File Offset: 0x000CEB69
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
			set
			{
				this._providerManifest = value;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000D0972 File Offset: 0x000CEB72
		public virtual ReadOnlyCollection<FacetDescription> FacetDescriptions
		{
			get
			{
				return this.ProviderManifest.GetFacetDescriptions(this);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003EAA RID: 16042 RVA: 0x000D0980 File Offset: 0x000CEB80
		public Type ClrEquivalentType
		{
			get
			{
				switch (this.PrimitiveTypeKind)
				{
				case PrimitiveTypeKind.Binary:
					return typeof(byte[]);
				case PrimitiveTypeKind.Boolean:
					return typeof(bool);
				case PrimitiveTypeKind.Byte:
					return typeof(byte);
				case PrimitiveTypeKind.DateTime:
					return typeof(DateTime);
				case PrimitiveTypeKind.Decimal:
					return typeof(decimal);
				case PrimitiveTypeKind.Double:
					return typeof(double);
				case PrimitiveTypeKind.Guid:
					return typeof(Guid);
				case PrimitiveTypeKind.Single:
					return typeof(float);
				case PrimitiveTypeKind.SByte:
					return typeof(sbyte);
				case PrimitiveTypeKind.Int16:
					return typeof(short);
				case PrimitiveTypeKind.Int32:
					return typeof(int);
				case PrimitiveTypeKind.Int64:
					return typeof(long);
				case PrimitiveTypeKind.String:
					return typeof(string);
				case PrimitiveTypeKind.Time:
					return typeof(TimeSpan);
				case PrimitiveTypeKind.DateTimeOffset:
					return typeof(DateTimeOffset);
				case PrimitiveTypeKind.Geometry:
				case PrimitiveTypeKind.GeometryPoint:
				case PrimitiveTypeKind.GeometryLineString:
				case PrimitiveTypeKind.GeometryPolygon:
				case PrimitiveTypeKind.GeometryMultiPoint:
				case PrimitiveTypeKind.GeometryMultiLineString:
				case PrimitiveTypeKind.GeometryMultiPolygon:
				case PrimitiveTypeKind.GeometryCollection:
					return typeof(DbGeometry);
				case PrimitiveTypeKind.Geography:
				case PrimitiveTypeKind.GeographyPoint:
				case PrimitiveTypeKind.GeographyLineString:
				case PrimitiveTypeKind.GeographyPolygon:
				case PrimitiveTypeKind.GeographyMultiPoint:
				case PrimitiveTypeKind.GeographyMultiLineString:
				case PrimitiveTypeKind.GeographyMultiPolygon:
				case PrimitiveTypeKind.GeographyCollection:
					return typeof(DbGeography);
				case PrimitiveTypeKind.HierarchyId:
					return typeof(HierarchyId);
				default:
					return null;
				}
			}
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x000D0AE6 File Offset: 0x000CECE6
		internal override IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return base.GetAssociatedFacetDescriptions().Concat(this.FacetDescriptions);
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x000D0AF9 File Offset: 0x000CECF9
		internal static void Initialize(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, DbProviderManifest providerManifest)
		{
			primitiveType._primitiveTypeKind = primitiveTypeKind;
			primitiveType._providerManifest = providerManifest;
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x000D0B09 File Offset: 0x000CED09
		public EdmType GetEdmPrimitiveType()
		{
			return MetadataItem.EdmProviderManifest.GetPrimitiveType(this.PrimitiveTypeKind);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x000D0B1B File Offset: 0x000CED1B
		public static ReadOnlyCollection<PrimitiveType> GetEdmPrimitiveTypes()
		{
			return MetadataItem.EdmProviderManifest.GetStoreTypes();
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x000D0B27 File Offset: 0x000CED27
		public static PrimitiveType GetEdmPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			return MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x04001545 RID: 5445
		private PrimitiveTypeKind _primitiveTypeKind;

		// Token: 0x04001546 RID: 5446
		private DbProviderManifest _providerManifest;
	}
}
