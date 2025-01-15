using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000E2 RID: 226
	internal sealed class ODataJsonLightErrorDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x0001F498 File Offset: 0x0001D698
		internal ODataJsonLightErrorDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
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

		// Token: 0x06000879 RID: 2169 RVA: 0x0001F4F8 File Offset: 0x0001D6F8
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

		// Token: 0x0600087A RID: 2170 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
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

		// Token: 0x0600087B RID: 2171 RVA: 0x0001F668 File Offset: 0x0001D868
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

		// Token: 0x0600087C RID: 2172 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		private void ReadODataErrorObject(ODataError error)
		{
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorObject(error, propertyName, duplicationPropertyNameChecker);
			});
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001F72C File Offset: 0x0001D92C
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

		// Token: 0x0600087E RID: 2174 RVA: 0x0001F78C File Offset: 0x0001D98C
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

		// Token: 0x0600087F RID: 2175 RVA: 0x0001F838 File Offset: 0x0001DA38
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
				object obj2 = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(obj as string, null, null, null, false, false, false, propertyName, default(bool?));
				error.GetInstanceAnnotations().Add(new ODataInstanceAnnotation(propertyName, obj2.ToODataValue()));
				return;
			}
			throw new ODataException(Strings.ODataJsonLightErrorDeserializer_TopLevelErrorValueWithInvalidProperty(propertyName));
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001F968 File Offset: 0x0001DB68
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

		// Token: 0x06000881 RID: 2177 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		private ODataErrorDetail ReadDetail()
		{
			ODataErrorDetail detail = new ODataErrorDetail();
			this.ReadJsonObjectInErrorPayload(delegate(string propertyName, DuplicatePropertyNamesChecker duplicationPropertyNameChecker)
			{
				this.ReadPropertyValueInODataErrorDetailObject(detail, propertyName);
			});
			return detail;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		private void ReadPropertyValueInODataErrorDetailObject(ODataErrorDetail detail, string propertyName)
		{
			if (propertyName != null)
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
				if (propertyName == "target")
				{
					detail.Target = base.JsonReader.ReadStringValue("target");
					return;
				}
			}
			base.JsonReader.SkipValue();
		}
	}
}
