using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000792 RID: 1938
	internal sealed class ODataHttpResponseReader : IODataPayloadReader, IDisposable
	{
		// Token: 0x060038D9 RID: 14553 RVA: 0x000B72D4 File Offset: 0x000B54D4
		public ODataHttpResponseReader(IEngineHost engineHost, HttpResource resource, Uri requestUri, HttpResponseData httpResponseData, Microsoft.OData.Edm.IEdmModel model = null)
		{
			this.engineHost = engineHost;
			this.resource = resource;
			this.requestUri = requestUri;
			this.httpResponseData = httpResponseData;
			this.model = model;
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000B7301 File Offset: 0x000B5501
		public bool IsNull
		{
			get
			{
				return this.HttpResponseData.StatusCode == 204;
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000B7315 File Offset: 0x000B5515
		public string ContextUrl
		{
			get
			{
				if (this.contextUrl == null)
				{
					this.contextUrl = ODataHttpResponseReader.GetContextUrl(this.HttpResponseData);
				}
				return this.contextUrl;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000B7336 File Offset: 0x000B5536
		// (set) Token: 0x060038DD RID: 14557 RVA: 0x000B733E File Offset: 0x000B553E
		public Microsoft.OData.Edm.IEdmModel EdmModel
		{
			get
			{
				return this.model;
			}
			set
			{
				this.model = value;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000B7347 File Offset: 0x000B5547
		private HttpResponseData HttpResponseData
		{
			get
			{
				this.AssertNotDisposed();
				return this.httpResponseData;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000B7355 File Offset: 0x000B5555
		private ODataResponseMessage ResponseMessage
		{
			get
			{
				if (this.responseMessage == null)
				{
					this.responseMessage = new ODataResponseMessage(this.HttpResponseData);
				}
				return this.responseMessage;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000B7376 File Offset: 0x000B5576
		private ODataMessageReader MessageReader
		{
			get
			{
				if (this.messageReader == null)
				{
					this.messageReader = new ODataMessageReader(this.ResponseMessage, ODataResponse.DefaultReaderSettings, this.model);
				}
				return this.messageReader;
			}
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000B73A4 File Offset: 0x000B55A4
		public ODataServiceDocument ReadServiceDocument()
		{
			ODataServiceDocument odataServiceDocument;
			try
			{
				odataServiceDocument = this.MessageReader.ReadServiceDocument();
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ServiceDocumentCouldNotBeParsed(this.requestUri, ex);
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return odataServiceDocument;
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000B73F8 File Offset: 0x000B55F8
		public Microsoft.OData.Edm.IEdmModel ReadMetadataDocument()
		{
			Microsoft.OData.Edm.IEdmModel edmModel;
			try
			{
				edmModel = this.MessageReader.ReadMetadataDocument();
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.InvalidMetadataDocument(this.engineHost, ex, this.resource.NewUrl(this.httpResponseData.ResponseUri.AbsoluteUri));
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return edmModel;
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000B7468 File Offset: 0x000B5668
		public ODataReaderWithResponse ToResourceReader(bool isResourceSet)
		{
			ODataReaderWithResponse odataReaderWithResponse2;
			try
			{
				ODataReaderWithResponse odataReaderWithResponse = ODataReaderWithResponse.CreateFromHttpResponseData(this.engineHost, this.HttpResponseData, this.requestUri, this.resource.Kind, isResourceSet, this.model);
				this.httpResponseData = null;
				odataReaderWithResponse2 = odataReaderWithResponse;
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return odataReaderWithResponse2;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000B74C8 File Offset: 0x000B56C8
		public IODataPayloadReader TransferOwnership()
		{
			IODataPayloadReader iodataPayloadReader;
			try
			{
				ODataHttpResponseReader odataHttpResponseReader = new ODataHttpResponseReader(this.engineHost, this.resource, this.requestUri, this.httpResponseData, this.model);
				odataHttpResponseReader.contextUrl = this.contextUrl;
				this.httpResponseData = null;
				iodataPayloadReader = odataHttpResponseReader;
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return iodataPayloadReader;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000B752C File Offset: 0x000B572C
		public IEnumerable<object> ReadCollection()
		{
			IEnumerable<object> value;
			try
			{
				value = this.GetValue<List<object>>(delegate
				{
					ODataCollectionReader odataCollectionReader = this.MessageReader.CreateODataCollectionReader();
					List<object> list = new List<object>();
					while (odataCollectionReader.Read())
					{
						if (odataCollectionReader.State == ODataCollectionReaderState.Value)
						{
							list.Add(ODataWrapperHelper.WrapValueIfNecessary(odataCollectionReader.Item));
						}
					}
					return list;
				});
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return value;
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000B756C File Offset: 0x000B576C
		public ODataError ReadError()
		{
			ODataError value;
			try
			{
				value = this.GetValue<ODataError>(() => this.MessageReader.ReadError());
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return value;
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000B75AC File Offset: 0x000B57AC
		public ODataPropertyWrapper ReadProperty()
		{
			ODataPropertyWrapper value;
			try
			{
				value = this.GetValue<ODataPropertyWrapper>(() => new ODataPropertyWrapper(this.MessageReader.ReadProperty()));
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return value;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000B75EC File Offset: 0x000B57EC
		public object ReadPrimitive(TypeValue expectedType)
		{
			object value;
			try
			{
				value = this.GetValue<object>(() => this.MessageReader.ReadValue(ODataTypeServices.GetEdmPrimitiveTypeReference(expectedType, false)));
			}
			finally
			{
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
			return value;
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000B7640 File Offset: 0x000B5840
		public void Dispose()
		{
			if (this.messageReader != null)
			{
				this.messageReader.Dispose();
				this.messageReader = null;
			}
			if (this.responseMessage != null)
			{
				this.responseMessage.Dispose();
				this.responseMessage = null;
			}
			if (this.httpResponseData != null)
			{
				this.httpResponseData.Dispose();
				this.httpResponseData = null;
			}
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000B769B File Offset: 0x000B589B
		private void AssertNotDisposed()
		{
			if (this.httpResponseData == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000B76B8 File Offset: 0x000B58B8
		private T GetValue<T>(Func<T> getValue)
		{
			T t;
			try
			{
				t = getValue();
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(this.engineHost, ex, this.requestUri, this.resource.Kind);
			}
			catch (IOException ex2)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(this.engineHost, ex2, this.requestUri, this.resource.Kind);
			}
			return t;
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000B772C File Offset: 0x000B592C
		private static string GetContextUrl(HttpResponseData httpResponseData)
		{
			ODataHttpResponseReader.<>c__DisplayClass33_0 CS$<>8__locals1 = new ODataHttpResponseReader.<>c__DisplayClass33_0();
			ODataHttpResponseReader.<>c__DisplayClass33_0 CS$<>8__locals2 = CS$<>8__locals1;
			HttpResponseDataWithContextUri httpResponseDataWithContextUri = httpResponseData as HttpResponseDataWithContextUri;
			CS$<>8__locals2.contextUrl = ((httpResponseDataWithContextUri != null) ? httpResponseDataWithContextUri.ContextUri : null);
			if (CS$<>8__locals1.contextUrl == null)
			{
				httpResponseData.DetectPriorToRead(false, delegate(Stream s, Func<long> getCurrentPosition)
				{
					using (StreamReader streamReader = new StreamReader(s))
					{
						CS$<>8__locals1.contextUrl = ODataHttpResponseReader.GetContextUrl(streamReader);
					}
				});
			}
			if (CS$<>8__locals1.contextUrl != null)
			{
				return CS$<>8__locals1.contextUrl;
			}
			throw ODataCommonErrors.UnableToDetectRequiredPayloadInformation(null);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000B7788 File Offset: 0x000B5988
		private static string GetContextUrl(StreamReader reader)
		{
			JsonTokenizer jsonTokenizer = new JsonTokenizer(reader, false, false, null);
			if (jsonTokenizer.GetNextToken() == JsonToken.RecordStart && jsonTokenizer.GetNextToken() == JsonToken.RecordKey && jsonTokenizer.GetTokenText() == "@odata.context" && jsonTokenizer.GetNextToken() == JsonToken.String)
			{
				return jsonTokenizer.GetTokenText();
			}
			return null;
		}

		// Token: 0x04001D5A RID: 7514
		private readonly IEngineHost engineHost;

		// Token: 0x04001D5B RID: 7515
		private readonly HttpResource resource;

		// Token: 0x04001D5C RID: 7516
		private readonly Uri requestUri;

		// Token: 0x04001D5D RID: 7517
		private Microsoft.OData.Edm.IEdmModel model;

		// Token: 0x04001D5E RID: 7518
		private HttpResponseData httpResponseData;

		// Token: 0x04001D5F RID: 7519
		private ODataResponseMessage responseMessage;

		// Token: 0x04001D60 RID: 7520
		private ODataMessageReader messageReader;

		// Token: 0x04001D61 RID: 7521
		private string contextUrl;
	}
}
