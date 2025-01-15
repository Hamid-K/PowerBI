using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000251 RID: 593
	internal class ODataJsonLightSerializer : ODataSerializer
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x00050F1C File Offset: 0x0004F11C
		internal ODataJsonLightSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool initContextUriBuilder = false)
			: base(jsonLightOutputContext)
		{
			ODataJsonLightSerializer <>4__this = this;
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.instanceAnnotationWriter = new SimpleLazy<JsonLightInstanceAnnotationWriter>(() => new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(jsonLightOutputContext, false), jsonLightOutputContext.TypeNameOracle));
			this.odataAnnotationWriter = new SimpleLazy<JsonLightODataAnnotationWriter>(() => new JsonLightODataAnnotationWriter(jsonLightOutputContext.JsonWriter, <>4__this.JsonLightOutputContext.OmitODataPrefix, <>4__this.MessageWriterSettings.Version));
			if (initContextUriBuilder)
			{
				this.ContextUriBuilder = ODataContextUriBuilder.Create(this.jsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri, this.jsonLightOutputContext.WritingResponse && !(this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel));
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00050FC7 File Offset: 0x0004F1C7
		internal ODataJsonLightOutputContext JsonLightOutputContext
		{
			get
			{
				return this.jsonLightOutputContext;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00050FCF File Offset: 0x0004F1CF
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonLightOutputContext.JsonWriter;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00050FDC File Offset: 0x0004F1DC
		internal JsonLightInstanceAnnotationWriter InstanceAnnotationWriter
		{
			get
			{
				return this.instanceAnnotationWriter.Value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00050FE9 File Offset: 0x0004F1E9
		internal JsonLightODataAnnotationWriter ODataAnnotationWriter
		{
			get
			{
				return this.odataAnnotationWriter.Value;
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00050FF6 File Offset: 0x0004F1F6
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method for consistency with other formats.")]
		internal void WritePayloadStart()
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00051009 File Offset: 0x0004F209
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method for consistency with other formats.")]
		internal void WritePayloadEnd()
		{
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0005101C File Offset: 0x0004F21C
		internal ODataContextUrlInfo WriteContextUriProperty(ODataPayloadKind payloadKind, Func<ODataContextUrlInfo> contextUrlInfoGen = null, ODataContextUrlInfo parentContextUrlInfo = null, string propertyName = null)
		{
			if (this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
			{
				return null;
			}
			ODataContextUrlInfo odataContextUrlInfo = null;
			if (contextUrlInfoGen != null)
			{
				odataContextUrlInfo = contextUrlInfoGen();
			}
			if (odataContextUrlInfo != null && odataContextUrlInfo.IsHiddenBy(parentContextUrlInfo))
			{
				return null;
			}
			Uri uri = this.ContextUriBuilder.BuildContextUri(payloadKind, odataContextUrlInfo);
			if (uri != null)
			{
				if (string.IsNullOrEmpty(propertyName))
				{
					this.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.context");
				}
				else
				{
					this.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.context");
				}
				this.JsonWriter.WritePrimitiveValue(uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString);
				this.allowRelativeUri = true;
				return odataContextUrlInfo;
			}
			return null;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000510C8 File Offset: 0x0004F2C8
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WritePayloadStart();
			payloadWriterAction();
			this.WritePayloadEnd();
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000510DC File Offset: 0x0004F2DC
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.JsonLightOutputContext.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(this.InstanceAnnotationWriter.WriteInstanceAnnotationsForError), error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
			});
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00051118 File Offset: 0x0004F318
		internal string UriToString(Uri uri)
		{
			Uri metadataDocumentUri = this.jsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			Uri uri2;
			if (this.jsonLightOutputContext.PayloadUriConverter != null)
			{
				uri2 = this.jsonLightOutputContext.PayloadUriConverter.ConvertPayloadUri(metadataDocumentUri, uri);
				if (uri2 != null)
				{
					return UriUtils.UriToString(uri2);
				}
			}
			uri2 = uri;
			if (!uri2.IsAbsoluteUri)
			{
				if (!this.allowRelativeUri)
				{
					if (metadataDocumentUri == null)
					{
						throw new ODataException(Strings.ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(UriUtils.UriToString(uri2)));
					}
					uri2 = UriUtils.UriToAbsoluteUri(metadataDocumentUri, uri);
				}
				else
				{
					uri2 = UriUtils.EnsureEscapedRelativeUri(uri2);
				}
			}
			return UriUtils.UriToString(uri2);
		}

		// Token: 0x04000B5A RID: 2906
		protected readonly ODataContextUriBuilder ContextUriBuilder;

		// Token: 0x04000B5B RID: 2907
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000B5C RID: 2908
		private readonly SimpleLazy<JsonLightInstanceAnnotationWriter> instanceAnnotationWriter;

		// Token: 0x04000B5D RID: 2909
		private readonly SimpleLazy<JsonLightODataAnnotationWriter> odataAnnotationWriter;

		// Token: 0x04000B5E RID: 2910
		private bool allowRelativeUri;
	}
}
