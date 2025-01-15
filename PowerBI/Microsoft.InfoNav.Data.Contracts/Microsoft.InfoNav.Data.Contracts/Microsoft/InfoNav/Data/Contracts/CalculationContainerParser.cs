using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Microsoft.InfoNav.Data.PrimitiveValues;
using Newtonsoft.Json.Linq;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200007F RID: 127
	internal sealed class CalculationContainerParser
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00007B78 File Offset: 0x00005D78
		internal CalculationContainerParser(DsrNames names, DictionaryEncodingHandler dictEnc)
		{
			this._names = names;
			this._dictEnc = dictEnc;
			this._containerToLastSeenCalculationsMap = new Dictionary<string, Calculation[]>(StringComparer.Ordinal);
			this._nullHandler = new NullValueEncodingHandler();
			this._repeatedHandler = new RepeatedValueEncodingHandler();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00007BB4 File Offset: 0x00005DB4
		internal List<Calculation> ParseCalculationsContainer(JObject calcContainerObj, string containerId, DsrCalculationsBuilder calcsBuilder)
		{
			JArray jarray = (JArray)calcContainerObj[this._names.CalcSchema];
			if (jarray != null)
			{
				List<CalculationMetadata> list = jarray.Parse(new Func<JObject, CalculationMetadata>(this.ParseCalculationMetadata));
				calcsBuilder.SetCalcMetadata(list);
			}
			if (calcsBuilder.Schema == null)
			{
				return null;
			}
			JArray jarray2 = (JArray)calcContainerObj[this._names.Calculations];
			this.SetupEncodingHandlers(calcContainerObj, containerId, calcsBuilder, jarray2);
			if (jarray2 == null)
			{
				return this.ParseOptimizedCalculations<JObject>(calcContainerObj, calcsBuilder, new Func<JObject, int, string, object>(CalculationContainerParser.ParseCalcValueFromParentProperty));
			}
			return this.ParseOptimizedCalculations<JArray>(jarray2, calcsBuilder, new Func<JArray, int, string, object>(CalculationContainerParser.ParseCalcValueFromArray));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00007C4C File Offset: 0x00005E4C
		private CalculationMetadata ParseCalculationMetadata(JObject jMetadata)
		{
			JToken jtoken = jMetadata[this._names.DictionaryId];
			return new CalculationMetadata
			{
				Id = (string)jMetadata[this._names.Id],
				Type = jMetadata[this._names.DataType].ToObject<ConceptualPrimitiveType>(),
				DictionaryId = ((jtoken != null) ? ((string)jtoken) : null)
			};
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00007CC1 File Offset: 0x00005EC1
		private static object ParseCalcValueFromParentProperty(JObject calcContainerObj, int index, string calcId)
		{
			return calcContainerObj[calcId];
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00007CCA File Offset: 0x00005ECA
		private static object ParseCalcValueFromArray(JArray calcArray, int index, string calcId)
		{
			return calcArray[index];
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007CD4 File Offset: 0x00005ED4
		private List<Calculation> ParseOptimizedCalculations<JValueType>(JValueType calcContainerObj, DsrCalculationsBuilder calcsBuilder, Func<JValueType, int, string, object> getValue) where JValueType : JToken
		{
			IReadOnlyList<CalculationMetadata> schema = calcsBuilder.Schema;
			List<Calculation> list = new List<Calculation>(schema.Count);
			int num = 0;
			for (int i = 0; i < schema.Count; i++)
			{
				CalculationMetadata calculationMetadata = schema[i];
				object obj;
				if (this._repeatedHandler.TryHandleValue(i, out obj))
				{
					num++;
				}
				else if (this._nullHandler.TryHandleValue(i, out obj))
				{
					num++;
					this._lastSeenCalculations[i] = this.CreateCalculation(calculationMetadata.Id, obj, true);
				}
				else
				{
					int num2 = i - num;
					object obj2 = getValue(calcContainerObj, num2, calculationMetadata.Id);
					bool flag;
					obj = this.ParseOptimizedCalculationValue(obj2, calculationMetadata, out flag);
					this._lastSeenCalculations[i] = this.CreateCalculation(calculationMetadata.Id, obj, !flag);
				}
				list.Add(this._lastSeenCalculations[i]);
			}
			return list;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007DAA File Offset: 0x00005FAA
		private Calculation CreateCalculation(string id, object value, bool isRawValue)
		{
			if (isRawValue)
			{
				return new Calculation
				{
					Id = id,
					RawValue = value
				};
			}
			return new Calculation
			{
				Id = id,
				JsonValue = value
			};
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007DD8 File Offset: 0x00005FD8
		private object ParseOptimizedCalculationValue(object jValue, CalculationMetadata calcMetadata, out bool isAlreadyVariantEncoded)
		{
			if (jValue == null)
			{
				isAlreadyVariantEncoded = true;
				return null;
			}
			object value = ((JValue)jValue).Value;
			object obj = null;
			isAlreadyVariantEncoded = false;
			if (this._dictEnc == null || !this._dictEnc.TryHandleValue(calcMetadata, value, new Func<CalculationMetadata, object, object>(CalculationContainerParser.ParseValue), out obj))
			{
				if (calcMetadata.Type == ConceptualPrimitiveType.Variant)
				{
					isAlreadyVariantEncoded = true;
				}
				else
				{
					obj = CalculationContainerParser.ParseValue(calcMetadata, value);
				}
			}
			if (!isAlreadyVariantEncoded)
			{
				return obj;
			}
			return value;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007E44 File Offset: 0x00006044
		private static object ParseValue(CalculationMetadata calcMetadata, object rawCalcValue)
		{
			if (rawCalcValue == null)
			{
				return rawCalcValue;
			}
			TypeCode typeCode = Type.GetTypeCode(rawCalcValue.GetType());
			if (typeCode != TypeCode.Boolean)
			{
				switch (typeCode)
				{
				case TypeCode.Int64:
				{
					object obj;
					if (CalculationContainerParser.TryParseJsonLongValue((long)rawCalcValue, calcMetadata.Type, out obj))
					{
						return obj;
					}
					break;
				}
				case TypeCode.Double:
				{
					object obj;
					if (CalculationContainerParser.TryParseJsonDoubleValue((double)rawCalcValue, calcMetadata.Type, out obj))
					{
						return obj;
					}
					break;
				}
				case TypeCode.Decimal:
				{
					object obj;
					if (CalculationContainerParser.TryParseJsonDecimalValue((decimal)rawCalcValue, calcMetadata.Type, out obj))
					{
						return obj;
					}
					break;
				}
				case TypeCode.DateTime:
					if (calcMetadata.Type != ConceptualPrimitiveType.DateTime)
					{
						throw new InvalidDataContractException("The raw value is DateTime, but the calculation is not DateTime type. It was likely converted by Newtonsoft due to default JsonReader settings. Try setting JsonReader.DateTimeParsing to 'None' to handle bug https://github.com/JamesNK/Newtonsoft.Json/issues/862.");
					}
					if (((DateTime)rawCalcValue).Kind == DateTimeKind.Unspecified)
					{
						return rawCalcValue;
					}
					throw new InvalidDataContractException("The raw value is DateTime and its Kind is specified, meaning it was created from string containing time zone. The DSE doesn't produce this, and the Parser behavior is undefined.");
				}
			}
			else if (calcMetadata.Type == ConceptualPrimitiveType.Boolean || calcMetadata.Type == ConceptualPrimitiveType.Variant)
			{
				return rawCalcValue;
			}
			string text = (string)rawCalcValue;
			if (calcMetadata.Type == ConceptualPrimitiveType.DateTime)
			{
				DateTime dateTime = XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
				DateTime dateTime2 = dateTime;
				if (dateTime2.Kind == DateTimeKind.Unspecified)
				{
					return new DateTimePrimitiveValue(dateTime).GetValueAsObject();
				}
				throw new InvalidDataContractException("The raw value is DateTime and its Kind is specified, meaning it was created from string containing time zone. The DSE doesn't produce this, and the Parser behavior is undefined.");
			}
			else
			{
				PrimitiveValue primitiveValue;
				if (PrimitiveValueEncoding.TryParseSimpleEncodedString(text, calcMetadata.Type, out primitiveValue))
				{
					return primitiveValue.GetValueAsObject();
				}
				throw new InvalidDataContractException(StringUtil.FormatInvariant("Unexpected value encoding for {0} calculation {1}: {2}", Enum.GetName(typeof(ConceptualPrimitiveType), calcMetadata.Type), calcMetadata.Id, text));
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007FA7 File Offset: 0x000061A7
		private static bool TryParseJsonDecimalValue(decimal rawCalcValue, ConceptualPrimitiveType calculationType, out object parsedValue)
		{
			if (calculationType == ConceptualPrimitiveType.Decimal)
			{
				parsedValue = rawCalcValue;
				return true;
			}
			if (calculationType != ConceptualPrimitiveType.Double)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = (double)rawCalcValue;
			return true;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00007FD1 File Offset: 0x000061D1
		private static bool TryParseJsonDoubleValue(double rawCalcValue, ConceptualPrimitiveType calculationType, out object parsedValue)
		{
			if (calculationType == ConceptualPrimitiveType.Decimal)
			{
				parsedValue = (decimal)rawCalcValue;
				return true;
			}
			if (calculationType != ConceptualPrimitiveType.Double)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = rawCalcValue;
			return true;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00007FFC File Offset: 0x000061FC
		private static bool TryParseJsonLongValue(long longCalcValue, ConceptualPrimitiveType calculationType, out object parsedValue)
		{
			switch (calculationType)
			{
			case ConceptualPrimitiveType.Decimal:
				parsedValue = longCalcValue;
				return true;
			case ConceptualPrimitiveType.Double:
				parsedValue = (double)longCalcValue;
				return true;
			case ConceptualPrimitiveType.Integer:
				break;
			case ConceptualPrimitiveType.Boolean:
				if (longCalcValue == 0L)
				{
					parsedValue = false;
					return true;
				}
				if (longCalcValue == 1L)
				{
					parsedValue = true;
					return true;
				}
				goto IL_0079;
			case ConceptualPrimitiveType.Date:
				goto IL_0079;
			case ConceptualPrimitiveType.DateTime:
			{
				DateTime dateTime;
				if (DateTimeToUnixMillisecondsConverter.TryParse(longCalcValue, out dateTime))
				{
					parsedValue = dateTime;
					return true;
				}
				goto IL_0079;
			}
			default:
				if (calculationType != ConceptualPrimitiveType.Variant)
				{
					goto IL_0079;
				}
				break;
			}
			parsedValue = longCalcValue;
			return true;
			IL_0079:
			parsedValue = null;
			return false;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008088 File Offset: 0x00006288
		private void SetupEncodingHandlers(JObject calcContainerObj, string containerId, DsrCalculationsBuilder calcsBuilder, JArray calcs)
		{
			int count = calcsBuilder.Schema.Count;
			if (count == 0)
			{
				return;
			}
			this._lastSeenCalculations = this.GetLastSeenValues(containerId, count);
			JToken jtoken = calcContainerObj[this._names.NullValues];
			if (jtoken != null)
			{
				this._nullHandler.SetCurrentValue((long)jtoken);
			}
			else
			{
				this._nullHandler.Disable();
			}
			JToken jtoken2 = calcContainerObj[this._names.RepeatedValues];
			if (jtoken2 != null)
			{
				RepeatedValueEncodingHandler repeatedHandler = this._repeatedHandler;
				long num = (long)jtoken2;
				object[] lastSeenCalculations = this._lastSeenCalculations;
				repeatedHandler.SetLastSeenValues(num, lastSeenCalculations);
				return;
			}
			this._repeatedHandler.Disable();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008120 File Offset: 0x00006320
		private Calculation[] GetLastSeenValues(string containerId, int count)
		{
			Calculation[] array;
			if (this._containerToLastSeenCalculationsMap.TryGetValue(containerId, out array))
			{
				return array;
			}
			array = new Calculation[count];
			this._containerToLastSeenCalculationsMap.Add(containerId, array);
			return array;
		}

		// Token: 0x040001A7 RID: 423
		private readonly DsrNames _names;

		// Token: 0x040001A8 RID: 424
		private readonly NullValueEncodingHandler _nullHandler;

		// Token: 0x040001A9 RID: 425
		private readonly RepeatedValueEncodingHandler _repeatedHandler;

		// Token: 0x040001AA RID: 426
		private readonly DictionaryEncodingHandler _dictEnc;

		// Token: 0x040001AB RID: 427
		private readonly Dictionary<string, Calculation[]> _containerToLastSeenCalculationsMap;

		// Token: 0x040001AC RID: 428
		private Calculation[] _lastSeenCalculations;
	}
}
