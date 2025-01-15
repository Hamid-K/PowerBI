using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000245 RID: 581
	internal sealed class ODataJsonLightErrorDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06001961 RID: 6497 RVA: 0x0004B1CC File Offset: 0x000493CC
		internal ODataJsonLightErrorDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0004B1D8 File Offset: 0x000493D8
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

		// Token: 0x06001963 RID: 6499 RVA: 0x0004B22C File Offset: 0x0004942C
		internal Task<ODataError> ReadTopLevelErrorAsync()
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			return base.ReadPayloadStartAsync(ODataPayloadKind.Error, propertyAndAnnotationCollector, false, false).FollowOnSuccessWith((Task t) => this.ReadTopLevelErrorImplementation()).FollowAlwaysWith(delegate(Task<ODataError> t)
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			});
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004B27C File Offset: 0x0004947C
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

		// Token: 0x06001965 RID: 6501 RVA: 0x0004B2F4 File Offset: 0x000494F4
		private void ReadJsonObjectInErrorPayload(Action<string, PropertyAndAnnotationCollector> readPropertyWithValue)
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = base.CreatePropertyAndAnnotationCollector();
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(propertyAndAnnotationCollector, new Func<string, object>(this.ReadErrorPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					if (this.JsonReader.NodeType == JsonNodeType.Property)
					{
						this.JsonReader.Read();
					}
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						readPropertyWithValue(propertyName, propertyAndAnnotationCollector);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_PropertyAnnotationWithoutPropertyForError(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightErrorDeserializer_InstanceAnnotationNotAllowedInErrorPayload(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						readPropertyWithValue(propertyName, propertyAndAnnotationCollector);
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

		// Token: 0x06001966 RID: 6502 RVA: 0x0004B36C File Offset: 0x0004956C
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

		// Token: 0x06001967 RID: 6503 RVA: 0x0004B3B8 File Offset: 0x000495B8
		private void ReadODataErrorObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, PropertyAndAnnotationCollector duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorObject(error, propertyName, duplicationPropertyNameChecker);
			});
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0004B3EC File Offset: 0x000495EC
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

		// Token: 0x06001969 RID: 6505 RVA: 0x0004B44C File Offset: 0x0004964C
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
			if (propertyName == "internalexception")
			{
				innerError.InnerError = this.ReadInnerError(recursionDepth);
				return;
			}
			if (!innerError.Properties.ContainsKey(propertyName))
			{
				innerError.Properties.Add(propertyName, base.JsonReader.ReadODataValue());
				return;
			}
			innerError.Properties[propertyName] = base.JsonReader.ReadODataValue();
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0004B520 File Offset: 0x00049720
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
				duplicationPropertyNameChecker.GetODataPropertyAnnotations(propertyName).TryGetValue("odata.type", out obj);
				object obj2 = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(obj as string, null, null, null, false, false, false, propertyName, null);
				error.GetInstanceAnnotations().Add(new ODataInstanceAnnotation(propertyName, obj2.ToODataValue()));
				return;
			}
			throw new ODataException(Strings.ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(propertyName));
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0004B63C File Offset: 0x0004983C
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

		// Token: 0x0600196C RID: 6508 RVA: 0x0004B684 File Offset: 0x00049884
		private ODataErrorDetail ReadDetail()
		{
			ODataErrorDetail detail = new ODataErrorDetail();
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, PropertyAndAnnotationCollector duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorDetailObject(detail, propertyName);
			});
			return detail;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0004B6C4 File Offset: 0x000498C4
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
