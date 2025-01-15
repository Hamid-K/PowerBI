using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000114 RID: 276
	internal abstract class ODataJsonLightDeserializer : ODataDeserializer
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00018A64 File Offset: 0x00016C64
		protected ODataJsonLightDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.jsonLightInputContext = jsonLightInputContext;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00018A74 File Offset: 0x00016C74
		internal IODataMetadataContext MetadataContext
		{
			get
			{
				Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified = base.MessageReaderSettings.ReaderBehavior.OperationsBoundToEntityTypeMustBeContainerQualified;
				IODataMetadataContext iodataMetadataContext;
				if ((iodataMetadataContext = this.metadataContext) == null)
				{
					iodataMetadataContext = (this.metadataContext = new ODataMetadataContext(base.ReadingResponse, operationsBoundToEntityTypeMustBeContainerQualified, this.JsonLightInputContext.EdmTypeResolver, base.Model, this.MetadataDocumentUri));
				}
				return iodataMetadataContext;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00018AC8 File Offset: 0x00016CC8
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonLightInputContext.JsonReader;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00018AD5 File Offset: 0x00016CD5
		internal ODataJsonLightMetadataUriParseResult MetadataUriParseResult
		{
			get
			{
				return this.metadataUriParseResult;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00018ADD File Offset: 0x00016CDD
		protected ODataJsonLightInputContext JsonLightInputContext
		{
			get
			{
				return this.jsonLightInputContext;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00018AE8 File Offset: 0x00016CE8
		private Uri MetadataDocumentUri
		{
			get
			{
				return (this.MetadataUriParseResult != null && this.MetadataUriParseResult.MetadataDocumentUri != null) ? this.MetadataUriParseResult.MetadataDocumentUri : null;
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00018B20 File Offset: 0x00016D20
		internal static bool TryParsePropertyAnnotation(string propertyAnnotationName, out string propertyName, out string annotationName)
		{
			int num = propertyAnnotationName.IndexOf('@');
			if (num <= 0 || num == propertyAnnotationName.Length - 1)
			{
				propertyName = null;
				annotationName = null;
				return false;
			}
			propertyName = propertyAnnotationName.Substring(0, num);
			annotationName = propertyAnnotationName.Substring(num + 1);
			return true;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00018B64 File Offset: 0x00016D64
		internal void ReadPayloadStart(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			string text = this.ReadPayloadStartImplementation(payloadKind, duplicatePropertyNamesChecker, isReadingNestedPayload, allowEmptyPayload);
			ODataJsonLightMetadataUriParseResult odataJsonLightMetadataUriParseResult = null;
			if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error)
			{
				odataJsonLightMetadataUriParseResult = ((this.jsonLightInputContext.PayloadKindDetectionState == null) ? null : this.jsonLightInputContext.PayloadKindDetectionState.MetadataUriParseResult);
				if (odataJsonLightMetadataUriParseResult == null && text != null)
				{
					odataJsonLightMetadataUriParseResult = ODataJsonLightMetadataUriParser.Parse(base.Model, text, payloadKind, base.Version, base.MessageReaderSettings.ReaderBehavior);
				}
			}
			this.metadataUriParseResult = odataJsonLightMetadataUriParseResult;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00018BD4 File Offset: 0x00016DD4
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00018BD8 File Offset: 0x00016DD8
		internal string ReadAndValidateAnnotationStringValue(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			ODataJsonLightReaderUtils.ValidateAnnotationStringValue(text, annotationName);
			return text;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00018BFC File Offset: 0x00016DFC
		internal Uri ReadAndValidateAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00018C18 File Offset: 0x00016E18
		internal long ReadAndValidateAnnotationStringValueAsLong(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			return (long)ODataJsonLightReaderUtils.ConvertValue(text, EdmCoreModel.Instance.GetInt64(false), base.MessageReaderSettings, base.Version, true, annotationName);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00018C54 File Offset: 0x00016E54
		internal Uri ProcessUriFromPayload(string uriFromPayload)
		{
			Uri uri = new Uri(uriFromPayload, 0);
			Uri metadataDocumentUri = this.MetadataDocumentUri;
			Uri uri2 = this.JsonLightInputContext.ResolveUri(metadataDocumentUri, uri);
			if (uri2 != null)
			{
				return uri2;
			}
			if (!uri.IsAbsoluteUri)
			{
				if (metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(uriFromPayload, "odata.metadata"));
				}
				uri = UriUtils.UriToAbsoluteUri(metadataDocumentUri, uri);
			}
			return uri;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00018CB4 File Offset: 0x00016EB4
		internal void ProcessProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, Func<string, object> readPropertyAnnotationValue, Action<ODataJsonLightDeserializer.PropertyParsingResult, string> handleProperty)
		{
			string text;
			ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult = this.ParseProperty(duplicatePropertyNamesChecker, readPropertyAnnotationValue, out text);
			while (propertyParsingResult == ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation && this.ShouldSkipCustomInstanceAnnotation(text))
			{
				duplicatePropertyNamesChecker.MarkPropertyAsProcessed(text);
				this.JsonReader.SkipValue();
				propertyParsingResult = this.ParseProperty(duplicatePropertyNamesChecker, readPropertyAnnotationValue, out text);
			}
			handleProperty.Invoke(propertyParsingResult, text);
			if (propertyParsingResult != ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject)
			{
				duplicatePropertyNamesChecker.MarkPropertyAsProcessed(text);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00018D0A File Offset: 0x00016F0A
		[Conditional("DEBUG")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00018D0C File Offset: 0x00016F0C
		private bool ShouldSkipCustomInstanceAnnotation(string annotationName)
		{
			return (!(this is ODataJsonLightErrorDeserializer) || base.MessageReaderSettings.ShouldIncludeAnnotation != null) && base.MessageReaderSettings.ShouldSkipAnnotation(annotationName);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018D34 File Offset: 0x00016F34
		private bool SkippedOverUnknownODataAnnotation(string annotationName, out string skippedRawJson)
		{
			if (ODataAnnotationNames.IsUnknownODataAnnotationName(annotationName))
			{
				this.JsonReader.Read();
				StringBuilder stringBuilder = new StringBuilder();
				this.JsonReader.SkipValue(stringBuilder);
				skippedRawJson = stringBuilder.ToString();
				return true;
			}
			skippedRawJson = null;
			return false;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00018D78 File Offset: 0x00016F78
		private ODataJsonLightDeserializer.PropertyParsingResult ParseProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, Func<string, object> readPropertyAnnotationValue, out string parsedPropertyName)
		{
			string text = null;
			parsedPropertyName = null;
			while (this.JsonReader.NodeType == JsonNodeType.Property)
			{
				string propertyName = this.JsonReader.GetPropertyName();
				string text2;
				string text3;
				bool flag = ODataJsonLightDeserializer.TryParsePropertyAnnotation(propertyName, out text2, out text3);
				text2 = text2 ?? propertyName;
				if (parsedPropertyName != null && string.CompareOrdinal(parsedPropertyName, text2) != 0)
				{
					if (ODataJsonLightReaderUtils.IsAnnotationProperty(parsedPropertyName))
					{
						throw new ODataException(Strings.ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(text, parsedPropertyName));
					}
					return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue;
				}
				else
				{
					duplicatePropertyNamesChecker.AnnotationCollector.ShouldCollectAnnotation = base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds == ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty;
					string text4 = null;
					if (flag)
					{
						duplicatePropertyNamesChecker.AnnotationCollector.TryPeekAndCollectAnnotationRawJson(this.JsonReader, text2, text3);
						if (ODataJsonLightReaderUtils.IsAnnotationProperty(text2) || !this.SkippedOverUnknownODataAnnotation(text3, out text4))
						{
							parsedPropertyName = text2;
							text = text3;
							this.ProcessPropertyAnnotation(text2, text3, duplicatePropertyNamesChecker, readPropertyAnnotationValue);
						}
					}
					else if (this.SkippedOverUnknownODataAnnotation(text2, out text4))
					{
						duplicatePropertyNamesChecker.AnnotationCollector.TryAddPropertyAnnotationRawJson("", text2, text4);
					}
					else
					{
						this.JsonReader.Read();
						parsedPropertyName = text2;
						if (ODataJsonLightUtils.IsMetadataReferenceProperty(text2))
						{
							return ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty;
						}
						if (!ODataJsonLightReaderUtils.IsAnnotationProperty(text2))
						{
							return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue;
						}
						duplicatePropertyNamesChecker.AnnotationCollector.TryPeekAndCollectAnnotationRawJson(this.JsonReader, "", text2);
						if (ODataJsonLightReaderUtils.IsODataAnnotationName(text2))
						{
							return ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation;
						}
						return ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation;
					}
				}
			}
			if (parsedPropertyName == null)
			{
				return ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject;
			}
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(parsedPropertyName))
			{
				throw new ODataException(Strings.ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(text, parsedPropertyName));
			}
			return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00018EC4 File Offset: 0x000170C4
		private void ProcessPropertyAnnotation(string annotatedPropertyName, string annotationName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, Func<string, object> readPropertyAnnotationValue)
		{
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(annotatedPropertyName) && string.CompareOrdinal(annotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(annotationName, annotatedPropertyName, "odata.type"));
			}
			this.JsonReader.Read();
			if (ODataJsonLightReaderUtils.IsODataAnnotationName(annotationName))
			{
				duplicatePropertyNamesChecker.AddODataPropertyAnnotation(annotatedPropertyName, annotationName, readPropertyAnnotationValue.Invoke(annotationName));
				return;
			}
			duplicatePropertyNamesChecker.AddCustomPropertyAnnotation(annotatedPropertyName, annotationName);
			this.JsonReader.SkipValue();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00018F30 File Offset: 0x00017130
		private string ReadPayloadStartImplementation(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			if (!isReadingNestedPayload)
			{
				this.JsonReader.Read();
				if (allowEmptyPayload && this.JsonReader.NodeType == JsonNodeType.EndOfInput)
				{
					return null;
				}
				this.JsonReader.ReadStartObject();
				if (payloadKind != ODataPayloadKind.Error)
				{
					bool flag = this.jsonLightInputContext.ReadingResponse && (this.jsonLightInputContext.PayloadKindDetectionState == null || this.jsonLightInputContext.PayloadKindDetectionState.MetadataUriParseResult == null);
					return this.ReadMetadataUriAnnotation(payloadKind, duplicatePropertyNamesChecker, flag);
				}
			}
			return null;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00018FB0 File Offset: 0x000171B0
		private string ReadMetadataUriAnnotation(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool failOnMissingMetadataUriAnnotation)
		{
			if (this.JsonReader.NodeType != JsonNodeType.Property)
			{
				if (!failOnMissingMetadataUriAnnotation || payloadKind == ODataPayloadKind.Unsupported)
				{
					return null;
				}
				throw new ODataException(Strings.ODataJsonLightDeserializer_MetadataLinkNotFoundAsFirstProperty);
			}
			else
			{
				string propertyName = this.JsonReader.GetPropertyName();
				if (string.CompareOrdinal("odata.metadata", propertyName) == 0)
				{
					if (duplicatePropertyNamesChecker != null)
					{
						duplicatePropertyNamesChecker.MarkPropertyAsProcessed(propertyName);
					}
					this.JsonReader.ReadNext();
					return this.JsonReader.ReadStringValue();
				}
				if (!failOnMissingMetadataUriAnnotation || payloadKind == ODataPayloadKind.Unsupported)
				{
					return null;
				}
				throw new ODataException(Strings.ODataJsonLightDeserializer_MetadataLinkNotFoundAsFirstProperty);
			}
		}

		// Token: 0x040002C4 RID: 708
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x040002C5 RID: 709
		private IODataMetadataContext metadataContext;

		// Token: 0x040002C6 RID: 710
		private ODataJsonLightMetadataUriParseResult metadataUriParseResult;

		// Token: 0x02000115 RID: 277
		internal enum PropertyParsingResult
		{
			// Token: 0x040002C8 RID: 712
			EndOfObject,
			// Token: 0x040002C9 RID: 713
			PropertyWithValue,
			// Token: 0x040002CA RID: 714
			PropertyWithoutValue,
			// Token: 0x040002CB RID: 715
			ODataInstanceAnnotation,
			// Token: 0x040002CC RID: 716
			CustomInstanceAnnotation,
			// Token: 0x040002CD RID: 717
			MetadataReferenceProperty
		}
	}
}
