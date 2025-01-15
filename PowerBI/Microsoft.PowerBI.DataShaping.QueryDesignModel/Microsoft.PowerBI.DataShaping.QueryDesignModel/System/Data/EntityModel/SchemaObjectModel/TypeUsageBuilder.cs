using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000059 RID: 89
	internal class TypeUsageBuilder
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x00012713 File Offset: 0x00010913
		internal TypeUsageBuilder(SchemaElement element)
		{
			this._element = element;
			this._facetValues = new Dictionary<string, object>();
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001272D File Offset: 0x0001092D
		internal TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00012735 File Offset: 0x00010935
		internal bool Nullable
		{
			get
			{
				return this._nullable == null || this._nullable.Value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00012751 File Offset: 0x00010951
		internal string Default
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00012759 File Offset: 0x00010959
		internal object DefaultAsObject
		{
			get
			{
				return this._defaultObject;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00012761 File Offset: 0x00010961
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._hasUserDefinedFacets;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001276C File Offset: 0x0001096C
		private bool TryGetFacets(EdmType edmType, bool complainOnMissingFacet, out Dictionary<string, Facet> calculatedFacets)
		{
			bool flag = true;
			Dictionary<string, Facet> dictionary = edmType.GetAssociatedFacetDescriptions().ToDictionary((FacetDescription f) => f.FacetName, (FacetDescription f) => f.DefaultValueFacet);
			calculatedFacets = new Dictionary<string, Facet>();
			foreach (Facet facet in dictionary.Values)
			{
				object obj;
				if (this._facetValues.TryGetValue(facet.Name, out obj))
				{
					if (facet.Description.IsConstant)
					{
						this._element.AddError(ErrorCode.ConstantFacetSpecifiedInSchema, EdmSchemaErrorSeverity.Error, this._element, Strings.ConstantFacetSpecifiedInSchema(facet.Name, edmType.Name));
						flag = false;
					}
					else
					{
						calculatedFacets.Add(facet.Name, Facet.Create(facet.Description, obj));
					}
					this._facetValues.Remove(facet.Name);
				}
				else if (complainOnMissingFacet && facet.Description.IsRequired)
				{
					this._element.AddError(ErrorCode.RequiredFacetMissing, EdmSchemaErrorSeverity.Error, Strings.RequiredFacetMissing(facet.Name, edmType.Name));
					flag = false;
				}
				else
				{
					calculatedFacets.Add(facet.Name, facet);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair in this._facetValues)
			{
				if (keyValuePair.Key == "StoreGeneratedPattern")
				{
					Facet facet2 = Facet.Create(Converter.StoreGeneratedPatternFacet, keyValuePair.Value);
					calculatedFacets.Add(facet2.Name, facet2);
				}
				else if (keyValuePair.Key == "ConcurrencyMode")
				{
					Facet facet3 = Facet.Create(Converter.ConcurrencyModeFacet, keyValuePair.Value);
					calculatedFacets.Add(facet3.Name, facet3);
				}
				else if (edmType is PrimitiveType && (edmType as PrimitiveType).PrimitiveTypeKind == PrimitiveTypeKind.String && keyValuePair.Key == "Collation")
				{
					Facet facet4 = Facet.Create(Converter.CollationFacet, keyValuePair.Value);
					calculatedFacets.Add(facet4.Name, facet4);
				}
				else
				{
					this._element.AddError(ErrorCode.FacetNotAllowedByType, EdmSchemaErrorSeverity.Error, Strings.FacetNotAllowed(keyValuePair.Key, edmType.Name));
				}
			}
			return flag;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000129F0 File Offset: 0x00010BF0
		internal void ValidateAndSetTypeUsage(EdmType edmType, bool complainOnMissingFacet)
		{
			Dictionary<string, Facet> dictionary;
			this.TryGetFacets(edmType, complainOnMissingFacet, out dictionary);
			this._typeUsage = TypeUsage.Create(edmType, dictionary.Values);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00012A1C File Offset: 0x00010C1C
		internal void ValidateAndSetTypeUsage(ScalarType scalar, bool complainOnMissingFacet)
		{
			Dictionary<string, Facet> dictionary;
			if (this.TryGetFacets(scalar.Type, complainOnMissingFacet, out dictionary))
			{
				switch (scalar.TypeKind)
				{
				case PrimitiveTypeKind.Binary:
					this.ValidateAndSetBinaryFacets(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.Boolean:
				case PrimitiveTypeKind.Byte:
				case PrimitiveTypeKind.Double:
				case PrimitiveTypeKind.Guid:
				case PrimitiveTypeKind.Single:
				case PrimitiveTypeKind.SByte:
				case PrimitiveTypeKind.Int16:
				case PrimitiveTypeKind.Int32:
				case PrimitiveTypeKind.Int64:
					break;
				case PrimitiveTypeKind.DateTime:
				case PrimitiveTypeKind.Time:
				case PrimitiveTypeKind.DateTimeOffset:
					this.ValidatePrecisionFacetsForDateTimeFamily(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.Decimal:
					this.ValidateAndSetDecimalFacets(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.String:
					this.ValidateAndSetStringFacets(scalar.Type, dictionary);
					break;
				default:
					throw new Exception("Did you miss a value");
				}
			}
			this._typeUsage = TypeUsage.Create(scalar.Type, dictionary.Values);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00012AE8 File Offset: 0x00010CE8
		internal bool HandleAttribute(XmlReader reader)
		{
			bool flag = this.InternalHandleAttribute(reader);
			this._hasUserDefinedFacets = this._hasUserDefinedFacets || flag;
			return flag;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00012B0C File Offset: 0x00010D0C
		private bool InternalHandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Nullable"))
			{
				this.HandleNullableAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "DefaultValue"))
			{
				this.HandleDefaultAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Precision"))
			{
				this.HandlePrecisionAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Scale"))
			{
				this.HandleScaleAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "StoreGeneratedPattern"))
			{
				this.HandleStoreGeneratedPatternAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ConcurrencyMode"))
			{
				this.HandleConcurrencyModeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "MaxLength"))
			{
				this.HandleMaxLengthAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Unicode"))
			{
				this.HandleUnicodeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Collation"))
			{
				this.HandleCollationAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "FixedLength"))
			{
				this.HandleIsFixedLengthAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Nullable"))
			{
				this.HandleNullableAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00012C0C File Offset: 0x00010E0C
		private void ValidateAndSetBinaryFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00012C18 File Offset: 0x00010E18
		private void ValidateAndSetDecimalFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			byte? b = null;
			Facet facet;
			if (facets.TryGetValue("Precision", out facet) && facet.Value != null)
			{
				b = new byte?((byte)facet.Value);
				FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "Precision");
				byte? b2 = b;
				int? num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
				int num2 = facet2.MinValue.Value;
				if (!((num.GetValueOrDefault() < num2) & (num != null)))
				{
					b2 = b;
					num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
					num2 = facet2.MaxValue.Value;
					if (!((num.GetValueOrDefault() > num2) & (num != null)))
					{
						goto IL_0133;
					}
				}
				this._element.AddError(ErrorCode.PrecisionOutOfRange, EdmSchemaErrorSeverity.Error, Strings.PrecisionOutOfRange(b, facet2.MinValue.Value, facet2.MaxValue.Value, primitiveType.Name));
			}
			IL_0133:
			Facet facet3;
			if (facets.TryGetValue("Scale", out facet3) && facet3.Value != null)
			{
				byte b3 = (byte)facet3.Value;
				FacetDescription facet4 = Helper.GetFacet(primitiveType.FacetDescriptions, "Scale");
				if ((int)b3 < facet4.MinValue.Value || (int)b3 > facet4.MaxValue.Value)
				{
					this._element.AddError(ErrorCode.ScaleOutOfRange, EdmSchemaErrorSeverity.Error, Strings.ScaleOutOfRange(b3, facet4.MinValue.Value, facet4.MaxValue.Value, primitiveType.Name));
					return;
				}
				if (b != null)
				{
					byte? b2 = b;
					int? num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
					int num2 = (int)b3;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						this._element.AddError(ErrorCode.BadPrecisionAndScale, EdmSchemaErrorSeverity.Error, Strings.BadPrecisionAndScale(b, b3));
					}
				}
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00012E70 File Offset: 0x00011070
		private void ValidatePrecisionFacetsForDateTimeFamily(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			byte? b = null;
			Facet facet;
			if (facets.TryGetValue("Precision", out facet) && facet.Value != null)
			{
				b = new byte?((byte)facet.Value);
				FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "Precision");
				byte? b2 = b;
				int? num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
				int num2 = facet2.MinValue.Value;
				if (!((num.GetValueOrDefault() < num2) & (num != null)))
				{
					b2 = b;
					num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
					num2 = facet2.MaxValue.Value;
					if (!((num.GetValueOrDefault() > num2) & (num != null)))
					{
						return;
					}
				}
				this._element.AddError(ErrorCode.PrecisionOutOfRange, EdmSchemaErrorSeverity.Error, Strings.PrecisionOutOfRange(b, facet2.MinValue.Value, facet2.MaxValue.Value, primitiveType.Name));
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00012FAB File Offset: 0x000111AB
		private void ValidateAndSetStringFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00012FB8 File Offset: 0x000111B8
		private void ValidateLengthFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			Facet facet;
			if (!facets.TryGetValue("MaxLength", out facet) || facet.Value == null)
			{
				return;
			}
			if (Helper.IsUnboundedFacetValue(facet))
			{
				return;
			}
			int num = (int)facet.Value;
			FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength");
			int value = facet2.MaxValue.Value;
			int value2 = facet2.MinValue.Value;
			if (num < value2 || num > value)
			{
				this._element.AddError(ErrorCode.InvalidSize, EdmSchemaErrorSeverity.Error, Strings.InvalidSize(num, value2, value, primitiveType.Name));
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00013060 File Offset: 0x00011260
		internal void HandleMaxLengthAttribute(XmlReader reader)
		{
			if (reader.Value.Trim() == "Max")
			{
				this._facetValues.Add("MaxLength", EdmConstants.UnboundedValue);
				return;
			}
			int num = 0;
			if (!this._element.HandleIntAttribute(reader, ref num))
			{
				return;
			}
			this._facetValues.Add("MaxLength", num);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000130C4 File Offset: 0x000112C4
		private void HandleNullableAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Nullable", flag);
				this._nullable = new bool?(flag);
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00013108 File Offset: 0x00011308
		internal void HandleStoreGeneratedPatternAttribute(XmlReader reader)
		{
			string value = reader.Value;
			StoreGeneratedPattern storeGeneratedPattern;
			if (value == "None")
			{
				storeGeneratedPattern = StoreGeneratedPattern.None;
			}
			else if (value == "Identity")
			{
				storeGeneratedPattern = StoreGeneratedPattern.Identity;
			}
			else
			{
				if (!(value == "Computed"))
				{
					return;
				}
				storeGeneratedPattern = StoreGeneratedPattern.Computed;
			}
			this._facetValues.Add("StoreGeneratedPattern", storeGeneratedPattern);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00013168 File Offset: 0x00011368
		internal void HandleConcurrencyModeAttribute(XmlReader reader)
		{
			string value = reader.Value;
			ConcurrencyMode concurrencyMode;
			if (value == "None")
			{
				concurrencyMode = ConcurrencyMode.None;
			}
			else
			{
				if (!(value == "Fixed"))
				{
					return;
				}
				concurrencyMode = ConcurrencyMode.Fixed;
			}
			this._facetValues.Add("ConcurrencyMode", concurrencyMode);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000131B5 File Offset: 0x000113B5
		private void HandleDefaultAttribute(XmlReader reader)
		{
			this._default = reader.Value;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000131C4 File Offset: 0x000113C4
		private void HandlePrecisionAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Precision", b);
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000131FC File Offset: 0x000113FC
		private void HandleScaleAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Scale", b);
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00013234 File Offset: 0x00011434
		private void HandleUnicodeAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Unicode", flag);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00013269 File Offset: 0x00011469
		private void HandleCollationAttribute(XmlReader reader)
		{
			if (string.IsNullOrEmpty(reader.Value))
			{
				return;
			}
			this._facetValues.Add("Collation", reader.Value);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00013290 File Offset: 0x00011490
		private void HandleIsFixedLengthAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("FixedLength", flag);
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000132C8 File Offset: 0x000114C8
		internal void ValidateDefaultValue(SchemaType type)
		{
			if (this._default == null)
			{
				return;
			}
			ScalarType scalarType = type as ScalarType;
			if (scalarType != null)
			{
				this.ValidateScalarMemberDefaultValue(scalarType);
				return;
			}
			this._element.AddError(ErrorCode.DefaultNotAllowed, EdmSchemaErrorSeverity.Error, Strings.DefaultNotAllowed);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00013304 File Offset: 0x00011504
		private void ValidateScalarMemberDefaultValue(ScalarType scalar)
		{
			if (scalar != null)
			{
				switch (scalar.TypeKind)
				{
				case PrimitiveTypeKind.Binary:
					this.ValidateBinaryDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Boolean:
					this.ValidateBooleanDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Byte:
					this.ValidateIntegralDefaultValue(scalar, 0L, 255L);
					return;
				case PrimitiveTypeKind.DateTime:
					this.ValidateDateTimeDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Decimal:
					this.ValidateDecimalDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Double:
					this.ValidateFloatingPointDefaultValue(scalar, double.MinValue, double.MaxValue);
					return;
				case PrimitiveTypeKind.Guid:
					this.ValidateGuidDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Single:
					this.ValidateFloatingPointDefaultValue(scalar, -3.4028234663852886E+38, 3.4028234663852886E+38);
					return;
				case PrimitiveTypeKind.Int16:
					this.ValidateIntegralDefaultValue(scalar, -32768L, 32767L);
					return;
				case PrimitiveTypeKind.Int32:
					this.ValidateIntegralDefaultValue(scalar, -2147483648L, 2147483647L);
					return;
				case PrimitiveTypeKind.Int64:
					this.ValidateIntegralDefaultValue(scalar, long.MinValue, long.MaxValue);
					return;
				case PrimitiveTypeKind.String:
					this._defaultObject = this._default;
					return;
				case PrimitiveTypeKind.Time:
					this.ValidateTimeDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.DateTimeOffset:
					this.ValidateDateTimeOffsetDefaultValue(scalar);
					return;
				}
				this._element.AddError(ErrorCode.DefaultNotAllowed, EdmSchemaErrorSeverity.Error, Strings.DefaultNotAllowed);
				return;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00013444 File Offset: 0x00011644
		private void ValidateBinaryDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			string text = Strings.InvalidDefaultBinaryWithNoMaxLength(this._default);
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, text);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00013481 File Offset: 0x00011681
		private void ValidateBooleanDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultBoolean(this._default));
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000134B0 File Offset: 0x000116B0
		private void ValidateIntegralDefaultValue(ScalarType scalar, long minValue, long maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultIntegral(this._default, minValue, maxValue));
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000134EC File Offset: 0x000116EC
		private void ValidateDateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTime(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffZ".Replace("\\", "")));
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001353C File Offset: 0x0001173C
		private void ValidateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultTime(this._default, "HH\\:mm\\:ss.fffffffZ".Replace("\\", "")));
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001358C File Offset: 0x0001178C
		private void ValidateDateTimeOffsetDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTimeOffset(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffffffz".Replace("\\", "")));
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000135DA File Offset: 0x000117DA
		private void ValidateDecimalDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDecimal(this._default, 38, 38));
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00013618 File Offset: 0x00011818
		private void ValidateFloatingPointDefaultValue(ScalarType scalar, double minValue, double maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultFloatingPoint(this._default, minValue, maxValue));
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00013653 File Offset: 0x00011853
		private void ValidateGuidDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultGuid(this._default));
			}
		}

		// Token: 0x040006D8 RID: 1752
		private readonly Dictionary<string, object> _facetValues;

		// Token: 0x040006D9 RID: 1753
		private readonly SchemaElement _element;

		// Token: 0x040006DA RID: 1754
		private string _default;

		// Token: 0x040006DB RID: 1755
		private object _defaultObject;

		// Token: 0x040006DC RID: 1756
		private bool? _nullable;

		// Token: 0x040006DD RID: 1757
		private TypeUsage _typeUsage;

		// Token: 0x040006DE RID: 1758
		private bool _hasUserDefinedFacets;
	}
}
