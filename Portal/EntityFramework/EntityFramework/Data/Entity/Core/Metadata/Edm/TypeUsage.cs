using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000500 RID: 1280
	[DebuggerDisplay("EdmType={EdmType}, Facets.Count={Facets.Count}")]
	public class TypeUsage : MetadataItem
	{
		// Token: 0x06003F5F RID: 16223 RVA: 0x000D2DF5 File Offset: 0x000D0FF5
		internal TypeUsage()
		{
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000D2DFD File Offset: 0x000D0FFD
		private TypeUsage(EdmType edmType)
			: base(MetadataItem.MetadataFlags.Readonly)
		{
			Check.NotNull<EdmType>(edmType, "edmType");
			this._edmType = edmType;
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000D2E1C File Offset: 0x000D101C
		private TypeUsage(EdmType edmType, IEnumerable<Facet> facets)
			: this(edmType)
		{
			MetadataCollection<Facet> metadataCollection = MetadataCollection<Facet>.Wrap(facets.ToList<Facet>());
			metadataCollection.SetReadOnly();
			this._facets = metadataCollection.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000D2E4F File Offset: 0x000D104F
		internal static TypeUsage Create(EdmType edmType)
		{
			return new TypeUsage(edmType);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000D2E57 File Offset: 0x000D1057
		internal static TypeUsage Create(EdmType edmType, FacetValues values)
		{
			return new TypeUsage(edmType, TypeUsage.GetDefaultFacetDescriptionsAndOverrideFacetValues(edmType, values));
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000D2E66 File Offset: 0x000D1066
		public static TypeUsage Create(EdmType edmType, IEnumerable<Facet> facets)
		{
			return new TypeUsage(edmType, facets);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000D2E6F File Offset: 0x000D106F
		internal TypeUsage ShallowCopy(FacetValues facetValues)
		{
			return TypeUsage.Create(this._edmType, TypeUsage.OverrideFacetValues(this.Facets, facetValues));
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000D2E88 File Offset: 0x000D1088
		internal TypeUsage ShallowCopy(params Facet[] facetValues)
		{
			return TypeUsage.Create(this._edmType, TypeUsage.OverrideFacetValues(this.Facets, facetValues));
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000D2EA1 File Offset: 0x000D10A1
		private static IEnumerable<Facet> OverrideFacetValues(IEnumerable<Facet> facets, IEnumerable<Facet> facetValues)
		{
			return facets.Except(facetValues, (Facet f1, Facet f2) => f1.EdmEquals(f2)).Union(facetValues);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000D2ECF File Offset: 0x000D10CF
		public static TypeUsage CreateDefaultTypeUsage(EdmType edmType)
		{
			Check.NotNull<EdmType>(edmType, "edmType");
			return TypeUsage.Create(edmType);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000D2EE4 File Offset: 0x000D10E4
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength, int maxLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw new ArgumentException(Strings.NotStringTypeForTypeUsage);
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000D2F58 File Offset: 0x000D1158
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw new ArgumentException(Strings.NotStringTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x000D2FC4 File Offset: 0x000D11C4
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength, int maxLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw new ArgumentException(Strings.NotBinaryTypeForTypeUsage);
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000D3024 File Offset: 0x000D1224
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw new ArgumentException(Strings.NotBinaryTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x000D307C File Offset: 0x000D127C
		public static TypeUsage CreateDateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTime)
			{
				throw new ArgumentException(Strings.NotDateTimeTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000D30B5 File Offset: 0x000D12B5
		public static TypeUsage CreateDateTimeOffsetTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTimeOffset)
			{
				throw new ArgumentException(Strings.NotDateTimeOffsetTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x000D30EF File Offset: 0x000D12EF
		public static TypeUsage CreateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Time)
			{
				throw new ArgumentException(Strings.NotTimeTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000D312C File Offset: 0x000D132C
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType, byte precision, byte scale)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw new ArgumentException(Strings.NotDecimalTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = new byte?(precision),
				Scale = new byte?(scale)
			});
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000D3188 File Offset: 0x000D1388
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw new ArgumentException(Strings.NotDecimalTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = TypeUsage.DefaultPrecisionFacetValue,
				Scale = TypeUsage.DefaultScaleFacetValue
			});
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x000D31E0 File Offset: 0x000D13E0
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.TypeUsage;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000D31E4 File Offset: 0x000D13E4
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public virtual EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000D31EC File Offset: 0x000D13EC
		[MetadataProperty(BuiltInTypeKind.Facet, true)]
		public virtual ReadOnlyMetadataCollection<Facet> Facets
		{
			get
			{
				if (this._facets == null)
				{
					MetadataCollection<Facet> metadataCollection = new MetadataCollection<Facet>(this.GetFacets());
					metadataCollection.SetReadOnly();
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<Facet>>(ref this._facets, metadataCollection.AsReadOnlyMetadataCollection(), null);
				}
				return this._facets;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000D3230 File Offset: 0x000D1430
		public TypeUsage ModelTypeUsage
		{
			get
			{
				if (this._modelTypeUsage == null)
				{
					EdmType edmType = this.EdmType;
					if (edmType.DataSpace == DataSpace.CSpace || edmType.DataSpace == DataSpace.OSpace)
					{
						return this;
					}
					TypeUsage typeUsage;
					if (Helper.IsRowType(edmType))
					{
						RowType rowType = (RowType)edmType;
						EdmProperty[] array = new EdmProperty[rowType.Properties.Count];
						for (int i = 0; i < array.Length; i++)
						{
							EdmProperty edmProperty = rowType.Properties[i];
							TypeUsage modelTypeUsage = edmProperty.TypeUsage.ModelTypeUsage;
							array[i] = new EdmProperty(edmProperty.Name, modelTypeUsage);
						}
						typeUsage = TypeUsage.Create(new RowType(array, rowType.InitializerMetadata), this.Facets);
					}
					else if (Helper.IsCollectionType(edmType))
					{
						typeUsage = TypeUsage.Create(new CollectionType(((CollectionType)edmType).TypeUsage.ModelTypeUsage), this.Facets);
					}
					else if (Helper.IsPrimitiveType(edmType))
					{
						typeUsage = ((PrimitiveType)edmType).ProviderManifest.GetEdmType(this);
						if (typeUsage == null)
						{
							throw new ProviderIncompatibleException(Strings.Mapping_ProviderReturnsNullType(this.ToString()));
						}
						if (!TypeSemantics.IsNullable(this))
						{
							typeUsage = TypeUsage.Create(typeUsage.EdmType, TypeUsage.OverrideFacetValues(typeUsage.Facets, new FacetValues
							{
								Nullable = new bool?(false)
							}));
						}
					}
					else
					{
						if (!Helper.IsEntityTypeBase(edmType) && !Helper.IsComplexType(edmType))
						{
							return null;
						}
						typeUsage = this;
					}
					Interlocked.CompareExchange<TypeUsage>(ref this._modelTypeUsage, typeUsage, null);
				}
				return this._modelTypeUsage;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000D339A File Offset: 0x000D159A
		public bool IsSubtypeOf(TypeUsage typeUsage)
		{
			return this.EdmType != null && typeUsage != null && this.EdmType.IsSubtypeOf(typeUsage.EdmType);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000D33BA File Offset: 0x000D15BA
		private IEnumerable<Facet> GetFacets()
		{
			return from facetDescription in this._edmType.GetAssociatedFacetDescriptions()
				select facetDescription.DefaultValueFacet;
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000D33EB File Offset: 0x000D15EB
		internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000D33F4 File Offset: 0x000D15F4
		internal override string Identity
		{
			get
			{
				if (this.Facets.Count == 0)
				{
					return this.EdmType.Identity;
				}
				if (this._identity == null)
				{
					StringBuilder stringBuilder = new StringBuilder(128);
					this.BuildIdentity(stringBuilder);
					string text = stringBuilder.ToString();
					Interlocked.CompareExchange<string>(ref this._identity, text, null);
				}
				return this._identity;
			}
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000D3450 File Offset: 0x000D1650
		private static IEnumerable<Facet> GetDefaultFacetDescriptionsAndOverrideFacetValues(EdmType type, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<FacetDescription>(type.GetAssociatedFacetDescriptions(), (FacetDescription fd) => fd, (FacetDescription fd) => fd.DefaultValueFacet, values);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000D34A8 File Offset: 0x000D16A8
		private static IEnumerable<Facet> OverrideFacetValues(IEnumerable<Facet> facets, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<Facet>(facets, (Facet f) => f.Description, (Facet f) => f, values);
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x000D34FA File Offset: 0x000D16FA
		private static IEnumerable<Facet> OverrideFacetValues<T>(IEnumerable<T> facetThings, Func<T, FacetDescription> getDescription, Func<T, Facet> getFacet, FacetValues values)
		{
			foreach (T t in facetThings)
			{
				FacetDescription facetDescription = getDescription(t);
				Facet facet;
				if (!facetDescription.IsConstant && values.TryGetFacet(facetDescription, out facet))
				{
					yield return facet;
				}
				else
				{
					yield return getFacet(t);
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000D3520 File Offset: 0x000D1720
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (this._identity != null)
			{
				builder.Append(this._identity);
				return;
			}
			builder.Append(this.EdmType.Identity);
			builder.Append("(");
			bool flag = true;
			for (int i = 0; i < this.Facets.Count; i++)
			{
				Facet facet = this.Facets[i];
				if (0 <= Array.BinarySearch<string>(TypeUsage._identityFacets, facet.Name, StringComparer.Ordinal))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						builder.Append(",");
					}
					builder.Append(facet.Name);
					builder.Append("=");
					builder.Append(facet.Value ?? string.Empty);
				}
			}
			builder.Append(")");
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000D35EC File Offset: 0x000D17EC
		public override string ToString()
		{
			return this.EdmType.ToString();
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000D35FC File Offset: 0x000D17FC
		internal override bool EdmEquals(MetadataItem item)
		{
			if (this == item)
			{
				return true;
			}
			if (item == null || BuiltInTypeKind.TypeUsage != item.BuiltInTypeKind)
			{
				return false;
			}
			TypeUsage typeUsage = (TypeUsage)item;
			if (!this.EdmType.EdmEquals(typeUsage.EdmType))
			{
				return false;
			}
			if (this._facets == null && typeUsage._facets == null)
			{
				return true;
			}
			if (this.Facets.Count != typeUsage.Facets.Count)
			{
				return false;
			}
			foreach (Facet facet in this.Facets)
			{
				Facet facet2;
				if (!typeUsage.Facets.TryGetValue(facet.Name, false, out facet2))
				{
					return false;
				}
				if (!object.Equals(facet.Value, facet2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000D36DC File Offset: 0x000D18DC
		private static void ValidateMaxLength(int maxLength)
		{
			if (maxLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxLength", Strings.InvalidMaxLengthSize);
			}
		}

		// Token: 0x0400158D RID: 5517
		private TypeUsage _modelTypeUsage;

		// Token: 0x0400158E RID: 5518
		private readonly EdmType _edmType;

		// Token: 0x0400158F RID: 5519
		private ReadOnlyMetadataCollection<Facet> _facets;

		// Token: 0x04001590 RID: 5520
		private string _identity;

		// Token: 0x04001591 RID: 5521
		private static readonly string[] _identityFacets = new string[] { "DefaultValue", "FixedLength", "MaxLength", "Nullable", "Precision", "Scale", "Unicode", "SRID" };

		// Token: 0x04001592 RID: 5522
		internal static readonly EdmConstants.Unbounded DefaultMaxLengthFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04001593 RID: 5523
		internal static readonly EdmConstants.Unbounded DefaultPrecisionFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04001594 RID: 5524
		internal static readonly EdmConstants.Unbounded DefaultScaleFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04001595 RID: 5525
		internal const bool DefaultUnicodeFacetValue = true;

		// Token: 0x04001596 RID: 5526
		internal const bool DefaultFixedLengthFacetValue = false;

		// Token: 0x04001597 RID: 5527
		internal static readonly byte? DefaultDateTimePrecisionFacetValue = null;
	}
}
