using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B3 RID: 179
	[DebuggerDisplay("EdmType={EdmType}, Facets.Count={Facets.Count}")]
	public sealed class TypeUsage : MetadataItem
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0001D83A File Offset: 0x0001BA3A
		private TypeUsage(EdmType edmType)
			: base(MetadataItem.MetadataFlags.Readonly)
		{
			EntityUtil.GenericCheckArgumentNull<EdmType>(edmType, "edmType");
			this._edmType = edmType;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0001D858 File Offset: 0x0001BA58
		private TypeUsage(EdmType edmType, IEnumerable<Facet> facets)
			: this(edmType)
		{
			MetadataCollection<Facet> metadataCollection = new MetadataCollection<Facet>(facets);
			metadataCollection.SetReadOnly();
			this._facets = metadataCollection.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0001D886 File Offset: 0x0001BA86
		internal static TypeUsage Create(EdmType edmType)
		{
			return new TypeUsage(edmType);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001D88E File Offset: 0x0001BA8E
		internal static TypeUsage Create(EdmType edmType, FacetValues values)
		{
			return new TypeUsage(edmType, TypeUsage.GetDefaultFacetDescriptionsAndOverrideFacetValues(edmType, values));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0001D89D File Offset: 0x0001BA9D
		internal static TypeUsage Create(EdmType edmType, IEnumerable<Facet> facets)
		{
			return new TypeUsage(edmType, facets);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0001D8A6 File Offset: 0x0001BAA6
		internal TypeUsage ShallowCopy(FacetValues facetValues)
		{
			return TypeUsage.Create(this._edmType, TypeUsage.OverrideFacetValues(this.Facets, facetValues));
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0001D8BF File Offset: 0x0001BABF
		public static TypeUsage CreateDefaultTypeUsage(EdmType edmType)
		{
			EntityUtil.CheckArgumentNull<EdmType>(edmType, "edmType");
			return TypeUsage.Create(edmType);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength, int maxLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw EntityUtil.NotStringTypeForTypeUsage();
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001D944 File Offset: 0x0001BB44
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw EntityUtil.NotStringTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength, int maxLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw EntityUtil.NotBinaryTypeForTypeUsage();
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001DA08 File Offset: 0x0001BC08
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw EntityUtil.NotBinaryTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001DA5B File Offset: 0x0001BC5B
		public static TypeUsage CreateDateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTime)
			{
				throw EntityUtil.NotDateTimeTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0001DA8F File Offset: 0x0001BC8F
		public static TypeUsage CreateDateTimeOffsetTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTimeOffset)
			{
				throw EntityUtil.NotDateTimeOffsetTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		public static TypeUsage CreateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Time)
			{
				throw EntityUtil.NotTimeTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType, byte precision, byte scale)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw EntityUtil.NotDecimalTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = new byte?(precision),
				Scale = new byte?(scale)
			});
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0001DB54 File Offset: 0x0001BD54
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw EntityUtil.NotDecimalTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = TypeUsage.DefaultPrecisionFacetValue,
				Scale = TypeUsage.DefaultScaleFacetValue
			});
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001DBA7 File Offset: 0x0001BDA7
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.TypeUsage;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001DBAB File Offset: 0x0001BDAB
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		[MetadataProperty(BuiltInTypeKind.Facet, true)]
		public ReadOnlyMetadataCollection<Facet> Facets
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

		// Token: 0x06000B9D RID: 2973 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		internal TypeUsage GetModelTypeUsage()
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
						TypeUsage modelTypeUsage = edmProperty.TypeUsage.GetModelTypeUsage();
						array[i] = new EdmProperty(edmProperty.Name, modelTypeUsage);
					}
					typeUsage = TypeUsage.Create(new RowType(array), this.Facets);
				}
				else if (Helper.IsCollectionType(edmType))
				{
					typeUsage = TypeUsage.Create(new CollectionType(((CollectionType)edmType).TypeUsage.GetModelTypeUsage()), this.Facets);
				}
				else if (Helper.IsRefType(edmType))
				{
					typeUsage = this;
				}
				else if (Helper.IsPrimitiveType(edmType))
				{
					typeUsage = ((PrimitiveType)edmType).ProviderManifest.GetEdmType(this);
					if (typeUsage == null)
					{
						throw new Exception(Strings.Mapping_ProviderReturnsNullType(this.ToString()));
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

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001DD6B File Offset: 0x0001BF6B
		public bool IsSubtypeOf(TypeUsage typeUsage)
		{
			return this.EdmType != null && typeUsage != null && this.EdmType.IsSubtypeOf(typeUsage.EdmType);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0001DD8B File Offset: 0x0001BF8B
		private IEnumerable<Facet> GetFacets()
		{
			foreach (FacetDescription facetDescription in this._edmType.GetAssociatedFacetDescriptions())
			{
				yield return facetDescription.DefaultValueFacet;
			}
			IEnumerator<FacetDescription> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001DD9B File Offset: 0x0001BF9B
		internal override void SetReadOnly()
		{
			throw new Exception("TypeUsage.SetReadOnly should not need to ever be called");
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
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

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001DE04 File Offset: 0x0001C004
		private static IEnumerable<Facet> GetDefaultFacetDescriptionsAndOverrideFacetValues(EdmType type, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<FacetDescription>(type.GetAssociatedFacetDescriptions(), (FacetDescription fd) => fd, (FacetDescription fd) => fd.DefaultValueFacet, values);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001DE5C File Offset: 0x0001C05C
		private static IEnumerable<Facet> OverrideFacetValues(IEnumerable<Facet> facets, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<Facet>(facets, (Facet f) => f.Description, (Facet f) => f, values);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001DEAE File Offset: 0x0001C0AE
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

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001DED4 File Offset: 0x0001C0D4
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
				if (0 <= Array.BinarySearch<string>(TypeUsage.s_identityFacets, facet.Name, StringComparer.Ordinal))
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

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		public override string ToString()
		{
			return this.EdmType.ToString();
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
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

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001E090 File Offset: 0x0001C290
		private static void ValidateMaxLength(int maxLength)
		{
			if (maxLength <= 0)
			{
				throw EntityUtil.ArgumentOutOfRange(Strings.InvalidMaxLengthSize, "maxLength");
			}
		}

		// Token: 0x040008B2 RID: 2226
		private TypeUsage _modelTypeUsage;

		// Token: 0x040008B3 RID: 2227
		private readonly EdmType _edmType;

		// Token: 0x040008B4 RID: 2228
		private ReadOnlyMetadataCollection<Facet> _facets;

		// Token: 0x040008B5 RID: 2229
		private string _identity;

		// Token: 0x040008B6 RID: 2230
		private static readonly string[] s_identityFacets = new string[] { "DefaultValue", "FixedLength", "MaxLength", "Nullable", "Precision", "Scale", "Unicode" };

		// Token: 0x040008B7 RID: 2231
		internal static readonly EdmConstants.Unbounded DefaultMaxLengthFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x040008B8 RID: 2232
		internal static readonly EdmConstants.Unbounded DefaultPrecisionFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x040008B9 RID: 2233
		internal static readonly EdmConstants.Unbounded DefaultScaleFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x040008BA RID: 2234
		internal static readonly bool DefaultUnicodeFacetValue = true;

		// Token: 0x040008BB RID: 2235
		internal static readonly bool DefaultFixedLengthFacetValue = false;

		// Token: 0x040008BC RID: 2236
		internal static readonly byte? DefaultDateTimePrecisionFacetValue = null;
	}
}
