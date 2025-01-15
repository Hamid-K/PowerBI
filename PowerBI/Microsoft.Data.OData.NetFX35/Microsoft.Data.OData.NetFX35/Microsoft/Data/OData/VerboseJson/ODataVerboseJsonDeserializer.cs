using System;
using System.Diagnostics;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001B8 RID: 440
	internal abstract class ODataVerboseJsonDeserializer : ODataDeserializer
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002D825 File Offset: 0x0002BA25
		protected ODataVerboseJsonDeserializer(ODataVerboseJsonInputContext jsonInputContext)
			: base(jsonInputContext)
		{
			this.jsonInputContext = jsonInputContext;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002D835 File Offset: 0x0002BA35
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonInputContext.JsonReader;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002D842 File Offset: 0x0002BA42
		protected ODataVerboseJsonInputContext VerboseJsonInputContext
		{
			get
			{
				return this.jsonInputContext;
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002D84A File Offset: 0x0002BA4A
		internal void ReadPayloadStart(bool isReadingNestedPayload)
		{
			this.ReadPayloadStart(isReadingNestedPayload, true);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002D854 File Offset: 0x0002BA54
		internal void ReadPayloadStart(bool isReadingNestedPayload, bool expectResponseWrapper)
		{
			if (!isReadingNestedPayload)
			{
				this.JsonReader.Read();
			}
			if (base.ReadingResponse && expectResponseWrapper)
			{
				this.JsonReader.ReadStartObject();
				while (this.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = this.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("d", text) == 0)
					{
						break;
					}
					this.JsonReader.SkipValue();
				}
				if (this.JsonReader.NodeType == JsonNodeType.EndObject)
				{
					throw new ODataException(Strings.ODataJsonDeserializer_DataWrapperPropertyNotFound);
				}
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002D8D3 File Offset: 0x0002BAD3
		internal void ReadPayloadEnd(bool isReadingNestedPayload)
		{
			this.ReadPayloadEnd(isReadingNestedPayload, true);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002D8E0 File Offset: 0x0002BAE0
		internal void ReadPayloadEnd(bool isReadingNestedPayload, bool expectResponseWrapper)
		{
			if (base.ReadingResponse && expectResponseWrapper)
			{
				while (this.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = this.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("d", text) == 0)
					{
						throw new ODataException(Strings.ODataJsonDeserializer_DataWrapperMultipleProperties);
					}
					this.JsonReader.SkipValue();
				}
				this.JsonReader.ReadEndObject();
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002D942 File Offset: 0x0002BB42
		internal Uri ProcessUriFromPayload(string uriFromPayload)
		{
			return this.ProcessUriFromPayload(uriFromPayload, true);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002D94C File Offset: 0x0002BB4C
		internal Uri ProcessUriFromPayload(string uriFromPayload, bool requireAbsoluteUri)
		{
			Uri uri = new Uri(uriFromPayload, 0);
			Uri uri2 = this.VerboseJsonInputContext.ResolveUri(base.MessageReaderSettings.BaseUri, uri);
			if (uri2 != null)
			{
				return uri2;
			}
			if (!uri.IsAbsoluteUri)
			{
				if (base.MessageReaderSettings.BaseUri != null)
				{
					uri = UriUtils.UriToAbsoluteUri(base.MessageReaderSettings.BaseUri, uri);
				}
				else if (requireAbsoluteUri)
				{
					throw new ODataException(Strings.ODataJsonDeserializer_RelativeUriUsedWithoutBaseUriSpecified(uriFromPayload));
				}
			}
			return uri;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002D9C3 File Offset: 0x0002BBC3
		[Conditional("DEBUG")]
		internal void AssertJsonCondition(params JsonNodeType[] allowedNodeTypes)
		{
		}

		// Token: 0x04000492 RID: 1170
		private readonly ODataVerboseJsonInputContext jsonInputContext;
	}
}
