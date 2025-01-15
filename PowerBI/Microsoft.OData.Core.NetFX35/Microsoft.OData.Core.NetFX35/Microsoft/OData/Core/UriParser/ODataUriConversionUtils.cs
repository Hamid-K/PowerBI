using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001AD RID: 429
	internal static class ODataUriConversionUtils
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x00036ABF File Offset: 0x00034CBF
		internal static string ConvertToUriPrimitiveLiteral(object value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(value, "value");
			return LiteralFormatter.ForConstantsWithoutEncoding.Format(value);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00036AD8 File Offset: 0x00034CD8
		internal static string ConvertToUriEnumLiteral(ODataEnumValue value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEnumValue>(value, "value");
			ExceptionUtils.CheckArgumentNotNull<string>(value.TypeName, "value.TypeName");
			ExceptionUtils.CheckArgumentNotNull<string>(value.Value, "value.Value");
			return string.Format(CultureInfo.InvariantCulture, "{0}'{1}'", new object[] { value.TypeName, value.Value });
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00036B3C File Offset: 0x00034D3C
		internal static object ConvertFromComplexOrCollectionValue(string value, IEdmModel model, IEdmTypeReference typeReference)
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			object obj2;
			using (StringReader stringReader = new StringReader(value))
			{
				using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(ODataFormat.Json, stringReader, new ODataMediaType("application", "json"), odataMessageReaderSettings, false, true, model, null))
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

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00036BE4 File Offset: 0x00034DE4
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

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00036CD4 File Offset: 0x00034ED4
		internal static string ConvertToUriComplexLiteral(ODataComplexValue complexValue, IEdmModel model, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataComplexValue>(complexValue, "complexValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(version),
					Indent = false
				};
				ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
				{
					serializer.WriteComplexValue(complexValue, null, false, true, serializer.CreateDuplicatePropertyNamesChecker());
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00036DAC File Offset: 0x00034FAC
		internal static string ConvertToUriEntityLiteral(ODataEntry entry, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				ODataWriter odataWriter = context.CreateODataEntryWriter(null, null);
				odataWriter.WriteStart(entry);
				odataWriter.WriteEnd();
			});
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00036E6C File Offset: 0x0003506C
		internal static string ConvertToUriEntitiesLiteral(IEnumerable<ODataEntry> entries, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataEntry>>(entries, "entries");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				ODataWriter odataWriter = context.CreateODataFeedWriter(null, null);
				odataWriter.WriteStart(new ODataFeed());
				foreach (ODataEntry odataEntry in entries)
				{
					odataWriter.WriteStart(odataEntry);
					odataWriter.WriteEnd();
				}
				odataWriter.WriteEnd();
			});
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00036ECC File Offset: 0x000350CC
		internal static string ConvertToUriEntityReferenceLiteral(ODataEntityReferenceLink link, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link);
			});
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00036F2C File Offset: 0x0003512C
		internal static string ConvertToUriEntityReferencesLiteral(ODataEntityReferenceLinks links, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "links");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links);
			});
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00036F90 File Offset: 0x00035190
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
					Indent = false
				};
				ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
				{
					serializer.WriteCollectionValue(collectionValue, null, null, false, true, false);
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00037038 File Offset: 0x00035238
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Centralized method for coercing numeric types in easiest to understand.")]
		internal static object CoerceNumericType(object primitiveValue, IEdmPrimitiveType targetEdmType)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(targetEdmType, "targetEdmType");
			Type type = primitiveValue.GetType();
			TypeCode typeCode = PlatformHelper.GetTypeCode(type);
			EdmPrimitiveTypeKind primitiveKind = targetEdmType.PrimitiveKind;
			switch (typeCode)
			{
			case 5:
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
				break;
			case 6:
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
				break;
			case 7:
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
				break;
			case 9:
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
				break;
			case 11:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = primitiveKind;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((long)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((long)primitiveValue);
				default:
					switch (edmPrimitiveTypeKind)
					{
					case EdmPrimitiveTypeKind.Int64:
						return primitiveValue;
					case EdmPrimitiveTypeKind.Single:
						return Convert.ToSingle((long)primitiveValue);
					}
					break;
				}
				break;
			}
			case 13:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind2 = primitiveKind;
				switch (edmPrimitiveTypeKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((float)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return double.Parse(((float)primitiveValue).ToString("R", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
				default:
					if (edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Single)
					{
						return primitiveValue;
					}
					break;
				}
				break;
			}
			case 14:
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
				{
					decimal num;
					if (decimal.TryParse(((double)primitiveValue).ToString("R", CultureInfo.InvariantCulture), ref num))
					{
						return num;
					}
					return Convert.ToDecimal((double)primitiveValue);
				}
				case EdmPrimitiveTypeKind.Double:
					return primitiveValue;
				}
				break;
			case 15:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind3 = primitiveKind;
				if (edmPrimitiveTypeKind3 == EdmPrimitiveTypeKind.Decimal)
				{
					return primitiveValue;
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00037400 File Offset: 0x00035600
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Centralized method for coercing temporal types in easiest to understand.")]
		internal static object CoerceTemporalType(object primitiveValue, IEdmPrimitiveType targetEdmType)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(targetEdmType, "targetEdmType");
			EdmPrimitiveTypeKind primitiveKind = targetEdmType.PrimitiveKind;
			if (primitiveKind == EdmPrimitiveTypeKind.Date)
			{
				if (primitiveValue is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)primitiveValue;
					return new Date(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
				}
				string text = primitiveValue as string;
				if (text != null)
				{
					return PlatformHelper.ConvertStringToDate(text);
				}
			}
			return null;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00037478 File Offset: 0x00035678
		private static void WriteJsonLightLiteral(IEdmModel model, ODataMessageWriterSettings messageWriterSettings, TextWriter textWriter, Action<ODataJsonLightValueSerializer> writeValue)
		{
			using (ODataJsonLightOutputContext odataJsonLightOutputContext = new ODataJsonLightOutputContext(ODataFormat.Json, textWriter, messageWriterSettings, model))
			{
				ODataJsonLightValueSerializer odataJsonLightValueSerializer = new ODataJsonLightValueSerializer(odataJsonLightOutputContext, false);
				writeValue.Invoke(odataJsonLightValueSerializer);
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000374C0 File Offset: 0x000356C0
		private static string ConverToJsonLightLiteral(IEdmModel model, Action<ODataOutputContext> writeAction)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(ODataVersion.V4),
					Indent = false
				};
				ODataMediaType odataMediaType = new ODataMediaType("application", "json");
				using (ODataJsonLightOutputContext odataJsonLightOutputContext = new ODataJsonLightOutputContext(ODataFormat.Json, memoryStream, odataMediaType, Encoding.UTF8, odataMessageWriterSettings, false, true, model, null))
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
