using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Reflection;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000B0 RID: 176
	public class ColumnModel : PropertyModel
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0001FE92 File Offset: 0x0001E092
		public ColumnModel(PrimitiveTypeKind type)
			: this(type, null)
		{
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0001FE9C File Offset: 0x0001E09C
		public ColumnModel(PrimitiveTypeKind type, TypeUsage typeUsage)
			: base(type, typeUsage)
		{
			this._clrType = PrimitiveType.GetEdmPrimitiveType(type).ClrEquivalentType;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0001FEC2 File Offset: 0x0001E0C2
		public virtual Type ClrType
		{
			get
			{
				return this._clrType;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0001FECC File Offset: 0x0001E0CC
		public virtual object ClrDefaultValue
		{
			get
			{
				if (this._clrType.IsValueType())
				{
					return Activator.CreateInstance(this._clrType);
				}
				if (this._clrType == typeof(string))
				{
					return string.Empty;
				}
				if (this._clrType == typeof(DbGeography))
				{
					return DbGeography.FromText("POINT(0 0)");
				}
				if (this._clrType == typeof(DbGeometry))
				{
					return DbGeometry.FromText("POINT(0 0)");
				}
				return new byte[0];
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0001FF59 File Offset: 0x0001E159
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0001FF61 File Offset: 0x0001E161
		public virtual bool? IsNullable { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0001FF6A File Offset: 0x0001E16A
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x0001FF72 File Offset: 0x0001E172
		public virtual bool IsIdentity { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0001FF7B File Offset: 0x0001E17B
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x0001FF83 File Offset: 0x0001E183
		public virtual bool IsTimestamp { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0001FF8C File Offset: 0x0001E18C
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0001FF94 File Offset: 0x0001E194
		public IDictionary<string, AnnotationValues> Annotations
		{
			get
			{
				return this._annotations;
			}
			set
			{
				this._annotations = value ?? new Dictionary<string, AnnotationValues>();
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0001FFA6 File Offset: 0x0001E1A6
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0001FFAE File Offset: 0x0001E1AE
		internal PropertyInfo ApiPropertyInfo
		{
			get
			{
				return this._apiPropertyInfo;
			}
			set
			{
				this._apiPropertyInfo = value;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		public bool IsNarrowerThan(ColumnModel column, DbProviderManifest providerManifest)
		{
			Check.NotNull<ColumnModel>(column, "column");
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			TypeUsage storeType = providerManifest.GetStoreType(base.TypeUsage);
			TypeUsage storeType2 = providerManifest.GetStoreType(column.TypeUsage);
			return ColumnModel._typeSize[this.Type] < ColumnModel._typeSize[column.Type] || (!(this.IsUnicode ?? true) && (column.IsUnicode ?? true)) || (!(this.IsNullable ?? true) && (column.IsNullable ?? true)) || ColumnModel.IsNarrowerThan(storeType, storeType2);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00020094 File Offset: 0x0001E294
		private static bool IsNarrowerThan(TypeUsage typeUsage, TypeUsage other)
		{
			foreach (string text in new string[] { "MaxLength", "Precision", "Scale" })
			{
				Facet facet;
				Facet facet2;
				if (typeUsage.Facets.TryGetValue(text, true, out facet) && other.Facets.TryGetValue(facet.Name, true, out facet2) && facet.Value != facet2.Value)
				{
					int num = Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture);
					int num2 = Convert.ToInt32(facet2.Value, CultureInfo.InvariantCulture);
					if (num < num2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00020134 File Offset: 0x0001E334
		internal override FacetValues ToFacetValues()
		{
			FacetValues facetValues = base.ToFacetValues();
			if (this.IsNullable != null)
			{
				facetValues.Nullable = new bool?(this.IsNullable.Value);
			}
			if (this.IsIdentity)
			{
				facetValues.StoreGeneratedPattern = new StoreGeneratedPattern?(StoreGeneratedPattern.Identity);
			}
			return facetValues;
		}

		// Token: 0x0400084D RID: 2125
		private readonly Type _clrType;

		// Token: 0x0400084E RID: 2126
		private PropertyInfo _apiPropertyInfo;

		// Token: 0x0400084F RID: 2127
		private IDictionary<string, AnnotationValues> _annotations = new Dictionary<string, AnnotationValues>();

		// Token: 0x04000853 RID: 2131
		private static readonly Dictionary<PrimitiveTypeKind, int> _typeSize = new Dictionary<PrimitiveTypeKind, int>
		{
			{
				PrimitiveTypeKind.Binary,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Boolean,
				1
			},
			{
				PrimitiveTypeKind.Byte,
				1
			},
			{
				PrimitiveTypeKind.DateTime,
				8
			},
			{
				PrimitiveTypeKind.DateTimeOffset,
				10
			},
			{
				PrimitiveTypeKind.Decimal,
				17
			},
			{
				PrimitiveTypeKind.Double,
				53
			},
			{
				PrimitiveTypeKind.Guid,
				16
			},
			{
				PrimitiveTypeKind.Int16,
				2
			},
			{
				PrimitiveTypeKind.Int32,
				4
			},
			{
				PrimitiveTypeKind.Int64,
				8
			},
			{
				PrimitiveTypeKind.SByte,
				1
			},
			{
				PrimitiveTypeKind.Single,
				4
			},
			{
				PrimitiveTypeKind.String,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Time,
				5
			},
			{
				PrimitiveTypeKind.HierarchyId,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Geometry,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.Geography,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryMultiPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryMultiLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryMultiPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeometryCollection,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyMultiPoint,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyMultiLineString,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyMultiPolygon,
				int.MaxValue
			},
			{
				PrimitiveTypeKind.GeographyCollection,
				int.MaxValue
			}
		};
	}
}
