using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x020007A5 RID: 1957
	internal sealed class ODataReaderWithResponse : ODataReader, IDisposable
	{
		// Token: 0x06003945 RID: 14661 RVA: 0x000B8452 File Offset: 0x000B6652
		public ODataReaderWithResponse(ODataReader reader, Uri requestUri, string resourceKind, ODataResponseMessage responseMessage, ODataMessageReader messageReader)
		{
			this.requestUri = requestUri;
			this.resourceKind = resourceKind;
			this.reader = reader;
			this.responseMessage = responseMessage;
			this.messageReader = messageReader;
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000B8480 File Offset: 0x000B6680
		public static ODataReaderWithResponse CreateFromHttpResponseData(IEngineHost engineHost, HttpResponseData httpResponseData, Uri requestUri, string resourceKind, bool isResourceSet, Microsoft.OData.Edm.IEdmModel model)
		{
			ODataResponseMessage odataResponseMessage = new ODataResponseMessage(httpResponseData);
			ODataMessageReaderSettings defaultReaderSettings = ODataResponse.DefaultReaderSettings;
			if (requestUri.PathAndQuery.Contains("$apply"))
			{
				defaultReaderSettings.Validations &= ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType;
			}
			ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, defaultReaderSettings, model);
			ODataReaderWithResponse odataReaderWithResponse = null;
			if (httpResponseData != null && httpResponseData.ContentType == "text/html")
			{
				throw ODataCommonErrors.UnsupportedFormat(httpResponseData.ContentType);
			}
			try
			{
				if (isResourceSet)
				{
					odataReaderWithResponse = new ODataReaderWithResponse(odataMessageReader.CreateODataResourceSetReader(), requestUri, resourceKind, odataResponseMessage, odataMessageReader);
				}
				else
				{
					odataReaderWithResponse = new ODataReaderWithResponse(odataMessageReader.CreateODataResourceReader(), requestUri, resourceKind, odataResponseMessage, odataMessageReader);
				}
			}
			catch (ODataContentTypeException ex)
			{
				throw ODataCommonErrors.ODataExceptionMessage(engineHost, ex, requestUri, resourceKind);
			}
			return odataReaderWithResponse;
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000B8530 File Offset: 0x000B6730
		public Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000B8538 File Offset: 0x000B6738
		public int StatusCode
		{
			get
			{
				return this.responseMessage.StatusCode;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x000B8545 File Offset: 0x000B6745
		public override ODataItem Item
		{
			get
			{
				return this.reader.Item;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000B8552 File Offset: 0x000B6752
		public override ODataReaderState State
		{
			get
			{
				return this.reader.State;
			}
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000B855F File Offset: 0x000B675F
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000B856C File Offset: 0x000B676C
		public bool IsPreferenceApplied(string name, string value)
		{
			HttpHeaderValueElement httpHeaderValueElement;
			return this.responseMessage.TryGetPreferenceApplied(name, out httpHeaderValueElement) && httpHeaderValueElement.Value == value;
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000B8597 File Offset: 0x000B6797
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.messageReader != null)
				{
					this.messageReader.Dispose();
				}
				if (this.responseMessage != null)
				{
					this.responseMessage.Dispose();
				}
			}
		}

		// Token: 0x04001D8C RID: 7564
		private readonly Uri requestUri;

		// Token: 0x04001D8D RID: 7565
		private readonly ODataReader reader;

		// Token: 0x04001D8E RID: 7566
		private readonly string resourceKind;

		// Token: 0x04001D8F RID: 7567
		private readonly ODataResponseMessage responseMessage;

		// Token: 0x04001D90 RID: 7568
		private readonly ODataMessageReader messageReader;

		// Token: 0x04001D91 RID: 7569
		private bool disposed;
	}
}
