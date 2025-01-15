using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C1 RID: 1217
	public class FacetDescription
	{
		// Token: 0x06003C21 RID: 15393 RVA: 0x000C72D5 File Offset: 0x000C54D5
		internal FacetDescription()
		{
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x000C72E0 File Offset: 0x000C54E0
		internal FacetDescription(string facetName, EdmType facetType, int? minValue, int? maxValue, object defaultValue, bool isConstant, string declaringTypeName)
		{
			this._facetName = facetName;
			this._facetType = facetType;
			this._minValue = minValue;
			this._maxValue = maxValue;
			if (defaultValue != null)
			{
				this._defaultValue = defaultValue;
			}
			else
			{
				this._defaultValue = FacetDescription._notInitializedSentinel;
			}
			this._isConstant = isConstant;
			this.Validate(declaringTypeName);
			if (this._isConstant)
			{
				FacetDescription.UpdateMinMaxValueForConstant(this._facetName, this._facetType, ref this._minValue, ref this._maxValue, this._defaultValue);
			}
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x000C7364 File Offset: 0x000C5564
		internal FacetDescription(string facetName, EdmType facetType, int? minValue, int? maxValue, object defaultValue)
		{
			Check.NotEmpty(facetName, "facetName");
			Check.NotNull<EdmType>(facetType, "facetType");
			if ((minValue != null || maxValue != null) && minValue != null)
			{
				bool flag = maxValue != null;
			}
			this._facetName = facetName;
			this._facetType = facetType;
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._defaultValue = defaultValue;
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000C73D7 File Offset: 0x000C55D7
		public virtual string FacetName
		{
			get
			{
				return this._facetName;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x000C73DF File Offset: 0x000C55DF
		public EdmType FacetType
		{
			get
			{
				return this._facetType;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000C73E7 File Offset: 0x000C55E7
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000C73EF File Offset: 0x000C55EF
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000C73F7 File Offset: 0x000C55F7
		public object DefaultValue
		{
			get
			{
				if (this._defaultValue == FacetDescription._notInitializedSentinel)
				{
					return null;
				}
				return this._defaultValue;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000C740E File Offset: 0x000C560E
		public virtual bool IsConstant
		{
			get
			{
				return this._isConstant;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000C7416 File Offset: 0x000C5616
		public bool IsRequired
		{
			get
			{
				return this._defaultValue == FacetDescription._notInitializedSentinel;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x000C7428 File Offset: 0x000C5628
		internal Facet DefaultValueFacet
		{
			get
			{
				if (this._defaultValueFacet == null)
				{
					Facet facet = Facet.Create(this, this.DefaultValue, true);
					Interlocked.CompareExchange<Facet>(ref this._defaultValueFacet, facet, null);
				}
				return this._defaultValueFacet;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000C7460 File Offset: 0x000C5660
		internal Facet NullValueFacet
		{
			get
			{
				if (this._nullValueFacet == null)
				{
					Facet facet = Facet.Create(this, null, true);
					Interlocked.CompareExchange<Facet>(ref this._nullValueFacet, facet, null);
				}
				return this._nullValueFacet;
			}
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x000C7492 File Offset: 0x000C5692
		public override string ToString()
		{
			return this.FacetName;
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x000C749C File Offset: 0x000C569C
		internal Facet GetBooleanFacet(bool value)
		{
			if (this._valueCache == null)
			{
				Interlocked.CompareExchange<Facet[]>(ref this._valueCache, new Facet[]
				{
					Facet.Create(this, true, true),
					Facet.Create(this, false, true)
				}, null);
			}
			if (!value)
			{
				return this._valueCache[1];
			}
			return this._valueCache[0];
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000C74FC File Offset: 0x000C56FC
		internal static bool IsNumericType(EdmType facetType)
		{
			if (Helper.IsPrimitiveType(facetType))
			{
				PrimitiveType primitiveType = (PrimitiveType)facetType;
				return primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Byte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.SByte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int16 || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int32;
			}
			return false;
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x000C7544 File Offset: 0x000C5744
		private static void UpdateMinMaxValueForConstant(string facetName, EdmType facetType, ref int? minValue, ref int? maxValue, object defaultValue)
		{
			if (FacetDescription.IsNumericType(facetType))
			{
				if (facetName == "Precision" || facetName == "Scale")
				{
					byte? b = (byte?)defaultValue;
					minValue = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
					b = (byte?)defaultValue;
					maxValue = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
					return;
				}
				minValue = (int?)defaultValue;
				maxValue = (int?)defaultValue;
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000C75F0 File Offset: 0x000C57F0
		private void Validate(string declaringTypeName)
		{
			if (this._defaultValue == FacetDescription._notInitializedSentinel)
			{
				if (this._isConstant)
				{
					throw new ArgumentException(Strings.MissingDefaultValueForConstantFacet(this._facetName, declaringTypeName));
				}
			}
			else if (FacetDescription.IsNumericType(this._facetType))
			{
				if (this._isConstant)
				{
					if (this._minValue != null != (this._maxValue != null) || (this._minValue != null && this._minValue.Value != this._maxValue.Value))
					{
						throw new ArgumentException(Strings.MinAndMaxValueMustBeSameForConstantFacet(this._facetName, declaringTypeName));
					}
				}
				else
				{
					if (this._minValue == null || this._maxValue == null)
					{
						throw new ArgumentException(Strings.BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(this._facetName, declaringTypeName));
					}
					int value = this._minValue.Value;
					int? num = this._maxValue;
					if ((value == num.GetValueOrDefault()) & (num != null))
					{
						throw new ArgumentException(Strings.MinAndMaxValueMustBeDifferentForNonConstantFacet(this._facetName, declaringTypeName));
					}
					num = this._minValue;
					int num2 = 0;
					if (!((num.GetValueOrDefault() < num2) & (num != null)))
					{
						num = this._maxValue;
						num2 = 0;
						if (!((num.GetValueOrDefault() < num2) & (num != null)))
						{
							num = this._minValue;
							int? maxValue = this._maxValue;
							if ((num.GetValueOrDefault() > maxValue.GetValueOrDefault()) & ((num != null) & (maxValue != null)))
							{
								throw new ArgumentException(Strings.MinMustBeLessThanMax(this._minValue.ToString(), this._facetName, declaringTypeName));
							}
							return;
						}
					}
					throw new ArgumentException(Strings.MinAndMaxMustBePositive(this._facetName, declaringTypeName));
				}
			}
		}

		// Token: 0x040014AE RID: 5294
		private readonly string _facetName;

		// Token: 0x040014AF RID: 5295
		private readonly EdmType _facetType;

		// Token: 0x040014B0 RID: 5296
		private readonly int? _minValue;

		// Token: 0x040014B1 RID: 5297
		private readonly int? _maxValue;

		// Token: 0x040014B2 RID: 5298
		private readonly object _defaultValue;

		// Token: 0x040014B3 RID: 5299
		private readonly bool _isConstant;

		// Token: 0x040014B4 RID: 5300
		private Facet _defaultValueFacet;

		// Token: 0x040014B5 RID: 5301
		private Facet _nullValueFacet;

		// Token: 0x040014B6 RID: 5302
		private Facet[] _valueCache;

		// Token: 0x040014B7 RID: 5303
		private static readonly object _notInitializedSentinel = new object();
	}
}
