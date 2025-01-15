using System;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C3 RID: 451
	internal class ODataVerboseJsonSerializer : ODataSerializer
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x0002EFD9 File Offset: 0x0002D1D9
		internal ODataVerboseJsonSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002EFE9 File Offset: 0x0002D1E9
		internal ODataVerboseJsonOutputContext VerboseJsonOutputContext
		{
			get
			{
				return this.verboseJsonOutputContext;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002EFF1 File Offset: 0x0002D1F1
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.verboseJsonOutputContext.JsonWriter;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0002EFFE File Offset: 0x0002D1FE
		internal void WritePayloadStart()
		{
			this.WritePayloadStart(false);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002F007 File Offset: 0x0002D207
		internal void WritePayloadStart(bool disableResponseWrapper)
		{
			ODataJsonWriterUtils.StartJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
			if (base.WritingResponse && !disableResponseWrapper)
			{
				this.JsonWriter.StartObjectScope();
				this.JsonWriter.WriteDataWrapper();
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002F03B File Offset: 0x0002D23B
		internal void WritePayloadEnd()
		{
			this.WritePayloadEnd(false);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002F044 File Offset: 0x0002D244
		internal void WritePayloadEnd(bool disableResponseWrapper)
		{
			if (base.WritingResponse && !disableResponseWrapper)
			{
				this.JsonWriter.EndObjectScope();
			}
			ODataJsonWriterUtils.EndJsonPaddingIfRequired(this.JsonWriter, base.MessageWriterSettings);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002F06D File Offset: 0x0002D26D
		internal void WriteTopLevelPayload(Action payloadWriterAction)
		{
			this.WriteTopLevelPayload(payloadWriterAction, false);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002F077 File Offset: 0x0002D277
		internal void WriteTopLevelPayload(Action payloadWriterAction, bool disableResponseWrapper)
		{
			this.WritePayloadStart(disableResponseWrapper);
			payloadWriterAction.Invoke();
			this.WritePayloadEnd(disableResponseWrapper);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WriteTopLevelPayload(delegate
			{
				ODataJsonWriterUtils.WriteError(this.VerboseJsonOutputContext.JsonWriter, null, error, includeDebugInformation, this.MessageWriterSettings.MessageQuotas.MaxNestingDepth, false);
			}, true);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002F10B File Offset: 0x0002D30B
		internal string UriToAbsoluteUriString(Uri uri)
		{
			return this.UriToUriString(uri, true);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002F115 File Offset: 0x0002D315
		internal string UriToUriString(Uri uri, bool makeAbsolute)
		{
			return ODataJsonWriterUtils.UriToUriString(this.verboseJsonOutputContext, uri, makeAbsolute);
		}

		// Token: 0x040004AD RID: 1197
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;
	}
}
