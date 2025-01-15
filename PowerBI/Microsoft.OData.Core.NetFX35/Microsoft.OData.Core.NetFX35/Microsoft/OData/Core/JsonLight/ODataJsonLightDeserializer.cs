using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B7 RID: 183
	internal abstract class ODataJsonLightDeserializer : ODataDeserializer
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x00016665 File Offset: 0x00014865
		protected ODataJsonLightDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.jsonLightInputContext = jsonLightInputContext;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00016678 File Offset: 0x00014878
		internal ODataUri ODataUri
		{
			get
			{
				if (this.isODataUriRead)
				{
					return this.odataUri;
				}
				if (this.ContextUriParseResult == null || this.ContextUriParseResult.NavigationSource == null || !(this.ContextUriParseResult.NavigationSource is IEdmContainedEntitySet) || this.contextUriParseResult.Path == null)
				{
					this.isODataUriRead = true;
					return this.odataUri = null;
				}
				this.odataUri = new ODataUri
				{
					Path = this.ContextUriParseResult.Path
				};
				this.isODataUriRead = true;
				return this.odataUri;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00016708 File Offset: 0x00014908
		internal IODataMetadataContext MetadataContext
		{
			get
			{
				Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified = base.MessageReaderSettings.ReaderBehavior.OperationsBoundToEntityTypeMustBeContainerQualified;
				IODataMetadataContext iodataMetadataContext;
				if ((iodataMetadataContext = this.metadataContext) == null)
				{
					iodataMetadataContext = (this.metadataContext = new ODataMetadataContext(base.ReadingResponse, operationsBoundToEntityTypeMustBeContainerQualified, this.JsonLightInputContext.EdmTypeResolver, base.Model, this.MetadataDocumentUri, this.ODataUri, this.JsonLightInputContext.MetadataLevel));
				}
				return iodataMetadataContext;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001676D File Offset: 0x0001496D
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonLightInputContext.JsonReader;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001677A File Offset: 0x0001497A
		internal ODataJsonLightContextUriParseResult ContextUriParseResult
		{
			get
			{
				return this.contextUriParseResult;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00016782 File Offset: 0x00014982
		internal ODataJsonLightInputContext JsonLightInputContext
		{
			get
			{
				return this.jsonLightInputContext;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001678A File Offset: 0x0001498A
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00016792 File Offset: 0x00014992
		protected Func<DuplicatePropertyNamesChecker, string, object> ReadPropertyCustomAnnotationValue { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001679C File Offset: 0x0001499C
		private Uri MetadataDocumentUri
		{
			get
			{
				return (this.ContextUriParseResult != null && this.ContextUriParseResult.MetadataDocumentUri != null) ? this.ContextUriParseResult.MetadataDocumentUri : null;
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000167D4 File Offset: 0x000149D4
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

		// Token: 0x06000686 RID: 1670 RVA: 0x00016818 File Offset: 0x00014A18
		internal void ReadPayloadStart(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			string text = this.ReadPayloadStartImplementation(payloadKind, duplicatePropertyNamesChecker, isReadingNestedPayload, allowEmptyPayload);
			ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = null;
			if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error && text != null)
			{
				odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(base.Model, text, payloadKind, base.MessageReaderSettings.ReaderBehavior, this.JsonLightInputContext.ReadingResponse);
			}
			this.contextUriParseResult = odataJsonLightContextUriParseResult;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00016869 File Offset: 0x00014A69
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "isReadingNestedPayload", Justification = "The parameter is used in debug builds.")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001686C File Offset: 0x00014A6C
		internal string ReadAndValidateAnnotationStringValue(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			ODataJsonLightReaderUtils.ValidateAnnotationValue(text, annotationName);
			return text;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001688E File Offset: 0x00014A8E
		internal string ReadAnnotationStringValue(string annotationName)
		{
			return this.JsonReader.ReadStringValue(annotationName);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001689C File Offset: 0x00014A9C
		internal Uri ReadAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			if (text == null)
			{
				return null;
			}
			if (!base.ReadingResponse)
			{
				return new Uri(text, 0);
			}
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000168D4 File Offset: 0x00014AD4
		internal Uri ReadAndValidateAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			if (!base.ReadingResponse)
			{
				return new Uri(text, 0);
			}
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00016900 File Offset: 0x00014B00
		internal long ReadAndValidateAnnotationAsLongForIeee754Compatible(string annotationName)
		{
			object obj = this.JsonReader.ReadPrimitiveValue();
			ODataJsonLightReaderUtils.ValidateAnnotationValue(obj, annotationName);
			if ((obj is string) ^ this.JsonReader.IsIeee754Compatible)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_ConflictBetweenInputFormatAndParameter("Edm.Int64"));
			}
			return (long)ODataJsonLightReaderUtils.ConvertValue(obj, EdmCoreModel.Instance.GetInt64(false), base.MessageReaderSettings, true, annotationName, this.JsonLightInputContext.PayloadValueConverter);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00016970 File Offset: 0x00014B70
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
					throw new ODataException(Strings.ODataJsonLightDeserializer_RelativeUriUsedWithouODataMetadataAnnotation(uriFromPayload, "odata.context"));
				}
				uri = UriUtils.UriToAbsoluteUri(metadataDocumentUri, uri);
			}
			return uri;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000169D0 File Offset: 0x00014BD0
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

		// Token: 0x0600068F RID: 1679 RVA: 0x00016A26 File Offset: 0x00014C26
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs access to this in Debug only.")]
		[Conditional("DEBUG")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00016A28 File Offset: 0x00014C28
		internal string ReadContextUriAnnotation(ODataPayloadKind payloadKind, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool failOnMissingContextUriAnnotation)
		{
			if (this.JsonReader.NodeType != JsonNodeType.Property)
			{
				if (!failOnMissingContextUriAnnotation || payloadKind == ODataPayloadKind.Unsupported)
				{
					return null;
				}
				throw new ODataException(Strings.ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty);
			}
			else
			{
				string propertyName = this.JsonReader.GetPropertyName();
				if (string.CompareOrdinal('@' + "odata.context", propertyName) == 0 || this.CompareSimplifiedODataAnnotation("@context", propertyName))
				{
					if (duplicatePropertyNamesChecker != null)
					{
						duplicatePropertyNamesChecker.MarkPropertyAsProcessed(propertyName);
					}
					this.JsonReader.ReadNext();
					return this.JsonReader.ReadStringValue();
				}
				if (!failOnMissingContextUriAnnotation || payloadKind == ODataPayloadKind.Unsupported)
				{
					return null;
				}
				throw new ODataException(Strings.ODataJsonLightDeserializer_ContextLinkNotFoundAsFirstProperty);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00016AC7 File Offset: 0x00014CC7
		protected bool CompareSimplifiedODataAnnotation(string simplifiedPropertyName, string propertyName)
		{
			return base.MessageReaderSettings.ODataSimplified && string.CompareOrdinal(simplifiedPropertyName, propertyName) == 0;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00016AE2 File Offset: 0x00014CE2
		protected string CompleteSimplifiedODataAnnotation(string annotationName)
		{
			if (base.MessageReaderSettings.ODataSimplified && annotationName.IndexOf('.') == -1)
			{
				annotationName = "odata." + annotationName;
			}
			return annotationName;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00016B0A File Offset: 0x00014D0A
		private bool ShouldSkipCustomInstanceAnnotation(string annotationName)
		{
			return (!(this is ODataJsonLightErrorDeserializer) || base.MessageReaderSettings.ShouldIncludeAnnotation != null) && base.MessageReaderSettings.ShouldSkipAnnotation(annotationName);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00016B2F File Offset: 0x00014D2F
		private static bool IsInstanceAnnotation(string annotationName)
		{
			return !string.IsNullOrEmpty(annotationName) && annotationName.get_Chars(0) == '@';
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00016B47 File Offset: 0x00014D47
		private bool SkippedOverUnknownODataAnnotation(string annotationName)
		{
			if (ODataAnnotationNames.IsUnknownODataAnnotationName(annotationName))
			{
				this.JsonReader.Read();
				this.JsonReader.SkipValue();
				return true;
			}
			return false;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00016B6C File Offset: 0x00014D6C
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
				bool flag2 = false;
				if (!flag)
				{
					flag2 = ODataJsonLightDeserializer.IsInstanceAnnotation(propertyName);
					text2 = (flag2 ? this.CompleteSimplifiedODataAnnotation(propertyName.Substring(1)) : propertyName);
				}
				if (parsedPropertyName != null && string.CompareOrdinal(parsedPropertyName, text2) != 0)
				{
					if (ODataJsonLightReaderUtils.IsAnnotationProperty(parsedPropertyName))
					{
						throw new ODataException(Strings.ODataJsonLightDeserializer_AnnotationTargetingInstanceAnnotationWithoutValue(text, parsedPropertyName));
					}
					return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue;
				}
				else if (flag)
				{
					text3 = this.CompleteSimplifiedODataAnnotation(text3);
					if (ODataJsonLightReaderUtils.IsAnnotationProperty(text2) || !this.SkippedOverUnknownODataAnnotation(text3))
					{
						parsedPropertyName = text2;
						text = text3;
						this.ProcessPropertyAnnotation(text2, text3, duplicatePropertyNamesChecker, readPropertyAnnotationValue);
					}
				}
				else if (!this.SkippedOverUnknownODataAnnotation(text2))
				{
					this.JsonReader.Read();
					parsedPropertyName = text2;
					if (!flag2 && ODataJsonLightUtils.IsMetadataReferenceProperty(text2))
					{
						return ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty;
					}
					if (!flag2 && !ODataJsonLightReaderUtils.IsAnnotationProperty(text2))
					{
						return ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue;
					}
					if (flag2 && ODataJsonLightReaderUtils.IsODataAnnotationName(text2))
					{
						return ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation;
					}
					return ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation;
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

		// Token: 0x06000697 RID: 1687 RVA: 0x00016C88 File Offset: 0x00014E88
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
			if (this.ShouldSkipCustomInstanceAnnotation(annotationName) || (this is ODataJsonLightErrorDeserializer && base.MessageReaderSettings.ShouldIncludeAnnotation == null))
			{
				duplicatePropertyNamesChecker.AddCustomPropertyAnnotation(annotatedPropertyName, annotationName, null);
				this.JsonReader.SkipValue();
				return;
			}
			duplicatePropertyNamesChecker.AddCustomPropertyAnnotation(annotatedPropertyName, annotationName, this.ReadPropertyCustomAnnotationValue.Invoke(duplicatePropertyNamesChecker, annotationName));
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00016D2C File Offset: 0x00014F2C
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
					bool readingResponse = this.jsonLightInputContext.ReadingResponse;
					return this.ReadContextUriAnnotation(payloadKind, duplicatePropertyNamesChecker, readingResponse);
				}
			}
			return null;
		}

		// Token: 0x04000311 RID: 785
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000312 RID: 786
		private IODataMetadataContext metadataContext;

		// Token: 0x04000313 RID: 787
		private ODataJsonLightContextUriParseResult contextUriParseResult;

		// Token: 0x04000314 RID: 788
		private ODataUri odataUri;

		// Token: 0x04000315 RID: 789
		private bool isODataUriRead;

		// Token: 0x020000B8 RID: 184
		internal enum PropertyParsingResult
		{
			// Token: 0x04000318 RID: 792
			EndOfObject,
			// Token: 0x04000319 RID: 793
			PropertyWithValue,
			// Token: 0x0400031A RID: 794
			PropertyWithoutValue,
			// Token: 0x0400031B RID: 795
			ODataInstanceAnnotation,
			// Token: 0x0400031C RID: 796
			CustomInstanceAnnotation,
			// Token: 0x0400031D RID: 797
			MetadataReferenceProperty
		}
	}
}
