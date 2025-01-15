using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000145 RID: 325
	internal class ODataJsonLightSerializer : ODataSerializer
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x0001B940 File Offset: 0x00019B40
		internal ODataJsonLightSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.instanceAnnotationWriter = new SimpleLazy<JsonLightInstanceAnnotationWriter>(() => new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(jsonLightOutputContext), jsonLightOutputContext.TypeNameOracle));
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001B990 File Offset: 0x00019B90
		internal ODataJsonLightOutputContext JsonLightOutputContext
		{
			get
			{
				return this.jsonLightOutputContext;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001B998 File Offset: 0x00019B98
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonLightOutputContext.JsonWriter;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001B9A5 File Offset: 0x00019BA5
		internal JsonLightInstanceAnnotationWriter InstanceAnnotationWriter
		{
			get
			{
				return this.instanceAnnotationWriter.Value;
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001B9B2 File Offset: 0x00019BB2
		internal void WritePayloadStart()
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001B9C5 File Offset: 0x00019BC5
		internal void WritePayloadEnd()
		{
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		internal void WriteMetadataUriProperty(Uri metadataUri)
		{
			this.JsonWriter.WriteName("odata.metadata");
			this.JsonWriter.WritePrimitiveValue(metadataUri.AbsoluteUri, base.Version);
			this.allowRelativeUri = true;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001BA08 File Offset: 0x00019C08
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WritePayloadStart();
			payloadWriterAction.Invoke();
			this.WritePayloadEnd();
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001BA80 File Offset: 0x00019C80
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.JsonLightOutputContext.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(this.InstanceAnnotationWriter.WriteInstanceAnnotations), error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
			});
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001BABC File Offset: 0x00019CBC
		internal string UriToString(Uri uri)
		{
			ODataMetadataDocumentUri metadataDocumentUri = this.jsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			Uri uri2 = ((metadataDocumentUri == null) ? null : metadataDocumentUri.BaseUri);
			Uri uri3;
			if (this.jsonLightOutputContext.UrlResolver != null)
			{
				uri3 = this.jsonLightOutputContext.UrlResolver.ResolveUrl(uri2, uri);
				if (uri3 != null)
				{
					return UriUtilsCommon.UriToString(uri3);
				}
			}
			uri3 = uri;
			if (!uri3.IsAbsoluteUri)
			{
				if (!this.allowRelativeUri)
				{
					if (uri2 == null)
					{
						throw new ODataException(Strings.ODataJsonLightSerializer_RelativeUriUsedWithoutMetadataDocumentUriOrMetadata(UriUtilsCommon.UriToString(uri3)));
					}
					uri3 = UriUtils.UriToAbsoluteUri(uri2, uri);
				}
				else
				{
					uri3 = UriUtils.EnsureEscapedRelativeUri(uri3);
				}
			}
			return UriUtilsCommon.UriToString(uri3);
		}

		// Token: 0x0400034F RID: 847
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000350 RID: 848
		private readonly SimpleLazy<JsonLightInstanceAnnotationWriter> instanceAnnotationWriter;

		// Token: 0x04000351 RID: 849
		private bool allowRelativeUri;
	}
}
