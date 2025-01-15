using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Utils
{
	// Token: 0x02000032 RID: 50
	public sealed class SafeExpressionsAndLegacyComparison
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004FAB File Offset: 0x000031AB
		public SafeExpressionsAndLegacyComparison(object safeExprResult, object legacyExprResult)
		{
			this._safeResult = safeExprResult;
			this._legacyResult = legacyExprResult;
			this._mismatchDetail = string.Empty;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004FCC File Offset: 0x000031CC
		public string MismatchDetail
		{
			get
			{
				return this._mismatchDetail;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004FD4 File Offset: 0x000031D4
		public bool CompareExpressionResults()
		{
			this._mismatchDetail = string.Empty;
			return this.CompareObjects(this._legacyResult, this._safeResult);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004FF4 File Offset: 0x000031F4
		private bool CompareObjects(object legacyResult, object safeResult)
		{
			bool flag;
			if (this.TryCheckForNullValues(legacyResult, safeResult, out flag))
			{
				if (!flag)
				{
					this.WriteMismatchDetails();
				}
				return flag;
			}
			TypeCode typeCode = Type.GetTypeCode(legacyResult.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(safeResult.GetType());
			if (typeCode != typeCode2)
			{
				if (!TypeUtils.IsNumeric(typeCode) || !TypeUtils.IsNumeric(typeCode2))
				{
					this.WriteMismatchDetails();
					return false;
				}
				legacyResult = Convert.ToDouble(legacyResult);
				safeResult = Convert.ToDouble(safeResult);
				typeCode = TypeCode.Double;
			}
			Type legacyType = legacyResult.GetType();
			if (legacyType.IsEnum)
			{
				Type safeType = safeResult.GetType();
				return this.CompareForMismatch(() => legacyType == safeType && legacyResult.Equals(safeResult));
			}
			if (legacyType.IsArray)
			{
				return this.CompareForMismatch(() => this.CompareArrays(legacyResult, safeResult));
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				return true;
			case TypeCode.Object:
				if (legacyType == typeof(Guid))
				{
					return this.CompareForMismatch(() => ((Guid)legacyResult).CompareTo(safeResult) == 0);
				}
				if (legacyType == typeof(DateTimeOffset))
				{
					return this.CompareForMismatch(() => ((DateTimeOffset)legacyResult).CompareTo((DateTimeOffset)safeResult) == 0);
				}
				if (legacyType == typeof(TimeSpan))
				{
					return this.CompareForMismatch(() => ((TimeSpan)legacyResult).CompareTo(safeResult) == 0);
				}
				return this.CompareForMismatch(() => this.CompareComplexObjects(legacyResult, safeResult));
			case TypeCode.Boolean:
				return this.CompareForMismatch(() => ((bool)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Char:
				return this.CompareForMismatch(() => ((char)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.SByte:
				return this.CompareForMismatch(() => ((sbyte)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Byte:
				return this.CompareForMismatch(() => ((byte)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Int16:
				return this.CompareForMismatch(() => ((short)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.UInt16:
				return this.CompareForMismatch(() => ((ushort)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Int32:
				return this.CompareForMismatch(() => ((int)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.UInt32:
				return this.CompareForMismatch(() => ((uint)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Int64:
				return this.CompareForMismatch(() => ((long)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.UInt64:
				return this.CompareForMismatch(() => ((ulong)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Single:
				return this.CompareForMismatch(() => ((float)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Double:
				return this.CompareForMismatch(() => ((double)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.Decimal:
				return this.CompareForMismatch(() => ((decimal)legacyResult).CompareTo(safeResult) == 0);
			case TypeCode.DateTime:
				return this.CompareForMismatch(() => this.CompareDateTime((DateTime)legacyResult, (DateTime)safeResult));
			case TypeCode.String:
				return this.CompareForMismatch(() => legacyResult.Equals(safeResult));
			}
			throw new InvalidOperationException(string.Format("Unknown TypeCode {0}", typeCode));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005344 File Offset: 0x00003544
		private bool CompareDateTime(DateTime a, DateTime b)
		{
			return (a - b).Duration().TotalSeconds < 5.0;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005373 File Offset: 0x00003573
		private bool CompareForMismatch(Func<bool> comparisonFunc)
		{
			bool flag = comparisonFunc();
			if (!flag)
			{
				this.WriteMismatchDetails();
			}
			return flag;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005384 File Offset: 0x00003584
		private bool CompareComplexObjects(object objA, object objB)
		{
			if (objA.Equals(objB))
			{
				return true;
			}
			PropertyInfo[] properties = objA.GetType().GetProperties();
			PropertyInfo[] properties2 = objB.GetType().GetProperties();
			int? num = ((properties != null) ? new int?(properties.Length) : null);
			int? num2 = ((properties2 != null) ? new int?(properties2.Length) : null);
			if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
			{
				return false;
			}
			foreach (PropertyInfo propertyInfo in properties)
			{
				PropertyInfo property = objB.GetType().GetProperty(propertyInfo.Name);
				object value = propertyInfo.GetValue(objA);
				object value2 = property.GetValue(objB);
				if (this.NeitherIsNull(value, value2))
				{
					if (!this.CompareObjects(value, value2))
					{
						return false;
					}
				}
				else if (this.OneIsNull(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005474 File Offset: 0x00003674
		private bool CompareArrays(object a1, object a2)
		{
			Array array = (Array)a1;
			Array array2 = (Array)a2;
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!this.CompareObjects(array.GetValue(i), array2.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000054C9 File Offset: 0x000036C9
		private bool TryCheckForNullValues(object legacyExprResult, object safeExprResult, out bool result)
		{
			result = false;
			if (this.BothAreNull(legacyExprResult, safeExprResult))
			{
				result = true;
				return true;
			}
			if (this.OneIsNull(legacyExprResult, safeExprResult))
			{
				result = false;
				return true;
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000054ED File Offset: 0x000036ED
		private bool NeitherIsNull(object objA, object objB)
		{
			return objA != null && objB != null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000054F8 File Offset: 0x000036F8
		private bool BothAreNull(object objA, object objB)
		{
			return objA == null && objB == null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005503 File Offset: 0x00003703
		private bool OneIsNull(object objA, object objB)
		{
			return (objA == null || objB == null) && objA != objB;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005514 File Offset: 0x00003714
		private void WriteMismatchDetails()
		{
			StringWriter stringWriter = new StringWriter();
			using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
			{
				jsonWriter.Formatting = Formatting.None;
				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("SafeExpressionsResult");
				this.WriteObjectProperties(jsonWriter, this._safeResult);
				jsonWriter.WritePropertyName("LegacyResult");
				this.WriteObjectProperties(jsonWriter, this._legacyResult);
				if (this.NeitherIsNull(this._safeResult, this._legacyResult))
				{
					this.WriteTypeBasedDifferences(jsonWriter, this._legacyResult, this._safeResult);
				}
				jsonWriter.WriteEndObject();
				this._mismatchDetail = stringWriter.ToString();
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000055C0 File Offset: 0x000037C0
		private void WriteObjectProperties(JsonWriter jsonWriter, object result)
		{
			Type type = ((result != null) ? result.GetType() : null);
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("ObjectType");
			jsonWriter.WriteValue((type != null) ? type.Name : null);
			if (result != null)
			{
				this.WriteTypeBasedDetails(jsonWriter, result, type);
			}
			jsonWriter.WriteEndObject();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005610 File Offset: 0x00003810
		private void WriteTypeBasedDetails(JsonWriter jsonWriter, object result, Type resultType)
		{
			switch (Type.GetTypeCode(resultType))
			{
			case TypeCode.Object:
			{
				if (resultType == typeof(Guid) || resultType == typeof(DateTimeOffset) || resultType == typeof(TimeSpan))
				{
					this.WriteObjectLength(jsonWriter, result.ToString().Length);
					return;
				}
				if (resultType.IsArray)
				{
					this.WriteArrayLength(jsonWriter, result);
					return;
				}
				PropertyInfo[] properties = resultType.GetProperties();
				int num = ((properties != null) ? properties.Length : 0);
				this.WriteObjectLength(jsonWriter, num);
				break;
			}
			case TypeCode.DBNull:
			case TypeCode.Char:
			case (TypeCode)17:
				break;
			case TypeCode.Boolean:
				jsonWriter.WritePropertyName("BooleanResult");
				jsonWriter.WriteValue((bool)result);
				return;
			case TypeCode.SByte:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign((sbyte)result));
				return;
			case TypeCode.Byte:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				return;
			case TypeCode.Int16:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign((short)result));
				return;
			case TypeCode.UInt16:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				return;
			case TypeCode.Int32:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign((int)result));
				return;
			case TypeCode.UInt32:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				return;
			case TypeCode.Int64:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign((long)result));
				return;
			case TypeCode.UInt64:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				return;
			case TypeCode.Single:
			{
				float num2 = (float)result;
				if (float.IsNaN(num2) || float.IsInfinity(num2))
				{
					jsonWriter.WritePropertyName("SingleResult");
					jsonWriter.WriteValue(num2);
					return;
				}
				this.WriteObjectLength(jsonWriter, num2.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign(num2));
				this.WriteDecimalIndexForNumericResult(jsonWriter, num2.ToString());
				return;
			}
			case TypeCode.Double:
			{
				double num3 = (double)result;
				if (double.IsNaN(num3) || double.IsInfinity(num3))
				{
					jsonWriter.WritePropertyName("DoubleResult");
					jsonWriter.WriteValue(num3);
					return;
				}
				this.WriteObjectLength(jsonWriter, num3.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign(num3));
				this.WriteDecimalIndexForNumericResult(jsonWriter, num3.ToString());
				return;
			}
			case TypeCode.Decimal:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				this.WriteSignForNumericResult(jsonWriter, Math.Sign((decimal)result));
				this.WriteDecimalIndexForNumericResult(jsonWriter, result.ToString());
				return;
			case TypeCode.DateTime:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				return;
			case TypeCode.String:
				this.WriteObjectLength(jsonWriter, result.ToString().Length);
				return;
			default:
				return;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000058EC File Offset: 0x00003AEC
		private void WriteTypeBasedDifferences(JsonWriter jsonWriter, object legacyResult, object safeExprResult)
		{
			TypeCode typeCode = Type.GetTypeCode(safeExprResult.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(legacyResult.GetType());
			if (typeCode != typeCode2)
			{
				return;
			}
			switch (typeCode)
			{
			case TypeCode.Object:
				if (this._safeResult.GetType() == typeof(Guid))
				{
					int num = ((Guid)this._safeResult).CompareTo((Guid)this._legacyResult);
					jsonWriter.WritePropertyName("GuidResultDiff");
					jsonWriter.WriteValue(num);
					this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
					return;
				}
				if (this._safeResult.GetType() == typeof(DateTimeOffset))
				{
					TimeSpan timeSpan = (DateTimeOffset)this._safeResult - (DateTimeOffset)this._legacyResult;
					jsonWriter.WritePropertyName("DateTimeOffsetResultDiffMs");
					jsonWriter.WriteValue(timeSpan.TotalMilliseconds);
					this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
					return;
				}
				if (this._safeResult.GetType() == typeof(TimeSpan))
				{
					TimeSpan timeSpan2 = (TimeSpan)this._safeResult - (TimeSpan)this._legacyResult;
					jsonWriter.WritePropertyName("TimeSpanResultDiffMs");
					jsonWriter.WriteValue(timeSpan2.TotalMilliseconds);
					this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
					return;
				}
				if (this._safeResult.GetType().IsArray)
				{
					this.WriteArrayMismatchingIndices(jsonWriter);
				}
				break;
			case TypeCode.DBNull:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case (TypeCode)17:
				break;
			case TypeCode.SByte:
			{
				long num2 = (long)((sbyte)this._safeResult - (sbyte)this._legacyResult);
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Byte:
			{
				long num2 = (long)((byte)this._safeResult - (byte)this._legacyResult);
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Int16:
			{
				long num2 = (long)((short)this._safeResult - (short)this._legacyResult);
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.UInt16:
			{
				long num2 = (long)((ushort)this._safeResult - (ushort)this._legacyResult);
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Int32:
			{
				long num2 = (long)((int)this._safeResult - (int)this._legacyResult);
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.UInt32:
			{
				long num2 = (long)((ulong)((uint)this._safeResult - (uint)this._legacyResult));
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Int64:
			{
				long num2 = (long)this._safeResult - (long)this._legacyResult;
				this.WriteNumericResultDiff(jsonWriter, num2);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.UInt64:
			{
				ulong num3 = (ulong)this._safeResult - (ulong)this._legacyResult;
				this.WriteNumericResultDiff(jsonWriter, num3);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Single:
			{
				float num4 = (float)this._safeResult - (float)this._legacyResult;
				this.WriteNumericResultDiff(jsonWriter, num4);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Double:
			{
				double num5 = (double)this._safeResult - (double)this._legacyResult;
				this.WriteNumericResultDiff(jsonWriter, num5);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.Decimal:
			{
				decimal num6 = (decimal)this._safeResult - (decimal)this._legacyResult;
				this.WriteNumericResultDiff(jsonWriter, num6);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.DateTime:
			{
				TimeSpan timeSpan3 = (DateTime)this._safeResult - (DateTime)this._legacyResult;
				jsonWriter.WritePropertyName("DateTimeResultDiffMs");
				jsonWriter.WriteValue(timeSpan3.TotalMilliseconds);
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			}
			case TypeCode.String:
				this.WriteMismatchingIndicesForSimpleObjects(jsonWriter, this._legacyResult.ToString(), this._safeResult.ToString());
				return;
			default:
				return;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005E2F File Offset: 0x0000402F
		private void WriteObjectLength(JsonWriter jsonWriter, int length)
		{
			jsonWriter.WritePropertyName("ResultLength");
			jsonWriter.WriteValue(length);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005E43 File Offset: 0x00004043
		private void WriteSignForNumericResult(JsonWriter jsonWriter, int sign)
		{
			jsonWriter.WritePropertyName("NumericResultSign");
			jsonWriter.WriteValue(sign);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005E57 File Offset: 0x00004057
		private void WriteDecimalIndexForNumericResult(JsonWriter jsonWriter, string strResult)
		{
			jsonWriter.WritePropertyName("NumericResultDecimalIndex");
			jsonWriter.WriteValue(strResult.IndexOf("."));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005E75 File Offset: 0x00004075
		private void WriteNumericResultDiff(JsonWriter jsonWriter, object diff)
		{
			jsonWriter.WritePropertyName("NumericResultDiff");
			jsonWriter.WriteValue(diff);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005E8C File Offset: 0x0000408C
		private void WriteMismatchingIndicesForSimpleObjects(JsonWriter jsonWriter, string legacyResult, string safeExprResult)
		{
			jsonWriter.WritePropertyName("MismatchingIndices");
			jsonWriter.WriteStartArray();
			int num = (string.IsNullOrEmpty(safeExprResult) ? 0 : safeExprResult.Length);
			int num2 = (string.IsNullOrEmpty(legacyResult) ? 0 : legacyResult.Length);
			int num3 = ((num > num2) ? num : num2);
			for (int i = 0; i < num3; i++)
			{
				if (i >= num2 || i >= num || safeExprResult[i] != legacyResult[i])
				{
					jsonWriter.WriteValue(i);
				}
			}
			jsonWriter.WriteEndArray();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005F08 File Offset: 0x00004108
		private void WriteArrayLength(JsonWriter jsonWriter, object result)
		{
			Array array = result as Array;
			jsonWriter.WritePropertyName("ResultLength");
			jsonWriter.WriteValue(array.Length);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005F34 File Offset: 0x00004134
		private void WriteArrayMismatchingIndices(JsonWriter jsonWriter)
		{
			jsonWriter.WritePropertyName("MismatchingIndices");
			jsonWriter.WriteStartArray();
			Array array = this._legacyResult as Array;
			Array array2 = this._safeResult as Array;
			int length = array2.Length;
			int length2 = array.Length;
			int num = ((length > length2) ? length : length2);
			for (int i = 0; i < num; i++)
			{
				if (i >= length2 || i >= length)
				{
					jsonWriter.WriteValue(i);
				}
				else
				{
					object value = array.GetValue(i);
					object value2 = array2.GetValue(i);
					if (!value.Equals(value2))
					{
						jsonWriter.WriteValue(i);
					}
				}
			}
			jsonWriter.WriteEndArray();
		}

		// Token: 0x04000052 RID: 82
		private const string SafeExpressionsResult = "SafeExpressionsResult";

		// Token: 0x04000053 RID: 83
		private const string LegacyResult = "LegacyResult";

		// Token: 0x04000054 RID: 84
		private const string ObjectType = "ObjectType";

		// Token: 0x04000055 RID: 85
		private const string ResultLength = "ResultLength";

		// Token: 0x04000056 RID: 86
		private const string MismatchingIndices = "MismatchingIndices";

		// Token: 0x04000057 RID: 87
		private const string BooleanResult = "BooleanResult";

		// Token: 0x04000058 RID: 88
		private const string SingleResult = "SingleResult";

		// Token: 0x04000059 RID: 89
		private const string DoubleResult = "DoubleResult";

		// Token: 0x0400005A RID: 90
		private const string NumericResultSign = "NumericResultSign";

		// Token: 0x0400005B RID: 91
		private const string NumericResultDecimalIndex = "NumericResultDecimalIndex";

		// Token: 0x0400005C RID: 92
		private const string Decimal = ".";

		// Token: 0x0400005D RID: 93
		private const string GuidResultDiff = "GuidResultDiff";

		// Token: 0x0400005E RID: 94
		private const string DateTimeResultDiffMs = "DateTimeResultDiffMs";

		// Token: 0x0400005F RID: 95
		private const string DateTimeOffsetResultDiffMs = "DateTimeOffsetResultDiffMs";

		// Token: 0x04000060 RID: 96
		private const string TimeSpanResultDiffMs = "TimeSpanResultDiffMs";

		// Token: 0x04000061 RID: 97
		private const string NumericResultDiff = "NumericResultDiff";

		// Token: 0x04000062 RID: 98
		private readonly object _safeResult;

		// Token: 0x04000063 RID: 99
		private readonly object _legacyResult;

		// Token: 0x04000064 RID: 100
		private string _mismatchDetail;
	}
}
