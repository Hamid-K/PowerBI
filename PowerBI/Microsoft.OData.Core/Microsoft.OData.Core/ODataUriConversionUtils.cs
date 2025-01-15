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
	// Token: 0x020000D6 RID: 214
	internal static class ODataUriConversionUtils
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x00019E4C File Offset: 0x0001804C
		internal static string ConvertToUriPrimitiveLiteral(object value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(value, "value");
			return LiteralFormatter.ForConstantsWithoutEncoding.Format(value);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00019E68 File Offset: 0x00018068
		internal static string ConvertToUriEnumLiteral(ODataEnumValue value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEnumValue>(value, "value");
			ExceptionUtils.CheckArgumentNotNull<string>(value.TypeName, "value.TypeName");
			ExceptionUtils.CheckArgumentNotNull<string>(value.Value, "value.Value");
			return string.Format(CultureInfo.InvariantCulture, "{0}'{1}'", new object[] { value.TypeName, value.Value });
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00019ECC File Offset: 0x000180CC
		internal static object ConvertFromResourceValue(string value, IEdmModel model, IEdmTypeReference typeReference)
		{
			return ODataUriConversionUtils.ConvertFromResourceOrCollectionValue(value, model, typeReference);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00019EE4 File Offset: 0x000180E4
		internal static object ConvertFromCollectionValue(string value, IEdmModel model, IEdmTypeReference typeReference)
		{
			return ODataUriConversionUtils.ConvertFromResourceOrCollectionValue(value, model, typeReference);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00019EFC File Offset: 0x000180FC
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

		// Token: 0x06000A1C RID: 2588 RVA: 0x00019FD0 File Offset: 0x000181D0
		internal static string ConvertToUriEntityLiteral(ODataResourceBase resource, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceBase>(resource, "resource");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				ODataWriter odataWriter = context.CreateODataUriParameterResourceWriter(null, null);
				ODataUriConversionUtils.WriteStartResource(odataWriter, resource);
				odataWriter.WriteEnd();
			});
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001A01C File Offset: 0x0001821C
		internal static string ConvertToUriEntitiesLiteral(IEnumerable<ODataResourceBase> entries, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataResourceBase>>(entries, "entries");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				ODataWriter odataWriter = context.CreateODataUriParameterResourceSetWriter(null, null);
				odataWriter.WriteStart(new ODataResourceSet());
				foreach (ODataResourceBase odataResourceBase in entries)
				{
					ODataUriConversionUtils.WriteStartResource(odataWriter, odataResourceBase);
					odataWriter.WriteEnd();
				}
				odataWriter.WriteEnd();
			});
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001A068 File Offset: 0x00018268
		internal static string ConvertToUriEntityReferenceLiteral(ODataEntityReferenceLink link, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link);
			});
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001A0B4 File Offset: 0x000182B4
		internal static string ConvertToUriEntityReferencesLiteral(ODataEntityReferenceLinks links, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "links");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			return ODataUriConversionUtils.ConverToJsonLightLiteral(model, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links);
			});
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001A100 File Offset: 0x00018300
		internal static string ConvertToResourceLiteral(ODataResourceValue resourceValue, IEdmModel model, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceValue>(resourceValue, "resourceValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(version),
					Validations = ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType,
					ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter("*")
				};
				ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
				{
					serializer.WriteResourceValue(resourceValue, null, true, serializer.CreateDuplicatePropertyNameChecker());
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001A1AC File Offset: 0x000183AC
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
					Validations = ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType,
					ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter("*")
				};
				ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
				{
					serializer.WriteCollectionValue(collectionValue, null, null, false, true, false);
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001A258 File Offset: 0x00018458
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
					if (decimal.TryParse(((double)primitiveValue).ToString("R", CultureInfo.InvariantCulture), out num))
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

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001A5CC File Offset: 0x000187CC
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

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001A654 File Offset: 0x00018854
		private static void WriteStartResource(ODataWriter writer, ODataResourceBase resource)
		{
			ODataDeletedResource odataDeletedResource = resource as ODataDeletedResource;
			if (odataDeletedResource != null)
			{
				writer.WriteStart(odataDeletedResource);
				return;
			}
			writer.WriteStart(resource as ODataResource);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001A680 File Offset: 0x00018880
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
				writeValue(odataJsonLightValueSerializer);
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001A6DC File Offset: 0x000188DC
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
					writeAction(odataJsonLightOutputContext);
					memoryStream.Position = 0L;
					text = new StreamReader(memoryStream).ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001A7AC File Offset: 0x000189AC
		private static object ConvertFromResourceOrCollectionValue(string value, IEdmModel model, IEdmTypeReference typeReference)
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
					object obj = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(null, typeReference, null, null, true, false, false, null, null);
					odataJsonLightPropertyAndValueDeserializer.ReadPayloadEnd(false);
					obj2 = obj;
				}
			}
			return obj2;
		}
	}
}
