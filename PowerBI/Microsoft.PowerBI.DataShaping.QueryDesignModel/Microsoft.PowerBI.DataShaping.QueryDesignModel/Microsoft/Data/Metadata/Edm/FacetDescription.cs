using System;
using System.Data;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000090 RID: 144
	public sealed class FacetDescription
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x00018674 File Offset: 0x00016874
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

		// Token: 0x06000A4E RID: 2638 RVA: 0x000186F8 File Offset: 0x000168F8
		internal FacetDescription(string facetName, EdmType facetType, int? minValue, int? maxValue, object defaultValue)
		{
			EntityUtil.CheckStringArgument(facetName, "facetName");
			EntityUtil.GenericCheckArgumentNull<EdmType>(facetType, "facetType");
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001876A File Offset: 0x0001696A
		public string FacetName
		{
			get
			{
				return this._facetName;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00018772 File Offset: 0x00016972
		public EdmType FacetType
		{
			get
			{
				return this._facetType;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001877A File Offset: 0x0001697A
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00018782 File Offset: 0x00016982
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001878A File Offset: 0x0001698A
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000187A1 File Offset: 0x000169A1
		public bool IsConstant
		{
			get
			{
				return this._isConstant;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000187A9 File Offset: 0x000169A9
		public bool IsRequired
		{
			get
			{
				return this._defaultValue == FacetDescription._notInitializedSentinel;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000187B8 File Offset: 0x000169B8
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x000187F0 File Offset: 0x000169F0
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

		// Token: 0x06000A58 RID: 2648 RVA: 0x00018822 File Offset: 0x00016A22
		public override string ToString()
		{
			return this.FacetName;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001882C File Offset: 0x00016A2C
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

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001888C File Offset: 0x00016A8C
		internal static bool IsNumericType(EdmType facetType)
		{
			if (Helper.IsPrimitiveType(facetType))
			{
				PrimitiveType primitiveType = (PrimitiveType)facetType;
				return primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Byte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.SByte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int16 || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int32;
			}
			return false;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000188D4 File Offset: 0x00016AD4
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

		// Token: 0x06000A5C RID: 2652 RVA: 0x00018980 File Offset: 0x00016B80
		private void Validate(string declaringTypeName)
		{
			if (this._defaultValue == FacetDescription._notInitializedSentinel)
			{
				if (this._isConstant)
				{
					throw EntityUtil.MissingDefaultValueForConstantFacet(this._facetName, declaringTypeName);
				}
			}
			else if (FacetDescription.IsNumericType(this._facetType))
			{
				if (this._isConstant)
				{
					if (this._minValue != null != (this._maxValue != null) || (this._minValue != null && this._minValue.Value != this._maxValue.Value))
					{
						throw EntityUtil.MinAndMaxValueMustBeSameForConstantFacet(this._facetName, declaringTypeName);
					}
				}
				else
				{
					if (this._minValue == null || this._maxValue == null)
					{
						throw EntityUtil.BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(this._facetName, declaringTypeName);
					}
					int value = this._minValue.Value;
					int? num = this._maxValue;
					if ((value == num.GetValueOrDefault()) & (num != null))
					{
						throw EntityUtil.MinAndMaxValueMustBeDifferentForNonConstantFacet(this._facetName, declaringTypeName);
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
								throw EntityUtil.MinMustBeLessThanMax(this._minValue.ToString(), this._facetName, declaringTypeName);
							}
							return;
						}
					}
					throw EntityUtil.MinAndMaxMustBePositive(this._facetName, declaringTypeName);
				}
			}
		}

		// Token: 0x04000836 RID: 2102
		private readonly string _facetName;

		// Token: 0x04000837 RID: 2103
		private readonly EdmType _facetType;

		// Token: 0x04000838 RID: 2104
		private readonly int? _minValue;

		// Token: 0x04000839 RID: 2105
		private readonly int? _maxValue;

		// Token: 0x0400083A RID: 2106
		private readonly object _defaultValue;

		// Token: 0x0400083B RID: 2107
		private readonly bool _isConstant;

		// Token: 0x0400083C RID: 2108
		private Facet _defaultValueFacet;

		// Token: 0x0400083D RID: 2109
		private Facet _nullValueFacet;

		// Token: 0x0400083E RID: 2110
		private Facet[] _valueCache;

		// Token: 0x0400083F RID: 2111
		private static object _notInitializedSentinel = new object();
	}
}
