using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200167B RID: 5755
	public sealed class TypeFacets
	{
		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x0600919B RID: 37275 RVA: 0x001E3BBF File Offset: 0x001E1DBF
		public int? NumericPrecisionBase
		{
			get
			{
				return this.numericPrecisionBase;
			}
		}

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x0600919C RID: 37276 RVA: 0x001E3BC7 File Offset: 0x001E1DC7
		public int? NumericPrecision
		{
			get
			{
				return this.numericPrecision;
			}
		}

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x0600919D RID: 37277 RVA: 0x001E3BCF File Offset: 0x001E1DCF
		public int? NumericScale
		{
			get
			{
				return this.numericScale;
			}
		}

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x0600919E RID: 37278 RVA: 0x001E3BD7 File Offset: 0x001E1DD7
		public int? DateTimePrecision
		{
			get
			{
				return this.dateTimePrecision;
			}
		}

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x0600919F RID: 37279 RVA: 0x001E3BDF File Offset: 0x001E1DDF
		public long? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x060091A0 RID: 37280 RVA: 0x001E3BE7 File Offset: 0x001E1DE7
		public bool? IsVariableLength
		{
			get
			{
				return this.isVariableLength;
			}
		}

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x060091A1 RID: 37281 RVA: 0x001E3BEF File Offset: 0x001E1DEF
		public string NativeTypeName
		{
			get
			{
				return this.nativeTypeName;
			}
		}

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x060091A2 RID: 37282 RVA: 0x001E3BF7 File Offset: 0x001E1DF7
		public string NativeDefaultExpression
		{
			get
			{
				return this.nativeDefaultExpression;
			}
		}

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x060091A3 RID: 37283 RVA: 0x001E3BFF File Offset: 0x001E1DFF
		public string NativeExpression
		{
			get
			{
				return this.nativeExpression;
			}
		}

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x060091A4 RID: 37284 RVA: 0x001E3C08 File Offset: 0x001E1E08
		public bool IsEmpty
		{
			get
			{
				return this.numericPrecisionBase == null && this.numericPrecision == null && this.numericScale == null && this.dateTimePrecision == null && this.maxLength == null && this.isVariableLength == null && this.nativeTypeName == null && this.nativeDefaultExpression == null && this.nativeExpression == null;
			}
		}

		// Token: 0x060091A5 RID: 37285 RVA: 0x001E3C80 File Offset: 0x001E1E80
		public TypeFacets AddNative(string typeName, string defaultExpression, string computedExpression)
		{
			return new TypeFacets(this.numericPrecisionBase, this.numericPrecision, this.numericScale, this.dateTimePrecision, this.maxLength, this.isVariableLength, typeName, defaultExpression, computedExpression);
		}

		// Token: 0x060091A6 RID: 37286 RVA: 0x001E3CBC File Offset: 0x001E1EBC
		public static TypeFacets NewNumeric(int? precisionBase, int? precision, int? scale, string nativeTypeName = null)
		{
			return new TypeFacets(precisionBase, precision, scale, null, null, null, nativeTypeName, null, null);
		}

		// Token: 0x060091A7 RID: 37287 RVA: 0x001E3CF0 File Offset: 0x001E1EF0
		public static TypeFacets NewDateTime(int? precision, string nativeTypeName = null)
		{
			return new TypeFacets(null, null, null, precision, null, null, nativeTypeName, null, null);
		}

		// Token: 0x060091A8 RID: 37288 RVA: 0x001E3D34 File Offset: 0x001E1F34
		public static TypeFacets NewText(long? maxLength, bool? isVariableLength, string nativeTypeName = null)
		{
			return new TypeFacets(null, null, null, null, maxLength, isVariableLength, nativeTypeName, null, null);
		}

		// Token: 0x060091A9 RID: 37289 RVA: 0x001E3D70 File Offset: 0x001E1F70
		public static TypeFacets NewBinary(long? maxLength, bool? isVariableLength, string nativeTypeName = null)
		{
			return new TypeFacets(null, null, null, null, maxLength, isVariableLength, nativeTypeName, null, null);
		}

		// Token: 0x060091AA RID: 37290 RVA: 0x001E3DAC File Offset: 0x001E1FAC
		public static TypeFacets FromRecord(RecordValue facets)
		{
			for (int i = 0; i < facets.Keys.Length; i++)
			{
				if (!TypeFacets.FacetsKeys.Contains(facets.Keys[i]))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.TypeFacets_UnknownFacet(facets.Keys[i]), facets, null);
				}
			}
			int? int32FieldOrNull = TypeFacets.GetInt32FieldOrNull(facets, "NumericPrecisionBase");
			int? int32FieldOrNull2 = TypeFacets.GetInt32FieldOrNull(facets, "NumericPrecision");
			int? int32FieldOrNull3 = TypeFacets.GetInt32FieldOrNull(facets, "NumericScale");
			if (int32FieldOrNull == null && (int32FieldOrNull2 != null || int32FieldOrNull3 != null))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TypeFacets_NumericPrecisionBaseNotSpecified, facets, null);
			}
			int? int32FieldOrNull4 = TypeFacets.GetInt32FieldOrNull(facets, "DateTimePrecision");
			long? int64FieldOrNull = TypeFacets.GetInt64FieldOrNull(facets, "MaxLength");
			Value fieldOrNull = TypeFacets.GetFieldOrNull(facets, "IsVariableLength");
			bool? flag = (fieldOrNull.IsNull ? null : new bool?(fieldOrNull.AsBoolean));
			Value fieldOrNull2 = TypeFacets.GetFieldOrNull(facets, "NativeTypeName");
			string text = (fieldOrNull2.IsNull ? null : fieldOrNull2.AsString);
			Value fieldOrNull3 = TypeFacets.GetFieldOrNull(facets, "NativeDefaultExpression");
			string text2 = (fieldOrNull3.IsNull ? null : fieldOrNull3.AsString);
			Value fieldOrNull4 = TypeFacets.GetFieldOrNull(facets, "NativeExpression");
			string text3 = (fieldOrNull4.IsNull ? null : fieldOrNull4.AsString);
			return new TypeFacets(int32FieldOrNull, int32FieldOrNull2, int32FieldOrNull3, int32FieldOrNull4, int64FieldOrNull, flag, text, text2, text3);
		}

		// Token: 0x060091AB RID: 37291 RVA: 0x001E3F14 File Offset: 0x001E2114
		private TypeFacets(int? numericPrecisionBase, int? numericPrecision, int? numericScale, int? dateTimePrecision, long? maxLength, bool? isVariableLength, string nativeTypeName = null, string nativeDefaultExpression = null, string nativeExpression = null)
		{
			this.numericPrecisionBase = numericPrecisionBase;
			this.numericPrecision = numericPrecision;
			this.numericScale = numericScale;
			this.dateTimePrecision = dateTimePrecision;
			this.maxLength = maxLength;
			this.isVariableLength = isVariableLength;
			this.nativeTypeName = nativeTypeName;
			this.nativeDefaultExpression = nativeDefaultExpression;
			this.nativeExpression = nativeExpression;
		}

		// Token: 0x060091AC RID: 37292 RVA: 0x001E3F6C File Offset: 0x001E216C
		public RecordValue ToRecord()
		{
			RecordTypeValue facetsType = TypeFacets.FacetsType;
			Value[] array = new Value[9];
			int num = 0;
			int? num2 = this.numericPrecisionBase;
			array[num] = TypeFacets.NewNumberValueOrNull((num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null);
			int num3 = 1;
			num2 = this.numericPrecision;
			array[num3] = TypeFacets.NewNumberValueOrNull((num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null);
			int num4 = 2;
			num2 = this.numericScale;
			array[num4] = TypeFacets.NewNumberValueOrNull((num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null);
			int num5 = 3;
			num2 = this.dateTimePrecision;
			array[num5] = TypeFacets.NewNumberValueOrNull((num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null);
			array[4] = TypeFacets.NewNumberValueOrNull(this.maxLength);
			array[5] = TypeFacets.NewLogicalValueOrNull(this.isVariableLength);
			array[6] = ((this.nativeTypeName != null) ? TextValue.New(this.nativeTypeName) : Value.Null);
			array[7] = ((this.nativeDefaultExpression != null) ? TextValue.New(this.nativeDefaultExpression) : Value.Null);
			array[8] = ((this.nativeExpression != null) ? TextValue.New(this.nativeExpression) : Value.Null);
			return RecordValue.New(facetsType, array);
		}

		// Token: 0x060091AD RID: 37293 RVA: 0x001E40C0 File Offset: 0x001E22C0
		private static Value GetFieldOrNull(RecordValue facets, string field)
		{
			Value value;
			if (facets.TryGetValue(field, out value))
			{
				return value;
			}
			return Value.Null;
		}

		// Token: 0x060091AE RID: 37294 RVA: 0x001E40E0 File Offset: 0x001E22E0
		private static int? GetInt32FieldOrNull(RecordValue facets, string field)
		{
			Value fieldOrNull = TypeFacets.GetFieldOrNull(facets, field);
			if (fieldOrNull.IsNull)
			{
				return null;
			}
			int asInteger = fieldOrNull.AsNumber.AsInteger32;
			if (asInteger < 0)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TypeFacets_MustHaveNonNegativeValues, facets, null);
			}
			return new int?(asInteger);
		}

		// Token: 0x060091AF RID: 37295 RVA: 0x001E4128 File Offset: 0x001E2328
		private static long? GetInt64FieldOrNull(RecordValue facets, string field)
		{
			Value fieldOrNull = TypeFacets.GetFieldOrNull(facets, field);
			if (fieldOrNull.IsNull)
			{
				return null;
			}
			long asInteger = fieldOrNull.AsNumber.AsInteger64;
			if (asInteger < 0L)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.TypeFacets_MustHaveNonNegativeValues, facets, null);
			}
			return new long?(asInteger);
		}

		// Token: 0x060091B0 RID: 37296 RVA: 0x001E4171 File Offset: 0x001E2371
		private static Value NewNumberValueOrNull(long? value)
		{
			if (value == null)
			{
				return Value.Null;
			}
			return NumberValue.New(value.Value);
		}

		// Token: 0x060091B1 RID: 37297 RVA: 0x001E418E File Offset: 0x001E238E
		private static Value NewLogicalValueOrNull(bool? value)
		{
			if (value == null)
			{
				return Value.Null;
			}
			return LogicalValue.New(value.Value);
		}

		// Token: 0x060091B2 RID: 37298 RVA: 0x001E41AC File Offset: 0x001E23AC
		public static bool CompareMaxLength(IEnumerable<TypeFacets> facets, out long? maxValue)
		{
			bool flag = false;
			maxValue = new long?(-1L);
			foreach (TypeFacets typeFacets in facets)
			{
				long? num = typeFacets.MaxLength;
				long? num2 = maxValue;
				long num3 = -1L;
				if ((num2.GetValueOrDefault() == num3) & (num2 != null))
				{
					maxValue = num;
				}
				else
				{
					num2 = num;
					long? num4 = maxValue;
					if (!((num2.GetValueOrDefault() == num4.GetValueOrDefault()) & (num2 != null == (num4 != null))))
					{
						flag = true;
						if (num == null)
						{
							maxValue = null;
							break;
						}
						if (maxValue == null)
						{
							break;
						}
						maxValue = new long?(Math.Max(num.Value, maxValue.Value));
					}
				}
			}
			return flag;
		}

		// Token: 0x060091B3 RID: 37299 RVA: 0x001E429C File Offset: 0x001E249C
		public static bool CompareDateTimePrecision(IEnumerable<TypeFacets> facets, out int? maxValue)
		{
			bool flag = false;
			maxValue = new int?(-1);
			foreach (TypeFacets typeFacets in facets)
			{
				int? num = typeFacets.DateTimePrecision;
				int? num2 = maxValue;
				int num3 = -1;
				if ((num2.GetValueOrDefault() == num3) & (num2 != null))
				{
					maxValue = num;
				}
				else
				{
					num2 = num;
					int? num4 = maxValue;
					if (!((num2.GetValueOrDefault() == num4.GetValueOrDefault()) & (num2 != null == (num4 != null))))
					{
						flag = true;
						if (num == null)
						{
							maxValue = null;
							break;
						}
						if (maxValue == null)
						{
							break;
						}
						maxValue = new int?(Math.Max(num.Value, maxValue.Value));
					}
				}
			}
			return flag;
		}

		// Token: 0x060091B4 RID: 37300 RVA: 0x001E4388 File Offset: 0x001E2588
		public static bool CompareForText(IEnumerable<TypeFacets> facets, out long? greatestMaxLength, out bool? typeNamesDiffer)
		{
			bool flag = false;
			greatestMaxLength = new long?(-1L);
			long? num = new long?(-1L);
			typeNamesDiffer = new bool?(false);
			string text = "";
			foreach (TypeFacets typeFacets in facets)
			{
				num = typeFacets.MaxLength;
				long? num2 = greatestMaxLength;
				long num3 = -1L;
				if ((num2.GetValueOrDefault() == num3) & (num2 != null))
				{
					greatestMaxLength = num;
				}
				else
				{
					num2 = num;
					long? num4 = greatestMaxLength;
					if (!((num2.GetValueOrDefault() == num4.GetValueOrDefault()) & (num2 != null == (num4 != null))))
					{
						flag = true;
						if (num == null)
						{
							greatestMaxLength = null;
							continue;
						}
						if (greatestMaxLength == null)
						{
							continue;
						}
						greatestMaxLength = new long?(Math.Max(num.Value, greatestMaxLength.Value));
					}
				}
				string text2 = typeFacets.NativeTypeName;
				if (text2 == null)
				{
					typeNamesDiffer = null;
				}
				else if (text == "")
				{
					text = text2;
				}
				else if (text2 != text && typeNamesDiffer != null)
				{
					typeNamesDiffer = new bool?(true);
				}
			}
			return flag;
		}

		// Token: 0x060091B5 RID: 37301 RVA: 0x001E44E4 File Offset: 0x001E26E4
		public static bool CompareForNumeric(IEnumerable<TypeFacets> facets, out int? maxIntegralDigits, out int? maxFractionalDigits, out bool majorDifferences)
		{
			bool flag = false;
			majorDifferences = true;
			maxIntegralDigits = null;
			maxFractionalDigits = null;
			int num = -1;
			int? num2 = new int?(-1);
			int? num3 = new int?(-1);
			foreach (TypeFacets typeFacets in facets)
			{
				int? num4 = typeFacets.NumericPrecisionBase;
				if (num4 != null && num == -1)
				{
					num = num4.Value;
				}
				else if (num4 == null || num4.Value != num)
				{
					return true;
				}
				if (num != 2)
				{
					num3 = typeFacets.NumericScale;
					if (num3 == null)
					{
						return true;
					}
					if (maxFractionalDigits == null)
					{
						maxFractionalDigits = num3;
					}
					else
					{
						int? num5 = maxFractionalDigits;
						int? num6 = num3;
						if (!((num5.GetValueOrDefault() == num6.GetValueOrDefault()) & (num5 != null == (num6 != null))))
						{
							flag = true;
							maxFractionalDigits = new int?(Math.Max(maxFractionalDigits.Value, num3.Value));
						}
					}
				}
				num2 = typeFacets.NumericPrecision;
				if (num2 == null)
				{
					return true;
				}
				int num7 = ((num == 2) ? num2.Value : (num2.Value - num3.Value));
				if (maxIntegralDigits == null)
				{
					maxIntegralDigits = new int?(num7);
				}
				else
				{
					int? num6 = maxIntegralDigits;
					int num8 = num7;
					if (!((num6.GetValueOrDefault() == num8) & (num6 != null)))
					{
						flag = true;
						maxIntegralDigits = new int?(Math.Max(maxIntegralDigits.Value, num7));
					}
				}
			}
			if (num == 2)
			{
				maxIntegralDigits = null;
				maxFractionalDigits = null;
			}
			majorDifferences = false;
			return flag;
		}

		// Token: 0x04004E31 RID: 20017
		public const string NumericPrecisionBaseKey = "NumericPrecisionBase";

		// Token: 0x04004E32 RID: 20018
		public const string NumericPrecisionKey = "NumericPrecision";

		// Token: 0x04004E33 RID: 20019
		public const string NumericScaleKey = "NumericScale";

		// Token: 0x04004E34 RID: 20020
		public const string DateTimePrecisionKey = "DateTimePrecision";

		// Token: 0x04004E35 RID: 20021
		public const string MaxLengthKey = "MaxLength";

		// Token: 0x04004E36 RID: 20022
		public const string IsVariableLengthKey = "IsVariableLength";

		// Token: 0x04004E37 RID: 20023
		public const string NativeTypeNameKey = "NativeTypeName";

		// Token: 0x04004E38 RID: 20024
		public const string NativeDefaultExpressionKey = "NativeDefaultExpression";

		// Token: 0x04004E39 RID: 20025
		public const string NativeExpressionKey = "NativeExpression";

		// Token: 0x04004E3A RID: 20026
		public static readonly Keys FacetsKeys = Keys.New(new string[] { "NumericPrecisionBase", "NumericPrecision", "NumericScale", "DateTimePrecision", "MaxLength", "IsVariableLength", "NativeTypeName", "NativeDefaultExpression", "NativeExpression" });

		// Token: 0x04004E3B RID: 20027
		public static readonly RecordTypeValue FacetsType = RecordTypeValue.New(RecordValue.New(TypeFacets.FacetsKeys, new Value[]
		{
			RecordTypeValue.NewField(TypeValue.Int32.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Int32.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Int32.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Int32.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Int64.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Logical.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Text.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Text.Nullable, null),
			RecordTypeValue.NewField(TypeValue.Text.Nullable, null)
		}), false);

		// Token: 0x04004E3C RID: 20028
		public static readonly TypeFacets None = new TypeFacets(null, null, null, null, null, null, null, null, null);

		// Token: 0x04004E3D RID: 20029
		private readonly int? numericPrecisionBase;

		// Token: 0x04004E3E RID: 20030
		private readonly int? numericPrecision;

		// Token: 0x04004E3F RID: 20031
		private readonly int? numericScale;

		// Token: 0x04004E40 RID: 20032
		private readonly int? dateTimePrecision;

		// Token: 0x04004E41 RID: 20033
		private readonly long? maxLength;

		// Token: 0x04004E42 RID: 20034
		private readonly bool? isVariableLength;

		// Token: 0x04004E43 RID: 20035
		private readonly string nativeTypeName;

		// Token: 0x04004E44 RID: 20036
		private readonly string nativeDefaultExpression;

		// Token: 0x04004E45 RID: 20037
		private readonly string nativeExpression;
	}
}
