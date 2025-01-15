using System;
using System.Collections.Generic;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200020C RID: 524
	internal sealed class ODataJsonLightErrorDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
		internal ODataJsonLightErrorDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0003E600 File Offset: 0x0003C800
		internal ODataError ReadTopLevelError()
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			ODataError odataError2;
			try
			{
				base.ReadPayloadStart(ODataPayloadKind.Error, propertyAndAnnotationCollector, false, false);
				ODataError odataError = this.ReadTopLevelErrorImplementation();
				odataError2 = odataError;
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return odataError2;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0003E654 File Offset: 0x0003C854
		private ODataError ReadTopLevelErrorImplementation()
		{
			ODataError odataError = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("error", text) != 0)
				{
					throw new ODataException(Strings.ODataJsonErrorDeserializer_TopLevelErrorWithInvalidProperty(text));
				}
				if (odataError != null)
				{
					throw new ODataException(Strings.ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName("error"));
				}
				odataError = new ODataError();
				this.ReadODataErrorObject(odataError);
			}
			base.JsonReader.ReadEndObject();
			base.ReadPayloadEnd(false);
			return odataError;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003E6CC File Offset: 0x0003C8CC
		private void ReadJsonObjectInErrorPayload(Action<string, PropertyAndAnnotationCollector> readPropertyWithValue)
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(propertyAndAnnotationCollector, new Func<string, object>(this.ReadErrorPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						readPropertyWithValue.Invoke(propertyName, propertyAndAnnotationCollector);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						readPropertyWithValue.Invoke(propertyName, propertyAndAnnotationCollector);
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

		// Token: 0x060014FA RID: 5370 RVA: 0x0003E73C File Offset: 0x0003C93C
		private object ReadErrorPropertyAnnotationValue(string propertyAnnotationName)
		{
			if (string.CompareOrdinal(propertyAnnotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationNotAllowedInErrorPayload(propertyAnnotationName));
			}
			string text = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName(base.JsonReader.ReadStringValue()));
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(propertyAnnotationName));
			}
			return text;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0003E788 File Offset: 0x0003C988
		private void ReadODataErrorObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, PropertyAndAnnotationCollector duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorObject(error, propertyName, duplicationPropertyNameChecker);
			});
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0003E7BC File Offset: 0x0003C9BC
		private ODataInnerError ReadInnerError(int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			ODataInnerError innerError = new ODataInnerError();
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
			{
				this.ReadPropertyValueInInnerError(recursionDepth, innerError, propertyName);
			});
			return innerError;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0003E81C File Offset: 0x0003CA1C
		private void ReadPropertyValueInInnerError(int recursionDepth, ODataInnerError innerError, string propertyName)
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
			if (!(propertyName == "internalexception"))
			{
				base.JsonReader.SkipValue();
				return;
			}
			innerError.InnerError = this.ReadInnerError(recursionDepth);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0003E8C0 File Offset: 0x0003CAC0
		private void ReadPropertyValueInODataErrorObject(ODataError error, string propertyName, PropertyAndAnnotationCollector duplicationPropertyNameChecker)
		{
			if (propertyName == "code")
			{
				error.ErrorCode = base.JsonReader.ReadStringValue("code");
				return;
			}
			if (propertyName == "message")
			{
				error.Message = base.JsonReader.ReadStringValue("message");
				return;
			}
			if (propertyName == "target")
			{
				error.Target = base.JsonReader.ReadStringValue("target");
				return;
			}
			if (propertyName == "details")
			{
				error.Details = this.ReadDetails();
				return;
			}
			if (propertyName == "innererror")
			{
				error.InnerError = this.ReadInnerError(0);
				return;
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(propertyName))
			{
				ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(base.JsonLightInputContext);
				object obj = null;
				duplicationPropertyNameChecker.GetODataPropertyAnnotations(propertyName).TryGetValue("odata.type", ref obj);
				object obj2 = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(obj as string, null, null, null, false, false, false, propertyName, default(bool?));
				error.GetInstanceAnnotations().Add(new ODataInstanceAnnotation(propertyName, obj2.ToODataValue()));
				return;
			}
			throw new ODataException(Strings.ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(propertyName));
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0003E9DC File Offset: 0x0003CBDC
		private ICollection<ODataErrorDetail> ReadDetails()
		{
			List<ODataErrorDetail> list = new List<ODataErrorDetail>();
			base.JsonReader.ReadStartArray();
			while (base.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				ODataErrorDetail odataErrorDetail = this.ReadDetail();
				list.Add(odataErrorDetail);
			}
			base.JsonReader.ReadEndArray();
			return list;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0003EA24 File Offset: 0x0003CC24
		private ODataErrorDetail ReadDetail()
		{
			ODataErrorDetail detail = new ODataErrorDetail();
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, PropertyAndAnnotationCollector duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorDetailObject(detail, propertyName);
			});
			return detail;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0003EA64 File Offset: 0x0003CC64
		private void ReadPropertyValueInODataErrorDetailObject(ODataErrorDetail detail, string propertyName)
		{
			if (propertyName == "code")
			{
				detail.ErrorCode = base.JsonReader.ReadStringValue("code");
				return;
			}
			if (propertyName == "message")
			{
				detail.Message = base.JsonReader.ReadStringValue("message");
				return;
			}
			if (!(propertyName == "target"))
			{
				base.JsonReader.SkipValue();
				return;
			}
			detail.Target = base.JsonReader.ReadStringValue("target");
		}
	}
}
