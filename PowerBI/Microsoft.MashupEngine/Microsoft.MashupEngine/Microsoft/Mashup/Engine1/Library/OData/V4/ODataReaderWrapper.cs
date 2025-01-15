using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200088D RID: 2189
	internal sealed class ODataReaderWrapper : ODataReader, IDisposable
	{
		// Token: 0x06003EF2 RID: 16114 RVA: 0x000CE279 File Offset: 0x000CC479
		public ODataReaderWrapper(IEngineHost engineHost, ODataReader reader, Uri requestUri, string resourceKind, ODataResponseMessage responseMessage, ODataMessageReader messageReader)
		{
			this.engineHost = engineHost;
			this.requestUri = requestUri;
			this.resourceKind = resourceKind;
			this.reader = reader;
			this.responseMessage = responseMessage;
			this.messageReader = messageReader;
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000CE2B0 File Offset: 0x000CC4B0
		public static ODataReaderWrapper CreateFromHttpResponseData(IEngineHost engineHost, HttpResponseData httpResponseData, Uri requestUri, string resourceKind, bool isFeed, Microsoft.OData.Edm.IEdmModel model)
		{
			ODataResponseMessage odataResponseMessage = new ODataResponseMessage(httpResponseData);
			ODataMessageReaderSettings defaultReaderSettings = ODataResponse.DefaultReaderSettings;
			if (requestUri.PathAndQuery.Contains("$apply"))
			{
				defaultReaderSettings.UndeclaredPropertyBehaviorKinds = ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty;
			}
			ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, defaultReaderSettings, model);
			ODataReaderWrapper odataReaderWrapper = null;
			if (httpResponseData != null && httpResponseData.ContentType == "text/html")
			{
				throw ODataCommonErrors.UnsupportedFormat(httpResponseData.ContentType);
			}
			try
			{
				if (isFeed)
				{
					odataReaderWrapper = new ODataReaderWrapper(engineHost, odataMessageReader.CreateODataFeedReader(), requestUri, resourceKind, odataResponseMessage, odataMessageReader);
				}
				else
				{
					odataReaderWrapper = new ODataReaderWrapper(engineHost, odataMessageReader.CreateODataEntryReader(), requestUri, resourceKind, odataResponseMessage, odataMessageReader);
				}
			}
			catch (ODataContentTypeException ex)
			{
				throw ODataCommonErrors.ODataExceptionMessage(engineHost, ex, requestUri, resourceKind);
			}
			return odataReaderWrapper;
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000CE35C File Offset: 0x000CC55C
		public int StatusCode
		{
			get
			{
				return this.responseMessage.StatusCode;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000CE369 File Offset: 0x000CC569
		public override ODataItem Item
		{
			get
			{
				return this.reader.Item;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x000CE376 File Offset: 0x000CC576
		public override ODataReaderState State
		{
			get
			{
				return this.reader.State;
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000CE384 File Offset: 0x000CC584
		public override bool Read()
		{
			if (this.State == ODataReaderState.Completed)
			{
				return false;
			}
			bool flag;
			try
			{
				flag = this.reader.Read();
			}
			catch (Microsoft.Spatial.ParseErrorException ex)
			{
				throw ODataCommonErrors.ODataExceptionMessage(this.engineHost, ex, this.requestUri, this.resourceKind);
			}
			catch (ODataException ex2)
			{
				throw ODataCommonErrors.ODataExceptionMessage(this.engineHost, ex2, this.requestUri, this.resourceKind);
			}
			catch (IOException ex3)
			{
				throw ODataCommonErrors.ODataExceptionMessage(this.engineHost, ex3, this.requestUri, this.resourceKind);
			}
			return flag;
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000CE424 File Offset: 0x000CC624
		public void Read(ODataReaderState expected)
		{
			this.VerifyState(expected);
			this.Read();
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000CE434 File Offset: 0x000CC634
		public T Read<T>(ODataReaderState expected) where T : ODataItem
		{
			this.VerifyState(expected);
			T t = (T)((object)this.Item);
			this.Read();
			return t;
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x000CE450 File Offset: 0x000CC650
		internal void VerifyState(ODataReaderState expected)
		{
			if (this.State != expected)
			{
				throw ODataCommonErrors.InvalidReaderState(this.State.ToString(), this.requestUri);
			}
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x000CE486 File Offset: 0x000CC686
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

		// Token: 0x0400210F RID: 8463
		private readonly IEngineHost engineHost;

		// Token: 0x04002110 RID: 8464
		private readonly Uri requestUri;

		// Token: 0x04002111 RID: 8465
		private readonly ODataReader reader;

		// Token: 0x04002112 RID: 8466
		private readonly string resourceKind;

		// Token: 0x04002113 RID: 8467
		private readonly ODataResponseMessage responseMessage;

		// Token: 0x04002114 RID: 8468
		private readonly ODataMessageReader messageReader;

		// Token: 0x04002115 RID: 8469
		private bool disposed;
	}
}
