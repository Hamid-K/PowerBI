using System;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000207 RID: 519
	internal static class ODataVerboseJsonReaderUtils
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x00037397 File Offset: 0x00035597
		internal static ODataVerboseJsonReaderUtils.FeedPropertyKind DetermineFeedPropertyKind(string propertyName)
		{
			if (string.CompareOrdinal("__count", propertyName) == 0)
			{
				return ODataVerboseJsonReaderUtils.FeedPropertyKind.Count;
			}
			if (string.CompareOrdinal("__next", propertyName) == 0)
			{
				return ODataVerboseJsonReaderUtils.FeedPropertyKind.NextPageLink;
			}
			if (string.CompareOrdinal("results", propertyName) == 0)
			{
				return ODataVerboseJsonReaderUtils.FeedPropertyKind.Results;
			}
			return ODataVerboseJsonReaderUtils.FeedPropertyKind.Unsupported;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000373C8 File Offset: 0x000355C8
		internal static object ConvertValue(object value, IEdmPrimitiveTypeReference primitiveTypeReference, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool validateNullValue, string propertyName)
		{
			if (value == null)
			{
				ReaderValidationUtils.ValidateNullValue(EdmCoreModel.Instance, primitiveTypeReference, messageReaderSettings, validateNullValue, version, propertyName);
				return null;
			}
			try
			{
				Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), false);
				ODataReaderBehavior readerBehavior = messageReaderSettings.ReaderBehavior;
				string text = value as string;
				if (text != null)
				{
					return ODataVerboseJsonReaderUtils.ConvertStringValue(text, primitiveClrType, version);
				}
				if (value is int)
				{
					return ODataVerboseJsonReaderUtils.ConvertInt32Value((int)value, primitiveClrType, primitiveTypeReference, readerBehavior != null && readerBehavior.UseV1ProviderBehavior);
				}
				if (value is double)
				{
					double num = (double)value;
					if (primitiveClrType == typeof(float))
					{
						return Convert.ToSingle(num);
					}
					if (!ODataVerboseJsonReaderUtils.IsV1PrimitiveType(primitiveClrType) || (primitiveClrType != typeof(double) && (readerBehavior == null || !readerBehavior.UseV1ProviderBehavior)))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDouble(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is bool)
				{
					if (primitiveClrType != typeof(bool) && (readerBehavior == null || readerBehavior.FormatBehaviorKind != ODataBehaviorKind.WcfDataServicesServer))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertBoolean(primitiveTypeReference.ODataFullName()));
					}
				}
				else
				{
					if (value is DateTime)
					{
						return ODataVerboseJsonReaderUtils.ConvertDateTimeValue((DateTime)value, primitiveClrType, primitiveTypeReference, readerBehavior);
					}
					if (value is DateTimeOffset && primitiveClrType != typeof(DateTimeOffset))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTimeOffset(primitiveTypeReference.ODataFullName()));
					}
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(primitiveTypeReference, ex);
			}
			return value;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00037550 File Offset: 0x00035750
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003756A File Offset: 0x0003576A
		internal static bool ErrorPropertyNotFound(ref ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertyFoundBitMask)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				return false;
			}
			propertiesFoundBitField |= propertyFoundBitMask;
			return true;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003757C File Offset: 0x0003577C
		internal static void ValidateMetadataStringProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MetadataPropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003758D File Offset: 0x0003578D
		internal static void VerifyMetadataPropertyNotFound(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask propertyFoundBitMask, string propertyName)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) != ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MultipleMetadataPropertiesWithSameName(propertyName));
			}
			propertiesFoundBitField |= propertyFoundBitMask;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000375A7 File Offset: 0x000357A7
		internal static void ValidateEntityReferenceLinksStringProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_EntityReferenceLinksPropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000375B8 File Offset: 0x000357B8
		internal static void ValidateCountPropertyInEntityReferenceLinks(long? propertyValue)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_EntityReferenceLinksInlineCountWithNullValue("__count"));
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000375D3 File Offset: 0x000357D3
		internal static void VerifyEntityReferenceLinksWrapperPropertyNotFound(ref ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask propertyFoundBitMask, string propertyName)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MultipleEntityReferenceLinksWrapperPropertiesWithSameName(propertyName));
			}
			propertiesFoundBitField |= propertyFoundBitMask;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000375EE File Offset: 0x000357EE
		internal static void VerifyErrorPropertyNotFound(ref ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertyFoundBitMask, string propertyName)
		{
			if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref propertiesFoundBitField, propertyFoundBitMask))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName(propertyName));
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00037605 File Offset: 0x00035805
		internal static void ValidateMediaResourceStringProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MediaResourcePropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00037616 File Offset: 0x00035816
		internal static void ValidateFeedProperty(object propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_FeedPropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00037628 File Offset: 0x00035828
		internal static string GetPayloadTypeName(object payloadItem)
		{
			if (payloadItem == null)
			{
				return null;
			}
			TypeCode typeCode = PlatformHelper.GetTypeCode(payloadItem.GetType());
			TypeCode typeCode2 = typeCode;
			if (typeCode2 == 3)
			{
				return "Edm.Boolean";
			}
			if (typeCode2 == 9)
			{
				return "Edm.Int32";
			}
			switch (typeCode2)
			{
			case 14:
				return "Edm.Double";
			case 16:
				return "Edm.DateTime";
			case 18:
				return "Edm.String";
			}
			ODataComplexValue odataComplexValue = payloadItem as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return odataComplexValue.TypeName;
			}
			ODataCollectionValue odataCollectionValue = payloadItem as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return odataCollectionValue.TypeName;
			}
			ODataEntry odataEntry = payloadItem as ODataEntry;
			if (odataEntry != null)
			{
				return odataEntry.TypeName;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVerboseJsonReader_ReadEntryStart));
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000376D8 File Offset: 0x000358D8
		private static object ConvertStringValue(string stringValue, Type targetType, ODataVersion version)
		{
			if (targetType == typeof(byte[]))
			{
				return Convert.FromBase64String(stringValue);
			}
			if (targetType == typeof(Guid))
			{
				return new Guid(stringValue);
			}
			if (targetType == typeof(TimeSpan))
			{
				return XmlConvert.ToTimeSpan(stringValue);
			}
			if (targetType == typeof(DateTimeOffset))
			{
				return PlatformHelper.ConvertStringToDateTimeOffset(stringValue);
			}
			if (targetType == typeof(DateTime) && version >= ODataVersion.V3)
			{
				try
				{
					return PlatformHelper.ConvertStringToDateTime(stringValue);
				}
				catch (FormatException)
				{
				}
			}
			return Convert.ChangeType(stringValue, targetType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00037784 File Offset: 0x00035984
		private static object ConvertInt32Value(int intValue, Type targetType, IEdmPrimitiveTypeReference primitiveTypeReference, bool usesV1ProviderBehavior)
		{
			if (targetType == typeof(short))
			{
				return Convert.ToInt16(intValue);
			}
			if (targetType == typeof(byte))
			{
				return Convert.ToByte(intValue);
			}
			if (targetType == typeof(sbyte))
			{
				return Convert.ToSByte(intValue);
			}
			if (targetType == typeof(float))
			{
				return Convert.ToSingle(intValue);
			}
			if (targetType == typeof(double))
			{
				return Convert.ToDouble(intValue);
			}
			if (targetType == typeof(decimal) || targetType == typeof(long))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt64OrDecimal);
			}
			if (!ODataVerboseJsonReaderUtils.IsV1PrimitiveType(targetType) || (targetType != typeof(int) && !usesV1ProviderBehavior))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt32(primitiveTypeReference.ODataFullName()));
			}
			return intValue;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00037864 File Offset: 0x00035A64
		private static object ConvertDateTimeValue(DateTime datetimeValue, Type targetType, IEdmPrimitiveTypeReference primitiveTypeReference, ODataReaderBehavior readerBehavior)
		{
			if (targetType == typeof(DateTimeOffset) && (datetimeValue.Kind == 2 || datetimeValue.Kind == 1))
			{
				return new DateTimeOffset(datetimeValue);
			}
			if (targetType != typeof(DateTime) && (readerBehavior == null || readerBehavior.FormatBehaviorKind != ODataBehaviorKind.WcfDataServicesServer))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTime(primitiveTypeReference.ODataFullName()));
			}
			return datetimeValue;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000378CE File Offset: 0x00035ACE
		private static bool IsV1PrimitiveType(Type targetType)
		{
			return targetType != typeof(DateTimeOffset) && targetType != typeof(TimeSpan);
		}

		// Token: 0x02000208 RID: 520
		internal enum FeedPropertyKind
		{
			// Token: 0x040005C4 RID: 1476
			Unsupported,
			// Token: 0x040005C5 RID: 1477
			Count,
			// Token: 0x040005C6 RID: 1478
			Results,
			// Token: 0x040005C7 RID: 1479
			NextPageLink
		}

		// Token: 0x02000209 RID: 521
		[Flags]
		internal enum EntityReferenceLinksWrapperPropertyBitMask
		{
			// Token: 0x040005C9 RID: 1481
			None = 0,
			// Token: 0x040005CA RID: 1482
			Count = 1,
			// Token: 0x040005CB RID: 1483
			Results = 2,
			// Token: 0x040005CC RID: 1484
			NextPageLink = 4
		}

		// Token: 0x0200020A RID: 522
		[Flags]
		internal enum ErrorPropertyBitMask
		{
			// Token: 0x040005CE RID: 1486
			None = 0,
			// Token: 0x040005CF RID: 1487
			Error = 1,
			// Token: 0x040005D0 RID: 1488
			Code = 2,
			// Token: 0x040005D1 RID: 1489
			Message = 4,
			// Token: 0x040005D2 RID: 1490
			MessageLanguage = 8,
			// Token: 0x040005D3 RID: 1491
			MessageValue = 16,
			// Token: 0x040005D4 RID: 1492
			InnerError = 32,
			// Token: 0x040005D5 RID: 1493
			TypeName = 64,
			// Token: 0x040005D6 RID: 1494
			StackTrace = 128
		}

		// Token: 0x0200020B RID: 523
		[Flags]
		internal enum MetadataPropertyBitMask
		{
			// Token: 0x040005D8 RID: 1496
			None = 0,
			// Token: 0x040005D9 RID: 1497
			Uri = 1,
			// Token: 0x040005DA RID: 1498
			Type = 2,
			// Token: 0x040005DB RID: 1499
			ETag = 4,
			// Token: 0x040005DC RID: 1500
			MediaUri = 8,
			// Token: 0x040005DD RID: 1501
			EditMedia = 16,
			// Token: 0x040005DE RID: 1502
			ContentType = 32,
			// Token: 0x040005DF RID: 1503
			MediaETag = 64,
			// Token: 0x040005E0 RID: 1504
			Properties = 128,
			// Token: 0x040005E1 RID: 1505
			Id = 256,
			// Token: 0x040005E2 RID: 1506
			Actions = 512,
			// Token: 0x040005E3 RID: 1507
			Functions = 1024
		}
	}
}
