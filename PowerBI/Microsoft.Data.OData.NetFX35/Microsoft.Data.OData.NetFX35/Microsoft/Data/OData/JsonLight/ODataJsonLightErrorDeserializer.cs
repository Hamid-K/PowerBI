using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000191 RID: 401
	internal sealed class ODataJsonLightErrorDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x00027AA8 File Offset: 0x00025CA8
		internal ODataJsonLightErrorDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00027AB4 File Offset: 0x00025CB4
		internal ODataError ReadTopLevelError()
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			ODataError odataError2;
			try
			{
				base.ReadPayloadStart(ODataPayloadKind.Error, duplicatePropertyNamesChecker, false, false);
				ODataError odataError = this.ReadTopLevelErrorImplementation();
				odataError2 = odataError;
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return odataError2;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00027B08 File Offset: 0x00025D08
		private ODataError ReadTopLevelErrorImplementation()
		{
			ODataError odataError = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("odata.error", text) != 0)
				{
					throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(text));
				}
				if (odataError != null)
				{
					throw new ODataException(Strings.ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName("odata.error"));
				}
				odataError = new ODataError();
				this.ReadODataErrorObject(odataError);
			}
			base.JsonReader.ReadEndObject();
			base.ReadPayloadEnd(false);
			return odataError;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00027C00 File Offset: 0x00025E00
		private void ReadJsonObjectInErrorPayload(Action<string, DuplicatePropertyNamesChecker> readPropertyWithValue)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(this.ReadErrorPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						readPropertyWithValue.Invoke(propertyName, duplicatePropertyNamesChecker);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						readPropertyWithValue.Invoke(propertyName, duplicatePropertyNamesChecker);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00027C78 File Offset: 0x00025E78
		private object ReadErrorPropertyAnnotationValue(string propertyAnnotationName)
		{
			if (string.CompareOrdinal(propertyAnnotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(propertyAnnotationName));
			}
			string text = base.JsonReader.ReadStringValue();
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(propertyAnnotationName));
			}
			return text;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00027CD8 File Offset: 0x00025ED8
		private void ReadODataErrorObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorObject(error, propertyName, duplicationPropertyNameChecker);
			});
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00027D28 File Offset: 0x00025F28
		private void ReadErrorMessageObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
			{
				this.ReadPropertyValueInMessageObject(error, propertyName);
			});
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00027D80 File Offset: 0x00025F80
		private ODataInnerError ReadInnerError(int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			ODataInnerError innerError = new ODataInnerError();
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
			{
				this.ReadPropertyValueInInnerError(recursionDepth, innerError, propertyName);
			});
			return innerError;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00027DE0 File Offset: 0x00025FE0
		private void ReadPropertyValueInInnerError(int recursionDepth, ODataInnerError innerError, string propertyName)
		{
			if (propertyName != null)
			{
				if (propertyName == "message")
				{
					innerError.Message = base.JsonReader.ReadStringValue("message");
					return;
				}
				if (propertyName == "type")
				{
					innerError.TypeName = base.JsonReader.ReadStringValue("type");
					return;
				}
				if (propertyName == "stacktrace")
				{
					innerError.StackTrace = base.JsonReader.ReadStringValue("stacktrace");
					return;
				}
				if (propertyName == "internalexception")
				{
					innerError.InnerError = this.ReadInnerError(recursionDepth);
					return;
				}
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00027E8C File Offset: 0x0002608C
		private void ReadPropertyValueInODataErrorObject(ODataError error, string propertyName, DuplicatePropertyNamesChecker duplicationPropertyNameChecker)
		{
			if (propertyName != null)
			{
				if (propertyName == "code")
				{
					error.ErrorCode = base.JsonReader.ReadStringValue("code");
					return;
				}
				if (propertyName == "message")
				{
					this.ReadErrorMessageObject(error);
					return;
				}
				if (propertyName == "innererror")
				{
					error.InnerError = this.ReadInnerError(0);
					return;
				}
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
			{
				ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(base.JsonLightInputContext);
				object obj = null;
				Dictionary<string, object> odataPropertyAnnotations = duplicationPropertyNameChecker.GetODataPropertyAnnotations(propertyName);
				if (odataPropertyAnnotations != null)
				{
					odataPropertyAnnotations.TryGetValue("odata.type", ref obj);
				}
				object obj2 = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(obj as string, null, null, null, false, false, false, propertyName, true);
				error.AddInstanceAnnotationForReading(propertyName, obj2);
				return;
			}
			throw new ODataException(Strings.ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(propertyName));
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00027F50 File Offset: 0x00026150
		private void ReadPropertyValueInMessageObject(ODataError error, string propertyName)
		{
			if (propertyName != null)
			{
				if (propertyName == "lang")
				{
					error.MessageLanguage = base.JsonReader.ReadStringValue("lang");
					return;
				}
				if (propertyName == "value")
				{
					error.Message = base.JsonReader.ReadStringValue("value");
					return;
				}
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
			{
				base.JsonReader.SkipValue();
				return;
			}
			throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorMessageValueWithInvalidProperty(propertyName));
		}
	}
}
