using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000207 RID: 519
	internal abstract class ODataJsonLightDeserializer : ODataDeserializer
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0003B89A File Offset: 0x00039A9A
		protected ODataJsonLightDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.jsonLightInputContext = jsonLightInputContext;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0003B8AC File Offset: 0x00039AAC
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

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0003B938 File Offset: 0x00039B38
		internal IODataMetadataContext MetadataContext
		{
			get
			{
				IODataMetadataContext iodataMetadataContext;
				if ((iodataMetadataContext = this.metadataContext) == null)
				{
					iodataMetadataContext = (this.metadataContext = new ODataMetadataContext(base.ReadingResponse, null, this.JsonLightInputContext.EdmTypeResolver, base.Model, this.MetadataDocumentUri, this.ODataUri, this.JsonLightInputContext.MetadataLevel));
				}
				return iodataMetadataContext;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0003B98C File Offset: 0x00039B8C
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonLightInputContext.JsonReader;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0003B999 File Offset: 0x00039B99
		internal ODataJsonLightContextUriParseResult ContextUriParseResult
		{
			get
			{
				return this.contextUriParseResult;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0003B9A1 File Offset: 0x00039BA1
		internal ODataJsonLightInputContext JsonLightInputContext
		{
			get
			{
				return this.jsonLightInputContext;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0003B9A9 File Offset: 0x00039BA9
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0003B9B1 File Offset: 0x00039BB1
		protected Func<PropertyAndAnnotationCollector, string, object> ReadPropertyCustomAnnotationValue { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0003B9BC File Offset: 0x00039BBC
		private Uri MetadataDocumentUri
		{
			get
			{
				return (this.ContextUriParseResult != null && this.ContextUriParseResult.MetadataDocumentUri != null) ? this.ContextUriParseResult.MetadataDocumentUri : null;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0003B9F4 File Offset: 0x00039BF4
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

		// Token: 0x060014A0 RID: 5280 RVA: 0x0003BA38 File Offset: 0x00039C38
		internal void ReadPayloadStart(ODataPayloadKind payloadKind, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			string text = this.ReadPayloadStartImplementation(payloadKind, propertyAndAnnotationCollector, isReadingNestedPayload, allowEmptyPayload);
			ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = null;
			if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error && text != null)
			{
				odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(base.Model, text, payloadKind, base.MessageReaderSettings.ClientCustomTypeResolver, this.JsonLightInputContext.ReadingResponse, this.JsonLightInputContext.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
			}
			this.contextUriParseResult = odataJsonLightContextUriParseResult;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0000250D File Offset: 0x0000070D
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "isReadingNestedPayload", Justification = "The parameter is used in debug builds.")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0003BA9C File Offset: 0x00039C9C
		internal string ReadAndValidateAnnotationStringValue(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			ODataJsonLightReaderUtils.ValidateAnnotationValue(text, annotationName);
			return text;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0003BABE File Offset: 0x00039CBE
		internal string ReadAnnotationStringValue(string annotationName)
		{
			return this.JsonReader.ReadStringValue(annotationName);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0003BACC File Offset: 0x00039CCC
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

		// Token: 0x060014A5 RID: 5285 RVA: 0x0003BB04 File Offset: 0x00039D04
		internal Uri ReadAndValidateAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			if (!base.ReadingResponse)
			{
				return new Uri(text, 0);
			}
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0003BB30 File Offset: 0x00039D30
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

		// Token: 0x060014A7 RID: 5287 RVA: 0x0003BBA0 File Offset: 0x00039DA0
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

		// Token: 0x060014A8 RID: 5288 RVA: 0x0003BC00 File Offset: 0x00039E00
		internal void ProcessProperty(PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue, Action<ODataJsonLightDeserializer.PropertyParsingResult, string> handleProperty)
		{
			string text;
			ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult = this.ParseProperty(propertyAndAnnotationCollector, readPropertyAnnotationValue, out text);
			while (propertyParsingResult == ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation && this.ShouldSkipCustomInstanceAnnotation(text))
			{
				this.JsonReader.SkipValue();
				propertyParsingResult = this.ParseProperty(propertyAndAnnotationCollector, readPropertyAnnotationValue, out text);
			}
			handleProperty.Invoke(propertyParsingResult, text);
			if (propertyParsingResult != ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject && propertyParsingResult != ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation)
			{
				propertyAndAnnotationCollector.MarkPropertyAsProcessed(text);
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs access to this in Debug only.")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0003BC54 File Offset: 0x00039E54
		internal string ReadContextUriAnnotation(ODataPayloadKind payloadKind, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool failOnMissingContextUriAnnotation)
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
				if (string.CompareOrdinal("@odata.context", propertyName) == 0 || this.CompareSimplifiedODataAnnotation("@context", propertyName))
				{
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

		// Token: 0x060014AB RID: 5291 RVA: 0x0003BCDD File Offset: 0x00039EDD
		protected bool CompareSimplifiedODataAnnotation(string simplifiedPropertyName, string propertyName)
		{
			return this.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingODataAnnotationWithoutPrefix && string.CompareOrdinal(simplifiedPropertyName, propertyName) == 0;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0003BCFD File Offset: 0x00039EFD
		protected string CompleteSimplifiedODataAnnotation(string annotationName)
		{
			if (this.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingODataAnnotationWithoutPrefix && annotationName.IndexOf('.') == -1)
			{
				annotationName = "odata." + annotationName;
			}
			return annotationName;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0003BD2A File Offset: 0x00039F2A
		private bool ShouldSkipCustomInstanceAnnotation(string annotationName)
		{
			return (!(this is ODataJsonLightErrorDeserializer) || base.MessageReaderSettings.ShouldIncludeAnnotation != null) && base.MessageReaderSettings.ShouldSkipAnnotation(annotationName);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0003BD4F File Offset: 0x00039F4F
		private static bool IsInstanceAnnotation(string annotationName)
		{
			return !string.IsNullOrEmpty(annotationName) && annotationName.get_Chars(0) == '@';
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003BD67 File Offset: 0x00039F67
		private bool SkippedOverUnknownODataAnnotation(string annotationName, out object annotationValue)
		{
			if (ODataAnnotationNames.IsUnknownODataAnnotationName(annotationName))
			{
				annotationValue = this.ReadODataOrCustomInstanceAnnotationValue(annotationName);
				return true;
			}
			annotationValue = null;
			return false;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0003BD80 File Offset: 0x00039F80
		private object ReadODataOrCustomInstanceAnnotationValue(string annotationName)
		{
			this.JsonReader.Read();
			object obj;
			if (this.JsonReader.NodeType != JsonNodeType.PrimitiveValue)
			{
				obj = this.JsonReader.ReadAsUntypedOrNullValue();
			}
			else
			{
				obj = this.JsonReader.Value;
				this.JsonReader.SkipValue();
			}
			return obj;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		private ODataJsonLightDeserializer.PropertyParsingResult ParseProperty(PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue, out string parsedPropertyName)
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
				else
				{
					object obj = null;
					if (flag)
					{
						text3 = this.CompleteSimplifiedODataAnnotation(text3);
						if (!ODataJsonLightReaderUtils.IsAnnotationProperty(text2) && this.SkippedOverUnknownODataAnnotation(text3, out obj))
						{
							propertyAndAnnotationCollector.AddODataPropertyAnnotation(text2, text3, obj);
						}
						else
						{
							parsedPropertyName = text2;
							text = text3;
							this.ProcessPropertyAnnotation(text2, text3, propertyAndAnnotationCollector, readPropertyAnnotationValue);
						}
					}
					else if (flag2 && this.SkippedOverUnknownODataAnnotation(text2, out obj))
					{
						propertyAndAnnotationCollector.AddODataScopeAnnotation(text2, obj);
					}
					else
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

		// Token: 0x060014B2 RID: 5298 RVA: 0x0003BF0D File Offset: 0x0003A10D
		private void ProcessPropertyAnnotation(string annotatedPropertyName, string annotationName, PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue)
		{
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(annotatedPropertyName) && string.CompareOrdinal(annotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(annotationName, annotatedPropertyName, "odata.type"));
			}
			this.ReadODataOrCustomInstanceAnnotationValue(annotatedPropertyName, annotationName, propertyAndAnnotationCollector, readPropertyAnnotationValue);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0003BF44 File Offset: 0x0003A144
		private void ReadODataOrCustomInstanceAnnotationValue(string annotatedPropertyName, string annotationName, PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue)
		{
			this.JsonReader.Read();
			if (ODataJsonLightReaderUtils.IsODataAnnotationName(annotationName))
			{
				propertyAndAnnotationCollector.AddODataPropertyAnnotation(annotatedPropertyName, annotationName, readPropertyAnnotationValue.Invoke(annotationName));
				return;
			}
			if (this.ShouldSkipCustomInstanceAnnotation(annotationName) || (this is ODataJsonLightErrorDeserializer && base.MessageReaderSettings.ShouldIncludeAnnotation == null))
			{
				propertyAndAnnotationCollector.CheckIfPropertyOpenForAnnotations(annotatedPropertyName, annotationName);
				this.JsonReader.SkipValue();
				return;
			}
			propertyAndAnnotationCollector.AddCustomPropertyAnnotation(annotatedPropertyName, annotationName, this.ReadPropertyCustomAnnotationValue.Invoke(propertyAndAnnotationCollector, annotationName));
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0003BFC0 File Offset: 0x0003A1C0
		private string ReadPayloadStartImplementation(ODataPayloadKind payloadKind, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool isReadingNestedPayload, bool allowEmptyPayload)
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
					return this.ReadContextUriAnnotation(payloadKind, propertyAndAnnotationCollector, readingResponse);
				}
			}
			return null;
		}

		// Token: 0x04000A18 RID: 2584
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000A19 RID: 2585
		private IODataMetadataContext metadataContext;

		// Token: 0x04000A1A RID: 2586
		private ODataJsonLightContextUriParseResult contextUriParseResult;

		// Token: 0x04000A1B RID: 2587
		private ODataUri odataUri;

		// Token: 0x04000A1C RID: 2588
		private bool isODataUriRead;

		// Token: 0x02000338 RID: 824
		internal enum PropertyParsingResult
		{
			// Token: 0x04000D40 RID: 3392
			EndOfObject,
			// Token: 0x04000D41 RID: 3393
			PropertyWithValue,
			// Token: 0x04000D42 RID: 3394
			PropertyWithoutValue,
			// Token: 0x04000D43 RID: 3395
			ODataInstanceAnnotation,
			// Token: 0x04000D44 RID: 3396
			CustomInstanceAnnotation,
			// Token: 0x04000D45 RID: 3397
			MetadataReferenceProperty
		}
	}
}
