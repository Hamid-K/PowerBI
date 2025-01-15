using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000BD RID: 189
	internal class ODataJsonLightSerializer : ODataSerializer
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x000183F8 File Offset: 0x000165F8
		internal ODataJsonLightSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool initContextUriBuilder = false)
			: base(jsonLightOutputContext)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.instanceAnnotationWriter = new SimpleLazy<JsonLightInstanceAnnotationWriter>(() => new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(jsonLightOutputContext, false), jsonLightOutputContext.TypeNameOracle));
			this.odataAnnotationWriter = new SimpleLazy<JsonLightODataAnnotationWriter>(() => new JsonLightODataAnnotationWriter(jsonLightOutputContext.JsonWriter, jsonLightOutputContext.MessageWriterSettings.ODataSimplified));
			if (initContextUriBuilder)
			{
				this.ContextUriBuilder = jsonLightOutputContext.CreateContextUriBuilder();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001847A File Offset: 0x0001667A
		internal ODataJsonLightOutputContext JsonLightOutputContext
		{
			get
			{
				return this.jsonLightOutputContext;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00018482 File Offset: 0x00016682
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonLightOutputContext.JsonWriter;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001848F File Offset: 0x0001668F
		internal JsonLightInstanceAnnotationWriter InstanceAnnotationWriter
		{
			get
			{
				return this.instanceAnnotationWriter.Value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001849C File Offset: 0x0001669C
		internal JsonLightODataAnnotationWriter ODataAnnotationWriter
		{
			get
			{
				return this.odataAnnotationWriter.Value;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000184A9 File Offset: 0x000166A9
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method for consistency with other formats.")]
		internal void WritePayloadStart()
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000184BC File Offset: 0x000166BC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method for consistency with other formats.")]
		internal void WritePayloadEnd()
		{
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000184D0 File Offset: 0x000166D0
		internal ODataContextUrlInfo WriteContextUriProperty(ODataPayloadKind payloadKind, Func<ODataContextUrlInfo> contextUrlInfoGen = null, ODataContextUrlInfo parentContextUrlInfo = null, string propertyName = null)
		{
			if (this.jsonLightOutputContext.ContextUrlLevel == ODataContextUrlLevel.None)
			{
				return null;
			}
			ODataContextUrlInfo odataContextUrlInfo = null;
			if (contextUrlInfoGen != null)
			{
				odataContextUrlInfo = contextUrlInfoGen.Invoke();
			}
			if (this.jsonLightOutputContext.ContextUrlLevel == ODataContextUrlLevel.OnDemand && odataContextUrlInfo != null && odataContextUrlInfo.IsHiddenBy(parentContextUrlInfo))
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
				this.JsonWriter.WritePrimitiveValue(uri.AbsoluteUri);
				this.allowRelativeUri = true;
				return odataContextUrlInfo;
			}
			return null;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00018575 File Offset: 0x00016775
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WritePayloadStart();
			payloadWriterAction.Invoke();
			this.WritePayloadEnd();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000185F0 File Offset: 0x000167F0
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.JsonLightOutputContext.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(this.InstanceAnnotationWriter.WriteInstanceAnnotationsForError), error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
			});
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001862C File Offset: 0x0001682C
		internal string UriToString(Uri uri)
		{
			Uri metadataDocumentUri = this.jsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri;
			Uri uri2;
			if (this.jsonLightOutputContext.UrlResolver != null)
			{
				uri2 = this.jsonLightOutputContext.UrlResolver.ResolveUrl(metadataDocumentUri, uri);
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

		// Token: 0x04000325 RID: 805
		protected readonly ODataContextUriBuilder ContextUriBuilder;

		// Token: 0x04000326 RID: 806
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000327 RID: 807
		private readonly SimpleLazy<JsonLightInstanceAnnotationWriter> instanceAnnotationWriter;

		// Token: 0x04000328 RID: 808
		private readonly SimpleLazy<JsonLightODataAnnotationWriter> odataAnnotationWriter;

		// Token: 0x04000329 RID: 809
		private bool allowRelativeUri;
	}
}
