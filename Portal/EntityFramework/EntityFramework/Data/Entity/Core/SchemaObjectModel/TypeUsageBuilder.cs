using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000324 RID: 804
	internal class TypeUsageBuilder
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x0006D442 File Offset: 0x0006B642
		internal TypeUsageBuilder(SchemaElement element)
		{
			this._element = element;
			this._facetValues = new Dictionary<string, object>();
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x0006D45C File Offset: 0x0006B65C
		internal TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x0006D464 File Offset: 0x0006B664
		internal bool Nullable
		{
			get
			{
				return this._nullable == null || this._nullable.Value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x0006D480 File Offset: 0x0006B680
		internal string Default
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x0006D488 File Offset: 0x0006B688
		internal object DefaultAsObject
		{
			get
			{
				return this._defaultObject;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x0006D490 File Offset: 0x0006B690
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._hasUserDefinedFacets;
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0006D498 File Offset: 0x0006B698
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
				else if (edmType is PrimitiveType && ((PrimitiveType)edmType).PrimitiveTypeKind == PrimitiveTypeKind.String && keyValuePair.Key == "Collation")
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

		// Token: 0x06002637 RID: 9783 RVA: 0x0006D71C File Offset: 0x0006B91C
		internal void ValidateAndSetTypeUsage(EdmType edmType, bool complainOnMissingFacet)
		{
			Dictionary<string, Facet> dictionary;
			this.TryGetFacets(edmType, complainOnMissingFacet, out dictionary);
			this._typeUsage = TypeUsage.Create(edmType, dictionary.Values);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0006D748 File Offset: 0x0006B948
		internal void ValidateAndSetTypeUsage(ScalarType scalar, bool complainOnMissingFacet)
		{
			Trace.Assert(this._element != null);
			Trace.Assert(scalar != null);
			if (Helper.IsSpatialType(scalar.Type) && !this._facetValues.ContainsKey("IsStrict") && !this._element.Schema.UseStrongSpatialTypes)
			{
				this._facetValues.Add("IsStrict", false);
			}
			Dictionary<string, Facet> dictionary;
			if (this.TryGetFacets(scalar.Type, complainOnMissingFacet, out dictionary))
			{
				switch (scalar.TypeKind)
				{
				case PrimitiveTypeKind.Binary:
					this.ValidateAndSetBinaryFacets(scalar.Type, dictionary);
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
				case PrimitiveTypeKind.Geometry:
				case PrimitiveTypeKind.Geography:
				case PrimitiveTypeKind.GeometryPoint:
				case PrimitiveTypeKind.GeometryLineString:
				case PrimitiveTypeKind.GeometryPolygon:
				case PrimitiveTypeKind.GeometryMultiPoint:
				case PrimitiveTypeKind.GeometryMultiLineString:
				case PrimitiveTypeKind.GeometryMultiPolygon:
				case PrimitiveTypeKind.GeometryCollection:
				case PrimitiveTypeKind.GeographyPoint:
				case PrimitiveTypeKind.GeographyLineString:
				case PrimitiveTypeKind.GeographyPolygon:
				case PrimitiveTypeKind.GeographyMultiPoint:
				case PrimitiveTypeKind.GeographyMultiLineString:
				case PrimitiveTypeKind.GeographyMultiPolygon:
				case PrimitiveTypeKind.GeographyCollection:
					this.ValidateSpatialFacets(scalar.Type, dictionary);
					break;
				}
			}
			this._typeUsage = TypeUsage.Create(scalar.Type, dictionary.Values);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0006D8B8 File Offset: 0x0006BAB8
		internal void ValidateEnumFacets(SchemaEnumType schemaEnumType)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this._facetValues)
			{
				if (keyValuePair.Key != "Nullable" && keyValuePair.Key != "StoreGeneratedPattern" && keyValuePair.Key != "ConcurrencyMode")
				{
					this._element.AddError(ErrorCode.FacetNotAllowedByType, EdmSchemaErrorSeverity.Error, Strings.FacetNotAllowed(keyValuePair.Key, schemaEnumType.FQName));
				}
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0006D960 File Offset: 0x0006BB60
		internal bool HandleAttribute(XmlReader reader)
		{
			bool flag = this.InternalHandleAttribute(reader);
			this._hasUserDefinedFacets = this._hasUserDefinedFacets || flag;
			return flag;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x0006D984 File Offset: 0x0006BB84
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
			if (SchemaElement.CanHandleAttribute(reader, "SRID"))
			{
				this.HandleSridAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0006DA9A File Offset: 0x0006BC9A
		private void ValidateAndSetBinaryFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
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

		// Token: 0x0600263E RID: 9790 RVA: 0x0006DCFC File Offset: 0x0006BEFC
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

		// Token: 0x0600263F RID: 9791 RVA: 0x0006DE37 File Offset: 0x0006C037
		private void ValidateAndSetStringFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0006DE44 File Offset: 0x0006C044
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

		// Token: 0x06002641 RID: 9793 RVA: 0x0006DEEC File Offset: 0x0006C0EC
		private void ValidateSpatialFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			if (this._facetValues.ContainsKey("ConcurrencyMode"))
			{
				this._element.AddError(ErrorCode.FacetNotAllowedByType, EdmSchemaErrorSeverity.Error, Strings.FacetNotAllowed("ConcurrencyMode", type.FullName));
			}
			Facet facet;
			if (this._element.Schema.DataModel == SchemaDataModelOption.EntityDataModel && (!facets.TryGetValue("IsStrict", out facet) || (bool)facet.Value))
			{
				this._element.AddError(ErrorCode.UnexpectedSpatialType, EdmSchemaErrorSeverity.Error, Strings.SpatialWithUseStrongSpatialTypesFalse);
			}
			Facet facet2;
			if (!facets.TryGetValue("SRID", out facet2) || facet2.Value == null)
			{
				return;
			}
			if (Helper.IsVariableFacetValue(facet2))
			{
				return;
			}
			int num = (int)facet2.Value;
			FacetDescription facet3 = Helper.GetFacet(primitiveType.FacetDescriptions, "SRID");
			int value = facet3.MaxValue.Value;
			int value2 = facet3.MinValue.Value;
			if (num < value2 || num > value)
			{
				this._element.AddError(ErrorCode.InvalidSystemReferenceId, EdmSchemaErrorSeverity.Error, Strings.InvalidSystemReferenceId(num, value2, value, primitiveType.Name));
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0006E00C File Offset: 0x0006C20C
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

		// Token: 0x06002643 RID: 9795 RVA: 0x0006E070 File Offset: 0x0006C270
		internal void HandleSridAttribute(XmlReader reader)
		{
			if (reader.Value.Trim() == "Variable")
			{
				this._facetValues.Add("SRID", EdmConstants.VariableValue);
				return;
			}
			int num = 0;
			if (!this._element.HandleIntAttribute(reader, ref num))
			{
				return;
			}
			this._facetValues.Add("SRID", num);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
		private void HandleNullableAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Nullable", flag);
				this._nullable = new bool?(flag);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0006E118 File Offset: 0x0006C318
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

		// Token: 0x06002646 RID: 9798 RVA: 0x0006E178 File Offset: 0x0006C378
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

		// Token: 0x06002647 RID: 9799 RVA: 0x0006E1C5 File Offset: 0x0006C3C5
		private void HandleDefaultAttribute(XmlReader reader)
		{
			this._default = reader.Value;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x0006E1D4 File Offset: 0x0006C3D4
		private void HandlePrecisionAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Precision", b);
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0006E20C File Offset: 0x0006C40C
		private void HandleScaleAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Scale", b);
			}
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0006E244 File Offset: 0x0006C444
		private void HandleUnicodeAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Unicode", flag);
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x0006E279 File Offset: 0x0006C479
		private void HandleCollationAttribute(XmlReader reader)
		{
			if (string.IsNullOrEmpty(reader.Value))
			{
				return;
			}
			this._facetValues.Add("Collation", reader.Value);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0006E2A0 File Offset: 0x0006C4A0
		private void HandleIsFixedLengthAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("FixedLength", flag);
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0006E2D8 File Offset: 0x0006C4D8
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

		// Token: 0x0600264E RID: 9806 RVA: 0x0006E314 File Offset: 0x0006C514
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

		// Token: 0x0600264F RID: 9807 RVA: 0x0006E454 File Offset: 0x0006C654
		private void ValidateBinaryDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			string text = Strings.InvalidDefaultBinaryWithNoMaxLength(this._default);
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, text);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0006E491 File Offset: 0x0006C691
		private void ValidateBooleanDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultBoolean(this._default));
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0006E4C0 File Offset: 0x0006C6C0
		private void ValidateIntegralDefaultValue(ScalarType scalar, long minValue, long maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultIntegral(this._default, minValue, maxValue));
			}
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0006E4FC File Offset: 0x0006C6FC
		private void ValidateDateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTime(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffZ".Replace("\\", "")));
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0006E54C File Offset: 0x0006C74C
		private void ValidateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultTime(this._default, "HH\\:mm\\:ss.fffffffZ".Replace("\\", "")));
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0006E59C File Offset: 0x0006C79C
		private void ValidateDateTimeOffsetDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTimeOffset(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffffffz".Replace("\\", "")));
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0006E5EA File Offset: 0x0006C7EA
		private void ValidateDecimalDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDecimal(this._default, 38, 38));
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0006E628 File Offset: 0x0006C828
		private void ValidateFloatingPointDefaultValue(ScalarType scalar, double minValue, double maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultFloatingPoint(this._default, minValue, maxValue));
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0006E663 File Offset: 0x0006C863
		private void ValidateGuidDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultGuid(this._default));
			}
		}

		// Token: 0x04000D65 RID: 3429
		private readonly Dictionary<string, object> _facetValues;

		// Token: 0x04000D66 RID: 3430
		private readonly SchemaElement _element;

		// Token: 0x04000D67 RID: 3431
		private string _default;

		// Token: 0x04000D68 RID: 3432
		private object _defaultObject;

		// Token: 0x04000D69 RID: 3433
		private bool? _nullable;

		// Token: 0x04000D6A RID: 3434
		private TypeUsage _typeUsage;

		// Token: 0x04000D6B RID: 3435
		private bool _hasUserDefinedFacets;
	}
}
