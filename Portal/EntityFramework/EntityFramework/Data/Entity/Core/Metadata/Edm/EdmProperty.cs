using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B0 RID: 1200
	public class EdmProperty : EdmMember
	{
		// Token: 0x06003ADD RID: 15069 RVA: 0x000C2910 File Offset: 0x000C0B10
		public static EdmProperty CreatePrimitive(string name, PrimitiveType primitiveType)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			return EdmProperty.CreateProperty(name, primitiveType);
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000C2931 File Offset: 0x000C0B31
		public static EdmProperty CreateEnum(string name, EnumType enumType)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<EnumType>(enumType, "enumType");
			return EdmProperty.CreateProperty(name, enumType);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000C2952 File Offset: 0x000C0B52
		public static EdmProperty CreateComplex(string name, ComplexType complexType)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<ComplexType>(complexType, "complexType");
			EdmProperty edmProperty = EdmProperty.CreateProperty(name, complexType);
			edmProperty.Nullable = false;
			return edmProperty;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000C297C File Offset: 0x000C0B7C
		public static EdmProperty Create(string name, TypeUsage typeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			EdmType edmType = typeUsage.EdmType;
			if (!Helper.IsPrimitiveType(edmType) && !Helper.IsEnumType(edmType) && !Helper.IsComplexType(edmType))
			{
				throw new ArgumentException(Strings.EdmProperty_InvalidPropertyType(edmType.FullName));
			}
			return new EdmProperty(name, typeUsage);
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000C29D8 File Offset: 0x000C0BD8
		private static EdmProperty CreateProperty(string name, EdmType edmType)
		{
			TypeUsage typeUsage = TypeUsage.Create(edmType, new FacetValues());
			return new EdmProperty(name, typeUsage);
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000C29F8 File Offset: 0x000C0BF8
		internal EdmProperty(string name, TypeUsage typeUsage)
			: base(name, typeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000C2A1A File Offset: 0x000C0C1A
		internal EdmProperty(string name, TypeUsage typeUsage, PropertyInfo propertyInfo, Type entityDeclaringType)
			: this(name, typeUsage)
		{
			this._propertyInfo = propertyInfo;
			this._entityDeclaringType = entityDeclaringType;
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000C2A33 File Offset: 0x000C0C33
		internal EdmProperty(string name)
			: this(name, TypeUsage.Create(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String)))
		{
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000C2A48 File Offset: 0x000C0C48
		internal PropertyInfo PropertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000C2A50 File Offset: 0x000C0C50
		internal Type EntityDeclaringType
		{
			get
			{
				return this._entityDeclaringType;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000C2A58 File Offset: 0x000C0C58
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmProperty;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000C2A5C File Offset: 0x000C0C5C
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x000C2A7D File Offset: 0x000C0C7D
		public bool Nullable
		{
			get
			{
				return (bool)this.TypeUsage.Facets["Nullable"].Value;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
				{
					Nullable = new bool?(value)
				});
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000C2AAC File Offset: 0x000C0CAC
		public string TypeName
		{
			get
			{
				return this.TypeUsage.EdmType.Name;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000C2ABE File Offset: 0x000C0CBE
		// (set) Token: 0x06003AEC RID: 15084 RVA: 0x000C2ADA File Offset: 0x000C0CDA
		public object DefaultValue
		{
			get
			{
				return this.TypeUsage.Facets["DefaultValue"].Value;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
				{
					DefaultValue = value
				});
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000C2AFF File Offset: 0x000C0CFF
		// (set) Token: 0x06003AEE RID: 15086 RVA: 0x000C2B07 File Offset: 0x000C0D07
		internal Func<object, object> ValueGetter
		{
			get
			{
				return this._memberGetter;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object>>(ref this._memberGetter, value, null);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000C2B17 File Offset: 0x000C0D17
		// (set) Token: 0x06003AF0 RID: 15088 RVA: 0x000C2B1F File Offset: 0x000C0D1F
		internal Action<object, object> ValueSetter
		{
			get
			{
				return this._memberSetter;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._memberSetter, value, null);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000C2B30 File Offset: 0x000C0D30
		internal bool IsKeyMember
		{
			get
			{
				EntityType entityType = this.DeclaringType as EntityType;
				return entityType != null && entityType.KeyMembers.Contains(this);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000C2B5A File Offset: 0x000C0D5A
		public bool IsCollectionType
		{
			get
			{
				return this.TypeUsage.EdmType is CollectionType;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000C2B6F File Offset: 0x000C0D6F
		public bool IsComplexType
		{
			get
			{
				return this.TypeUsage.EdmType is ComplexType;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000C2B84 File Offset: 0x000C0D84
		public bool IsPrimitiveType
		{
			get
			{
				return this.TypeUsage.EdmType is PrimitiveType;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000C2B99 File Offset: 0x000C0D99
		public bool IsEnumType
		{
			get
			{
				return this.TypeUsage.EdmType is EnumType;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000C2BAE File Offset: 0x000C0DAE
		public bool IsUnderlyingPrimitiveType
		{
			get
			{
				return this.IsPrimitiveType || this.IsEnumType;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000C2BC0 File Offset: 0x000C0DC0
		public ComplexType ComplexType
		{
			get
			{
				return this.TypeUsage.EdmType as ComplexType;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000C2BD2 File Offset: 0x000C0DD2
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x000C2BE4 File Offset: 0x000C0DE4
		public PrimitiveType PrimitiveType
		{
			get
			{
				return this.TypeUsage.EdmType as PrimitiveType;
			}
			internal set
			{
				Check.NotNull<PrimitiveType>(value, "value");
				Util.ThrowIfReadOnly(this);
				StoreGeneratedPattern storeGeneratedPattern = this.StoreGeneratedPattern;
				ConcurrencyMode concurrencyMode = this.ConcurrencyMode;
				List<Facet> list = new List<Facet>();
				foreach (FacetDescription facetDescription in value.GetAssociatedFacetDescriptions())
				{
					Facet facet;
					if (this.TypeUsage.Facets.TryGetValue(facetDescription.FacetName, false, out facet) && ((facet.Value == null && facet.Description.DefaultValue != null) || (facet.Value != null && !facet.Value.Equals(facet.Description.DefaultValue))))
					{
						list.Add(facet);
					}
				}
				this.TypeUsage = TypeUsage.Create(value, FacetValues.Create(list));
				if (storeGeneratedPattern != StoreGeneratedPattern.None)
				{
					this.StoreGeneratedPattern = storeGeneratedPattern;
				}
				if (concurrencyMode != ConcurrencyMode.None)
				{
					this.ConcurrencyMode = concurrencyMode;
				}
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000C2CD8 File Offset: 0x000C0ED8
		public EnumType EnumType
		{
			get
			{
				return this.TypeUsage.EdmType as EnumType;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000C2CEA File Offset: 0x000C0EEA
		public PrimitiveType UnderlyingPrimitiveType
		{
			get
			{
				if (!this.IsUnderlyingPrimitiveType)
				{
					return null;
				}
				if (!this.IsEnumType)
				{
					return this.PrimitiveType;
				}
				return this.EnumType.UnderlyingType;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000C2D10 File Offset: 0x000C0F10
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x000C2D18 File Offset: 0x000C0F18
		public ConcurrencyMode ConcurrencyMode
		{
			get
			{
				return MetadataHelper.GetConcurrencyMode(this);
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[] { Facet.Create(Converter.ConcurrencyModeFacet, value) });
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000C2D4A File Offset: 0x000C0F4A
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x000C2D52 File Offset: 0x000C0F52
		public StoreGeneratedPattern StoreGeneratedPattern
		{
			get
			{
				return MetadataHelper.GetStoreGeneratedPattern(this);
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[] { Facet.Create(Converter.StoreGeneratedPatternFacet, value) });
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000C2D84 File Offset: 0x000C0F84
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000C2DB8 File Offset: 0x000C0FB8
		public CollectionKind CollectionKind
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("CollectionKind", false, out facet))
				{
					return CollectionKind.None;
				}
				return (CollectionKind)facet.Value;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[] { Facet.Create(MetadataItem.CollectionKindFacetDescription, value) });
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000C2DEC File Offset: 0x000C0FEC
		public bool IsMaxLengthConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000C2E20 File Offset: 0x000C1020
		// (set) Token: 0x06003B04 RID: 15108 RVA: 0x000C2E64 File Offset: 0x000C1064
		public int? MaxLength
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet))
				{
					return null;
				}
				return facet.Value as int?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				int? maxLength = this.MaxLength;
				int? num = value;
				if (!((maxLength.GetValueOrDefault() == num.GetValueOrDefault()) & (maxLength != null == (num != null))))
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						MaxLength = value
					});
				}
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000C2EC8 File Offset: 0x000C10C8
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x000C2EF7 File Offset: 0x000C10F7
		public bool IsMaxLength
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.IsUnbounded;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						MaxLength = EdmConstants.UnboundedValue
					});
				}
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000C2F28 File Offset: 0x000C1128
		public bool IsFixedLengthConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("FixedLength", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000C2F5C File Offset: 0x000C115C
		// (set) Token: 0x06003B09 RID: 15113 RVA: 0x000C2FA0 File Offset: 0x000C11A0
		public bool? IsFixedLength
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("FixedLength", false, out facet))
				{
					return null;
				}
				return facet.Value as bool?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				bool? isFixedLength = this.IsFixedLength;
				bool? flag = value;
				if (!((isFixedLength.GetValueOrDefault() == flag.GetValueOrDefault()) & (isFixedLength != null == (flag != null))))
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						FixedLength = value
					});
				}
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x000C3004 File Offset: 0x000C1204
		public bool IsUnicodeConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Unicode", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000C3038 File Offset: 0x000C1238
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000C307C File Offset: 0x000C127C
		public bool? IsUnicode
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Unicode", false, out facet))
				{
					return null;
				}
				return facet.Value as bool?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				bool? isUnicode = this.IsUnicode;
				bool? flag = value;
				if (!((isUnicode.GetValueOrDefault() == flag.GetValueOrDefault()) & (isUnicode != null == (flag != null))))
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						Unicode = value
					});
				}
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000C30E0 File Offset: 0x000C12E0
		public bool IsPrecisionConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Precision", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000C3114 File Offset: 0x000C1314
		// (set) Token: 0x06003B0F RID: 15119 RVA: 0x000C3158 File Offset: 0x000C1358
		public byte? Precision
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Precision", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				byte? b = this.Precision;
				int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				b = value;
				int? num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						Precision = value
					});
				}
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000C31FC File Offset: 0x000C13FC
		public bool IsScaleConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Scale", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000C3230 File Offset: 0x000C1430
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000C3274 File Offset: 0x000C1474
		public byte? Scale
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Scale", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				byte? b = this.Scale;
				int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				b = value;
				int? num2 = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
				if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						Scale = value
					});
				}
			}
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000C3317 File Offset: 0x000C1517
		public void SetMetadataProperties(IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotNull<IEnumerable<MetadataProperty>>(metadataProperties, "metadataProperties");
			Util.ThrowIfReadOnly(this);
			base.AddMetadataProperties(metadataProperties);
		}

		// Token: 0x0400146B RID: 5227
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x0400146C RID: 5228
		private readonly Type _entityDeclaringType;

		// Token: 0x0400146D RID: 5229
		private Func<object, object> _memberGetter;

		// Token: 0x0400146E RID: 5230
		private Action<object, object> _memberSetter;
	}
}
