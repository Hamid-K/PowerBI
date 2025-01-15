using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000240 RID: 576
	internal abstract class ODataJsonLightDeserializer : ODataDeserializer
	{
		// Token: 0x060018F4 RID: 6388 RVA: 0x00047C4D File Offset: 0x00045E4D
		protected ODataJsonLightDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.jsonLightInputContext = jsonLightInputContext;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x00047C60 File Offset: 0x00045E60
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

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x00047CEC File Offset: 0x00045EEC
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

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x00047D40 File Offset: 0x00045F40
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonLightInputContext.JsonReader;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x00047D4D File Offset: 0x00045F4D
		internal ODataJsonLightContextUriParseResult ContextUriParseResult
		{
			get
			{
				return this.contextUriParseResult;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00047D55 File Offset: 0x00045F55
		internal ODataJsonLightInputContext JsonLightInputContext
		{
			get
			{
				return this.jsonLightInputContext;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x00047D5D File Offset: 0x00045F5D
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x00047D65 File Offset: 0x00045F65
		protected Func<PropertyAndAnnotationCollector, string, object> ReadPropertyCustomAnnotationValue { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00047D70 File Offset: 0x00045F70
		private Uri MetadataDocumentUri
		{
			get
			{
				return (this.ContextUriParseResult != null && this.ContextUriParseResult.MetadataDocumentUri != null) ? this.ContextUriParseResult.MetadataDocumentUri : null;
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00047DA8 File Offset: 0x00045FA8
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

		// Token: 0x060018FE RID: 6398 RVA: 0x00047DEC File Offset: 0x00045FEC
		internal void ReadPayloadStart(ODataPayloadKind payloadKind, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			string text = this.ReadPayloadStartImplementation(payloadKind, propertyAndAnnotationCollector, isReadingNestedPayload, allowEmptyPayload);
			ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = null;
			if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error && text != null)
			{
				odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(base.Model, text, payloadKind, base.MessageReaderSettings.ClientCustomTypeResolver, this.JsonLightInputContext.ReadingResponse || payloadKind == ODataPayloadKind.Delta, this.JsonLightInputContext.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
			}
			this.contextUriParseResult = odataJsonLightContextUriParseResult;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00047E58 File Offset: 0x00046058
		internal Task ReadPayloadStartAsync(ODataPayloadKind payloadKind, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool isReadingNestedPayload, bool allowEmptyPayload)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				string text = this.ReadPayloadStartImplementation(payloadKind, propertyAndAnnotationCollector, isReadingNestedPayload, allowEmptyPayload);
				if (!isReadingNestedPayload && payloadKind != ODataPayloadKind.Error && text != null)
				{
					this.contextUriParseResult = ODataJsonLightContextUriParser.Parse(this.Model, text, payloadKind, this.MessageReaderSettings.ClientCustomTypeResolver, this.JsonLightInputContext.ReadingResponse, true);
				}
			});
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0000239D File Offset: 0x0000059D
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "isReadingNestedPayload", Justification = "The parameter is used in debug builds.")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00047EA0 File Offset: 0x000460A0
		internal string ReadAndValidateAnnotationStringValue(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			ODataJsonLightReaderUtils.ValidateAnnotationValue(text, annotationName);
			return text;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00047EC2 File Offset: 0x000460C2
		internal string ReadAnnotationStringValue(string annotationName)
		{
			return this.JsonReader.ReadStringValue(annotationName);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00047ED0 File Offset: 0x000460D0
		internal Uri ReadAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.JsonReader.ReadStringValue(annotationName);
			if (text == null)
			{
				return null;
			}
			if (!base.ReadingResponse)
			{
				return new Uri(text, UriKind.RelativeOrAbsolute);
			}
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00047F08 File Offset: 0x00046108
		internal Uri ReadAndValidateAnnotationStringValueAsUri(string annotationName)
		{
			string text = this.ReadAndValidateAnnotationStringValue(annotationName);
			if (!base.ReadingResponse)
			{
				return new Uri(text, UriKind.RelativeOrAbsolute);
			}
			return this.ProcessUriFromPayload(text);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00047F34 File Offset: 0x00046134
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

		// Token: 0x06001906 RID: 6406 RVA: 0x00047FA4 File Offset: 0x000461A4
		internal Uri ProcessUriFromPayload(string uriFromPayload)
		{
			Uri uri = new Uri(uriFromPayload, UriKind.RelativeOrAbsolute);
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

		// Token: 0x06001907 RID: 6407 RVA: 0x00048004 File Offset: 0x00046204
		internal void ProcessProperty(PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue, Action<ODataJsonLightDeserializer.PropertyParsingResult, string> handleProperty)
		{
			string text;
			ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult = this.ParseProperty(propertyAndAnnotationCollector, readPropertyAnnotationValue, out text);
			while (propertyParsingResult == ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation && this.ShouldSkipCustomInstanceAnnotation(text))
			{
				this.JsonReader.Read();
				this.JsonReader.SkipValue();
				propertyParsingResult = this.ParseProperty(propertyAndAnnotationCollector, readPropertyAnnotationValue, out text);
			}
			handleProperty(propertyParsingResult, text);
			if (propertyParsingResult != ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject && propertyParsingResult != ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation)
			{
				propertyAndAnnotationCollector.MarkPropertyAsProcessed(text);
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs access to this in Debug only.")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00048064 File Offset: 0x00046264
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
					if (propertyAndAnnotationCollector != null)
					{
						propertyAndAnnotationCollector.MarkPropertyAsProcessed(propertyName);
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

		// Token: 0x0600190A RID: 6410 RVA: 0x000480F7 File Offset: 0x000462F7
		protected bool CompareSimplifiedODataAnnotation(string simplifiedPropertyName, string propertyName)
		{
			return this.JsonLightInputContext.OptionalODataPrefix && string.CompareOrdinal(simplifiedPropertyName, propertyName) == 0;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00048112 File Offset: 0x00046312
		protected string CompleteSimplifiedODataAnnotation(string annotationName)
		{
			if (this.JsonLightInputContext.OptionalODataPrefix && annotationName.IndexOf('.') == -1)
			{
				annotationName = "odata." + annotationName;
			}
			return annotationName;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0004813A File Offset: 0x0004633A
		private bool ShouldSkipCustomInstanceAnnotation(string annotationName)
		{
			return (!(this is ODataJsonLightErrorDeserializer) || base.MessageReaderSettings.ShouldIncludeAnnotation != null) && base.MessageReaderSettings.ShouldSkipAnnotation(annotationName);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0004815F File Offset: 0x0004635F
		private static bool IsInstanceAnnotation(string annotationName)
		{
			return !string.IsNullOrEmpty(annotationName) && annotationName[0] == '@';
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00048177 File Offset: 0x00046377
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

		// Token: 0x0600190F RID: 6415 RVA: 0x00048190 File Offset: 0x00046390
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

		// Token: 0x06001910 RID: 6416 RVA: 0x000481E0 File Offset: 0x000463E0
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
				if (flag && string.CompareOrdinal(this.CompleteSimplifiedODataAnnotation(text3), "odata.delta") == 0)
				{
					this.JsonReader.Read();
					parsedPropertyName = text2;
					return ODataJsonLightDeserializer.PropertyParsingResult.NestedDeltaResourceSet;
				}
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

		// Token: 0x06001911 RID: 6417 RVA: 0x00048339 File Offset: 0x00046539
		private void ProcessPropertyAnnotation(string annotatedPropertyName, string annotationName, PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue)
		{
			if (ODataJsonLightReaderUtils.IsAnnotationProperty(annotatedPropertyName) && string.CompareOrdinal(annotationName, "odata.type") != 0)
			{
				throw new ODataException(Strings.ODataJsonLightDeserializer_OnlyODataTypeAnnotationCanTargetInstanceAnnotation(annotationName, annotatedPropertyName, "odata.type"));
			}
			this.ReadODataOrCustomInstanceAnnotationValue(annotatedPropertyName, annotationName, propertyAndAnnotationCollector, readPropertyAnnotationValue);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00048370 File Offset: 0x00046570
		private void ReadODataOrCustomInstanceAnnotationValue(string annotatedPropertyName, string annotationName, PropertyAndAnnotationCollector propertyAndAnnotationCollector, Func<string, object> readPropertyAnnotationValue)
		{
			this.JsonReader.Read();
			if (ODataJsonLightReaderUtils.IsODataAnnotationName(annotationName))
			{
				propertyAndAnnotationCollector.AddODataPropertyAnnotation(annotatedPropertyName, annotationName, readPropertyAnnotationValue(annotationName));
				return;
			}
			if (this.ShouldSkipCustomInstanceAnnotation(annotationName) || (this is ODataJsonLightErrorDeserializer && base.MessageReaderSettings.ShouldIncludeAnnotation == null))
			{
				propertyAndAnnotationCollector.CheckIfPropertyOpenForAnnotations(annotatedPropertyName, annotationName);
				this.JsonReader.SkipValue();
				return;
			}
			propertyAndAnnotationCollector.AddCustomPropertyAnnotation(annotatedPropertyName, annotationName, this.ReadPropertyCustomAnnotationValue(propertyAndAnnotationCollector, annotationName));
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000483EC File Offset: 0x000465EC
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

		// Token: 0x04000B32 RID: 2866
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000B33 RID: 2867
		private IODataMetadataContext metadataContext;

		// Token: 0x04000B34 RID: 2868
		private ODataJsonLightContextUriParseResult contextUriParseResult;

		// Token: 0x04000B35 RID: 2869
		private ODataUri odataUri;

		// Token: 0x04000B36 RID: 2870
		private bool isODataUriRead;

		// Token: 0x020003F2 RID: 1010
		internal enum PropertyParsingResult
		{
			// Token: 0x04000F98 RID: 3992
			EndOfObject,
			// Token: 0x04000F99 RID: 3993
			PropertyWithValue,
			// Token: 0x04000F9A RID: 3994
			PropertyWithoutValue,
			// Token: 0x04000F9B RID: 3995
			ODataInstanceAnnotation,
			// Token: 0x04000F9C RID: 3996
			CustomInstanceAnnotation,
			// Token: 0x04000F9D RID: 3997
			MetadataReferenceProperty,
			// Token: 0x04000F9E RID: 3998
			NestedDeltaResourceSet
		}
	}
}
