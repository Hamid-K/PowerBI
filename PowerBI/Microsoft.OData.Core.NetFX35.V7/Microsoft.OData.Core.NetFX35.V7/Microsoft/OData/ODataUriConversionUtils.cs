using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000D3 RID: 211
	internal static class ODataUriConversionUtils
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00016B94 File Offset: 0x00014D94
		internal static string ConvertToUriPrimitiveLiteral(object value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(value, "value");
			return LiteralFormatter.ForConstantsWithoutEncoding.Format(value);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00016BB0 File Offset: 0x00014DB0
		internal static string ConvertToUriEnumLiteral(ODataEnumValue value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEnumValue>(value, "value");
			ExceptionUtils.CheckArgumentNotNull<string>(value.TypeName, "value.TypeName");
			ExceptionUtils.CheckArgumentNotNull<string>(value.Value, "value.Value");
			return string.Format(CultureInfo.InvariantCulture, "{0}'{1}'", new object[] { value.TypeName, value.Value });
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00016C14 File Offset: 0x00014E14
		internal static object ConvertFromCollectionValue(string value, IEdmModel model, IEdmTypeReference typeReference)
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			odataMessageReaderSettings.Validations &= ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType;
			object obj2;
			using (StringReader stringReader = new StringReader(value))
			{
				ODataMessageInfo odataMessageInfo = new ODataMessageInfo
				{
					MediaType = new ODataMediaType("application", "json"),
					Model = model,
					IsResponse = false,
					IsAsync = false,
					MessageStream = null
				};
				using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(stringReader, odataMessageInfo, odataMessageReaderSettings))
				{
					ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(odataJsonLightInputContext);
					odataJsonLightPropertyAndValueDeserializer.JsonReader.Read();
					object obj = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(null, typeReference, null, null, true, false, false, null, default(bool?));
					odataJsonLightPropertyAndValueDeserializer.ReadPayloadEnd(false);
					obj2 = obj;
				}
			}
			return obj2;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00016CF0 File Offset: 0x00014EF0
		internal static object VerifyAndCoerceUriPrimitiveLiteral(object primitiveValue, string literalValue, IEdmModel model, IEdmTypeReference expectedTypeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<string>(literalValue, "literalValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(expectedTypeReference, "expectedTypeReference");
			ODataNullValue odataNullValue = primitiveValue as ODataNullValue;
			if (odataNullValue != null)
			{
				if (!expectedTypeReference.IsNullable)
				{
					throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType(expectedTypeReference.FullName()));
				}
				return odataNullValue;
			}
			else
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = expectedTypeReference.AsPrimitiveOrNull();
				if (edmPrimitiveTypeReference == null)
				{
					throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(expectedTypeReference.FullName(), literalValue));
				}
				object obj = ODataUriConversionUtils.CoerceNumericType(primitiveValue, edmPrimitiveTypeReference.PrimitiveDefinition());
				if (obj != null)
				{
					return obj;
				}
				obj = ODataUriConversionUtils.CoerceTemporalType(primitiveValue, edmPrimitiveTypeReference.PrimitiveDefinition());
				if (obj != null)
				{
					return obj;
				}
				Type type = primitiveValue.GetType();
				Type nonNullableType = TypeUtils.GetNonNullableType(EdmLibraryExtensions.GetPrimitiveClrType(edmPrimitiveTypeReference));
				if (nonNullableType.IsAssignableFrom(type))
				{
					return primitiveValue;
				}
				throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(edmPrimitiveTypeReference.FullName(), literalValue));
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00016DC4 File Offset: 0x00014FC4
		internal static string ConvertToUriEntityLiteral(ODataResource resource, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResource>(resource, "resource");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				ODataWriter odataWriter = context.CreateODataUriParameterResourceWriter(null, null);
				odataWriter.WriteStart(resource);
				odataWriter.WriteEnd();
			});
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00016E10 File Offset: 0x00015010
		internal static string ConvertToUriEntitiesLiteral(IEnumerable<ODataResource> entries, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataResource>>(entries, "entries");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				ODataWriter odataWriter = context.CreateODataUriParameterResourceSetWriter(null, null);
				odataWriter.WriteStart(new ODataResourceSet());
				foreach (ODataResource odataResource in entries)
				{
					odataWriter.WriteStart(odataResource);
					odataWriter.WriteEnd();
				}
				odataWriter.WriteEnd();
			});
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00016E5C File Offset: 0x0001505C
		internal static string ConvertToUriEntityReferenceLiteral(ODataEntityReferenceLink link, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link);
			});
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00016EA8 File Offset: 0x000150A8
		internal static string ConvertToUriEntityReferencesLiteral(ODataEntityReferenceLinks links, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "links");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links);
			});
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00016EF4 File Offset: 0x000150F4
		internal static string ConvertToUriCollectionLiteral(ODataCollectionValue collectionValue, IEdmModel model, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionValue>(collectionValue, "collectionValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(version),
					Validations = ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType
				};
				ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
				{
					serializer.WriteCollectionValue(collectionValue, null, null, false, true, false);
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00016F90 File Offset: 0x00015190
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Centralized method for coercing numeric types in easiest to understand.")]
		internal static object CoerceNumericType(object primitiveValue, IEdmPrimitiveType targetEdmType)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(targetEdmType, "targetEdmType");
			EdmPrimitiveTypeKind primitiveKind = targetEdmType.PrimitiveKind;
			if (primitiveValue is sbyte)
			{
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Int16:
					return Convert.ToInt16((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.SByte:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((sbyte)primitiveValue);
				}
			}
			if (primitiveValue is byte)
			{
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Byte:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Int16:
					return Convert.ToInt16((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((byte)primitiveValue);
				}
			}
			if (primitiveValue is short)
			{
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((short)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((short)primitiveValue);
				case EdmPrimitiveTypeKind.Int16:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32((short)primitiveValue);
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((short)primitiveValue);
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((short)primitiveValue);
				}
			}
			if (primitiveValue is int)
			{
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((int)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((int)primitiveValue);
				case EdmPrimitiveTypeKind.Int32:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((int)primitiveValue);
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((int)primitiveValue);
				}
			}
			if (primitiveValue is long)
			{
				if (primitiveKind <= EdmPrimitiveTypeKind.Double)
				{
					if (primitiveKind == EdmPrimitiveTypeKind.Decimal)
					{
						return Convert.ToDecimal((long)primitiveValue);
					}
					if (primitiveKind == EdmPrimitiveTypeKind.Double)
					{
						return Convert.ToDouble((long)primitiveValue);
					}
				}
				else
				{
					if (primitiveKind == EdmPrimitiveTypeKind.Int64)
					{
						return primitiveValue;
					}
					if (primitiveKind == EdmPrimitiveTypeKind.Single)
					{
						return Convert.ToSingle((long)primitiveValue);
					}
				}
			}
			if (primitiveValue is float)
			{
				if (primitiveKind == EdmPrimitiveTypeKind.Decimal)
				{
					return Convert.ToDecimal((float)primitiveValue);
				}
				if (primitiveKind == EdmPrimitiveTypeKind.Double)
				{
					return double.Parse(((float)primitiveValue).ToString("R", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
				}
				if (primitiveKind == EdmPrimitiveTypeKind.Single)
				{
					return primitiveValue;
				}
			}
			if (primitiveValue is double)
			{
				if (primitiveKind != EdmPrimitiveTypeKind.Decimal)
				{
					if (primitiveKind == EdmPrimitiveTypeKind.Double)
					{
						return primitiveValue;
					}
				}
				else
				{
					decimal num;
					if (decimal.TryParse(((double)primitiveValue).ToString("R", CultureInfo.InvariantCulture), ref num))
					{
						return num;
					}
					return Convert.ToDecimal((double)primitiveValue);
				}
			}
			if (primitiveValue is decimal && primitiveKind == EdmPrimitiveTypeKind.Decimal)
			{
				return primitiveValue;
			}
			return null;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00017304 File Offset: 0x00015504
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Centralized method for coercing temporal types in easiest to understand.")]
		internal static object CoerceTemporalType(object primitiveValue, IEdmPrimitiveType targetEdmType)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(targetEdmType, "targetEdmType");
			EdmPrimitiveTypeKind primitiveKind = targetEdmType.PrimitiveKind;
			if (primitiveKind != EdmPrimitiveTypeKind.DateTimeOffset)
			{
				if (primitiveKind == EdmPrimitiveTypeKind.Date)
				{
					string text = primitiveValue as string;
					if (text != null)
					{
						return PlatformHelper.ConvertStringToDate(text);
					}
				}
			}
			else if (primitiveValue is Date)
			{
				Date date = (Date)primitiveValue;
				return new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, new TimeSpan(0L));
			}
			return null;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001738C File Offset: 0x0001558C
		private static void WriteJsonLightLiteral(IEdmModel model, ODataMessageWriterSettings messageWriterSettings, TextWriter textWriter, Action<ODataJsonLightValueSerializer> writeValue)
		{
			ODataMessageInfo odataMessageInfo = new ODataMessageInfo
			{
				Model = model,
				IsAsync = false,
				IsResponse = false
			};
			using (ODataJsonLightOutputContext odataJsonLightOutputContext = new ODataJsonLightOutputContext(textWriter, odataMessageInfo, messageWriterSettings))
			{
				ODataJsonLightValueSerializer odataJsonLightValueSerializer = new ODataJsonLightValueSerializer(odataJsonLightOutputContext, false);
				writeValue.Invoke(odataJsonLightValueSerializer);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000173E8 File Offset: 0x000155E8
		private static string ConverToJsonLightLiteral(IEdmModel model, Action<ODataOutputContext> writeAction)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(ODataVersion.V4),
					Validations = ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType
				};
				ODataMediaType odataMediaType = new ODataMediaType("application", "json");
				ODataMessageInfo odataMessageInfo = new ODataMessageInfo
				{
					MessageStream = memoryStream,
					Encoding = Encoding.UTF8,
					IsAsync = false,
					IsResponse = false,
					MediaType = odataMediaType,
					Model = model
				};
				using (ODataJsonLightOutputContext odataJsonLightOutputContext = new ODataJsonLightOutputContext(odataMessageInfo, odataMessageWriterSettings))
				{
					writeAction.Invoke(odataJsonLightOutputContext);
					memoryStream.Position = 0L;
					text = new StreamReader(memoryStream).ReadToEnd();
				}
			}
			return text;
		}
	}
}
