using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000218 RID: 536
	internal class ODataJsonLightSerializer : ODataSerializer
	{
		// Token: 0x060015D2 RID: 5586 RVA: 0x00042A38 File Offset: 0x00040C38
		internal ODataJsonLightSerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool initContextUriBuilder = false)
			: base(jsonLightOutputContext)
		{
			ODataJsonLightSerializer <>4__this = this;
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.instanceAnnotationWriter = new SimpleLazy<JsonLightInstanceAnnotationWriter>(() => new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(jsonLightOutputContext, false), jsonLightOutputContext.TypeNameOracle));
			this.odataAnnotationWriter = new SimpleLazy<JsonLightODataAnnotationWriter>(() => new JsonLightODataAnnotationWriter(jsonLightOutputContext.JsonWriter, <>4__this.JsonLightOutputContext.ODataSimplifiedOptions.EnableWritingODataAnnotationWithoutPrefix));
			if (initContextUriBuilder)
			{
				this.ContextUriBuilder = jsonLightOutputContext.CreateContextUriBuilder();
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00042AB3 File Offset: 0x00040CB3
		internal ODataJsonLightOutputContext JsonLightOutputContext
		{
			get
			{
				return this.jsonLightOutputContext;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00042ABB File Offset: 0x00040CBB
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonLightOutputContext.JsonWriter;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00042AC8 File Offset: 0x00040CC8
		internal JsonLightInstanceAnnotationWriter InstanceAnnotationWriter
		{
			get
			{
				return this.instanceAnnotationWriter.Value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00042AD5 File Offset: 0x00040CD5
		internal JsonLightODataAnnotationWriter ODataAnnotationWriter
		{
			get
			{
				return this.odataAnnotationWriter.Value;
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00042AE2 File Offset: 0x00040CE2
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method for consistency with other formats.")]
		internal void WritePayloadStart()
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00042AF5 File Offset: 0x00040CF5
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This method is an instance method for consistency with other formats.")]
		internal void WritePayloadEnd()
		{
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00042B08 File Offset: 0x00040D08
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

		// Token: 0x060015DA RID: 5594 RVA: 0x00042BAD File Offset: 0x00040DAD
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WritePayloadStart();
			payloadWriterAction.Invoke();
			this.WritePayloadEnd();
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00042BC4 File Offset: 0x00040DC4
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.JsonLightOutputContext.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(this.InstanceAnnotationWriter.WriteInstanceAnnotationsForError), error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
			});
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00042C00 File Offset: 0x00040E00
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

		// Token: 0x04000A3E RID: 2622
		protected readonly ODataContextUriBuilder ContextUriBuilder;

		// Token: 0x04000A3F RID: 2623
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000A40 RID: 2624
		private readonly SimpleLazy<JsonLightInstanceAnnotationWriter> instanceAnnotationWriter;

		// Token: 0x04000A41 RID: 2625
		private readonly SimpleLazy<JsonLightODataAnnotationWriter> odataAnnotationWriter;

		// Token: 0x04000A42 RID: 2626
		private bool allowRelativeUri;
	}
}
